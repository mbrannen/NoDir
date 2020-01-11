using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoDir
{

    public partial class Form1 : Form
    {
        public bool isCanceled;
        DateTime timeStart;
        
        
        public Form1()
        {

            InitializeComponent();
            InitializeBackgroundWorker();
            TB_selectFolder.Text = "F:\\CLSINC\\WORD\\TESTING";
            timer1.Stop();
            timeStart = DateTime.Now;
            
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
           backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            if(Global.mainNodes !=null)
                Array.Clear(Global.mainNodes, 0, Global.mainNodes.Length);
            e.Result = createMainNodes((string[])e.Argument, worker, e);
            
        }
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                timer1.Stop();
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                //add nodes already stored in memory
                Global.mainNodes = (TreeNode[])e.Result;
                timer1.Stop();
                foreach (TreeNode tn in Global.mainNodes)
                {
                    treeView1.Nodes[0].Nodes.Add(tn);
                    treeView1.Nodes[0].Expand();
                    treeView1.Update();
                }
            }
            else
            {
                //add all the nodes
                timer1.Stop();
                Global.mainNodes = (TreeNode[])e.Result;
                foreach (TreeNode tn in Global.mainNodes)
                {
                    if(tn != null)
                    {
                        treeView1.Nodes[0].Nodes.Add(tn);
                        treeView1.Nodes[0].Expand();
                        treeView1.Update();
                    }
                    progressBar.Value = 100;
                }
            }

            // Disable the Cancel button.
            B_cancel.Enabled = false;
            B_parseDirectory.Enabled = true;
            B_parseFile.Enabled = true;
            B_parseAllFiles.Enabled = true;
        }
        private void backgroundWorker1_ProgressChanged(object sender,
    ProgressChangedEventArgs e)
        {
            
            this.progressBar.Maximum = 100;
            this.progressBar.Value = e.ProgressPercentage;
            this.L_progressBar.Text = e.ProgressPercentage.ToString() + "/100";
        }
        private void B_selectFolder_Click(object sender, EventArgs e)
        {
            selectFolder();
        }
        private void B_parseDirectory_Click(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync(@"" + TB_selectFolder.Text);
            timeStart = DateTime.Now;
            timer1.Start();

            try 
            {
              B_parseFile.Enabled = false;
              B_parseAllFiles.Enabled = false;
              B_parseDirectory.Enabled = false; 
              B_cancel.Enabled = true;
              parseMainFolder(@"" + TB_selectFolder.Text);
            }
            catch (ArgumentException er) 
            {
              B_parseFile.Enabled = true;
              B_parseAllFiles.Enabled = true;
              B_parseDirectory.Enabled = true;
              B_cancel.Enabled = false;
              MessageBox.Show("Please Select a Directory!");
            }
            
        }


        private void selectFolder()
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TB_selectFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private TreeNode addNode(FileNode fn)
        {

            Color color = new Color();
            if (fn.isRealFile)
                color = Color.LightGreen;
            else
                color = Color.LightSalmon;
            TreeNode cn = new TreeNode(fn.fullPath.Name)
            {
                Tag = fn,
                ToolTipText = fn.fullPath.FullName,
                BackColor = color
            };

            return cn;
            
        }
        private void parseMainFolder(string directory)
        {
            
            treeView1.ShowNodeToolTips = true;
            FileNode main = new FileNode(directory, true);
            TreeNode mainNode = new TreeNode(main.fullPath.Name)
            {
                Tag = main,
                ToolTipText = main.fullPath.FullName,
                BackColor = Color.LightCyan

            };
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(mainNode);
            treeView1.Nodes[0].Expand();

            string[] filePaths = Directory.GetFiles(directory);
            

            progressBar.Maximum = filePaths.Length;
            progressBar.Step = 1;
            backgroundWorker1.RunWorkerAsync(filePaths);
            //TURN BELOW INTO OWN FUNCTION RETURN ARRAY OF NODES
            //foreach (string s in filePaths)
            //{

            //    FileNode fn = new FileNode(s, false);

            //    TreeNode node = new TreeNode(fn.fileName.Name)
            //    {
            //        Tag = fn,
            //        ToolTipText = fn.fullPath.FullName,
            //        BackColor = Color.LightBlue
            //    };
            //    treeView1.Nodes[0].Nodes.Add(node);
            //    progressBar.PerformStep();
            //    L_progressBar.Text = progressBar.Value.ToString() + "/" + progressBar.Maximum;
            //    System.Threading.Thread.Sleep(500); //used this for testing progress bar functionality, it works!
            //    treeView1.Nodes[0].Expand();
            //    treeView1.Update();
            //    L_progressBar.Update();
            //    progressBar.Update();

 
            //}


            //System.Threading.Thread.Sleep(400); //brief pause, why not? //edit: on second thought, the progress bar is fucked. TODO: backgroundWorker DONE
            progressBar.Value = 0;
                
            
        }
        private TreeNode[] createMainNodes(string[] filePaths,BackgroundWorker worker, DoWorkEventArgs e)
        {
            TreeNode[] tna = new TreeNode[filePaths.Length];
            int counter = 0;
            int percentComplete = 0;
            foreach (string s in filePaths)
            {
                if (worker.CancellationPending)
                {
                    e.Result = tna;
                    worker.CancelAsync();
                    return tna;
                }

                FileNode fn = new FileNode(s, false);

                TreeNode node = new TreeNode(fn.fileName.Name)
                {
                    Tag = fn,
                    ToolTipText = fn.fullPath.FullName,
                    BackColor = Color.LightBlue
                };
                tna[counter] = node;
                percentComplete = (int)(((float)counter / (float)filePaths.Length) * 100);
                worker.ReportProgress(percentComplete);

                counter++;
 

                //System.Threading.Thread.Sleep(500); //used this for testing progress bar functionality, it works!


            }
            return tna;



        }

        private void parseSelectedFile(FileNode fn)
        {
            
            //fn.GetContents(fn.fullPath);
            foreach (FileNode child in fn.childNodes)
            {
                TreeNode cn = new TreeNode(child.fullPath.Name)
                {
                    Tag = child,
                    ToolTipText = child.fullPath.FullName

                };

                treeView1.SelectedNode.Nodes.Add(cn);
            }
        }
        private void B_parseFile_Click(object sender, EventArgs e)
        { 
            parseNodesRecursive(treeView1.SelectedNode);
        }
        private void B_parseAllFiles_Click(object sender, EventArgs e)
        {
            parseTreeRecursive(treeView1);
        }
        private void parseTreeRecursive(TreeView tv)
        {
            
            TreeNodeCollection tnc = treeView1.Nodes[0].Nodes;
            foreach(TreeNode tn in tnc)
            {
                
                parseNodesRecursive(tn);
                
            }
        }
        private void parseNodesRecursive(TreeNode tn)
        {
            if (tn.Nodes.Count > 0)
                tn.Nodes.Clear();
            treeView1.SelectedNode = tn;
            FileNode fn = (FileNode)treeView1.SelectedNode.Tag;
            if (fn.childNodes.Count != 0)
            {
                foreach (FileNode child in fn.childNodes)
                {
                    //TreeNode cn = new TreeNode(child.fullPath.Name)
                    //{
                    //    Tag = child,
                    //    ToolTipText = child.fullPath.FullName,


                    //};
                    tn.Nodes.Add(addNode(child));
                }

                foreach (TreeNode childTn in tn.Nodes)
                {
                    parseNodesRecursive(childTn);
                }
            }
        }


        private void TB_selectFolder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TB_Drive.Text = TB_selectFolder.Text.Substring(0, 3);
            }
            catch(ArgumentOutOfRangeException aore)
            {
                MessageBox.Show("STOP TYPING IT IN DUMMY");
            }
        }


        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LV_fileInfo.Items.Clear();
            FileNode fn = (FileNode)treeView1.SelectedNode.Tag;
            ListViewItem item0 = new ListViewItem(new string[]
            {"Exists",
            fn.isRealFile.ToString()});
            ListViewItem item1 = new ListViewItem(new string[]
            {"Last Modified",
            fn.lastModified.ToString()});
            ListViewItem item2 = new ListViewItem(new string[]
            {"Last Accessed",
            fn.lastAccessed.ToString()});

            LV_fileInfo.Items.AddRange(
            new ListViewItem[] { item0, item1, item2 });
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                isCanceled = true;
            }
        }

        private void TreeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                isCanceled = true;
                MessageBox.Show("This event canceled: " + isCanceled.ToString());
            }
        }

        private void B_cancel_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            B_cancel.Enabled = false;
        }

        private void L_progressBar_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            TB_timer1.Text = (DateTime.Now - timeStart).Minutes.ToString() + ":" + (DateTime.Now - timeStart).Seconds.ToString();
        }
    }
    static class Global
    {
        public static TreeNode[] mainNodes { get; set; }
    }
}
