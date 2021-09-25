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
    public class CorpEarningSetUpsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CorpSetUpService _corpSetUpService;

        public CorpEarningSetUpsController(ApplicationDbContext context, CorpSetUpService corpSetUpService)
        {
            _context = context;
            _corpSetUpService = corpSetUpService;
        }

        // GET: CorpEarningSetUps
        public async Task<IActionResult> Index()
        {
            var CorpReg = await _context.corpRegs.ToListAsync();
            ViewBag.SetupExist = await  _context.OurCorpEarningSetup.Include(c => c.CorpEarning).Include(c => c.CorpReg).ToListAsync();
           // var applicationDbContext = _context.OurCorpEarningSetup.Include(c => c.CorpEarning).Include(c => c.CorpReg);
            return View(CorpReg);
        }

        // GET: CorpEarningSetUps/Details/5
        public async Task<IActionResult> Details(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                return NotFound();
            }

            var corpEarningSetUp = _corpSetUpService.MyCorpSetUp(Name);
            if (corpEarningSetUp == null)
            {
                return NotFound();
            }
            return View(corpEarningSetUp);
        }

        // GET: CorpEarningSetUps/Create
        public IActionResult Create()
        {
            ViewData["CalculatedItemId"] = new SelectList(_context.Set<CorpEarning>(), "id", "CalculatedItem");
            ViewData["Corpid"] = new SelectList(_context.corpRegs, "id", "Name");
            return View();
        }

        // POST: CorpEarningSetUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Applied,Corpid,CalculatedItemId")] CorpEarningSetUp corpEarningSetUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corpEarningSetUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalculatedItemId"] = new SelectList(_context.Set<CorpEarning>(), "id", "id", corpEarningSetUp.CorpEarningid);
            ViewData["Corpid"] = new SelectList(_context.corpRegs, "id", "id", corpEarningSetUp.Corpid);
            return View(corpEarningSetUp);
        }

        [HttpGet]
        public async Task<IActionResult> EditConfigure(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                //return NotFound();
                return View();
            }

            var corpEarningSetUp = _corpSetUpService.MyCorpSetUp(Name);
            if (corpEarningSetUp == null)
            {
                return NotFound();
            }
            ViewBag.Coperative = _context.corpRegs.FirstOrDefault(l => l.Name == Name);
           // ViewData["CalculatedItemId"] = new SelectList(_context.Set<CorpEarning>(), "id", "id", corpEarningSetUp.CorpEarningid);
            //ViewData["Corpid"] = new SelectList(_context.corpRegs, "id", "id", corpEarningSetUp.Corpid);
            return View(corpEarningSetUp);
        }
        [HttpPost]
        public async Task<IActionResult> EditConfigure(List<OurCorpSetUp> ourCorpSetUps , int k, string Name)
        {

            var AddResult = await _corpSetUpService.CreateCorpEarningAsync(ourCorpSetUps, Name);

           if (AddResult > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ourCorpSetUps);
            }
            
        }

        // GET: CorpEarningSetUps/Edit/5
        public async Task<IActionResult> Edit(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                return NotFound();
            }

            var corpEarningSetUp =  _corpSetUpService.MyCorpSetUp(Name);
            if (corpEarningSetUp == null)
            {
                return NotFound();
            }
             return View(corpEarningSetUp);
        }

        // POST: CorpEarningSetUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<OurCorpSetUp> ourCorpSetUps, int k, int Corpid)
        {
            var AddResult =  _corpSetUpService.UpdateEarning(ourCorpSetUps, Corpid).Result;

            if (AddResult > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ourCorpSetUps);
            }
        }
           
        

        // GET: CorpEarningSetUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corpEarningSetUp = await _context.OurCorpEarningSetup
                .Include(c => c.CorpEarning)
                .Include(c => c.CorpReg)
                .FirstOrDefaultAsync(m => m.id == id);
            if (corpEarningSetUp == null)
            {
                return NotFound();
            }

            return View(corpEarningSetUp);
        }

        // POST: CorpEarningSetUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corpEarningSetUp = await _context.OurCorpEarningSetup.FindAsync(id);
            _context.OurCorpEarningSetup.Remove(corpEarningSetUp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorpEarningSetUpExists(int id)
        {
            return _context.OurCorpEarningSetup.Any(e => e.id == id);
        }
    }
}
