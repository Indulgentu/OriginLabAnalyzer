using System;
using System.IO;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private ListView.ListViewItemCollection getListViewItems(ListView lstview)
        {
            ListView.ListViewItemCollection temp = new ListView.ListViewItemCollection(new ListView());
            if (!lstview.InvokeRequired)
            {
                foreach (ListViewItem item in lstview.Items)
                    temp.Add((ListViewItem)item.Clone());
                return temp;
            }
            else
                return (ListView.ListViewItemCollection)this.Invoke(new GetItems(getListViewItems), new object[] { lstview });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.TXT)|*.TXT";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select Data";
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) 
            {
                AddToList(openFileDialog1.FileNames, listView1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                String[] files = Directory.GetFiles(PathToData, "*.txt", SearchOption.AllDirectories);

                AddToList(files, listView1);
                // Do something with selected folder string
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            AsyncLaunchWB();

        }

        private async void AsyncLaunchWB()
        {
            await Task.Run(() =>
            {
                WorkbookReader wbr = new WorkbookReader();
                int c = 0;
                ListView.ListViewItemCollection items = getListViewItems(listView1);
                foreach (ListViewItem f in items)
                {
                    c++;
                    wbr.InitWorkBook(f.SubItems[1].Text, f.SubItems[0].Text.Substring(0, f.SubItems[0].Text.LastIndexOf(".")));
                    Console.WriteLine(f.SubItems[0].Text.Substring(0, f.SubItems[0].Text.LastIndexOf(".")));
                    UpdateProgress("Processing: " + f.SubItems[0].Text.Substring(0, f.SubItems[0].Text.LastIndexOf(".")) + " (" + c + "/" + items.Count + ")");
                }
                wbr.GenerateGraphs();
                WorkbookReader.App.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOW_BRING_TO_FRONT;
                WorkbookReader.App.CanClose = true;
                UpdateProgress("Ready");
            });
        }

        private void UpdateProgress(String t)
        {
            MethodInvoker inv = delegate
            {
                this.toolStripStatusLabel1.Text = t;
            };

            this.Invoke(inv);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddToList(String[] Files, ListView list)
        {

            if(Files.Length <= 0)
            {
                return;
            }

            foreach(String f in Files)
            {
                if (list.Items.Count <= 0)
                {
                    String Name = Path.GetFileName(f);
                    String[] row = { Name, f };
                    list.Items.Add(new ListViewItem(row));
                }else if (list.Items.Count > 0 && list.FindItemWithText(f, true, 0) == null)
                {
                    String Name = Path.GetFileName(f);
                    String[] row = { Name, f };
                    list.Items.Add(new ListViewItem(row));
                }
            }

            listView1.View = View.Details;
            button2.Enabled = true;

        }

    }
}
