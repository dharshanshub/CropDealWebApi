using CropDealWebAPI.Models;
using CropDealWebAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewCropController : ControllerBase
    {
        private readonly ViewCropService _Service;

        public ViewCropController(ViewCropService service)
        {

            _Service = service;
        }

        [HttpGet]
        public  List<ViewCrop> GetCrops()
        {
            return _Service.ViewCrops();

        }
    }
}
