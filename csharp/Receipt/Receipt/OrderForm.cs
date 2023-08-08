using Receipt.Models;
using DataAccess.Models;
using Sql = DbAccess.Models.SqliteVendorAccess;
using System.Net;
using System;
using DA = ApiClient.Models.DataAccess;
using DbAccess.Models;
using DataModel.Models;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Receipt
{
    public partial class OrderForm : Form
    {
        Customer currentCustomer = null;
        List<Vendor> vendors = new List<Vendor>();
        List<OrderService> selectedOrderServices = new List<OrderService>();
        AutoCompleteStringCollection autoName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoEmail = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoPhone = new AutoCompleteStringCollection();
        public OrderForm()
        {
            InitializeComponent();
            fillGridCustomers();
            setNameAuto();
            setPhoneAuto();
            setEmailAuto();
            readVendors();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            cmbVendors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void setNameAuto()
        {
            txtFirstName.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtFirstName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFirstName.AutoCompleteCustomSource = autoName;
        }

        private void setPhoneAuto()
        {
            txtContactNo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtContactNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtContactNo.AutoCompleteCustomSource = autoPhone;
        }

        private void setEmailAuto()
        {
            txtEmail.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtEmail.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtEmail.AutoCompleteCustomSource = autoEmail;
        }

        private void btnAddClick(object sender, EventArgs e)
        {
            Customer customer;
            // ContactId is not empty the user want to add an order to the selected Customer
            if (txtContactID.Text == string.Empty)
            {
                customer = new Customer();
            } else
            {
                if(currentCustomer != null)
                {
                    customer = currentCustomer;
                } else
                {
                    customer = new Customer();
                }
            }
               
            if (txtFirstName.Text == string.Empty)
            {
                MessageBox.Show("First name cannot be empty");
                return;
            }
            if (txtLastName.Text == string.Empty)
            {
                MessageBox.Show("Last name cannot be empty");
                return;
            }
            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Email cannot be empty");
                return;
            }

            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.LastName = txtLastName.Text;
            customer.Email = txtEmail.Text;
            customer.Phone = txtContactNo.Text;
            customer.Address = txtAddress.Text;

            // Create or update db
            SqliteCustomerAccess.Init();
            bool success;
            if (txtContactID.Text == string.Empty)
            {
                var order = createOrder();
                customer.Orders.Add(order);
                success = SqliteCustomerAccess.create(customer);
            } else
            {
                var order = createOrder(true);
                customer.Orders.Add(order);
                SqliteCustomerAccess.update(customer);
                success = true;
            }
                
            if (success)
            {
                selectedOrderServices.Clear();
                fillSelectedServices();
            }

            fillGridCustomers();
            clearForm();
        }

        private Order createOrder(bool clearIds = false)
        {
            // Prepare order
            var order = new Order();
            order.Date = DateTime.Now;
            order.Total = Decimal.Parse(lblTotal.Text);
            if(clearIds)
            {
                foreach(var selectedOrderService in selectedOrderServices)
                {
                    selectedOrderService.Id = 0;
                    selectedOrderService.OrderId = 0;
                }
            }
            order.OrderServices = selectedOrderServices;
            return order;
        }

        private void btnUpdateClick(object sender, EventArgs e)
        {
            Customer customer = (Customer)grdCutomers.CurrentRow.DataBoundItem;

            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.Address = txtAddress.Text;
            customer.Email = txtEmail.Text;
            customer.Phone = txtContactNo.Text;

            // Take the older order
            Order order = (Order)grdOrders.CurrentRow.DataBoundItem;
            var _order = customer.Orders.Find(x => x.Id == order.Id);
            if (_order != null)
            {
                _order.OrderServices.Clear();
                _order.OrderServices = selectedOrderServices;
            }

            SqliteCustomerAccess.Init();
            SqliteCustomerAccess.update(customer);

            fillGridCustomers();
            clearForm();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnClearClick(object sender, EventArgs e)
        {
            clearForm();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void clearForm()
        {
            txtContactID.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            grdOrders.DataSource = null;
            selectedOrderServices.Clear();
            fillSelectedServices();
            currentCustomer = null;
        }

        private void fillForm(Customer customer)
        {
            txtContactID.Text = (String)customer.Id.ToString();
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            txtContactNo.Text = customer.Phone;
            txtEmail.Text = customer.Email;
            txtAddress.Text = customer.Address;
            selectedOrderServices.Clear(); 
            selectedOrderServices = customer.Orders.First().OrderServices.ToList();
            fillGridOrders(customer.Orders);
        }

        private void fillGridOrders(List<Order> orders)
        {
            grdOrders.DataSource = orders;
        }

        private void fillGridCustomers()
        {
            SqliteCustomerAccess.Init();
            var customers = SqliteCustomerAccess.read();
            grdCutomers.DataSource = customers;
            foreach(Customer customer in customers)
            {
                autoName.Add(customer.Name);
                autoEmail.Add(customer.Email);
                autoPhone.Add(customer.Phone);
            }
        }

        private void grdPersons_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SqliteDataAccess.Init();
            Customer customer = (Customer)grdCutomers.CurrentRow.DataBoundItem;
            if(customer != null)
            {
                fillForm(customer);
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(confirmDelete())
            {
                Customer customer = (Customer)grdCutomers.CurrentRow.DataBoundItem;

                SqliteCustomerAccess.Init();
                SqliteCustomerAccess.deleteCustomer(customer);

                fillGridCustomers();
                clearForm();
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            readVendors();
        }

        private async void readVendors()
        {
            //await Task.Run(() => DA.Run());
            Sql.Init();
            vendors = Sql.read();
            int count = vendors.Count;
            fillVendors();
        }

        private void fillVendors()
        {
            cmbVendors.Items.Clear();
            var _vendors = vendors.ToArray();
            cmbVendors.DataSource = _vendors;
            cmbVendors.DisplayMember = "Name";
            cmbVendors.ValueMember = "Id";
            var _products = _vendors[0].Products.ToArray();
            fillProducts(_products);
            fillSelectedServices();
        }

        private void fillSelectedServices()
        {
            grdSelectedServices.DataSource = null;
            if (selectedOrderServices != null && selectedOrderServices.Count > 0)
            {
                grdSelectedServices.DataSource = selectedOrderServices;
            }
            getTotal();
        }
        
        private void fillProducts(Product[] products)
        {
            //cmbProducts.Items.Clear();
            cmbProducts.DataSource = null;
            cmbProducts.DataSource = products;
            cmbProducts.DisplayMember = "Name";
            cmbProducts.ValueMember = "Id";
            var _services = products[0].Services.ToArray();
            fillServices(_services);
        }

        private void fillServices(Service[] services)
        {
            grdServices.DataSource = null;
            grdServices.DataSource = services;
        }

        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedVendor = cmbVendors.SelectedItem as Vendor;
            var _products = selectedVendor.Products.ToArray();
            fillProducts(_products);
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedProduct = cmbProducts.SelectedItem as Product;
            if (selectedProduct != null)
            {
                var _services = selectedProduct.Services.ToArray();
                if (_services != null)
                {
                    fillServices(_services);
                }
            }

        }

        private bool confirmDelete()
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void getTotal()
        {
            decimal sum = 0;
            lblTotal.Text = "0";
            foreach (var orderService in selectedOrderServices)
            {
                sum += orderService.Price;
            }
            lblTotal.Text = sum.ToString();
        }

        public void grdOrders_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Order order = (Order)grdOrders.CurrentRow.DataBoundItem;
            if(order != null)
            {
                selectedOrderServices = order.OrderServices.ToList();
                fillSelectedServices();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer customer = (Customer)grdCutomers.CurrentRow.DataBoundItem;
            var _order = grdOrders?.CurrentRow?.DataBoundItem;
            if (_order != null)
            {
                Order order = (Order)grdOrders.CurrentRow.DataBoundItem;
                List<OrderService> orderServices = selectedOrderServices;
                order.OrderServices= orderServices;
                updateCustomer(customer);
                ThermalPrinter printer = new ThermalPrinter(customer, order, orderServices);
                printer.PrintReceipt();
            }

        }

        private bool updateCustomer(Customer customer)
        {
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.Address = txtAddress.Text;
            customer.Email = txtEmail.Text;
            customer.Phone = txtContactNo.Text;

            SqliteCustomerAccess.Init();
            SqliteCustomerAccess.update(customer);
            return true;
        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(txtFirstName.Text);
            if(e.KeyData == Keys.Enter)
            {
                List<Customer> customers = grdCutomers.DataSource as List<Customer>;
                Customer c = customers.Find(c => c.Name== txtFirstName.Text);
                if(c!=null)
                {
                    fillForm(c);
                    currentCustomer = c;
                }
            }
        }

        private void grdServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /**
         * Add the clicked service to selectedOrderServices if not exist
         */
        private void grdServices_MouseClick(object sender, MouseEventArgs e)
        {
            Service service = (Service)grdServices.CurrentRow.DataBoundItem;

            if (service != null)
            {
                var _serviceExist = selectedOrderServices.Find(x => x.ServiceId == service.Id);
                if (_serviceExist == null)
                {
                    selectedOrderServices.Add(OrderService.FromService(service));
                    fillSelectedServices();
                }
            }
        }

        private void grdSelectedServices_MouseClick(object sender, MouseEventArgs e)
        {
            if (confirmDelete())
            {
                OrderService orderService = (OrderService)grdSelectedServices.CurrentRow.DataBoundItem;
                if (orderService != null)
                {
                    selectedOrderServices.Remove(orderService);
                    // New order service cannot be removed from db
                    if (orderService.Id != 0)
                    {
                        SqliteCustomerAccess.Init();
                        SqliteCustomerAccess.deleteOrderService(orderService);
                    }
                    fillSelectedServices();
                }
            }
        }
    }
}