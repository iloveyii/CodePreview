namespace Receipt
{
    partial class Settings
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
            this.tabApi = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDatabasePath = new System.Windows.Forms.Button();
            this.txtDatabasePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApiEndpoint = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLogoPath2 = new System.Windows.Forms.Button();
            this.txtLogoPath2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCutReceipt = new System.Windows.Forms.CheckBox();
            this.rdoBoth = new System.Windows.Forms.RadioButton();
            this.rdoShort = new System.Windows.Forms.RadioButton();
            this.rdoFull = new System.Windows.Forms.RadioButton();
            this.btnLogoPath = new System.Windows.Forms.Button();
            this.txtLogoPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fleDatabase = new System.Windows.Forms.OpenFileDialog();
            this.tabApi.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabApi
            // 
            this.tabApi.Controls.Add(this.tabPage1);
            this.tabApi.Controls.Add(this.tabPage2);
            this.tabApi.Controls.Add(this.tabPage3);
            this.tabApi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabApi.Location = new System.Drawing.Point(12, 17);
            this.tabApi.Name = "tabApi";
            this.tabApi.SelectedIndex = 0;
            this.tabApi.Size = new System.Drawing.Size(857, 486);
            this.tabApi.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.btnDatabasePath);
            this.tabPage1.Controls.Add(this.txtDatabasePath);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtApiEndpoint);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(10);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(15);
            this.tabPage1.Size = new System.Drawing.Size(849, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "API";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDatabasePath
            // 
            this.btnDatabasePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatabasePath.Location = new System.Drawing.Point(185, 140);
            this.btnDatabasePath.Name = "btnDatabasePath";
            this.btnDatabasePath.Size = new System.Drawing.Size(93, 35);
            this.btnDatabasePath.TabIndex = 6;
            this.btnDatabasePath.Text = "Browse";
            this.btnDatabasePath.UseVisualStyleBackColor = true;
            this.btnDatabasePath.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDatabasePath
            // 
            this.txtDatabasePath.Location = new System.Drawing.Point(184, 90);
            this.txtDatabasePath.Name = "txtDatabasePath";
            this.txtDatabasePath.Size = new System.Drawing.Size(352, 24);
            this.txtDatabasePath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Database location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Endpoint";
            // 
            // txtApiEndpoint
            // 
            this.txtApiEndpoint.Location = new System.Drawing.Point(181, 39);
            this.txtApiEndpoint.Name = "txtApiEndpoint";
            this.txtApiEndpoint.Size = new System.Drawing.Size(355, 24);
            this.txtApiEndpoint.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLogoPath2);
            this.tabPage2.Controls.Add(this.txtLogoPath2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnLogoPath);
            this.tabPage2.Controls.Add(this.txtLogoPath);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(849, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Receipt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLogoPath2
            // 
            this.btnLogoPath2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogoPath2.Location = new System.Drawing.Point(146, 171);
            this.btnLogoPath2.Name = "btnLogoPath2";
            this.btnLogoPath2.Size = new System.Drawing.Size(93, 35);
            this.btnLogoPath2.TabIndex = 13;
            this.btnLogoPath2.Text = "Browse";
            this.btnLogoPath2.UseVisualStyleBackColor = true;
            this.btnLogoPath2.Click += new System.EventHandler(this.btnLogoPath2_Click);
            // 
            // txtLogoPath2
            // 
            this.txtLogoPath2.Location = new System.Drawing.Point(145, 136);
            this.txtLogoPath2.Name = "txtLogoPath2";
            this.txtLogoPath2.Size = new System.Drawing.Size(352, 24);
            this.txtLogoPath2.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(68, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "Logo 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Printer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCutReceipt);
            this.groupBox1.Controls.Add(this.rdoBoth);
            this.groupBox1.Controls.Add(this.rdoShort);
            this.groupBox1.Controls.Add(this.rdoFull);
            this.groupBox1.Location = new System.Drawing.Point(148, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 191);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receipt Type";
            // 
            // chkCutReceipt
            // 
            this.chkCutReceipt.AutoSize = true;
            this.chkCutReceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCutReceipt.Location = new System.Drawing.Point(207, 59);
            this.chkCutReceipt.Name = "chkCutReceipt";
            this.chkCutReceipt.Size = new System.Drawing.Size(98, 22);
            this.chkCutReceipt.TabIndex = 3;
            this.chkCutReceipt.Text = "Cut reciept";
            this.chkCutReceipt.UseVisualStyleBackColor = true;
            // 
            // rdoBoth
            // 
            this.rdoBoth.AutoSize = true;
            this.rdoBoth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoBoth.Location = new System.Drawing.Point(43, 126);
            this.rdoBoth.MinimumSize = new System.Drawing.Size(100, 0);
            this.rdoBoth.Name = "rdoBoth";
            this.rdoBoth.Size = new System.Drawing.Size(100, 22);
            this.rdoBoth.TabIndex = 2;
            this.rdoBoth.TabStop = true;
            this.rdoBoth.Text = "Both";
            this.rdoBoth.UseVisualStyleBackColor = true;
            // 
            // rdoShort
            // 
            this.rdoShort.AutoSize = true;
            this.rdoShort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoShort.Location = new System.Drawing.Point(43, 89);
            this.rdoShort.MinimumSize = new System.Drawing.Size(100, 0);
            this.rdoShort.Name = "rdoShort";
            this.rdoShort.Size = new System.Drawing.Size(100, 22);
            this.rdoShort.TabIndex = 1;
            this.rdoShort.TabStop = true;
            this.rdoShort.Text = "Short";
            this.rdoShort.UseVisualStyleBackColor = true;
            // 
            // rdoFull
            // 
            this.rdoFull.AutoSize = true;
            this.rdoFull.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoFull.Location = new System.Drawing.Point(43, 52);
            this.rdoFull.MinimumSize = new System.Drawing.Size(100, 0);
            this.rdoFull.Name = "rdoFull";
            this.rdoFull.Size = new System.Drawing.Size(100, 22);
            this.rdoFull.TabIndex = 0;
            this.rdoFull.TabStop = true;
            this.rdoFull.Text = "Full";
            this.rdoFull.UseVisualStyleBackColor = true;
            // 
            // btnLogoPath
            // 
            this.btnLogoPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogoPath.Location = new System.Drawing.Point(149, 74);
            this.btnLogoPath.Name = "btnLogoPath";
            this.btnLogoPath.Size = new System.Drawing.Size(93, 35);
            this.btnLogoPath.TabIndex = 8;
            this.btnLogoPath.Text = "Browse";
            this.btnLogoPath.UseVisualStyleBackColor = true;
            this.btnLogoPath.Click += new System.EventHandler(this.btnLogoPath_Click);
            // 
            // txtLogoPath
            // 
            this.txtLogoPath.Location = new System.Drawing.Point(148, 39);
            this.txtLogoPath.Name = "txtLogoPath";
            this.txtLogoPath.Size = new System.Drawing.Size(352, 24);
            this.txtLogoPath.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Logo";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(849, 455);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sync";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(202, 122);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(136, 32);
            this.button6.TabIndex = 3;
            this.button6.Text = "Sync";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "All data";
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(202, 62);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(136, 32);
            this.button5.TabIndex = 1;
            this.button5.Text = "Sync";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Services";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(772, 541);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(651, 541);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 35);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 588);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabApi);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.tabApi.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabApi;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button btnDatabasePath;
        private TextBox txtDatabasePath;
        private Label label2;
        private Label label1;
        private TextBox txtApiEndpoint;
        private Button btnSave;
        private Button btnCancel;
        private OpenFileDialog fleDatabase;
        private Label label4;
        private GroupBox groupBox1;
        private CheckBox chkCutReceipt;
        private RadioButton rdoBoth;
        private RadioButton rdoShort;
        private RadioButton rdoFull;
        private Button btnLogoPath;
        private TextBox txtLogoPath;
        private Label label3;
        private Button button5;
        private Label label5;
        private Button button6;
        private Label label6;
        private Button btnLogoPath2;
        private TextBox txtLogoPath2;
        private Label label7;
    }
}