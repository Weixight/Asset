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
    public class WeightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeightController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weight
        public async Task<IActionResult> Index()
        {
            return View(await _context.EarninWeights.ToListAsync());
        }

        // GET: Weight/Details/5
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

        // GET: Weight/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weight/Create
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

        // GET: Weight/Edit/5
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

        // POST: Weight/Edit/5
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

        // GET: Weight/Delete/5
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

        // POST: Weight/Delete/5
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
