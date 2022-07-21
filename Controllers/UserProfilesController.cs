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
using CropDealWebAPI.Repository;
using CropDealWebAPI.Service;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase

    {
       
        private readonly IMapper mapper;
        private readonly UserProfileService _Service;

        public UserProfilesController( IMapper mapper,UserProfileService service )
        {
            
            this.mapper = mapper;
            _Service = service;
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

                var users = await _Service.GetUser();
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
                
                var userProfile = await _Service.GetUserById(id);

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
                var userProfile = await _Service.GetUserById(id);
                if (userProfile == null)
                {
                    return NotFound();
                }
               

                mapper.Map(userProfileDto, userProfile);
               

                if (_Service == null)
                {
                    return Problem("Entity set 'CropDealContext.UserProfiles'  is null.");
                }

                var val = _Service.UpdateUser(userProfile);
                if (val == null)
                {
                    return BadRequest();
                }
                return NoContent();


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
                if (_Service == null)
                {
                    return Problem("Entity set 'CropDealContext.UserProfiles'  is null.");
                }
               var res=  _Service.CreateUser(userProfile);
                if (res == null)
                {
                    return BadRequest();
                }

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
                if (_Service == null)
                {
                    return NotFound();
                }
                var userProfile = await _Service.GetUserById(id);
                if (userProfile == null)
                {
                    return NotFound();
                }

               var result = _Service.DeleteUser(userProfile);
                if (result == null)
                {
                    return BadRequest();
                }

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
                return _Service.UserExists(id);
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
