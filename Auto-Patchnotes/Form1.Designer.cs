namespace PatchnoteGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"> true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.changelistLabel1 = new System.Windows.Forms.Label();
            this.changelistLabel2 = new System.Windows.Forms.Label();
            this.branchLabel1 = new System.Windows.Forms.Label();
            this.changelistTextBox1 = new System.Windows.Forms.TextBox();
            this.changelistTextBox2 = new System.Windows.Forms.TextBox();
            this.branchTextBox1 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slimNotesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // changelistLabel1
            //
            this.changelistLabel1.AutoSize = true;
            this.changelistLabel1.Location = new System.Drawing.Point(33, 12);
            this.changelistLabel1.Name = "changelistLabel1";
            this.changelistLabel1.Size = new System.Drawing.Size(65, 13);
            this.changelistLabel1.TabIndex = 0;
            this.changelistLabel1.Text = "Changelist 1";
            //
            // changelistLabel2
            //
            this.changelistLabel2.AutoSize = true;
            this.changelistLabel2.Location = new System.Drawing.Point(32, 51);
            this.changelistLabel2.Name = "changelistLabel2";
            this.changelistLabel2.Size = new System.Drawing.Size(65, 13);
            this.changelistLabel2.TabIndex = 1;
            this.changelistLabel2.Text = "Changelist 2";
            //
            // branchLabel1
            //
            this.branchLabel1.AutoSize = true;
            this.branchLabel1.Location = new System.Drawing.Point(32, 89);
            this.branchLabel1.Name = "branchLabel1";
            this.branchLabel1.Size = new System.Drawing.Size(41, 13);
            this.branchLabel1.TabIndex = 2;
            this.branchLabel1.Text = "Branch";
            this.toolTip1.SetToolTip(this.branchLabel1, "Enter branch in the format X-XX-X\\X-XX-X_NAME\r\nFor console and upgrade branches, " +
        "just enter the branch name (ie CONSOLE_MAIN or UPGRADE_MAIN)");
            //
            // changelistTextBox1
            //
            this.changelistTextBox1.Location = new System.Drawing.Point(105, 9);
            this.changelistTextBox1.MaxLength = 6;
            this.changelistTextBox1.Name = "changelistTextBox1";
            this.changelistTextBox1.Size = new System.Drawing.Size(100, 20);
            this.changelistTextBox1.TabIndex = 3;
            //
            // changelistTextBox2
            //
            this.changelistTextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.changelistTextBox2.Location = new System.Drawing.Point(105, 48);
            this.changelistTextBox2.MaxLength = 6;
            this.changelistTextBox2.Name = "changelistTextBox2";
            this.changelistTextBox2.Size = new System.Drawing.Size(100, 20);
            this.changelistTextBox2.TabIndex = 4;
            this.changelistTextBox2.Text = "#head";
            //
            // branchTextBox1
            //
            this.branchTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.branchTextBox1.Location = new System.Drawing.Point(105, 86);
            this.branchTextBox1.Name = "branchTextBox1";
            this.branchTextBox1.Size = new System.Drawing.Size(100, 20);
            this.branchTextBox1.TabIndex = 5;
            //
            // slimNotesButton
            //
            this.slimNotesButton.Location = new System.Drawing.Point(61, 131);
            this.slimNotesButton.Name = "slimNotesButton";
            this.slimNotesButton.Size = new System.Drawing.Size(113, 23);
            this.slimNotesButton.TabIndex = 7;
            this.slimNotesButton.Text = "Start";
            this.slimNotesButton.UseVisualStyleBackColor = true;
            this.slimNotesButton.Click += new System.EventHandler(this.slimNotesButton_Click);
            //
            // MainWindow
            //
            this.AcceptButton = this.slimNotesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 166);
            this.Controls.Add(this.slimNotesButton);
            this.Controls.Add(this.branchTextBox1);
            this.Controls.Add(this.changelistTextBox2);
            this.Controls.Add(this.changelistTextBox1);
            this.Controls.Add(this.branchLabel1);
            this.Controls.Add(this.changelistLabel2);
            this.Controls.Add(this.changelistLabel1);
            this.Name = "MainWindow";
            this.Text = "Patchnote Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changelistLabel1;
        private System.Windows.Forms.Label changelistLabel2;
        private System.Windows.Forms.Label branchLabel1;
        private System.Windows.Forms.TextBox changelistTextBox1;
        private System.Windows.Forms.TextBox changelistTextBox2;
        private System.Windows.Forms.TextBox branchTextBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button slimNotesButton;
    }
}

