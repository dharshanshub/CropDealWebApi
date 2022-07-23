
using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealWebAPI.Repository
{
    public class UserProfileRepository : IRepository<UserProfile, int>
    {
        CropDealContext  _context;
        
        public UserProfileRepository(CropDealContext context) => _context = context;

        #region CreateUser
        /// <summary>
        /// this method is used to create user
        /// </summary>
        /// <param name="context"></param>
        public async Task<int> CreateAsync(UserProfile userProfile)
        {
            try
            {

                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync();
                var response = StatusCodes.Status201Created;
                return response;
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }
        #endregion

        #region DeleteAsync
        /// <summary>
        /// this method is used to delete user
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public async  Task<int> DeleteAsync(UserProfile userProfile)
        {
            try
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
                var response = StatusCodes.Status200OK;
                return response;
            }catch(Exception ex)
            {
                throw;
            }
            finally { }
        }
        #endregion

        #region UserExists
        /// <summary>
        /// this method is used to see wheater user exsists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool Exists(int id)
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

        #region GetUsers
        /// <summary>
        /// this method is used to get all users
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<UserProfile>> GetAsync()
        {
            try
            {
                return await _context.UserProfiles.AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region GetUserbyId
        /// <summary>
        /// this method is used to get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<UserProfile> GetIdAsync(int id)
        {
            try
            {

                return await _context.UserProfiles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.UserId == id);
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region
        /// <summary>
        /// this method is used to update User
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task<int> UpdateAsync(UserProfile item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var response = StatusCodes.Status200OK;
                return response;
            }catch (Exception ex)
            {
                throw;
            }
            finally { }

        }
        #endregion



    }
}
