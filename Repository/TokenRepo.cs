using CropDealWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CropDealWebAPI.Repository
{
    public class TokenRepo : IToken
    {
        CropDealContext _context;
        private readonly IConfiguration _configuration;

        public TokenRepo(CropDealContext context, IConfiguration config)
        {
            _context = context;
           _configuration=config;
        }


        #region Token Creation
        /// <summary>
        /// this method is used to create token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string CreateToken(Login login)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Email, login.Email),
                new Claim(ClaimTypes.Role, login.Role)
                 };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;


            }
            catch (Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at CreateToken in Token");
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
    }
}
