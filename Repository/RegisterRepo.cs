using CropDealWebAPI.Dtos.UserProfile;
using CropDealWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CropDealWebAPI.Repository
{
    public class RegisterRepo:IRegisterRepository<CreateUserDto,UserProfile>
    {
        CropDealContext _context;
        public RegisterRepo(CropDealContext context) => _context = context;

        #region RegisterUser
        /// <summary>
        /// this method is used to register User
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ActionResult<UserProfile>> CreateAsync(CreateUserDto userProfileDto)
        {
            try
            {

                using var hmac = new HMACSHA512();
                var user = new UserProfile
                {
                    UserName = userProfileDto.UserName,
                    UserEmail = userProfileDto.UserEmail,
                    UserPhnumber = userProfileDto.UserPhnumber,
                    UserType = userProfileDto.UserType,
                    UserBankName = userProfileDto.UserBankName,
                    UserIfsc = userProfileDto.UserIfsc,
                    UserAccnumber = userProfileDto.UserAccnumber,
                    UserAddress = userProfileDto.UserAddress,

                    UserPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userProfileDto.UserPassword)),
                    UserPasswordSalt = hmac.Key
                };

               _context.UserProfiles.Add(user);
               await _context.SaveChangesAsync();
               return user;
            }
            catch (Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error caused at CreateAsync in Register");
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

        public bool UserExists(CreateUserDto item)
        {
            try
            {
                return (_context.UserProfiles?.Any(e => e.UserEmail == item.UserEmail)).GetValueOrDefault();
            }
            catch(Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at UserExists in Register");
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
    }
    }
