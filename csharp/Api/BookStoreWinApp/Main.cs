using BookStore.Services.BookService;
using BookStore.Services.SettingsService;
using Microsoft.Extensions.Logging;

namespace BookStoreWinApp
{
    public partial class FormMain : Form
    {
        ISettingsService settingsService;
        IBookService bookService;
        Dictionary<string, string> settings = new Dictionary<string, string>();

        Form activeForm;
        private Button currentButton;

        Form formSearch;
        Color buttonHoverColor = Color.Violet;

        public readonly ILogger logger;


        public FormMain(IBookService bookService, ISettingsService settingsService, FormSearch formSearch, ILogger<Form> logger)
        {
            InitializeComponent();
            this.bookService = bookService;
            this.settingsService = settingsService;
            this.formSearch = formSearch;
            this.logger = logger;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GridBooksSizeSet();
            settings = settingsService.GetAllDictionary();
            // OpenChildForm(formSearch, sender);
            ButtonSearchForm_Click(sender, e);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            GridBooksSizeSet();
        }

        private void GridBooksSizeSet()
        {
            var marginX = 20;
            var marginY = 40;

            var gutterLeft = 200;

            pnlDesktop.Left = gutterLeft + marginX;
            pnlDesktop.Width = this.Width - (marginX * 2) - gutterLeft;
            pnlDesktop.Height = this.Height - (marginX * 2) - grpControls.Height - 20;


            grpControls.Left = gutterLeft + marginX;
            grpControls.Width = pnlDesktop.Width - marginX;

            pnlLeft.Width = gutterLeft;
            pnlLeft.Height = this.Height;
        }
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Hide();
            }
            SetActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            if (childForm != this)
            {
                pnlDesktop.Controls.Add(childForm);
            }
            pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Width = 100;
            childForm.Show();
            // lblTitle.Text = childForm.Text;
        }

        private void SetActiveButton(object btnSender)
        {
            var form = btnSender.GetType();
            if (form.Name == "FormMain")
            {
                setActiveColors(ButtonSearchForm);
                return;
            }
            DeActivateButtons();
            if (btnSender != null && currentButton != (Button)btnSender)
            {
                currentButton = (Button)btnSender;
                setActiveColors(currentButton);
            }
        }

        private void setActiveColors(Button btnSender)
        {
            currentButton = btnSender;
            currentButton.BackColor = buttonHoverColor;
            currentButton.ForeColor = Color.Blue;
            currentButton.MouseLeave += CurrentButton_MouseLeave;
            currentButton.MouseEnter += CurrentButton_MouseLeave;
        }

        private void CurrentButton_MouseLeave(object? sender, EventArgs e)
        {
            currentButton.BackColor = buttonHoverColor;
        }

        private void DeActivateButtons()
        {
            foreach (Control previousButton in pnlLeft.Controls)
            {
                if (previousButton == null)
                {
                    continue;
                }
                if (previousButton.GetType() == typeof(Button))
                {
                    previousButton.BackColor = Color.Transparent;
                    previousButton.ForeColor = Color.Gainsboro;
                }
            }
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSettings(bookService, settingsService, logger), sender);
        }

        private void ButtonSearchForm_Click(object sender, EventArgs e)
        {
            OpenChildForm(formSearch, sender);
        }

        private void ButtonSettings_MouseHover(object sender, EventArgs e)
        {
            var button = (Control)sender;
            button.BackColor = buttonHoverColor;
        }

        private void ButtonSettings_MouseLeave(object sender, EventArgs e)
        {
            var button = (Control)sender;
            button.BackColor = Color.Transparent;
        }

        private void ButtonSettings_MouseEnter(object sender, EventArgs e)
        {
            var button = (Control)sender;
            button.BackColor = buttonHoverColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void ButtonEntry_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormEntry(bookService, settingsService, null), sender);
        }
    }
}