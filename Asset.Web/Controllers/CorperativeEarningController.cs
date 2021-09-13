using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.Models;

namespace Asset.Web.Controllers
{
    public class CorperativeEarningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorperativeEarningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CorperativeEarning
        public async Task<IActionResult> Index()
        {
            return View(await _context.corpRegs.ToListAsync());
        }

        // GET: CorperativeEarning/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs
                .FirstOrDefaultAsync(m => m.id == id);
            if (corpReg == null)
            {
                return NotFound();
            }

            return View(corpReg);
        }

        // GET: CorperativeEarning/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CorperativeEarning/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CopRegNum,Name,Address,State,LGA,Phone,Email,Website,RegState,RegLGA,CACRegno,AssetPrefix")] CorpReg corpReg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corpReg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(corpReg);
        }

        // GET: CorperativeEarning/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs.FindAsync(id);
            if (corpReg == null)
            {
                return NotFound();
            }
            return View(corpReg);
        }

        // POST: CorperativeEarning/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CopRegNum,Name,Address,State,LGA,Phone,Email,Website,RegState,RegLGA,CACRegno,AssetPrefix")] CorpReg corpReg)
        {
            if (id != corpReg.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corpReg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorpRegExists(corpReg.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(corpReg);
        }

        // GET: CorperativeEarning/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs
                .FirstOrDefaultAsync(m => m.id == id);
            if (corpReg == null)
            {
                return NotFound();
            }

            return View(corpReg);
        }

        // POST: CorperativeEarning/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corpReg = await _context.corpRegs.FindAsync(id);
            _context.corpRegs.Remove(corpReg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorpRegExists(int id)
        {
            return _context.corpRegs.Any(e => e.id == id);
        }
    }
}
