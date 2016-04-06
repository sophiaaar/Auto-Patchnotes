using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PatchnoteGenerator
{
    class PatchnoteGenerator
    {
        public static List<string> notes = new List<string>();
        public static List<string> changelists = new List<string>();

        public static void StartSlimParse(string changelist1, string changelist2, string branch)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string tempFile = currentDirectory + @"\Patchnotes\PatchnotesSlimTemp.txt";
            //this file will get overwritten each time, and it contains all changes, nothing removed

            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";

            if (changelist2 == "head" || changelist2 == "#head")
            {
                p.StartInfo.Arguments = @"/c p4 changes -l -s submitted " + branch + "...@" + changelist1 + ",#head >" + tempFile;
            }
            else
            {
                p.StartInfo.Arguments = @"/c p4 changes -l -s submitted " + branch + "...@" + changelist1 + ",@" + changelist2 + " >" + tempFile;
            }
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            p.Start();
            p.WaitForExit();


            List<string> unusedNotes = new List<string>();

            string line;
            string stringSeparator = "------------------------------------------------------------------------------------";
            using (StreamReader file = new StreamReader(tempFile))
            {
                notes.Add(stringSeparator);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("HEADLESS.APB") || line.Contains("AutoMate: Binaries") || line.Contains("Updating SDD") || line.Contains("StripOutEditorData"))
                    {
                        unusedNotes.Add(line);
                    }
                    else if (line.Contains("Change "))
                    {
                        string changelist = Regex.Match(line, @"\d{6}").Value;
                        unusedNotes.Add(line);
                        notes.Add(stringSeparator);
                        notes.Add(changelist);
                        changelists.Add(changelist);
                    }
                    else
                    {
                        notes.Add(line);
                        GetNames(line, branch);
                    }
                }
            }

            string[] friendlyBranch = branch.Split('/');
            string branchName = friendlyBranch[friendlyBranch.Length - 1];

            string outputFile = currentDirectory + @"\Patchnotes\Patchnotes-" + branchName + DateTime.Now.ToString("yyyyMMddHHmm") + "_Slim.txt";

            string line2;
            using (StreamReader file = new StreamReader(tempFile))
            {
                while ((line2 = file.ReadLine()) != null)
                {
                    if (line2.Contains("AUTO-ACCEPTED"))
                    {
                        notes.Remove(line2);
                    }
                }
            }

            notes.Reverse();
            notes.RemoveAll(l => string.IsNullOrWhiteSpace(l));

            // Organise the notes the right way round
            string contents = string.Join(" ", notes);
            string[] chunks = Regex.Split(contents, stringSeparator);
            List<string> notes3 = new List<string>();

            foreach (string chunk in chunks)
            {
                if (!string.IsNullOrWhiteSpace(chunk))
                {
                    string[] section = chunk.Split(new string[] { Environment.NewLine, @"\t", @"\r\n", @"\n", Convert.ToChar(9).ToString() }, StringSplitOptions.None);
                    string[] newSection = section.Reverse().ToArray();
                    string[] firstLine = newSection[0].Split(' ');
                    Regex rgx = new Regex(@"^\d{6}$");
                    if (!rgx.IsMatch(newSection[0].Trim()))
                    {
                        notes3.Add("\r\n" + stringSeparator);
                        if (firstLine.Count() > 1)
                        {
                            string changelist = firstLine[firstLine.Count() - 2];
                            notes3.Add(changelist + ':');
                        }
                        notes3.AddRange(newSection);
                    }
                }
            }
            notes3.Add("\r\n" + stringSeparator);
            File.WriteAllLines(outputFile, notes3.Where(l => !l.Contains("Jenkins:")));

            if (new FileInfo(outputFile).Length == 0)
            {
                MessageBox.Show("Parse failed. Please ensure that Changelist 2 is the larger number, then try again.");
            }
            else
            {
                MessageBox.Show("Patchnotes are located at " + outputFile, "Parsing finished");
                notes.Clear();
            }
        }

        public static void ParseDesc(string branchStart, string newBranch, string newChangelist)
        {
            Process q = new Process();
            q.StartInfo.UseShellExecute = false;
            q.StartInfo.CreateNoWindow = true;
            q.StartInfo.RedirectStandardOutput = true;
            q.StartInfo.FileName = "cmd.exe";
            q.StartInfo.Arguments = "/c p4 changes -m 1 -l -s submitted " + branchStart + newBranch + "...@" + newChangelist;
            q.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            q.Start();

            string output = q.StandardOutput.ReadToEnd();
            q.WaitForExit();

            //notes2.Add(output);

            GetNames(output, branchStart + newBranch);
        }

        public static void GetNames(string line, string branch)
        {
            string changelist = Regex.Match(line, @"\d{6}").Value;
            string newBranchRgx = Regex.Match(line, @"(.*?)_(.*?) @").Value;
            if (!string.IsNullOrWhiteSpace(newBranchRgx))
            {
                string[] newBranchArray = newBranchRgx.Split(' ');
                string newBranch = newBranchArray[newBranchArray.Count() - 2];

                string branchStart;
                string[] branches = branch.Split('/');
                string branchNum = string.Empty;

                if (newBranch.Contains("UPGRADE"))
                {
                    branchStart = @"//depot/APB/Branches/Upgrade/";
                }
                else if (newBranch.Contains("IMPORT_"))
                {
                    branchStart = @"//Console/Branches/Import/";
                }
                else if (newBranch.Contains("FUTURE"))
                {
                    branchStart = @"//depot/APB/Branches/Future/";
                }
                else if (branches[2] == "depot")
                {
                    branchStart = @"//depot/APB/Branches/Versions/";
                    if (!string.IsNullOrEmpty(newBranch))
                    {
                        branchNum = newBranch.Remove(newBranch.IndexOf('_')) + '/';
                    }
                }
                else
                {
                    branchStart = @"//Console/Branches/";
                }

                if (!changelists.Contains(changelist))
                {
                    changelists.Add(changelist);
                    ParseDesc(branchStart, branchNum + newBranch, changelist);
                }
            }
        }
    }
}
