using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OriginLabAnalyzer
{
    public partial class ProgressForm : Form
    {
        private double Count = 0;
        private int ErrCount = 0;
        List<String> Files = new List<String>();
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void UpdateProgress(double Total, String File, String POut, bool Err)
        {
            MethodInvoker inv = delegate
            {
                Count++;
                ErrCount += Err ? 1 : 0;
                progress_text.Text += (Count > 1) ? ", " + File : " " + File;
                this.progress_bar.Value = Convert.ToInt32(Count/Total * 100);
                if(!Err)
                {
                    Console.WriteLine(File);
                    Files.Add(File);
                }

                if(progress_bar.Value == progress_bar.Maximum)
                {
                    if(ErrCount == Total)
                    {
                        MessageBox.Show("Your files could not be parsed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                    OriginLabAnalyzer f1 = (OriginLabAnalyzer)Application.OpenForms["OriginLabAnalyzer"];
                    foreach(String str in Files)
                    {
                        Console.WriteLine(str.Split(new[] { "mZ" }, StringSplitOptions.None)[0]);
                        f1.AddToListInvoke(Directory.GetFiles(POut + ((str.Contains("li")) ? str.Split(new[] { "li" }, StringSplitOptions.None)[0] : str.Split(new[] { "mZ" }, StringSplitOptions.None)[0]) + @"\" + str.Split('.')[0], str.Split('.')[0] + "*.txt", SearchOption.TopDirectoryOnly));
                    }
                    MessageBox.Show((ErrCount > 0) ? "Parsing finished with errors. Files skipped: " + ErrCount : "Your files were parsed successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
            };

            this.Invoke(inv);
        }

    }
}
