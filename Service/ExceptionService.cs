using CropDealWebAPI.Repository;

namespace CropDealWebAPI.Service
{
    public class ExceptionService
    {
        private IExceptionRepositry _repositry;

        public ExceptionService(IExceptionRepositry repositry)
        {
            _repositry = repositry;
        }

        public async Task AddException(Exception ex,string Causedat, object requestBodyJson)
        {
            await _repositry.AddException(ex, Causedat, requestBodyJson);
        }
    }
}
