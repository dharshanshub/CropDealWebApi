namespace CropDealWebAPI.Repository
{
    public interface IExceptionRepositry
    {
        Task AddException(Exception ex, string Causedat,object requestBodyJson);

    }
}
