using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.Models;
using Asset.Web.ViewModels;

namespace Asset.Web.Controllers
{
    public class MyEarningsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MyEarningList _myEarningList;

        public MyEarningsController(ApplicationDbContext context, MyEarningList myEarningList)
        {
            _context = context;
            _myEarningList = myEarningList;
        }

        // GET: MyEarnings
        public async Task<IActionResult> Index()
        {
            return View(await _context.corpRegs.ToListAsync());
        }

        // GET: MyEarnings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpEarning = await _context.MyEarning
                .FirstOrDefaultAsync(m => m.id == id);
            if (corpEarning == null)
            {
                return NotFound();
            }

            return View(corpEarning);
        }

        // GET: MyEarnings/Create
        public IActionResult Create(int id)
        {
            var MyEarning = _myEarningList.TheEarningList(id);
            return View(MyEarning);
        }
        [HttpPost]
        public IActionResult MyCreate(decimal Year_one, decimal Year_Two, decimal Year_Three, List<CorpEarning> MyCorpEarning)
        {
            var Taker = _myEarningList.Create(MyCorpEarning);
            return View();
        }

        // POST: MyEarnings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ValueAmount,ValueDate,DateCreated,DateUpdated,CreatedBy,UpdatedBy,Corpid,CorpEarningid")] CorpEarning corpEarning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corpEarning);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(corpEarning);
        }

        // GET: MyEarnings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpEarning = await _context.MyEarning.FindAsync(id);
            if (corpEarning == null)
            {
                return NotFound();
            }
            return View(corpEarning);
        }

        // POST: MyEarnings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ValueAmount,ValueDate,DateCreated,DateUpdated,CreatedBy,UpdatedBy,Corpid,CorpEarningid")] CorpEarning corpEarning)
        {
            if (id != corpEarning.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corpEarning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorpEarningExists(corpEarning.id))
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
            return View(corpEarning);
        }

        // GET: MyEarnings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpEarning = await _context.MyEarning
                .FirstOrDefaultAsync(m => m.id == id);
            if (corpEarning == null)
            {
                return NotFound();
            }

            return View(corpEarning);
        }

        // POST: MyEarnings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corpEarning = await _context.MyEarning.FindAsync(id);
            _context.MyEarning.Remove(corpEarning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorpEarningExists(int id)
        {
            return _context.MyEarning.Any(e => e.id == id);
        }
    }
}
