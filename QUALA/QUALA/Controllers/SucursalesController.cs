using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUALA.Data;
using QUALA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QUALA.Controllers
{
    [ApiController]
    [Route("api/sucursales")]
    public class SucursalesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SucursalesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NSucursales>>> GetSucursales()
        {
            var sucursales = await _context.Sucursales.Include(s => s.Moneda).ToListAsync();
            return Ok(sucursales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NSucursales>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales.Include(s => s.Moneda).FirstOrDefaultAsync(s => s.MonedaId == id);
            if (sucursal == null)
            {
                return NotFound();
            }
            return Ok(sucursal);
        }

        [HttpPost]
        public async Task<ActionResult<NSucursales>> CreateSucursal(NSucursales sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.RegistroId }, sucursal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSucursal(int id, NSucursales sucursal)
        {
            if (id != sucursal.RegistroId)
            {
                return BadRequest();
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursales.Any(e => e.RegistroId == id);
        }
    }
}
