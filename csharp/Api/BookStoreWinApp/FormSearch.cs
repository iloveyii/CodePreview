using BookStore.Models;
using BookStore.Services.BookService;
using BookStore.Services.SettingsService;
using BookStoreWinApp.Models;


namespace BookStoreWinApp
{
    public partial class FormSearch : Form
    {
        List<Book> books;
        IBookService bookService;
        ISettingsService settingsService;
        Dictionary<string, string> settings = new Dictionary<string, string>();
        Form activeForm;
        Control searchFormPnlDesktop;

        public FormSearch(IBookService bookService, ISettingsService settingsService)
        {
            InitializeComponent();
            this.bookService = bookService;
            this.settingsService = settingsService;
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            GridBooks.ShowCellToolTips = false;
            // LoadBooks();
            GridBooksColumnWidthSet();
            settings = settingsService.GetAllDictionary();

        }

        private void LoadBooks()
        {
            var booksTask = Task.Run(() =>
            {
                books = bookService.GetAllBooks();
            });
            booksTask.Wait();
            GridBooks.DataSource = books;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            books = bookService.SearchByTitle(txtSearch.Text);
            GridBooks.DataSource = books;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            LoadBooks();
            txtSearch.Text = string.Empty;
        }

        private void GridBooksColumnWidthSet()
        {
            GridBooks.Columns[0].Width = 20;
            GridBooks.Columns[0].HeaderText = "#";

            GridBooks.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridBooks.Columns[1].FillWeight = 60;

            GridBooks.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridBooks.Columns[2].FillWeight = 10;

            GridBooks.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridBooks.Columns[3].FillWeight = 10;

            GridBooks.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GridBooks.Columns[4].FillWeight = 10;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GridBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var p = settings["PdfFilesLocation"];

            var pdfFileName = books[e.RowIndex].PdfPath;
            var category = books[e.RowIndex].Category;
            if (pdfFileName is null)
            {
                return;
            }
            var fullFilePath = (Path.GetFullPath(Path.Combine(new string[] { p, category, pdfFileName })));

            if (GridBooks.Columns[e.ColumnIndex].Name == "Locate")
            {
                Helper.LocateFile(fullFilePath);
            }

            if (GridBooks.Columns[e.ColumnIndex].Name == "View")
            {
                Helper.OpenFile(fullFilePath);
            }
        }

        private void ShowBookImage(int rowIndex)
        {
            if (rowIndex == 0 || rowIndex.ToString() is null)
            {
                rowIndex = 0;
            }

            if (books.Count < 1)
            {
                return;
            }

            try
            {
                var i = settings["ImagesLocation"];
                var category = books[rowIndex].Category;
                var imageFileName = Path.GetFileName(books[rowIndex].ImagePath);
                var imageFilePathFull = books[rowIndex].ImagePath;
                var b = imageFilePathFull.Contains("/");
                var fullFilePath = (Path.GetFullPath(Path.Combine(new string[] { i, category, imageFileName })));
                var fullFilePathSingleSlash = fullFilePath.Replace(@"\\", @"\");
                if (imageFilePathFull.Contains("\\") || imageFilePathFull.Contains("/"))
                {
                    picBook.Image = Image.FromFile(imageFilePathFull);
                }
                else
                {
                    if (File.Exists(fullFilePathSingleSlash))
                    {
                        picBook.Image = Image.FromFile(fullFilePathSingleSlash);
                    }
                }
            }
            catch
            {
                picBook.Image = picBook.InitialImage;
            }
        }

        private void SetActiveButton(object btnSender)
        {
            // throw new NotImplementedException();
        }

        private void GridBooks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ShowBookImage(e.RowIndex);
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
        }

        private void ButtonSearchForm_Click(object sender, EventArgs e)
        {
        }

        private void FormSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            this.Parent = null;
            e.Cancel = true;*/
        }

        private void tmrLoadForm_Tick(object sender, EventArgs e)
        {
            LoadBooks();
            tmrLoadForm.Stop();
        }

        private void GridBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GridBooks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex.ToString() is null)
            {
                return;
            }
            Book book = books[e.RowIndex];
            FormEntry childForm = new FormEntry(bookService, settingsService, book);
            var p = this.Parent as Panel;
            this.Visible = false;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //pnlDesktop.Controls.Add(childForm);
            //pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Width = 100;
            childForm.Show();
            p.Controls.Clear();
            p.Controls.Add(childForm);
        }
    }
}