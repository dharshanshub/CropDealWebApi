using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CropDealWebAPI.Models;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropOnSalesController : ControllerBase
    {
        private readonly CropDealContext _context;

        public CropOnSalesController(CropDealContext context)
        {
            _context = context;
        }

        // GET: api/CropOnSales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropOnSale>>> GetCropOnSales()
        {
          if (_context.CropOnSales == null)
          {
              return NotFound();
          }
            return await _context.CropOnSales.ToListAsync();
        }

        // GET: api/CropOnSales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropOnSale>> GetCropOnSale(int id)
        {
          if (_context.CropOnSales == null)
          {
              return NotFound();
          }
            var cropOnSale = await _context.CropOnSales.FindAsync(id);

            if (cropOnSale == null)
            {
                return NotFound();
            }

            return cropOnSale;
        }

        // PUT: api/CropOnSales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropOnSale(int id, CropOnSale cropOnSale)
        {
            if (id != cropOnSale.CropAdId)
            {
                return BadRequest();
            }

            _context.Entry(cropOnSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropOnSaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CropOnSales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CropOnSale>> PostCropOnSale(CropOnSale cropOnSale)
        {
          if (_context.CropOnSales == null)
          {
              return Problem("Entity set 'CropDealContext.CropOnSales'  is null.");
          }
            _context.CropOnSales.Add(cropOnSale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCropOnSale", new { id = cropOnSale.CropAdId }, cropOnSale);
        }

        // DELETE: api/CropOnSales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCropOnSale(int id)
        {
            if (_context.CropOnSales == null)
            {
                return NotFound();
            }
            var cropOnSale = await _context.CropOnSales.FindAsync(id);
            if (cropOnSale == null)
            {
                return NotFound();
            }

            _context.CropOnSales.Remove(cropOnSale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CropOnSaleExists(int id)
        {
            return (_context.CropOnSales?.Any(e => e.CropAdId == id)).GetValueOrDefault();
        }
    }
}
