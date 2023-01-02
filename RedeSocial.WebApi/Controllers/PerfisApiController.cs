using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PerfisApiController : ControllerBase
    {
        private readonly RedeSocialDbContext _context;

        public PerfisApiController(RedeSocialDbContext context)
        {
            _context = context;
        }

        // GET: api/PerfisApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfis()
        {
            return await _context.Perfils_.ToListAsync();
        }

        // GET: api/PerfisApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> GetPerfil(Guid id)
        {
            var perfil = await _context.Perfils_.FindAsync(id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // PUT: api/PerfisApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(Guid id, Perfil perfil)
        {
            if (id != perfil.Id)
            {
                return BadRequest();
            }

            _context.Entry(perfil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(id))
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

        // POST: api/PerfisApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Perfil>> PostPerfil(Perfil perfil)
        {
            _context.Perfils_.Add(perfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfil", new { id = perfil.Id }, perfil);
        }

        // DELETE: api/PerfisApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfil(Guid id)
        {
            var perfil = await _context.Perfils_.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }

            _context.Perfils_.Remove(perfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilExists(Guid id)
        {
            return _context.Perfils_.Any(e => e.Id == id);
        }
    }
}
