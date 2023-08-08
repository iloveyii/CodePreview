using DataModel.Models;
using DbAccess.Models;
using Receipt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receipt
{
    public partial class Main : Form
    {
        private Button currentButton;
        private Form activeForm;


        public object ObjectReader { get; private set; }

        public Main()
        {
            InitializeComponent();
        }

        private void SetActiveButton (object btnSender)
        {
            DeActivateButtons();
            if(btnSender != null && currentButton != (Button)btnSender)
            {
                currentButton= (Button)btnSender;
                currentButton.BackColor = Color.FromArgb(51, 61, 125);
                currentButton.ForeColor = Color.White;
            }
        }

        private void DeActivateButtons()
        {
            foreach(Control previousButton in pnlMenu.Controls)
            {
                if(previousButton == null)
                {
                    continue;
                }
                if(previousButton.GetType() == typeof(Button))
                {
                    previousButton.BackColor = Color.FromArgb(51, 51, 76);
                    previousButton.ForeColor = Color.Gainsboro;
                }
            }
        }
        public void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OrderForm(), sender);
            SetActiveButton(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
            OpenChildForm(new Orders(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
            OpenChildForm(new Settings(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender);
            OpenChildForm(new Login(), sender);
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            SetActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(childForm);
            pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Width = 100;
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        public void hidePanel(bool hide)
        {
            pnlMenu.Visible = !hide;
        }

        public Control getDesktop()
        {
            return pnlDesktop;
        }
    }


}
