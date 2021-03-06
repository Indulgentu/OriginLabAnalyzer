﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace OriginLabAnalyzer
{
    public partial class OriginLabAnalyzer : Form
    {

        private delegate ListView.ListViewItemCollection GetItems(ListView lstview);
        public OriginLabAnalyzer()
        {
            InitializeComponent();
        }

        private ListView.ListViewItemCollection getListViewItems(ListView lstview)
        {
            ListView.ListViewItemCollection temp = new ListView.ListViewItemCollection(new ListView());
            if (!lstview.InvokeRequired)
            {
                foreach (ListViewItem item in lstview.Items)
                {
                    temp.Add((ListViewItem)item.Clone());
                }
                return temp;
            }
            else
            {
                return (ListView.ListViewItemCollection)this.Invoke(new GetItems(getListViewItems), new object[] { lstview });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.TXT)|*.TXT|CSV Files (*.CSV)|*.CSV";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select Data";
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) 
            {
                String[] CSVFiles = openFileDialog1.FileNames.Where(f => Path.GetExtension(f).ToUpperInvariant() == ".CSV").ToArray();
                if(CSVFiles.Length > 0)
                {
                    Import_Wizard IW = new Import_Wizard();

                    IW.AddToList(CSVFiles);

                    IW.ShowDialog();
                    return;
                }
                AddToList(openFileDialog1.FileNames);
            }
        }
      
        private void browse_btn_Click(object sender, EventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select Data Folder";
            dlg.IsFolderPicker = true;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var PathToData = dlg.FileName;
                textBox1.Text = PathToData;
                try
                {
                    String[] ParsedFiles = Directory.GetFiles(PathToData, "*.txt", SearchOption.TopDirectoryOnly);
                    String[] CSVFiles = Directory.GetFiles(PathToData, "*.csv", SearchOption.TopDirectoryOnly);
                    if(CSVFiles.Length > 0)
                    {
                        Import_Wizard IW = new Import_Wizard();

                        IW.AddToList(CSVFiles);

                        IW.ShowDialog();
                    }
                    AddToList(ParsedFiles);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btn_analyze_Click(object sender, EventArgs e)
        {
            AsyncLaunchWB();
        }

        private async void AsyncLaunchWB()
        {
            btn_analyze.Enabled = false;
            await Task.Run(() =>
            {
                UpdateProgress("Started WorkBook import...");
                WorkbookReader wbr = new WorkbookReader();
                int c = 0;
                ListView.ListViewItemCollection items = getListViewItems(listView1);
                foreach (ListViewItem f in items)
                {
                    String Path = f.SubItems[1].Text;
                    String FileName = f.SubItems[0].Text.Substring(0, f.SubItems[0].Text.LastIndexOf("."));
                    if (WorkbookReader.App.Pages.Count > 0 && WorkbookReader.App.Pages["Book"].Layers[FileName] != null)
                    {
                        continue;
                    }
                    try
                    {
                        c++;
                        wbr.InitWorkBook(Path, FileName, new Classes.CSVObject(FileName, Path).LoadOptionFile());
                        UpdateProgress("Processing: " + FileName + " (" + c + "/" + items.Count + ")");
                    }catch
                    {
                        MessageBox.Show(FileName + " was skipped. ");
                    }
                }
                wbr.GenerateGraphs();
                WorkbookReader.App.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOW_BRING_TO_FRONT;
         
                UpdateProgress("Ready");
            });
            btn_kill_origin.Enabled = true;
            btn_origin_clear.Enabled = true;
            btn_analyze.Enabled = true;
        }

        private void UpdateProgress(String t)
        {
            MethodInvoker inv = delegate
            {
                this.toolStripStatusLabel1.Text = t;
            };

            this.Invoke(inv);
        }

        public void AddToList(String[] Files)
        {

            if(Files.Length <= 0)
            {
                return;
            }

            foreach(String f in Files)
            {
                if (listView1.Items.Count <= 0 || listView1.Items.Count > 0 && listView1.FindItemWithText(f, true, 0) == null)
                {
                    String Name = Path.GetFileName(f);
                    String[] row = { Name, f };
                    listView1.Items.Add(new ListViewItem(row));
                }
            }

            listView1.View = View.Details;
            btn_analyze.Enabled = true;
        }

        public void AddToListInvoke(String[] Files)
        {
            MethodInvoker inv = delegate
            {
                AddToList(Files);
            };

            this.Invoke(inv);
        }

        private void list_drag_complete(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string str in files)
            {
                if(Path.GetExtension(str).ToUpperInvariant() == ".CSV")
                {
                    Import_Wizard IW = new Import_Wizard();

                    IW.AddToList(files);

                    IW.ShowDialog();
                    return;
                }
            }
            AddToList(files);
        }

        private void list_drag_enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                string[] files = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in files)
                {
                    if (Path.GetExtension(filename).ToUpperInvariant() != ".TXT" && Path.GetExtension(filename).ToUpperInvariant() != ".CSV")
                    {
                        e.Effect = DragDropEffects.None;
                        break;
                    }
                }
            }
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem li in ((ListView)sender).SelectedItems)
                {
                    li.Remove();
                }
                if(listView1.Items.Count <= 0)
                {
                    btn_analyze.Enabled = false;
                }
            }
        }

        private void fromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select CSV Folder";
            dlg.IsFolderPicker = true;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var PathToData = dlg.FileName;
                textBox1.Text = PathToData;
                String[] files = Directory.GetFiles(PathToData, "*.csv", SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    Import_Wizard IW = new Import_Wizard();

                    IW.AddToList(files);

                    IW.ShowDialog();
                }
            }
        }

        private void fromFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV (*.CSV)|*.CSV";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select CSV";
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                //AddToList(openFileDialog1.FileNames, listView1);
                if (openFileDialog1.FileNames.Length > 0)
                {
                    Import_Wizard IW = new Import_Wizard();

                    IW.AddToList(openFileDialog1.FileNames);

                    IW.ShowDialog();
                }
            }
        }

        private void sadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Content\help.html");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (WorkbookReader.App != null)
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to exit Origin?", "Exit", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        WorkbookReader.App.EndSession();
                        WorkbookReader.App.Exit();
                        WorkbookReader.App = null;
                        btn_kill_origin.Enabled = false;
                        btn_origin_clear.Enabled = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void btn_origin_clear_Click(object sender, EventArgs e)
        {
            try
            {

                if (WorkbookReader.App != null)
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to reset the current Origin instance?", "Reset", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        WorkbookReader.App.Reset(true, true);
                        WorkbookReader.App.NewProject();
                    }
                }
            }
            catch
            {

            }
        }

        private void OriginLabAnalyzer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WorkbookReader.App != null)
            {
                DialogResult dr = MessageBox.Show("Origin is still running. If you don't stop it before closing this app, you'll have to manually close it from task manager. Kill it now?", "Exit OLA", MessageBoxButtons.YesNoCancel);
                switch (dr)
                {
                    case DialogResult.Yes:
                        WorkbookReader.App.EndSession();
                        WorkbookReader.App.Exit();
                        WorkbookReader.App = null;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default: break;
                }
            }
        }

    }
}
