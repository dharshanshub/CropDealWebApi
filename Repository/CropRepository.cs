using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealWebAPI.Repository
{
    public class CropRepository : IRepository<Crop, int>
    {

        CropDealContext _context;
        public CropRepository(CropDealContext context) => _context = context;

        #region CreateCrop
        /// <summary>
        /// this method is used to create crop
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task<int> CreateAsync(Crop item)
        {
            try
            {
                _context.Crops.Add(item);
                await _context.SaveChangesAsync();
                var response = StatusCodes.Status201Created;
                return response;
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

        #region DeleteCrop
        /// <summary>
        /// Crops deleted
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task<int> DeleteAsync(Crop item)
        {
            try
            {
                _context.Crops.Remove(item);
                await _context.SaveChangesAsync();
                var response = StatusCodes.Status200OK;
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { }
        }
        #endregion

        #region CropExists
        /// <summary>
        /// this method is used to check if the crops exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            try
            {
                return (_context.Crops?.Any(e => e.CropId == id)).GetValueOrDefault();
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

        #region GetCropById
        /// <summary>
        /// this method is used to get crop by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        async Task<Crop> IRepository<Crop, int>.GetIdAsync(int id)
        {
            try
            {
                return await _context.Crops
                     .AsNoTracking()
                     .FirstOrDefaultAsync(c => c.CropId == id);
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region UpdateCrop
        /// <summary>
        /// this method is used update Crop
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public async Task<int> UpdateAsync(Crop item)
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
            finally
            {

            }
        }
        #endregion

        #region GetAllCrops
         
        /// <summary>
        /// get crops by id
        /// </summary>
        /// <returns></returns>

        async Task<IEnumerable<Crop>> IRepository<Crop, int>.GetAsync()
        {
            try
            {
                return await _context.Crops.AsNoTracking().ToListAsync();
            }catch (Exception ex)
            {
                throw;
            }
            finally { }
        }
        #endregion


    }
}
