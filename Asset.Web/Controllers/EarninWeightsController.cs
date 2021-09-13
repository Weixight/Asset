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
    public class EarninWeightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EarninWeightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EarninWeights
        public async Task<IActionResult> Index()
        {
            return View(await _context.EarninWeights.ToListAsync());
        }

        // GET: EarninWeights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var earninWeight = await _context.EarninWeights
                .FirstOrDefaultAsync(m => m.id == id);
            if (earninWeight == null)
            {
                return NotFound();
            }

            return View(earninWeight);
        }

        // GET: EarninWeights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EarninWeights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ValuePercentage,ValueDate,ValueYear,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] EarninWeight earninWeight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(earninWeight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(earninWeight);
        }

        // GET: EarninWeights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var earninWeight = await _context.EarninWeights.FindAsync(id);
            if (earninWeight == null)
            {
                return NotFound();
            }
            return View(earninWeight);
        }

        // POST: EarninWeights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ValuePercentage,ValueDate,ValueYear,CreatedDate,UpdatedDate,CreatedBy,UpdatedBy")] EarninWeight earninWeight)
        {
            if (id != earninWeight.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(earninWeight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EarninWeightExists(earninWeight.id))
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
            return View(earninWeight);
        }

        // GET: EarninWeights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var earninWeight = await _context.EarninWeights
                .FirstOrDefaultAsync(m => m.id == id);
            if (earninWeight == null)
            {
                return NotFound();
            }

            return View(earninWeight);
        }

        // POST: EarninWeights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var earninWeight = await _context.EarninWeights.FindAsync(id);
            _context.EarninWeights.Remove(earninWeight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EarninWeightExists(int id)
        {
            return _context.EarninWeights.Any(e => e.id == id);
        }
    }
}
