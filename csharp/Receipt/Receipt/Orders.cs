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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
            fillGridCustomers();
        }

        private void fillGridCustomers()
        {
            SqliteCustomerAccess.Init();
            var customers = SqliteCustomerAccess.read();
            grdCustomers.AutoGenerateColumns = false;
            fillTable(customers);
            return;
            grdCustomers.DataSource = customers;


            var bm = Receipt.Properties.Resources.cart31;

            DataGridViewImageCell iconCell = new DataGridViewImageCell();

            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
            //iconColumn.Image = bm;
            //iconColumn.Image = Convert_Text_to_Image("A", "monospace", 20);

            iconColumn.Image = Bmp.ConvertTextToImageBg("A", "sans", 15, Color.Gainsboro, Color.Black, 50, 50);
            iconColumn.Name = "Tree";
            iconColumn.HeaderText = "Nice tree";
            grdCustomers.Columns.Insert(2, iconColumn);
        }

        private void fillGridOrders(List<Order> orders)
        {
            grdOrders.DataSource = orders;
            grdOrders.Columns[0].Width = 25;
            grdOrders.Columns[1].Width = 45;
        }

        private void fillGridServices(List<OrderService> orderServices)
        {
            grdServices.DataSource = orderServices;
            grdServices.Columns[0].Width = 25;
            grdServices.Columns[1].Width = 45;
        }

        private void fillTable(List<Customer> customers)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Prefix", typeof(Image));

            foreach (Customer customer in customers)
            {
                string p = customer.Name.Substring(0, 1);
                var prefix = Bmp.ConvertTextToImageBg(p, "sans", 10, Colors.RandomColor(), Color.Black, 30, 30);
                DataRow row = dt.Rows.Add(customer.Id, customer.Name, customer.Phone, customer.Email, prefix);
            }
            grdCustomers.DataSource = customers;
            grdCustomers.Columns[0].Width = 45;
            grdCustomers.Columns[1].Width = 45;
            grdCustomers.Columns[5].Width = 35;
            grdCustomers.Columns[6].Width = 35;
            grdCustomers.Refresh();
        }

        private void grdOrders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Order order = (Order)grdOrders.CurrentRow.DataBoundItem;
            fillGridServices(order.OrderServices);
        }

        private void grdCustomers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Customer customer = (Customer)grdCustomers.CurrentRow.DataBoundItem;
            fillGridOrders(customer.Orders);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Clear")
            {
                btnSearch.Text = "Search";
                fillGridCustomers();
            }
            else
            {
                btnSearch.Text = "Clear";
                searchDataGrid(txtSearch.Text);
            }
        }

        private void searchDataGrid(string search)
        {
            if (search.Length > 0)
            {
                List<Customer> list = (List<Customer>)grdCustomers.DataSource;
                //List<Customer> result = list.FindAll(c => c.Name.Contains(search));
                List<Customer> result = list.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
                grdCustomers.DataSource = null;
                grdCustomers.Rows.Clear();
                grdCustomers.DataSource = result;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(e.ToString());
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
