using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace PatchnoteGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ParseNotes(bool fullNotes)
        {
            slimNotesButton.Enabled = false;

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo outputDir = new DirectoryInfo(currentDirectory + @"\Patchnotes");

            if (!outputDir.Exists)
            {
                outputDir.Create();
            }

            int parsedValue;
            if (!int.TryParse(changelistTextBox1.Text, out parsedValue))
            {
                MessageBox.Show("Changelist 1 must be a number");
                slimNotesButton.Enabled = true;
                return;
            }

            if (changelistTextBox1.Text.Length != 6)
            {
                MessageBox.Show("Changelist must be 6 digits long");
                slimNotesButton.Enabled = true;
                return;
            }

            if (changelistTextBox1.Text == string.Empty)
            {
                MessageBox.Show("Changelist cannot be blank");
                slimNotesButton.Enabled = true;
                return;
            }

            if (changelistTextBox2.Text == string.Empty)
            {
                MessageBox.Show("Changelist cannot be blank");
                slimNotesButton.Enabled = true;
                return;
            }

            // A regex to match the format of your branches. Can be changed, or not used
            string pattern = "^\\d{1}-\\d{2}-\\d{1}/\\d{1}-\\d{2}-\\d{1}";

            if (branchTextBox1.Text == string.Empty)
            {
                MessageBox.Show("Please enter a branch");
                slimNotesButton.Enabled = true;
                return;
            }

            string branch;

            if (Regex.IsMatch(branchTextBox1.Text, pattern))
            {
                branch = @"//depot/" + branchTextBox1.Text;
                PatchnoteGenerator.StartParse(changelistTextBox1.Text, changelistTextBox2.Text, branch);
            }
            else
            {
                MessageBox.Show(@"Enter branch in the format X-XX-X/X-XX-X_NAME.", "Invalid branch");
            }

            slimNotesButton.Enabled = true;
        }

        private void slimNotesButton_Click(object sender, EventArgs e)
        {
            ParseNotes(false);
        }
    }
}
