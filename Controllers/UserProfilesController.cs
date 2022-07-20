using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CropDealWebAPI.Models;
using CropDealWebAPI.Dtos.UserProfile;
using AutoMapper;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly CropDealContext _context;
        private readonly IMapper mapper;

        public UserProfilesController(CropDealContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/UserProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUserProfiles()
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            var users = await _context.UserProfiles.ToListAsync();
            var usersDto = mapper.Map<IEnumerable<GetUserDto>>(users);
            return Ok(usersDto);
        }

        // GET: api/UserProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserProfile(int id)
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            var userProfile = await _context.UserProfiles.FindAsync(id);

            if (userProfile == null)
            {
                return NotFound();
            }
            var userDto = mapper.Map<GetUserDto>(userProfile);
            return userDto;
        }

        // PUT: api/UserProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, UpdateUserDto userProfileDto)
        {

            if (id != userProfileDto.UserId)
            {
                return BadRequest();

            }
            var userProfile = await _context.UserProfiles.FindAsync(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            mapper.Map(userProfileDto, userProfile);
            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> PostUserProfile(CreateUserDto userProfileDto)
        {
            var userProfile = mapper.Map<UserProfile>(userProfileDto);
            if (_context.UserProfiles == null)
            {
                return Problem("Entity set 'CropDealContext.UserProfiles'  is null.");
            }
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfile", new { id = userProfile.UserId }, userProfile);
        }

        // DELETE: api/UserProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            if (_context.UserProfiles == null)
            {
                return NotFound();
            }
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfileExists(int id)
        {
            return (_context.UserProfiles?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
