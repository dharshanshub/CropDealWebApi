using CropDealWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDealWebAPI.Repository
{
    public class CropOnSaleRepository : IRepository<CropOnSale, int>
    {
        CropDealContext _context;
        public CropOnSaleRepository(CropDealContext context) => _context = context;
        public async Task<int> CreateAsync(CropOnSale item)
        {
            try
            {
                _context.CropOnSales.Add(item);
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

        public async Task<int> DeleteAsync(CropOnSale item)
        {
            _context.CropOnSales.Remove(item);
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;
        }

        public bool Exists(int id)
        {
            return (_context.CropOnSales?.Any(e => e.CropAdId == id)).GetValueOrDefault();
        }


        public async Task<IEnumerable<CropOnSale>> GetAsync()
        {
            return await _context.CropOnSales.AsNoTracking().ToListAsync();
        }

        public async Task<CropOnSale> GetIdAsync(int id)
        {
            return await _context.CropOnSales
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CropAdId == id);
        }

        public async Task<int> UpdateAsync(CropOnSale item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var response = StatusCodes.Status200OK;
            return response;
        }
    }
}
