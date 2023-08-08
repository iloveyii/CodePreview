using DbAccess.Models;
using SettingsModel = DataModel.Models.Settings;
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
    public partial class Settings : Form
    {
        private SettingsModel settings;
        public Settings()
        {
            InitializeComponent();
            loadSettings();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fleDatabase.InitialDirectory = Application.ExecutablePath;
            fleDatabase.Filter = "Database files (.db)|*.db|SQL files (.sql)|*.sql";
            fleDatabase.FilterIndex = 1;
            fleDatabase.RestoreDirectory = true;

            if (fleDatabase.ShowDialog() == DialogResult.OK)
            {
                var filePath = fleDatabase.FileName;
                txtDatabasePath.Text = filePath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogoPath_Click(object sender, EventArgs e)
        {
            fleDatabase.InitialDirectory = Application.ExecutablePath;
            fleDatabase.Filter = "PNG files (.png)|*.png|Jpeg files (.jpeg)|*.jpeg|All files(.*)|*.*";
            fleDatabase.FilterIndex = 1;
            fleDatabase.RestoreDirectory = true;

            if (fleDatabase.ShowDialog() == DialogResult.OK)
            {
                var filePath = fleDatabase.FileName;
                txtLogoPath.Text = filePath;
            }
        }

        private void btnLogoPath2_Click(object sender, EventArgs e)
        {
            fleDatabase.InitialDirectory = Application.ExecutablePath;
            fleDatabase.Filter = "PNG files (.png)|*.png|Jpeg files (.jpeg)|*.jpeg|All files(.*)|*.*";
            fleDatabase.FilterIndex = 1;
            fleDatabase.RestoreDirectory = true;

            if (fleDatabase.ShowDialog() == DialogResult.OK)
            {
                var filePath = fleDatabase.FileName;
                txtLogoPath2.Text = filePath;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SettingsModel settings = new SettingsModel();

            if(txtApiEndpoint.Text == String.Empty)
            {
                MessageBox.Show("Api endpoint cannot be empty.");
                return;
            }
            if (txtDatabasePath.Text == String.Empty)
            {
                MessageBox.Show("Database path cannot be empty.");
                return;
            }
            if (txtLogoPath.Text == String.Empty)
            {
                MessageBox.Show("Logo path cannot be empty.");
                return;
            }
            if (txtLogoPath2.Text == String.Empty)
            {
                MessageBox.Show("Logo path 2 cannot be empty.");
                return;
            }
            if (rdoBoth.Checked.ToString() == String.Empty)
            {
                rdoBoth.Checked = false;
            }
            if (chkCutReceipt.Checked.ToString() == String.Empty)
            {
                chkCutReceipt.Checked = false;
            }
            settings.ApiEndpoint = txtApiEndpoint.Text;
            settings.ApiDatabasePath = txtDatabasePath.Text;
            settings.ReceiptLogoPath = txtLogoPath.Text;
            settings.ReceiptLogoPath2= txtLogoPath2.Text;
            settings.ReceiptReceiptType = rdoBoth.Checked.ToString();
            settings.ReceiptReceiptCut = chkCutReceipt.Checked.ToString();
            saveSettings(settings);
        }

        private void saveSettings(SettingsModel settings)
        {
            SqliteSettingsAccess.Init();
            var _settings = SqliteSettingsAccess.readOne(1);
            if(_settings == null)
            {
                _settings = new SettingsModel();
                _settings = settings;
                SqliteSettingsAccess.create(_settings);           
            } else
            {
                _settings = settings;
                _settings.Id = 1;
                SqliteSettingsAccess.update(_settings);
            }
        }

        private SettingsModel loadSettings()
        {
            SqliteSettingsAccess.Init();
            settings = SqliteSettingsAccess.readOne(1);
            if(settings != null)
            {
                txtApiEndpoint.Text = settings.ApiEndpoint;
                txtDatabasePath.Text = settings.ApiDatabasePath;
                txtLogoPath.Text = settings.ReceiptLogoPath;
                txtLogoPath2.Text = settings.ReceiptLogoPath2;
                //rdoFull.Checked = (bool) settings.ReceiptReceiptType.Length > 0;
            } 

            return settings;
        }
    }
}
