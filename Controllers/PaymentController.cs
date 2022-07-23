using CropDealWebAPI.Models;
using CropDealWebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _Service;

        public PaymentController(PaymentService service)
        {

            _Service = service;
        }

        [HttpPost]
        public IActionResult AddPayment(Payment payment)
        {
            return Ok( _Service.AddPayment(payment));

        }
        [HttpGet("FarmerInvoice")]
        public List<Invoice> GetInvoice(int UserId)
        {
            return _Service.ViewInvoiceAsync(UserId);

        }
        [HttpGet("DealerInvoice")]
        public List<Invoice> GetDealerInvoice(int UserId)
        {
            return _Service.ViewDealerInvoiceAsync(UserId);

        }

    }
}
