using CropDealWebAPI.Models;

namespace CropDealWebAPI.Repository
{
    public interface IPaymentRepository
    {
        public string AddPayment(Payment payment);
        List<Invoice> ViewInvoiceAsync(int UserId);

        List<Invoice> ViewDealerInvoiceAsync(int UserId);
    }
}
