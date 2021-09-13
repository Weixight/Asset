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
    public class AverageMaintanableEarningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AverageMaintanableEarningsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AverageMaintanableEarnings
        public async Task<IActionResult> Index()
        {
            return View(await _context.AverageMaintanableEarnings.ToListAsync());
        }

        // GET: AverageMaintanableEarnings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var averageMaintanableEarnings = await _context.AverageMaintanableEarnings
            //    .FirstOrDefaultAsync(m => m.id == id);
            //if (averageMaintanableEarnings == null)
            //{
            //    return NotFound();
            //}

            //return View(averageMaintanableEarnings);
            return View();
        }

        // GET: AverageMaintanableEarnings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AverageMaintanableEarnings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CorpId,CorpName,Revenue,Costofgoodssold,Grossprofit,Operatingexpenses,Otherexpenses,Depreciation,Profitbeforetax,Incometaxpaid,Profitaftertax,Year,DateCreated,DateUpdated,CreatedBy,UpdatedBy,Month,ValueDate,Weighted")] AverageMaintanableEarnings averageMaintanableEarnings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(averageMaintanableEarnings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(averageMaintanableEarnings);
        }

        // GET: AverageMaintanableEarnings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var averageMaintanableEarnings = await _context.AverageMaintanableEarnings.FindAsync(id);
            if (averageMaintanableEarnings == null)
            {
                return NotFound();
            }
            return View(averageMaintanableEarnings);
        }

        // POST: AverageMaintanableEarnings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CorpId,CorpName,Revenue,Costofgoodssold,Grossprofit,Operatingexpenses,Otherexpenses,Depreciation,Profitbeforetax,Incometaxpaid,Profitaftertax,Year,DateCreated,DateUpdated,CreatedBy,UpdatedBy,Month,ValueDate,Weighted")] AverageMaintanableEarnings averageMaintanableEarnings)
        {
            if (id != averageMaintanableEarnings.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(averageMaintanableEarnings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AverageMaintanableEarningsExists(averageMaintanableEarnings.id))
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
            return View(averageMaintanableEarnings);
        }

        // GET: AverageMaintanableEarnings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var averageMaintanableEarnings = await _context.AverageMaintanableEarnings
                .FirstOrDefaultAsync(m => m.id == id);
            if (averageMaintanableEarnings == null)
            {
                return NotFound();
            }

            return View(averageMaintanableEarnings);
        }

        // POST: AverageMaintanableEarnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var averageMaintanableEarnings = await _context.AverageMaintanableEarnings.FindAsync(id);
            _context.AverageMaintanableEarnings.Remove(averageMaintanableEarnings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AverageMaintanableEarningsExists(int id)
        {
            return _context.AverageMaintanableEarnings.Any(e => e.id == id);
        }
    }
}
