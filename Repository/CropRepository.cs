using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealWebAPI.Repository
{
    public class CropRepository:IRepository<Crop,int>
    {

        CropDealContext _context;
        public CropRepository(CropDealContext context) => _context = context;
        

        public async Task<int> CreateAsync(Crop item)
        {
            _context.Crops.Add(item);
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status201Created;
            return response;
        }

        

        public async Task<int> DeleteAsync(Crop item)
        {
            _context.Crops.Remove(item);
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;
        }

        public bool Exists(int id)
        {
            return (_context.Crops?.Any(e => e.CropId == id)).GetValueOrDefault();
        }

        

        public async Task<UserProfile> GetIdAsync(int id)
        {

            return await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task<int> UpdateAsync(UserProfile item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;

        }

        public async Task<int> UpdateAsync(Crop item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;
        }

        async Task<IEnumerable<Crop>> IRepository<Crop, int>.GetAsync()
        {
            return await _context.Crops.AsNoTracking().ToListAsync();
        }

       async Task<Crop> IRepository<Crop, int>.GetIdAsync(int id)
        {
            return await _context.Crops
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CropId == id);
        }
    }
}
