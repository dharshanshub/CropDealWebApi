using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CropDealWebAPI.Repository
{
    public class ExceptionRepositry:IExceptionRepositry
    {
        CropDealContext _context;
        public IConfiguration _configuration { get; }
        public ExceptionRepositry(CropDealContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region Exception Logging
        /// <summary>
        /// this method is used fro exception logging
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="causedAt"></param>
        public async Task AddException(Exception ex, string causedAt, object requestBodyJson)
        {


            string filePath = @"E:\Error.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine(causedAt);
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                if (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);


                }
            }
            _context.Database.OpenConnection();
            _context.Database.BeginTransaction();
            var exceptionLogObj = new ExceptionLog();
            exceptionLogObj.Data = JsonConvert.SerializeObject(requestBodyJson, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            exceptionLogObj.Date = DateTime.Now;
            exceptionLogObj.ErrorDescription = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            exceptionLogObj.StackTrace = ex.StackTrace;
            _context.ExceptionLogs.Add(exceptionLogObj);
            await _context.SaveChangesAsync();
            _context.Database.RollbackTransaction();
            _context.Database.CloseConnection();






        }
        #endregion
    }
}
