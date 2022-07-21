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
        #region GetUserProfile
        /// <summary>
        /// this action is used to get all the users
        /// </summary>
        /// <returns></returns>
        // GET: api/UserProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUserProfiles()
        {
            try
            {
                if (_context.UserProfiles == null)
                {
                    return NotFound();
                }
                var users = await _context.UserProfiles.ToListAsync();
                var usersDto = mapper.Map<IEnumerable<GetUserDto>>(users);
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {

            }


        }
        #endregion

        #region GetUserProfileById
        /// <summary>
        /// this action is used to get users by Id
        /// </summary>
        /// <returns></returns>

        // GET: api/UserProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserProfile(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region UpdateUserProfile
        /// <summary>
        /// this action is used to update user profile
        /// </summary>
        /// <returns></returns>

        // PUT: api/UserProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, UpdateUserDto userProfileDto)
        {
            try
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
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region RegisterUserProfile
        /// <summary>
        /// this action is used to post the User
        /// </summary>
        /// <returns></returns>

        // POST: api/UserProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> PostUserProfile(CreateUserDto userProfileDto)
        {
            try
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
            catch(Exception ex)
            {
                throw;
            }
            finally { }
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// this action is used to deleta a user,
        /// </summary>
        /// <returns></returns>

        // DELETE: api/UserProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            try
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
            }catch(Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region UserExists
        /// <summary>
        /// this action is used to check if the user exists
        /// </summary>
        /// <returns></returns>

        private bool UserProfileExists(int id)
        {
            try
            {
                return (_context.UserProfiles?.Any(e => e.UserId == id)).GetValueOrDefault();
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion
    }
}
