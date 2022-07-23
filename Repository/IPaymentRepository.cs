using CropDealWebAPI.Models;

namespace CropDealWebAPI.Repository
{
    public interface IPaymentRepository
    {
        public string AddPayment(Payment payment);
    }
}
