using CropDealWebAPI.Models;

namespace CropDealWebAPI.Repository
{
    public class PaymentC:IPaymentRepository
    {
        CropDealContext _context;
        public PaymentC(CropDealContext context) => _context = context;

        public string AddPayment(Payment payment)
        {
            var Crpid = payment.CropAdid;

            var query =
                from f in _context.CropOnSales
                    where Crpid == f.CropAdId
                join g in _context.UserProfiles
                     on payment.FarmerId equals g.UserId
                join d in _context.UserProfiles
                     on payment.UserId equals d.UserId
                select new Invoice()
                {
                    CropName = f.CropName,
                    CropQty = f.CropQty,
                    CropType = f.CropType,
                    OrderTotal = f.CropPrice,
                    InvoiceDate = DateTime.Now,
                    FarmerAccNumber = g.UserAccnumber,
                    DealerAccNumber = d.UserAccnumber

                };
            List<Invoice> invoices = query.ToList();
            Invoice invoice1 = new Invoice();
            foreach(var invoice in invoices)
            {
                invoice1.CropName = invoice.CropName;
                invoice1.CropQty = invoice.CropQty;
                invoice1.CropType = invoice.CropType;
                invoice1.OrderTotal = invoice.OrderTotal;
                invoice1.InvoiceDate = invoice.InvoiceDate;
                invoice1.FarmerAccNumber= invoice.FarmerAccNumber;
                invoice1.DealerAccNumber= invoice.DealerAccNumber;
                
            }
           
            if (AddInvoice(invoice1))
            {
                return "200";
            }
            else
            {
                return "400";
            }

        }
        public bool AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
             _context.SaveChangesAsync();
            var response = true;
            return response;

        }


    }
}
