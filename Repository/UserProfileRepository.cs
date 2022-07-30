
using CropDealWebAPI.Dtos.UserProfile;
using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
        public Task<int> CreateAsync(UserProfile item)
        {
            throw new NotImplementedException();
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
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at DeleteAsync in UserProfile");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return 404;
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

        public bool Exists(int email)
        {
            try
            {
                return (_context.UserProfiles?.Any(e => e.UserId == email)).GetValueOrDefault();
            }catch (Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at Exists Method in UserProfile");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return false;
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
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at GetAsync in UserProfile");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return null;
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
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at GetIdAsync in UserProfile");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return null;
            }
            finally
            {

            }
        }
        #endregion

        #region UpdateAsync
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
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at UpdateAsync in UserProfile");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return 404;
            }
            finally { }

        }
        #endregion



    }
}
