using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web;
using Asset.Web.Data;
using Asset.Web.Models;

namespace Asset.Web.Controllers
{
    public class LiabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LiabilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Liabilities
        public async Task<IActionResult> Index(string Name)
        {
            if (Name != null)
            {
                TempData["id"] = Name;
               // var MyAsset = _context.KopAssets.ToList();
                var MyLiability = _context.Liabilities.Where(k=>k.CorpNName == Name).ToList();
                ViewBag.NonCurrentLiab = MyLiability.Where(k=>k.Tenure>12).Sum(k=>k.Value);
                ViewBag.CurrentLiab = MyLiability.Where(k => k.Tenure <= 12).Sum(k => k.Value);
                ViewBag.Liability = MyLiability.Sum(k => k.Value);
                ViewBag.Name = Name;

                return View(await _context.Liabilities.Where(k=>k.CorpNName ==Name).ToListAsync());
            }
            else
            {
                var MyLiability = _context.Liabilities.Where(k => k.Name == Name).ToList();
                ViewBag.NonCurrentLiab = MyLiability.Where(k => k.Tenure > 12).Sum(k => k.Value);
                ViewBag.CurrentLiab = MyLiability.Where(k => k.Tenure <= 12).Sum(k => k.Value);
                ViewBag.Liability = MyLiability.Sum(k => k.Value);
                return View(await _context.Liabilities.ToListAsync());
            }
        }

        // GET: Liabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liability = await _context.Liabilities
                .FirstOrDefaultAsync(m => m.id == id);
            if (liability == null)
            {
                return NotFound();
            }

            return View(liability);
        }

        // GET: Liabilities/Create
        public IActionResult Create(string Name)
        {
            ViewBag.id = TempData["id"];
            string myid = TempData["id"].ToString() ;
            ViewBag.Type = _context.AssetTypes.Where(k => k.AL == "Liability").ToList();
            var myCorpDetail = _context.corpRegs.FirstOrDefault(K => K.Name == myid);
            ViewBag.Name = myCorpDetail.Name;
            ViewBag.CorpRegno = myCorpDetail.CopRegNum;
            var MyAssetNo = _context.AssetNumber.ToList();
            if (MyAssetNo.Count == 0)
            {
                ViewBag.AsstNo = myCorpDetail.AssetPrefix + "-" + 1;
            }
            else
            {
                ViewBag.AsstNo = myCorpDetail.AssetPrefix + "-" + MyAssetNo.Max(K => K.AssetId) + 1;
            }

            return View();
        }

        // POST: Liabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Type,Tenure,Value,LiabilityRegNo,CopAssetRegNo,CorpNName,CorpId,AssetDate")] Liability liability)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            _context.Add(liability);
                            await _context.SaveChangesAsync();
                            Assetno MyAsset = new Assetno();
                            MyAsset.Name = "Asset";
                            MyAsset.Cop = liability.CopAssetRegNo;
                            _context.AssetNumber.Add(MyAsset);
                            _context.SaveChanges();
                            transaction.Commit();
                            return RedirectToAction("Index", "CorpRegs");

                        }
                        catch (Exception ex)
                        {
                            string ErrMessage = ex.Message.ToString();
                            transaction.Rollback();
                           
                        }
                      
                    }
                   
                }
            }
            return RedirectToAction(nameof(Index));
        }
    

        // GET: Liabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liability = await _context.Liabilities.FindAsync(id);
            if (liability == null)
            {
                return NotFound();
            }
            return View(liability);
        }

        // POST: Liabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Type,Tenure,Value,LiabilityRegNo,CopAssetRegNo,CorpNName,CorpId,AssetDate")] Liability liability)
        {
            if (id != liability.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(liability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LiabilityExists(liability.id))
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
            return View(liability);
        }

        // GET: Liabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var liability = await _context.Liabilities
                .FirstOrDefaultAsync(m => m.id == id);
            if (liability == null)
            {
                return NotFound();
            }

            return View(liability);
        }

        // POST: Liabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var liability = await _context.Liabilities.FindAsync(id);
            _context.Liabilities.Remove(liability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LiabilityExists(int id)
        {
            return _context.Liabilities.Any(e => e.id == id);
        }
    }
}
