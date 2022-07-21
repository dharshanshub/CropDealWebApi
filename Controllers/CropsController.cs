using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CropDealWebAPI.Models;
using CropDealWebAPI.Dtos.Crop;
using AutoMapper;

namespace CropDealWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly CropDealContext _context;
        private readonly IMapper mapper;

        public CropsController(CropDealContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Crops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCropDto>>> GetCrops()
        {
           
          if (_context.Crops == null)
          {
              return NotFound();
          }
            var crops= await _context.Crops.ToListAsync();
            var cropsDto = mapper.Map<IEnumerable<GetCropDto>>(crops);
            return Ok(cropsDto);
        }

        // GET: api/Crops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCropDto>> GetCrop(int id)
        {
          if (_context.Crops == null)
          {
              return NotFound();
          }
            var crop = await _context.Crops.FindAsync(id);

            if (crop == null)
            {
                return NotFound();
            }
            
            var cropDto = mapper.Map<GetCropDto>(crop);
          

            return cropDto;
        }


        // POST: api/Crops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateCropDto>> PostCrop(CreateCropDto cropDto)
        {
            var crop = mapper.Map<Crop>(cropDto);
            if (_context.Crops == null)
          {
              return Problem("Entity set 'CropDealContext.Crops'  is null.");
          }
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrop", new { id = crop.CropId }, crop);
        }

        // DELETE: api/Crops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            if (_context.Crops == null)
            {
                return NotFound();
            }
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CropExists(int id)
        {
            return (_context.Crops?.Any(e => e.CropId == id)).GetValueOrDefault();
        }
    }
}
