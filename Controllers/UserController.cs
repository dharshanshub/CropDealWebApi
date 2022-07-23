using CropDealWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CropDealContext _context;
        public UserController(CropDealContext context)
        {
            _context = context;

        }

        [HttpPut("{id}")]
        public IActionResult ChangeUserStatus(int id, string UserStatus)
        {
            try
            {
                (from p in _context.UserProfiles
                 where p.UserId == id
                 select p).ToList()
                        .ForEach(x => x.UserStatus = UserStatus);

                _context.SaveChanges();
                return Ok();
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}
