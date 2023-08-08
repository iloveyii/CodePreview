namespace BookStoreWinApp
{
    partial class FormSettings
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
            ButtonImagesLocation = new Button();
            label2 = new Label();
            lblImagesLocation = new Label();
            ButtonPdfLocation = new Button();
            label1 = new Label();
            lblPdfLocation = new Label();
            openFileDialog1 = new OpenFileDialog();
            fileSystemWatcher1 = new FileSystemWatcher();
            groupBox2 = new GroupBox();
            ButtonDatabaseConnect = new Button();
            ButtonDatabaseFileLocation = new Button();
            label3 = new Label();
            lblDatabaseFileLocation = new Label();
            groupBox3 = new GroupBox();
            prgExtract = new ProgressBar();
            lblStatus = new Label();
            ButtonStartExtract = new Button();
            ButtonExtractImages = new Button();
            label4 = new Label();
            txtPdfPath = new TextBox();
            label5 = new Label();
            tmrStatus = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ButtonImagesLocation);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lblImagesLocation);
            groupBox1.Controls.Add(ButtonPdfLocation);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblPdfLocation);
            groupBox1.Location = new Point(50, 43);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(752, 217);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "File Locations";
            // 
            // ButtonImagesLocation
            // 
            ButtonImagesLocation.FlatStyle = FlatStyle.Flat;
            ButtonImagesLocation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonImagesLocation.Location = new Point(582, 123);
            ButtonImagesLocation.Name = "ButtonImagesLocation";
            ButtonImagesLocation.Size = new Size(108, 30);
            ButtonImagesLocation.TabIndex = 6;
            ButtonImagesLocation.Text = "Browse..";
            ButtonImagesLocation.UseVisualStyleBackColor = true;
            ButtonImagesLocation.Click += ButtonImagesLocation_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.FlatStyle = FlatStyle.Flat;
            label2.ForeColor = Color.FromArgb(0, 0, 192);
            label2.Location = new Point(50, 120);
            label2.Name = "label2";
            label2.Padding = new Padding(7, 9, 7, 9);
            label2.Size = new Size(131, 35);
            label2.TabIndex = 5;
            label2.Text = "Image Files Location";
            // 
            // lblImagesLocation
            // 
            lblImagesLocation.BackColor = Color.White;
            lblImagesLocation.BorderStyle = BorderStyle.FixedSingle;
            lblImagesLocation.FlatStyle = FlatStyle.Flat;
            lblImagesLocation.Location = new Point(50, 120);
            lblImagesLocation.Name = "lblImagesLocation";
            lblImagesLocation.Padding = new Padding(140, 0, 0, 0);
            lblImagesLocation.Size = new Size(643, 35);
            lblImagesLocation.TabIndex = 4;
            lblImagesLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ButtonPdfLocation
            // 
            ButtonPdfLocation.FlatStyle = FlatStyle.Flat;
            ButtonPdfLocation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonPdfLocation.Location = new Point(582, 66);
            ButtonPdfLocation.Name = "ButtonPdfLocation";
            ButtonPdfLocation.Size = new Size(108, 30);
            ButtonPdfLocation.TabIndex = 3;
            ButtonPdfLocation.Text = "Browse..";
            ButtonPdfLocation.UseVisualStyleBackColor = true;
            ButtonPdfLocation.Click += ButtonPdfLocation_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.FlatStyle = FlatStyle.Flat;
            label1.ForeColor = Color.FromArgb(0, 0, 192);
            label1.Location = new Point(50, 63);
            label1.Name = "label1";
            label1.Padding = new Padding(7, 9, 7, 9);
            label1.Size = new Size(119, 35);
            label1.TabIndex = 2;
            label1.Text = "PDF Files Location";
            // 
            // lblPdfLocation
            // 
            lblPdfLocation.BackColor = Color.White;
            lblPdfLocation.BorderStyle = BorderStyle.FixedSingle;
            lblPdfLocation.FlatStyle = FlatStyle.Flat;
            lblPdfLocation.Location = new Point(50, 63);
            lblPdfLocation.Name = "lblPdfLocation";
            lblPdfLocation.Padding = new Padding(125, 0, 0, 0);
            lblPdfLocation.Size = new Size(643, 35);
            lblPdfLocation.TabIndex = 1;
            lblPdfLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ButtonDatabaseConnect);
            groupBox2.Controls.Add(ButtonDatabaseFileLocation);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(lblDatabaseFileLocation);
            groupBox2.Location = new Point(50, 287);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(752, 170);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Connect to Database";
            // 
            // ButtonDatabaseConnect
            // 
            ButtonDatabaseConnect.FlatStyle = FlatStyle.Flat;
            ButtonDatabaseConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonDatabaseConnect.Location = new Point(548, 110);
            ButtonDatabaseConnect.Name = "ButtonDatabaseConnect";
            ButtonDatabaseConnect.Size = new Size(147, 30);
            ButtonDatabaseConnect.TabIndex = 14;
            ButtonDatabaseConnect.Text = "Connect Database";
            ButtonDatabaseConnect.UseVisualStyleBackColor = true;
            ButtonDatabaseConnect.Click += ButtonDatabaseConnect_Click;
            // 
            // ButtonDatabaseFileLocation
            // 
            ButtonDatabaseFileLocation.FlatStyle = FlatStyle.Flat;
            ButtonDatabaseFileLocation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonDatabaseFileLocation.Location = new Point(587, 50);
            ButtonDatabaseFileLocation.Name = "ButtonDatabaseFileLocation";
            ButtonDatabaseFileLocation.Size = new Size(108, 30);
            ButtonDatabaseFileLocation.TabIndex = 13;
            ButtonDatabaseFileLocation.Text = "Browse..";
            ButtonDatabaseFileLocation.UseVisualStyleBackColor = true;
            ButtonDatabaseFileLocation.Click += ButtonDatabaseFileLocation_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.FlatStyle = FlatStyle.Flat;
            label3.ForeColor = Color.FromArgb(0, 0, 192);
            label3.Location = new Point(55, 47);
            label3.Name = "label3";
            label3.Padding = new Padding(7, 9, 7, 9);
            label3.Size = new Size(141, 35);
            label3.TabIndex = 12;
            label3.Text = "Database File Location";
            // 
            // lblDatabaseFileLocation
            // 
            lblDatabaseFileLocation.BackColor = Color.White;
            lblDatabaseFileLocation.BorderStyle = BorderStyle.FixedSingle;
            lblDatabaseFileLocation.FlatStyle = FlatStyle.Flat;
            lblDatabaseFileLocation.Location = new Point(55, 47);
            lblDatabaseFileLocation.Name = "lblDatabaseFileLocation";
            lblDatabaseFileLocation.Padding = new Padding(140, 0, 0, 0);
            lblDatabaseFileLocation.Size = new Size(643, 35);
            lblDatabaseFileLocation.TabIndex = 11;
            lblDatabaseFileLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(prgExtract);
            groupBox3.Controls.Add(lblStatus);
            groupBox3.Controls.Add(ButtonStartExtract);
            groupBox3.Controls.Add(ButtonExtractImages);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(txtPdfPath);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(50, 484);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(752, 226);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Extract images";
            // 
            // prgExtract
            // 
            prgExtract.Location = new Point(51, 173);
            prgExtract.Minimum = 1;
            prgExtract.Name = "prgExtract";
            prgExtract.Size = new Size(647, 15);
            prgExtract.Step = 1;
            prgExtract.TabIndex = 17;
            prgExtract.Value = 40;
            prgExtract.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.Green;
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(55, 147);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Status:";
            // 
            // ButtonStartExtract
            // 
            ButtonStartExtract.FlatStyle = FlatStyle.Flat;
            ButtonStartExtract.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonStartExtract.Location = new Point(548, 118);
            ButtonStartExtract.Name = "ButtonStartExtract";
            ButtonStartExtract.Size = new Size(147, 30);
            ButtonStartExtract.TabIndex = 14;
            ButtonStartExtract.Text = "Start Extract";
            ButtonStartExtract.UseVisualStyleBackColor = true;
            ButtonStartExtract.Click += ButtonStartExtract_Click;
            // 
            // ButtonExtractImages
            // 
            ButtonExtractImages.FlatStyle = FlatStyle.Flat;
            ButtonExtractImages.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonExtractImages.Location = new Point(587, 50);
            ButtonExtractImages.Name = "ButtonExtractImages";
            ButtonExtractImages.Size = new Size(108, 30);
            ButtonExtractImages.TabIndex = 13;
            ButtonExtractImages.Text = "Browse..";
            ButtonExtractImages.UseVisualStyleBackColor = true;
            ButtonExtractImages.Click += ButtonExtractImages_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.FlatStyle = FlatStyle.Flat;
            label4.ForeColor = Color.FromArgb(0, 0, 192);
            label4.Location = new Point(55, 47);
            label4.Name = "label4";
            label4.Padding = new Padding(7, 9, 7, 9);
            label4.Size = new Size(119, 35);
            label4.TabIndex = 12;
            label4.Text = "PDF Files Location";
            // 
            // txtPdfPath
            // 
            txtPdfPath.AcceptsReturn = true;
            txtPdfPath.BorderStyle = BorderStyle.None;
            txtPdfPath.Location = new Point(179, 56);
            txtPdfPath.Margin = new Padding(1);
            txtPdfPath.Multiline = true;
            txtPdfPath.Name = "txtPdfPath";
            txtPdfPath.PlaceholderText = "Enter path";
            txtPdfPath.Size = new Size(399, 22);
            txtPdfPath.TabIndex = 19;
            txtPdfPath.WordWrap = false;
            // 
            // label5
            // 
            label5.BackColor = Color.White;
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Location = new Point(55, 47);
            label5.Name = "label5";
            label5.Padding = new Padding(140, 0, 0, 0);
            label5.Size = new Size(643, 35);
            label5.TabIndex = 18;
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tmrStatus
            // 
            tmrStatus.Enabled = true;
            tmrStatus.Interval = 3000;
            tmrStatus.Tick += tmrSuccess_Tick;
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(895, 861);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MinimumSize = new Size(700, 0);
            Name = "FormSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += FormSettings_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private OpenFileDialog openFileDialog1;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label1;
        private Label lblPdfLocation;
        private Button ButtonPdfLocation;
        private Button ButtonImagesLocation;
        private Label label2;
        private Label lblImagesLocation;
        private GroupBox groupBox2;
        private Button ButtonDatabaseConnect;
        private Button ButtonDatabaseFileLocation;
        private Label label3;
        private Label lblDatabaseFileLocation;
        private GroupBox groupBox3;
        private Button ButtonExtractImages;
        public Label lblStatus;
        public Label label4;
        private System.Windows.Forms.Timer tmrSuccess;
        public System.Windows.Forms.Timer tmrStatus;
        public ProgressBar prgExtract;
        public Button ButtonStartExtract;
        public TextBox txtPdfPath;
        private Label label5;
    }
}