
using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealWebAPI.Repository
{
    public class UserProfileRepository : IRepository<UserProfile, int>
    {
        CropDealContext  _context;
       public UserProfileRepository(CropDealContext context) => _context = context;
        public async Task<int> CreateAsync(UserProfile userProfile)
        {
            
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status201Created;
            return response;

        }

        public async  Task<int> DeleteAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;
        }

        public bool Exists(int id)
        {
           return  (_context.UserProfiles?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<UserProfile>> GetAsync()
        {
            return await _context.UserProfiles.AsNoTracking() .ToListAsync();
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

        
    }
}
