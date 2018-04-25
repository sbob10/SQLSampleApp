using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSampleApp
{
    public class BusinessLogic
    {

        private DataBaseLogic _dbLogic;

        public BusinessLogic()
        {
            _dbLogic = new DataBaseLogic();
        }


        public Result AddCustomer(Customer customer) 
        {
            if (String.IsNullOrEmpty(customer.Name))
                return new Result(Message.NAME_EMPTY_NULL, Status.Error);

            return _dbLogic.AddCustomer(customer);
        }

        public Result<List<Customer>> GetCustomer(String name)
        {
            if (String.IsNullOrEmpty(name))
                return new Result<List<Customer>>(null,Message.NAME_EMPTY_NULL, Status.Error);

            return _dbLogic.GetCustomer(name);
        }

        public async Task<Result<List<Customer>>> GetCustomerAsync(String name) => await Task.Run(() => this.GetCustomer(name));

        public Result<Fortune> GetFortuneForCustomer(int ID)
        {
            //TBD validation

            return _dbLogic.GetFortuneForCustomer(ID);
        }

        public async Task<Result<Fortune>> GetFortuneForCustomerAsync(int ID) => await Task.Run(() => this.GetFortuneForCustomer(ID));

        public Result SendMailTo(String customerMail)
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.Subject = "This is the subject";
                mailItem.To = customerMail;
                mailItem.Body = "This is the message.";
                mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
                mailItem.Display(false);
                mailItem.Send();

                return new Result(Status.Success);
            }
            catch(Exception e)
            {
                return new Result(message: Message.NULL, status: Status.Error, exception: e);
            }

        }

        public async Task<Result> SendMailToAsync(String customerMail) => await Task.Run(() => this.SendMailTo(customerMail));

        public Result ExportToPdf(Customer customer, Fortune fortune)
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                doc.Info.Title = "Kundenreport - " + customer.Name;
                PdfPage page = doc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont fontBold = new XFont("Times New Roman", 20, XFontStyle.Bold);
                XFont fontNormal = new XFont("Times New Roman", 18, XFontStyle.Regular);

                gfx.DrawString("Kundenreport:", fontBold, XBrushes.Black, new XRect(50, 10, 600, 25), XStringFormats.TopLeft);
                gfx.DrawString("Name: " + customer.Name, fontNormal, XBrushes.Black, new XRect(50, 50, 600, 25), XStringFormats.TopLeft);
                gfx.DrawString("Email: " + customer.Mail, fontNormal, XBrushes.Black, new XRect(50, 75, 600, 25), XStringFormats.TopLeft);
                gfx.DrawString("Telefonnummer: " + customer.PhoneNumber, fontNormal, XBrushes.Black, new XRect(50, 100, 600, 25), XStringFormats.TopLeft);

                XPen pen = new XPen(XColors.DarkBlue, 2.5);

                gfx.DrawPie(pen, XBrushes.Gold, 10, 0, 100, 90, -120, 75);
                gfx.DrawPie(pen, XBrushes.Gold, 10, 50, 100, 90, 80, 70);
                gfx.DrawPie(pen, XBrushes.Gold, 150, 80, 60, 60, 35, 290);

                const string filename = "Kundenreport.pdf";
                doc.Save(filename);

                return new Result(status: Status.Success);
            }
            catch(Exception e)
            {
                return new Result(status: Status.Error);
            }
        }

        public async Task<Result> ExportToPdfAsync(Customer customer, Fortune fortune) => await Task.Run(() => this.ExportToPdf(customer, fortune));



    }
}
