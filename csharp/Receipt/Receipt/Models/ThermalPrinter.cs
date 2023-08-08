using DataModel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receipt.Models
{
    public class ThermalPrinter
    {
        public static int paperSize = 200;
        public Customer _customer;
        public Order _order;
        public List<OrderService> _orderServices;
        private string type;

        public ThermalPrinter(Customer customer, Order order, List<OrderService> orderServices) {
            _customer = customer;
            _order = order;
            _orderServices = orderServices;
        }
        public void PrintReceipt(string type = "full") 
        { 
            this.type = type;
            PrintDialog printDialog = new PrintDialog();
            var printDocument = new PrintDocument();
            printDialog.Document = printDocument;
            Settings(printDialog, printDocument);
            //printDialog.ShowDialog();
        }
        public void Settings(PrintDialog printDialog, PrintDocument printDocument)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            Font font = new Font("calibri", 15);
            PaperSize paperSize= new PaperSize("Custom", ThermalPrinter.paperSize, 1000);
            // Print Dialog
            printDialog.Document.DefaultPageSettings.PaperSize = paperSize;
            // Print Document
            printDocument.DefaultPageSettings.PaperSize.Height = paperSize.Height;
            printDocument.DefaultPageSettings.PaperSize.Width = 520;
            //printDocument.PrinterSettings.PrinterName = "POS";
            printDocument.PrinterSettings.PrinterName = printerSettings.PrinterName;

            printDocument.PrintPage += new PrintPageEventHandler(dailyDep);
            printDocument.Print();
            printDocument.PrintPage -= new PrintPageEventHandler(dailyDep);
        }

        private void dailyDep(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                Writer writer = new Writer(graphics);
                if(type == "full")
                {
                    header(writer);
                    customerInfo(writer);
                    services(writer);
                    hours(writer);
                    footer(writer);
                } 

                if(type == "short")
                {
                    customerInfo(writer);
                    services(writer);
                }

                if (type == "both")
                {
                    header(writer);
                    customerInfo(writer);
                    services(writer);
                    hours(writer);
                    footer(writer);

                    customerInfo(writer);
                    services(writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void header(Writer writer)
        {
            writer
                .insertImage("C:\\Users\\ali\\projects\\Receipt\\jojomobil.jpeg", 0.95)
                .vSpace(Writer.SPACE_LARGE * 10);
        }
        private void customerInfo(Writer writer)
        {
            writer
                .line()
                .setFontSize(Writer.FONT_MEDIUM)
                .vSpace(Writer.SPACE_SMALL)
                .text(this._customer.FirstName + " " + this._customer.LastName)
                .text("Date: " + this._order.Date)
                .line()
                .vSpace(Writer.SPACE_MEDIUM);
        }

        private void services(Writer writer)
        {
            foreach (var orderService in this._orderServices)
            {
                writer
                .setInitialColumns(2)
                .firstColText(orderService.Name)
                .nextColText(orderService.Price.ToString());
            }

            writer
                .vSpace(Writer.SPACE_MEDIUM)
                .line()
                .setInitialColumns(2)
                .firstColText("Total")
                .nextColText(this._order.TotalPrice.ToString())
                .vSpace(Writer.SPACE_MEDIUM);
        }

        private void hours(Writer writer)
        {
            writer
                .text("MON    FRI 11:00 - 17:00")
                .text("LÖRDAG 11:00 - 16:00")
                .text("SÖNDAG Stängt")
                .vSpace(Writer.SPACE_MEDIUM);
        }
        private void footer(Writer writer)
        {
            writer
                .setFontSize(Writer.FONT_SMALL)
                .text("Specialister på att laga, serva och reparera")
                .text("mobiltelefoner surfplattor och datorer.")
                .text("Våra ledord är service snabbhet och")
                .text("kvalitet.Följ oss på Instagram ...@jojo_mobil")
                .text("Med 3 månader garanti...utan Skada!");
        }
    }
}
