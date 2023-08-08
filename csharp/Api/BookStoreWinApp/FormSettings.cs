using BookStore.Models;
using BookStore.Services.BookService;
using BookStore.Services.SettingsService;
using BookStoreWinApp.Models;
using System.Configuration;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStoreWinApp
{
    public partial class FormSettings : Form
    {
        public IBookService bookService;
        public List<Book> books;

        public ISettingsService settingsService;
        public Dictionary<string, string> settings = new Dictionary<string, string>();
        public readonly ILogger logger;

        public FormSettings(IBookService bookService, ISettingsService settingsService, ILogger logger)
        {
            InitializeComponent();

            this.settingsService = settingsService;
            settings = settingsService.GetAllDictionary();

            this.bookService = bookService;
            books = bookService.GetAllBooks();

            this.logger = logger;
        }

        private void ButtonPdfLocation_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult = fbd.ShowDialog();

            if (DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                lblPdfLocation.Text = fbd.SelectedPath;
                Save("PdfFilesLocation", fbd.SelectedPath);
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form load of FormSettings");
            LoadSettings();
            tmrStatus.Start();
        }

        private void ButtonImagesLocation_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult = fbd.ShowDialog();

            if (DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                lblImagesLocation.Text = fbd.SelectedPath;
                Save("ImagesLocation", fbd.SelectedPath);
            }
        }

        private void Save(string Key, string Value)
        {
            var settings = new Settings() { Key = Key, Value = Value };
            settingsService.UpSert(settings);
        }

        private void LoadSettings()
        {
            var pdfLocation = settingsService.SearchByKey("PdfFilesLocation");
            var imageLocation = settingsService.SearchByKey("ImagesLocation");

            if (pdfLocation != null)
            {
                lblPdfLocation.Text = pdfLocation.Value;
            }

            if (imageLocation != null)
            {
                lblImagesLocation.Text = imageLocation.Value;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        private Dictionary<string, List<string>> GetAllFiles(string path)
        {
            var files = ManageFiles.GetAllFiles(path, "pdf");
            return files;
        }

        private void BtnExtract_Click(object sender, EventArgs e)
        {
            // BtnExtract.Enabled = false;
            var pdfPath = ConfigurationManager.AppSettings.Get("pdfDirectoryPath");
            var relPath = Path.GetFullPath(pdfPath);
            var files = GetAllFiles(relPath);

            foreach (var file in files)
            {
                var fullPathImageDirectory = Path.GetFullPath(ConfigurationManager.AppSettings.Get("imageDirectoryPath") + "/" + file.Key);
                if (!Directory.Exists(fullPathImageDirectory))
                {
                    Directory.CreateDirectory(fullPathImageDirectory);
                }
                foreach (var fullPathPdfFile in file.Value)
                {
                    if (File.Exists(fullPathPdfFile))
                    {
                        // DataExtract.ExtractImage(fullPathImageDirectory, fullPathPdfFile);
                    }
                    else
                    {
                        MessageBox.Show("Path does not exist " + pdfPath);
                    }
                }

            }

            // BtnExtract.Enabled = true;
        }

        private void ButtonDatabaseFileLocation_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.InitialDirectory = "./";
            ofd.Filter = "DB files (*.db)|*.db";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;
            DialogResult = ofd.ShowDialog();

            if (DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
            {
                lblDatabaseFileLocation.Text = ofd.FileName;
                // Save("ImagesLocation", ofd.SelectedPath);
            }
        }

        private void ButtonDatabaseConnect_Click(object sender, EventArgs e)
        {
            var options = new DbContextOptionsBuilder<SqliteContext>()
            .UseSqlite(lblDatabaseFileLocation.Text)
            .Options;

            try
            {
                var db = new SqliteContext(options);
                var canConnect = db.Database.CanConnect();

                if (canConnect)
                {
                    MessageBox.Show("Connection is successful", "Database Connection", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Connection is not successful", "Database Connection", MessageBoxButtons.OK);
            }
        }

        private void ButtonExtractImages_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult = fbd.ShowDialog();

            if (DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                txtPdfPath.Text = fbd.SelectedPath;
            }
        }

        private void ButtonStartExtract_Click(object sender, EventArgs e)
        {
            var formExtract = new FormExtract();
            formExtract.ExtractDirectory();
        }

        private void tmrSuccess_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer of FormSettings ticks");
        }
    }
}
