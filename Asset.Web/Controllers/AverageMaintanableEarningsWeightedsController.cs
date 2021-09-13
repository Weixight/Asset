using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Asset.Web.Controllers
{
    public class AverageMaintanableEarningsWeightedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AverageMaintanableEarningsWeightedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AverageMaintanableEarningsWeighteds
        public async Task<IActionResult> Index()
        {
            return View(await _context.AverageMaintanableEarningsWeighteds.ToListAsync());
        }

        // GET: AverageMaintanableEarningsWeighteds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var averageMaintanableEarningsWeighted = await _context.AverageMaintanableEarningsWeighteds
                .FirstOrDefaultAsync(m => m.id == id);
            if (averageMaintanableEarningsWeighted == null)
            {
                return NotFound();
            }

            return View(averageMaintanableEarningsWeighted);
        }

        // GET: AverageMaintanableEarningsWeighteds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AverageMaintanableEarningsWeighteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CalculatedItem,DateCreated,DateUpdated,CreatedBy,UpdatedBy")] AverageMaintanableEarningsWeighted averageMaintanableEarningsWeighted)
        {
            if (ModelState.IsValid)
            {
                averageMaintanableEarningsWeighted.DateCreated = System.DateTime.Now;
                averageMaintanableEarningsWeighted.DateUpdated = System.DateTime.Now;
                averageMaintanableEarningsWeighted.CreatedBy = HttpContext.Session.GetString("LogSession");
                averageMaintanableEarningsWeighted.UpdatedBy = HttpContext.Session.GetString("LogSession");

                _context.Add(averageMaintanableEarningsWeighted);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(averageMaintanableEarningsWeighted);
        }

        // GET: AverageMaintanableEarningsWeighteds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var averageMaintanableEarningsWeighted = await _context.AverageMaintanableEarningsWeighteds.FindAsync(id);
            if (averageMaintanableEarningsWeighted == null)
            {
                return NotFound();
            }
            return View(averageMaintanableEarningsWeighted);
        }

        // POST: AverageMaintanableEarningsWeighteds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CalculatedItem,DateCreated,DateUpdated,CreatedBy,UpdatedBy")] AverageMaintanableEarningsWeighted averageMaintanableEarningsWeighted)
        {
            if (id != averageMaintanableEarningsWeighted.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(averageMaintanableEarningsWeighted);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AverageMaintanableEarningsWeightedExists(averageMaintanableEarningsWeighted.id))
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
            return View(averageMaintanableEarningsWeighted);
        }

        // GET: AverageMaintanableEarningsWeighteds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var averageMaintanableEarningsWeighted = await _context.AverageMaintanableEarningsWeighteds
                .FirstOrDefaultAsync(m => m.id == id);
            if (averageMaintanableEarningsWeighted == null)
            {
                return NotFound();
            }

            return View(averageMaintanableEarningsWeighted);
        }

        // POST: AverageMaintanableEarningsWeighteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var averageMaintanableEarningsWeighted = await _context.AverageMaintanableEarningsWeighteds.FindAsync(id);
            _context.AverageMaintanableEarningsWeighteds.Remove(averageMaintanableEarningsWeighted);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AverageMaintanableEarningsWeightedExists(int id)
        {
            return _context.AverageMaintanableEarningsWeighteds.Any(e => e.id == id);
        }
    }
}
