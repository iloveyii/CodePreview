using BookStore.Models;
using BookStore.Services.BookService;
using BookStore.Services.SettingsService;
using BookStoreWinApp.Models;
using ImageLibrary.Models;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStoreWinApp
{
    public partial class FormEntry : Form
    {
        public IBookService bookService;
        public ISettingsService settingsService;
        Dictionary<string, string> settings = new Dictionary<string, string>();
        List<Book> books;
        List<string> categories;
        Book currentBook;

        public FormEntry(IBookService bookService, ISettingsService settingsService, Book? book)
        {
            InitializeComponent();
            this.settingsService = settingsService;
            this.bookService = bookService;
            this.currentBook = book;
        }

        private void FormEntry_Load(object sender, EventArgs e)
        {
            settings = settingsService.GetAllDictionary();
            categories = bookService.GetAllCategories();
            ComboCategory.Items.Clear();
            ComboCategory.DataSource = categories;

            if (currentBook != null)
            {
                txtTitle.Text = currentBook.Title;
                txtAuthors.Text = currentBook.AuthorNames;
                txtPages.Text = currentBook.Pages.ToString();
                txtPublicationYear.Text = currentBook.Pages.ToString();
                var pdfFullPath = $"{settings["PdfFilesLocation"]}\\{currentBook.Category}\\{currentBook.PdfPath}";
                txtPdfFile.Text = pdfFullPath;
                picBook.ImageLocation = currentBook.ImagePath;
            }
        }

        private bool ValidateForm()
        {
            if (txtTitle.Text == string.Empty)
            {
                lblError.Text = "Title cannot be empty";
                return false;
            }
            int c;
            if (txtPublicationYear.Text != string.Empty)
            {
                var pYear = int.TryParse(txtPublicationYear.Text, out c);
                if (!pYear)
                {
                    lblError.Text = "Publication year should be either a number or empty";
                    return false;
                }
            }

            if (txtPages.Text != string.Empty && !int.TryParse(txtPages.Text, out c))
            {
                var bPages = int.TryParse(txtPages.Text, out c);
                if (!bPages)
                {
                    lblError.Text = "Number of pages should be either a number or empty";
                    return false;
                }
            }

            if (txtPdfFile.Text != string.Empty && !File.Exists(txtPdfFile.Text))
            {
                lblError.Text = $"PDF file does not exist: {txtPdfFile.Text}";
                return false;
            }

            return true;
        }

        private string ExtractImage()
        {
            var biggerImagePath = "";
            if (File.Exists(txtPdfFile.Text))
            {
                var fullPathImageDirectory = Path.GetDirectoryName(txtPdfFile.Text);
                var parentDir = Path.GetFileName(ManageFiles.ParentDirectory(txtPdfFile.Text));
                var imageSubDir = $"{fullPathImageDirectory}\\{parentDir}";
                var imageCleanFileName = ManageFiles.GetCleanImageFileName(txtPdfFile.Text);
                //biggerImagePath = DataExtract.ExtractImage(imageSubDir, imageCleanFileName, txtPdfFile.Text);
                biggerImagePath = ExtractUsingGemBox(txtPdfFile.Text, Path.Combine(fullPathImageDirectory, imageCleanFileName + ".jpeg"));
            }
            else
            {
                MessageBox.Show("Path does not exist " + txtPdfFile.Text);
            }

            return biggerImagePath;
        }

        private string ExtractUsingGemBox(string pdfFilePath, string imageFilePath)
        {
            //ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            //var document = PdfDocument.Load(pdfFilePath);
            //var imageOptions = new ImageSaveOptions(ImageSaveFormat.Jpeg)
            //{
            //    PageNumber = 0,
            //    Width = 650
            //};

            //var imageEditing = new ImageEdit(imageFilePath);
            //var incrementedImageFilePath = imageFilePath;
            //if (File.Exists(imageFilePath))
            //{
            //    incrementedImageFilePath = imageEditing.SaveAsIncremented(false);
            //}

            // Pack error
            // document.Save(incrementedImageFilePath);

            // Use PdfEdit - Unable to load DLL
            //var pdfEdit = new PdfEdit(pdfFilePath, 650);
            //pdfEdit.SaveCoverImageToLinux(incrementedImageFilePath);

            // Try streams
            GetPdfFirstPageAsImage(pdfFilePath, imageFilePath);

            return imageFilePath;

            // Use ImageMagick
            var settings = new MagickReadSettings()
            {
                Density = new Density(300, 300),
                Width = 650
            };

            using var images = new MagickImageCollection();
            images.Read(pdfFilePath, settings);
            var page = 1;
            foreach (var image in images)
            {
                image.Format = MagickFormat.Jpeg;
                image.Write(imageFilePath);
                page++;
                break;
            }
            // Resize it 
            //imageEditing = new ImageEdit(incrementedImageFilePath);
            //imageEditing.CropCenter(650, 900);
            //var resizedPath = imageEditing.SaveAsIncremented();
            return imageFilePath;
        }

        private void SaveToDatabase(Book book)
        {
            lblStatus.Text = "Extracting image...";
            lblStatus.Visible = true;
            lblStatus.BackColor = Color.Orange;

            var imageTask = Task.Run(() =>
            {
                return ExtractImage();
            });

            imageTask.ContinueWith(task =>
            {
                book.ImagePath = imageTask.Result;
                lblStatus.Text = "Done !";
                lblStatus.BackColor = Color.Green;
                picBook.ImageLocation = imageTask.Result;

                if (currentBook != null)
                {
                    currentBook.update(book);
                    bookService.UpdateBook(currentBook.Id, book);
                }
                else
                {
                    bookService.CreateBook(book);
                }
            });
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) { return; }

            Book book = new Book()
            {
                Title = txtTitle.Text,
                AuthorNames = txtAuthors.Text,
                Category = ComboCategory.Text
            };

            if (txtPages.Text != string.Empty)
            {
                book.Pages = Convert.ToInt32(txtPages.Text);
            }

            if (txtPublicationYear.Text != string.Empty)
            {
                book.PublicationYear = Convert.ToInt32(txtPublicationYear.Text);
            }

            if (txtPdfFile.Text != string.Empty)
            {
                book.PdfPath = txtPdfFile.Text;
            }

            SaveToDatabase(book);
            ClearForm();
            lblSuccess.Visible = true;
            tmrShowSuccess.Enabled = true;
            tmrShowSuccess.Start();
        }


        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtAuthors.Text = string.Empty;
            txtPublicationYear.Text = string.Empty;
            txtAuthors.Text = string.Empty;
            lblError.Text = string.Empty;
        }

        private void GetPdfFirstPageAsImage(string pdfPath, string imagePath)
        {
            var tiffImage = TiffImage.Extract(pdfPath, imagePath);
        }

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            if (txtPdfFile.Text == string.Empty)
            {
                ofd.InitialDirectory = "./";
            }
            else
            {
                var pathDir = Path.GetDirectoryName(txtPdfFile.Text);
                ofd.InitialDirectory = pathDir;

            }
            ofd.Filter = "PDF files (*.pdf)|*.pdf";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;
            DialogResult = ofd.ShowDialog();

            if (DialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
            {
                txtPdfFile.Text = ofd.FileName;

                // Save("ImagesLocation", ofd.SelectedPath);
            }
        }

        private void tmrShowSuccess_Tick(object sender, EventArgs e)
        {
            lblSuccess.Visible = false;
            tmrShowSuccess.Stop();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
