using System.Windows.Forms;
namespace NoDir
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
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.b_selectFolder = new System.Windows.Forms.Button();
            this.TB_selectFolder = new System.Windows.Forms.TextBox();
            this.B_parseDirectory = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.L_progressBar = new System.Windows.Forms.Label();
            this.B_parseFile = new System.Windows.Forms.Button();
            this.B_parseAllFiles = new System.Windows.Forms.Button();
            this.TB_Drive = new System.Windows.Forms.TextBox();
            this.L_Drive = new System.Windows.Forms.Label();
            this.LV_fileInfo = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.B_cancel = new System.Windows.Forms.Button();
            this.TB_timer1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 84);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(398, 425);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView1_KeyDown);
            // 
            // b_selectFolder
            // 
            this.b_selectFolder.Location = new System.Drawing.Point(417, 13);
            this.b_selectFolder.Name = "b_selectFolder";
            this.b_selectFolder.Size = new System.Drawing.Size(130, 23);
            this.b_selectFolder.TabIndex = 1;
            this.b_selectFolder.Text = "Select Folder";
            this.b_selectFolder.UseVisualStyleBackColor = true;
            this.b_selectFolder.Click += new System.EventHandler(this.B_selectFolder_Click);
            // 
            // TB_selectFolder
            // 
            this.TB_selectFolder.Location = new System.Drawing.Point(553, 15);
            this.TB_selectFolder.Name = "TB_selectFolder";
            this.TB_selectFolder.Size = new System.Drawing.Size(237, 20);
            this.TB_selectFolder.TabIndex = 2;
            this.TB_selectFolder.TextChanged += new System.EventHandler(this.TB_selectFolder_TextChanged);
            // 
            // B_parseDirectory
            // 
            this.B_parseDirectory.Location = new System.Drawing.Point(12, 12);
            this.B_parseDirectory.Name = "B_parseDirectory";
            this.B_parseDirectory.Size = new System.Drawing.Size(130, 23);
            this.B_parseDirectory.TabIndex = 3;
            this.B_parseDirectory.Text = "Parse Directory";
            this.B_parseDirectory.UseVisualStyleBackColor = true;
            this.B_parseDirectory.Click += new System.EventHandler(this.B_parseDirectory_Click);
            this.B_parseDirectory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView1_KeyDown);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 515);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(397, 23);
            this.progressBar.Step = 2;
            this.progressBar.TabIndex = 4;
            // 
            // L_progressBar
            // 
            this.L_progressBar.AutoSize = true;
            this.L_progressBar.Location = new System.Drawing.Point(415, 520);
            this.L_progressBar.Name = "L_progressBar";
            this.L_progressBar.Size = new System.Drawing.Size(35, 13);
            this.L_progressBar.TabIndex = 5;
            this.L_progressBar.Text = "NULL";
            this.L_progressBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.L_progressBar.Click += new System.EventHandler(this.L_progressBar_Click);
            // 
            // B_parseFile
            // 
            this.B_parseFile.Location = new System.Drawing.Point(149, 12);
            this.B_parseFile.Name = "B_parseFile";
            this.B_parseFile.Size = new System.Drawing.Size(75, 23);
            this.B_parseFile.TabIndex = 6;
            this.B_parseFile.Text = "Parse File";
            this.B_parseFile.UseVisualStyleBackColor = true;
            this.B_parseFile.Click += new System.EventHandler(this.B_parseFile_Click);
            // 
            // B_parseAllFiles
            // 
            this.B_parseAllFiles.Location = new System.Drawing.Point(231, 12);
            this.B_parseAllFiles.Name = "B_parseAllFiles";
            this.B_parseAllFiles.Size = new System.Drawing.Size(112, 23);
            this.B_parseAllFiles.TabIndex = 7;
            this.B_parseAllFiles.Text = "Parse All Files";
            this.B_parseAllFiles.UseVisualStyleBackColor = true;
            this.B_parseAllFiles.Click += new System.EventHandler(this.B_parseAllFiles_Click);
            // 
            // TB_Drive
            // 
            this.TB_Drive.Location = new System.Drawing.Point(553, 42);
            this.TB_Drive.Name = "TB_Drive";
            this.TB_Drive.ReadOnly = true;
            this.TB_Drive.Size = new System.Drawing.Size(17, 20);
            this.TB_Drive.TabIndex = 8;
            // 
            // L_Drive
            // 
            this.L_Drive.AutoSize = true;
            this.L_Drive.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.L_Drive.Location = new System.Drawing.Point(515, 46);
            this.L_Drive.Name = "L_Drive";
            this.L_Drive.Size = new System.Drawing.Size(35, 13);
            this.L_Drive.TabIndex = 9;
            this.L_Drive.Text = "Drive:";
            this.L_Drive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LV_fileInfo
            // 
            this.LV_fileInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1});
            this.LV_fileInfo.GridLines = true;
            this.LV_fileInfo.Location = new System.Drawing.Point(504, 160);
            this.LV_fileInfo.Name = "LV_fileInfo";
            this.LV_fileInfo.Size = new System.Drawing.Size(434, 199);
            this.LV_fileInfo.TabIndex = 10;
            this.LV_fileInfo.UseCompatibleStateImageBehavior = false;
            this.LV_fileInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Field";
            this.columnHeader0.Width = 25;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Value";
            this.columnHeader1.Width = 25;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // B_cancel
            // 
            this.B_cancel.Enabled = false;
            this.B_cancel.Location = new System.Drawing.Point(13, 42);
            this.B_cancel.Name = "B_cancel";
            this.B_cancel.Size = new System.Drawing.Size(129, 23);
            this.B_cancel.TabIndex = 11;
            this.B_cancel.Text = "Cancel";
            this.B_cancel.UseVisualStyleBackColor = true;
            this.B_cancel.Click += new System.EventHandler(this.B_cancel_Click);
            // 
            // TB_timer1
            // 
            this.TB_timer1.Enabled = false;
            this.TB_timer1.Location = new System.Drawing.Point(418, 489);
            this.TB_timer1.Name = "TB_timer1";
            this.TB_timer1.Size = new System.Drawing.Size(57, 20);
            this.TB_timer1.TabIndex = 12;
            this.TB_timer1.Text = "00:00";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 546);
            this.Controls.Add(this.TB_timer1);
            this.Controls.Add(this.B_cancel);
            this.Controls.Add(this.LV_fileInfo);
            this.Controls.Add(this.L_Drive);
            this.Controls.Add(this.TB_Drive);
            this.Controls.Add(this.B_parseAllFiles);
            this.Controls.Add(this.B_parseFile);
            this.Controls.Add(this.L_progressBar);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.B_parseDirectory);
            this.Controls.Add(this.TB_selectFolder);
            this.Controls.Add(this.b_selectFolder);
            this.Controls.Add(this.treeView1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button b_selectFolder;
        private System.Windows.Forms.TextBox TB_selectFolder;
        private System.Windows.Forms.Button B_parseDirectory;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label L_progressBar;
        private System.Windows.Forms.Button B_parseFile;
        private System.Windows.Forms.Button B_parseAllFiles;
        private System.Windows.Forms.TextBox TB_Drive;
        private System.Windows.Forms.Label L_Drive;
        private System.Windows.Forms.ListView LV_fileInfo;
        private ColumnHeader columnHeader0;
        private ColumnHeader columnHeader1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button B_cancel;
        private TextBox TB_timer1;
        private Timer timer1;
    }
}

