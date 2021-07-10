using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.Models;

namespace Asset.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorpRegController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KorpRegController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KorpReg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorpReg>>> GetcorpRegs()
        {
            return await _context.corpRegs.ToListAsync();
        }

        // GET: api/KorpReg/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CorpReg>> GetCorpReg(int id)
        {
            var corpReg = await _context.corpRegs.FindAsync(id);

            if (corpReg == null)
            {
                return NotFound();
            }

            return corpReg;
        }

        // PUT: api/KorpReg/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorpReg(int id, CorpReg corpReg)
        {
            if (id != corpReg.id)
            {
                return BadRequest();
            }

            _context.Entry(corpReg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorpRegExists(id))
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

        // POST: api/KorpReg
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CorpReg>> PostCorpReg(CorpReg corpReg)
        {
            _context.corpRegs.Add(corpReg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCorpReg", new { id = corpReg.id }, corpReg);
        }

        // DELETE: api/KorpReg/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CorpReg>> DeleteCorpReg(int id)
        {
            var corpReg = await _context.corpRegs.FindAsync(id);
            if (corpReg == null)
            {
                return NotFound();
            }

            _context.corpRegs.Remove(corpReg);
            await _context.SaveChangesAsync();

            return corpReg;
        }

        private bool CorpRegExists(int id)
        {
            return _context.corpRegs.Any(e => e.id == id);
        }
    }
}
