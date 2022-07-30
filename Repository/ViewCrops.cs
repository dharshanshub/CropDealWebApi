﻿using CropDealWebAPI.Dtos;
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
                               FarmerAddress=c.UserAddress,
                               FarmerName = c.UserName,
                               FarmerPhnumber=c.UserPhnumber
                            });

                List<ViewCrop> list1 = query.ToList();


                return list1;
            }
            catch (Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at ViewCropsAsync in ViewCrop");
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


        #region ViewFarmerCrops
        /// <summary>
        /// this method is used to view the crops posted by the famers
        /// </summary>
        /// <returns></returns>


        public List<ViewCrop> ViewFarmerCropsAsync(UserId id)
        {
            try
            {

                var query = (from a in _context.CropOnSales where id.Id == a.FarmerId
                             join b in _context.Crops on a.CropId equals b.CropId
                            
                             select new ViewCrop()
                             {
                                 CropAdId = a.CropAdId,
                                 CropName = a.CropName,
                                 CropType = a.CropType,
                                 CropQty = a.CropQty,
                                 CropPrice = a.CropPrice,        
                                 CropImage = b.CropImage,
                                
                             });

                List<ViewCrop> list1 = query.ToList();


                return list1;
            }
            catch (Exception ex)
            {
                string filePath = @"D:\Error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Error Caused at ViewFarmerCropsAsync in ViewCrops");
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
