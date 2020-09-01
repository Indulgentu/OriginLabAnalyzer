using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OriginLabAnalyzer.Classes;

namespace OriginLabAnalyzer
{
    public partial class Import_Wizard : Form
    {

        private Dictionary<String, CSVObject> CSVOptions = new Dictionary<string, CSVObject>();
        private CSVObject CurrObj;
        private String[] Opts = { "u_k", "p_col_fact", "p_exp_fact", "p_exp_min", "dt_int", "Pexp_c1neg_dt", "i_experim" };
        public String PathOut;

        public Import_Wizard()
        {
            InitializeComponent();
        }

        private void imp_wiz_out_click(object sender, EventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select Output Folder";
            dlg.IsFolderPicker = true;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var PathToData = dlg.FileName;
                imp_wizard_output_text.Text = PathToData;
                PathOut = PathToData + @"\";
                // Do something with selected folder string
                imp_button.Enabled = true;
                imp_all_btn.Enabled = true;
            }
        }

        private void imp_wiz_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox DropDown = (ComboBox)sender;

            if (DropDown.SelectedItem != null)
            {
                CurrObj = CSVOptions[DropDown.SelectedItem.ToString()];
                foreach(string str in Opts)
                {
                    ((TextBox)Controls.Find(str + "_input", true)[0]).Text = CurrObj.GetOption(str).ToString();
                }
                options_group.Enabled = true;
                save_imp_btn.Enabled = true;
            }

        }

        public void AddToList(String[] Files)
        {
            if(Files.Length > 0)
            {
                foreach(string str in Files)
                {
                    CSVOptions.Add(Path.GetFileName(str), new CSVObject(Path.GetFileName(str), str));
                    imp_wiz_files.Items.Add(Path.GetFileName(str));
                    imp_wiz_files.SelectedIndex = 0;
                }
            }
        }

        private void save_imp_btn_Click(object sender, EventArgs e)
        {
            foreach (string str in Opts)
            {
                if (save_all_checkbox.Checked) {
                    foreach (CSVObject obj in CSVOptions.Values)
                    {
                        obj.SetOption(str, ((TextBox)Controls.Find(str + "_input", true)[0]).Text);
                    }
                }else
                {
                    CurrObj.SetOption(str, ((TextBox)Controls.Find(str + "_input", true)[0]).Text);
                }
            }
        }

        private void ChangeText(object sender, EventArgs e)
        {
            TextBox Input = (TextBox)sender;
            try
            {
                if (Input.Text != "" || Input.Text != " ")
                {
                    Helper.ParseDouble(Input.Text);
                }
            }
            catch
            {

                Input.Text = CurrObj.GetOption(Input.Name.Substring(0, Input.Name.LastIndexOf("_"))).ToString();
                //Input.Text = CSVOptions[Input.Name.Substring(0, Input.Name.LastIndexOf("_"))].GetOption(Input.Name.Substring(0, Input.Name.LastIndexOf("_")));
            }
        }

        private void imp_button_Click(object sender, EventArgs e)
        {
            ProgressForm pf = new ProgressForm();

            Task.Run(() => {
                try
                {
                    DataParser dp = new DataParser(CurrObj.Path, PathOut, CurrObj);
                    pf.UpdateProgress(1, CurrObj.FileName, PathOut, false);
                    dp = null;
                }
                catch (Exception ex)
                {
                    pf.UpdateProgress(1, CurrObj.FileName, PathOut, true);
                    MessageBox.Show(ex.Message + ". File: " + CurrObj.FileName);
                }

            });
            pf.ShowDialog();
            if (check_close_after_imp.Checked)
            {
                this.Close();
            }
        }

        private void imp_btn_all_Click(object sender, EventArgs e)
        {
            ProgressForm pf = new ProgressForm();

            foreach (CSVObject obj in CSVOptions.Values)
            {
                Task.Run(() => {
                    try
                    {
                        DataParser dp = new DataParser(obj.Path, PathOut, obj);
                        pf.UpdateProgress(CSVOptions.Values.Count, obj.FileName, PathOut, false);
                        dp = null;
                    }
                    catch (Exception ex)
                    {
                        pf.UpdateProgress(CSVOptions.Values.Count, obj.FileName, PathOut, true);
                        MessageBox.Show(ex.Message + ". File: " + obj.FileName);
                    }

                });
            }
            pf.ShowDialog();
            if (check_close_after_imp.Checked)
            {
                this.Close();
            }
        }
    }
}
