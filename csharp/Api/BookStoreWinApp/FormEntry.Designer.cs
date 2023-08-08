namespace BookStoreWinApp
{
    partial class FormEntry
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
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            lblStatus = new Label();
            picBook = new PictureBox();
            lblSuccess = new Label();
            ButtonUpload = new Button();
            label6 = new Label();
            txtPdfFile = new TextBox();
            txtAuthors = new TextBox();
            btnSave = new Button();
            txtPublicationYear = new TextBox();
            txtPages = new TextBox();
            lblError = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            ComboCategory = new ComboBox();
            label2 = new Label();
            txtTitle = new TextBox();
            label1 = new Label();
            tmrShowSuccess = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBook).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(lblStatus);
            groupBox1.Controls.Add(picBook);
            groupBox1.Controls.Add(lblSuccess);
            groupBox1.Controls.Add(ButtonUpload);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtPdfFile);
            groupBox1.Controls.Add(txtAuthors);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtPublicationYear);
            groupBox1.Controls.Add(txtPages);
            groupBox1.Controls.Add(lblError);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(931, 496);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Enter a new book";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // lblStatus
            // 
            lblStatus.AccessibleRole = AccessibleRole.Application;
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.FromArgb(255, 192, 128);
            lblStatus.Location = new Point(685, 108);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 15);
            lblStatus.TabIndex = 17;
            lblStatus.Text = "Status: ";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // picBook
            // 
            picBook.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picBook.Location = new Point(685, 138);
            picBook.Name = "picBook";
            picBook.Size = new Size(229, 305);
            picBook.SizeMode = PictureBoxSizeMode.Zoom;
            picBook.TabIndex = 16;
            picBook.TabStop = false;
            // 
            // lblSuccess
            // 
            lblSuccess.AutoSize = true;
            lblSuccess.BackColor = Color.Teal;
            lblSuccess.BorderStyle = BorderStyle.FixedSingle;
            lblSuccess.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSuccess.ForeColor = Color.White;
            lblSuccess.Location = new Point(48, 404);
            lblSuccess.Name = "lblSuccess";
            lblSuccess.Padding = new Padding(5);
            lblSuccess.Size = new Size(158, 27);
            lblSuccess.TabIndex = 15;
            lblSuccess.Text = "Book saved successfully !";
            lblSuccess.Visible = false;
            // 
            // ButtonUpload
            // 
            ButtonUpload.FlatStyle = FlatStyle.Flat;
            ButtonUpload.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonUpload.Location = new Point(515, 300);
            ButtonUpload.Name = "ButtonUpload";
            ButtonUpload.Size = new Size(75, 23);
            ButtonUpload.TabIndex = 14;
            ButtonUpload.Text = "Browse";
            ButtonUpload.UseVisualStyleBackColor = true;
            ButtonUpload.Click += ButtonUpload_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(48, 304);
            label6.Name = "label6";
            label6.Size = new Size(90, 15);
            label6.TabIndex = 13;
            label6.Text = "Pdf file location";
            // 
            // txtPdfFile
            // 
            txtPdfFile.BorderStyle = BorderStyle.FixedSingle;
            txtPdfFile.Location = new Point(229, 300);
            txtPdfFile.Name = "txtPdfFile";
            txtPdfFile.Size = new Size(284, 23);
            txtPdfFile.TabIndex = 12;
            // 
            // txtAuthors
            // 
            txtAuthors.BorderStyle = BorderStyle.FixedSingle;
            txtAuthors.Location = new Point(229, 252);
            txtAuthors.Name = "txtAuthors";
            txtAuthors.Size = new Size(284, 23);
            txtAuthors.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(541, 404);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(108, 32);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtPublicationYear
            // 
            txtPublicationYear.BorderStyle = BorderStyle.FixedSingle;
            txtPublicationYear.Location = new Point(229, 156);
            txtPublicationYear.Name = "txtPublicationYear";
            txtPublicationYear.Size = new Size(134, 23);
            txtPublicationYear.TabIndex = 7;
            // 
            // txtPages
            // 
            txtPages.BorderStyle = BorderStyle.FixedSingle;
            txtPages.Location = new Point(229, 204);
            txtPages.Name = "txtPages";
            txtPages.Size = new Size(206, 23);
            txtPages.TabIndex = 8;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.FromArgb(192, 0, 0);
            lblError.Location = new Point(48, 408);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(48, 256);
            label5.Name = "label5";
            label5.Size = new Size(158, 15);
            label5.TabIndex = 11;
            label5.Text = "Authors ( comma separated)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 208);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 9;
            label4.Text = "Number of pages";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 160);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Year of publication";
            // 
            // ComboCategory
            // 
            ComboCategory.DropDownHeight = 75;
            ComboCategory.FlatStyle = FlatStyle.System;
            ComboCategory.FormattingEnabled = true;
            ComboCategory.IntegralHeight = false;
            ComboCategory.ItemHeight = 15;
            ComboCategory.Location = new Point(229, 108);
            ComboCategory.Name = "ComboCategory";
            ComboCategory.Size = new Size(206, 23);
            ComboCategory.TabIndex = 1;
            ComboCategory.Tag = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 112);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 5;
            label2.Text = "Category";
            // 
            // txtTitle
            // 
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.Location = new Point(229, 60);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(420, 23);
            txtTitle.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 64);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 4;
            label1.Text = "Title";
            // 
            // tmrShowSuccess
            // 
            tmrShowSuccess.Interval = 5000;
            tmrShowSuccess.Tick += tmrShowSuccess_Tick;
            // 
            // FormEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 523);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTitle);
            Controls.Add(ComboCategory);
            Controls.Add(groupBox1);
            Name = "FormEntry";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormEntry";
            Load += FormEntry_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBook).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private Button btnSave;
        private TextBox txtAuthors;
        private Label label5;
        private TextBox txtPages;
        private Label label4;
        private TextBox txtPublicationYear;
        private Label label3;
        private ComboBox ComboCategory;
        private Label label2;
        private TextBox txtTitle;
        private Label label1;
        private Label lblError;
        private Button ButtonUpload;
        private Label label6;
        private Label lblSuccess;
        private PictureBox picBook;
        public Label lblStatus;
        public TextBox txtPdfFile;
        public System.Windows.Forms.Timer tmrShowSuccess;
    }
}