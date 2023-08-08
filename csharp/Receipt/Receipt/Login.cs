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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = (Main)this.Parent.Parent;
            //main.Hide();
            main.hidePanel(true);
            centerMe(main);
        }

        private void centerMe(Main main)
        {
            var d = main.getDesktop();
            grpLogin.Left = (d.Width - grpLogin.Width - 24)/2;
            grpLogin.Top = (d.Height - grpLogin.Height - 24)/2;
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            Main main = (Main)this.Parent.Parent;
            main.hidePanel(false);
            main.btnProducts_Click(sender, e);
        }
    }
}
