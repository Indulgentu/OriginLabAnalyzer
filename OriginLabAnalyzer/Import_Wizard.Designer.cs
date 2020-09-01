namespace OriginLabAnalyzer
{
    partial class Import_Wizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import_Wizard));
            this.imp_wizard_selected_files = new System.Windows.Forms.Label();
            this.imp_wiz_files = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imp_wizard_output_text = new System.Windows.Forms.TextBox();
            this.imp_wiz_browse_out = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.options_group = new System.Windows.Forms.GroupBox();
            this.save_all_checkbox = new System.Windows.Forms.CheckBox();
            this.i_experim_input = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Pexp_c1neg_dt_input = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dt_int_input = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.p_exp_min_input = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.p_exp_fact_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.p_col_fact_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.u_k_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.save_imp_btn = new System.Windows.Forms.Button();
            this.imp_button = new System.Windows.Forms.Button();
            this.imp_all_btn = new System.Windows.Forms.Button();
            this.v = new System.Windows.Forms.ToolTip(this.components);
            this.check_close_after_imp = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.options_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // imp_wizard_selected_files
            // 
            this.imp_wizard_selected_files.AutoSize = true;
            this.imp_wizard_selected_files.Location = new System.Drawing.Point(7, 19);
            this.imp_wizard_selected_files.Name = "imp_wizard_selected_files";
            this.imp_wizard_selected_files.Size = new System.Drawing.Size(59, 13);
            this.imp_wizard_selected_files.TabIndex = 0;
            this.imp_wizard_selected_files.Text = "Select File:";
            // 
            // imp_wiz_files
            // 
            this.imp_wiz_files.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imp_wiz_files.FormattingEnabled = true;
            this.imp_wiz_files.Location = new System.Drawing.Point(10, 35);
            this.imp_wiz_files.Name = "imp_wiz_files";
            this.imp_wiz_files.Size = new System.Drawing.Size(104, 21);
            this.imp_wiz_files.TabIndex = 1;
            this.imp_wiz_files.SelectedIndexChanged += new System.EventHandler(this.imp_wiz_files_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output Path:";
            // 
            // imp_wizard_output_text
            // 
            this.imp_wizard_output_text.Enabled = false;
            this.imp_wizard_output_text.Location = new System.Drawing.Point(10, 84);
            this.imp_wizard_output_text.Name = "imp_wizard_output_text";
            this.imp_wizard_output_text.Size = new System.Drawing.Size(357, 20);
            this.imp_wizard_output_text.TabIndex = 3;
            // 
            // imp_wiz_browse_out
            // 
            this.imp_wiz_browse_out.Location = new System.Drawing.Point(372, 83);
            this.imp_wiz_browse_out.Name = "imp_wiz_browse_out";
            this.imp_wiz_browse_out.Size = new System.Drawing.Size(75, 22);
            this.imp_wiz_browse_out.TabIndex = 4;
            this.imp_wiz_browse_out.Text = "Browse...";
            this.imp_wiz_browse_out.UseVisualStyleBackColor = true;
            this.imp_wiz_browse_out.Click += new System.EventHandler(this.imp_wiz_out_click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imp_wiz_files);
            this.groupBox1.Controls.Add(this.imp_wiz_browse_out);
            this.groupBox1.Controls.Add(this.imp_wizard_selected_files);
            this.groupBox1.Controls.Add(this.imp_wizard_output_text);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 114);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // options_group
            // 
            this.options_group.BackColor = System.Drawing.SystemColors.Control;
            this.options_group.Controls.Add(this.check_close_after_imp);
            this.options_group.Controls.Add(this.save_all_checkbox);
            this.options_group.Controls.Add(this.i_experim_input);
            this.options_group.Controls.Add(this.label8);
            this.options_group.Controls.Add(this.Pexp_c1neg_dt_input);
            this.options_group.Controls.Add(this.label7);
            this.options_group.Controls.Add(this.dt_int_input);
            this.options_group.Controls.Add(this.label6);
            this.options_group.Controls.Add(this.p_exp_min_input);
            this.options_group.Controls.Add(this.label5);
            this.options_group.Controls.Add(this.p_exp_fact_input);
            this.options_group.Controls.Add(this.label4);
            this.options_group.Controls.Add(this.p_col_fact_input);
            this.options_group.Controls.Add(this.label3);
            this.options_group.Controls.Add(this.u_k_input);
            this.options_group.Controls.Add(this.label2);
            this.options_group.Enabled = false;
            this.options_group.Location = new System.Drawing.Point(12, 151);
            this.options_group.Name = "options_group";
            this.options_group.Size = new System.Drawing.Size(454, 179);
            this.options_group.TabIndex = 7;
            this.options_group.TabStop = false;
            this.options_group.Text = "Import Options";
            // 
            // save_all_checkbox
            // 
            this.save_all_checkbox.AutoSize = true;
            this.save_all_checkbox.Location = new System.Drawing.Point(349, 135);
            this.save_all_checkbox.Name = "save_all_checkbox";
            this.save_all_checkbox.Size = new System.Drawing.Size(98, 17);
            this.save_all_checkbox.TabIndex = 19;
            this.save_all_checkbox.Text = "Apply to all files";
            this.save_all_checkbox.UseVisualStyleBackColor = true;
            // 
            // i_experim_input
            // 
            this.i_experim_input.Location = new System.Drawing.Point(240, 39);
            this.i_experim_input.Name = "i_experim_input";
            this.i_experim_input.Size = new System.Drawing.Size(83, 20);
            this.i_experim_input.TabIndex = 18;
            this.i_experim_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Exp Current:";
            // 
            // Pexp_c1neg_dt_input
            // 
            this.Pexp_c1neg_dt_input.Location = new System.Drawing.Point(124, 132);
            this.Pexp_c1neg_dt_input.Name = "Pexp_c1neg_dt_input";
            this.Pexp_c1neg_dt_input.Size = new System.Drawing.Size(83, 20);
            this.Pexp_c1neg_dt_input.TabIndex = 16;
            this.Pexp_c1neg_dt_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(121, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Neg. Min. Time:";
            // 
            // dt_int_input
            // 
            this.dt_int_input.Location = new System.Drawing.Point(124, 84);
            this.dt_int_input.Name = "dt_int_input";
            this.dt_int_input.Size = new System.Drawing.Size(83, 20);
            this.dt_int_input.TabIndex = 14;
            this.dt_int_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Integration Interval:";
            // 
            // p_exp_min_input
            // 
            this.p_exp_min_input.Location = new System.Drawing.Point(124, 39);
            this.p_exp_min_input.Name = "p_exp_min_input";
            this.p_exp_min_input.Size = new System.Drawing.Size(83, 20);
            this.p_exp_min_input.TabIndex = 12;
            this.p_exp_min_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Minimum Power:";
            // 
            // p_exp_fact_input
            // 
            this.p_exp_fact_input.Location = new System.Drawing.Point(10, 132);
            this.p_exp_fact_input.Name = "p_exp_fact_input";
            this.p_exp_fact_input.Size = new System.Drawing.Size(83, 20);
            this.p_exp_fact_input.TabIndex = 10;
            this.p_exp_fact_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "G Exp Factor:";
            // 
            // p_col_fact_input
            // 
            this.p_col_fact_input.Location = new System.Drawing.Point(10, 84);
            this.p_col_fact_input.Name = "p_col_fact_input";
            this.p_col_fact_input.Size = new System.Drawing.Size(83, 20);
            this.p_col_fact_input.TabIndex = 8;
            this.p_col_fact_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "P Column Factor:";
            // 
            // u_k_input
            // 
            this.u_k_input.Location = new System.Drawing.Point(10, 39);
            this.u_k_input.Name = "u_k_input";
            this.u_k_input.Size = new System.Drawing.Size(83, 20);
            this.u_k_input.TabIndex = 6;
            this.u_k_input.Leave += new System.EventHandler(this.ChangeText);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cathode V Drop:";
            // 
            // save_imp_btn
            // 
            this.save_imp_btn.Enabled = false;
            this.save_imp_btn.Location = new System.Drawing.Point(229, 336);
            this.save_imp_btn.Name = "save_imp_btn";
            this.save_imp_btn.Size = new System.Drawing.Size(75, 23);
            this.save_imp_btn.TabIndex = 8;
            this.save_imp_btn.Text = "Save";
            this.save_imp_btn.UseVisualStyleBackColor = true;
            this.save_imp_btn.Click += new System.EventHandler(this.save_imp_btn_Click);
            // 
            // imp_button
            // 
            this.imp_button.Enabled = false;
            this.imp_button.Location = new System.Drawing.Point(310, 336);
            this.imp_button.Name = "imp_button";
            this.imp_button.Size = new System.Drawing.Size(75, 23);
            this.imp_button.TabIndex = 9;
            this.imp_button.Text = "Import";
            this.imp_button.UseVisualStyleBackColor = true;
            this.imp_button.Click += new System.EventHandler(this.imp_button_Click);
            // 
            // imp_all_btn
            // 
            this.imp_all_btn.Enabled = false;
            this.imp_all_btn.Location = new System.Drawing.Point(391, 336);
            this.imp_all_btn.Name = "imp_all_btn";
            this.imp_all_btn.Size = new System.Drawing.Size(75, 23);
            this.imp_all_btn.TabIndex = 10;
            this.imp_all_btn.Text = "Import All";
            this.imp_all_btn.UseVisualStyleBackColor = true;
            this.imp_all_btn.Click += new System.EventHandler(this.imp_btn_all_Click);
            // 
            // v
            // 
            this.v.IsBalloon = true;
            this.v.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // check_close_after_imp
            // 
            this.check_close_after_imp.AutoSize = true;
            this.check_close_after_imp.Location = new System.Drawing.Point(282, 156);
            this.check_close_after_imp.Name = "check_close_after_imp";
            this.check_close_after_imp.Size = new System.Drawing.Size(165, 17);
            this.check_close_after_imp.TabIndex = 20;
            this.check_close_after_imp.Text = "Close this window after import";
            this.check_close_after_imp.UseVisualStyleBackColor = true;
            // 
            // Import_Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 368);
            this.Controls.Add(this.imp_all_btn);
            this.Controls.Add(this.imp_button);
            this.Controls.Add(this.save_imp_btn);
            this.Controls.Add(this.options_group);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Import_Wizard";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CSV Import Wizard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.options_group.ResumeLayout(false);
            this.options_group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label imp_wizard_selected_files;
        private System.Windows.Forms.ComboBox imp_wiz_files;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imp_wizard_output_text;
        private System.Windows.Forms.Button imp_wiz_browse_out;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox options_group;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox u_k_input;
        private System.Windows.Forms.TextBox i_experim_input;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Pexp_c1neg_dt_input;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dt_int_input;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox p_exp_min_input;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox p_exp_fact_input;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox p_col_fact_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button save_imp_btn;
        private System.Windows.Forms.Button imp_button;
        private System.Windows.Forms.Button imp_all_btn;
        private System.Windows.Forms.ToolTip v;
        private System.Windows.Forms.CheckBox save_all_checkbox;
        private System.Windows.Forms.CheckBox check_close_after_imp;
    }
}