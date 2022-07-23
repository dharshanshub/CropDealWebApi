using CropDealWebAPI.Models;

namespace CropDealWebAPI.Repository
{
    public class ViewCrops : IViewCropRepository
    {
        CropDealContext _context;
        public ViewCrops(CropDealContext context) => _context = context;

        #region ViewCropsAd
        /// <summary>
        /// this method is used to view the crops posted by the famers
        /// </summary>
        /// <returns></returns>
        public List<ViewCrop> ViewCropsAsync()
        {
            try
            {
               
                var query = (from a in _context.CropOnSales
                             join b in _context.Crops on a.CropId equals b.CropId
                            join c in _context.UserProfiles on a.FarmerId equals c.UserId
                            select new ViewCrop () { CropAdId =a.CropAdId, 
                                CropName = a.CropName,
                               CropType = a.CropType, 
                               CropQty = a.CropQty, 
                              CropPrice =  a.CropPrice,
                               FarmerId = a.FarmerId, 
                               CropImage = b.CropImage,
                               FarmerAddress=c.UserAddress
                            });

                List<ViewCrop> list1 = query.ToList();


                return list1;
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
    }
}
