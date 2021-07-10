using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web;
using Asset.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Asset.Web.Models;

namespace Asset.Web.Controllers
{
    public class KopAssets1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public KopAssets1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KopAssets1 wasiu
        public async Task<IActionResult> Index(string Name)
        {
            if(Name != null)
            {
                TempData["id"] = Name;
                var MyAsset = _context.KopAssets.Where(k => k.CorpNName == Name).ToList();
                ViewBag.NonCurrentasset = MyAsset.Where(k => k.Tenure > 12).Sum(k => k.Value);
                ViewBag.CurrentAsset = MyAsset.Where(k => k.Tenure <= 12).Sum(k => k.Value);
                ViewBag.Asset = MyAsset.Sum(k => k.Value);
                return View(await _context.KopAssets.Where(k => k.CorpNName == Name).ToListAsync());

            }
            else
            {
                var MyAsset = _context.KopAssets.Where(k => k.CorpNName == Name).ToList();
                ViewBag.NonCurrentasset = MyAsset.Where(k => k.Tenure > 12).Sum(k => k.Value);
                ViewBag.CurrentAsset = MyAsset.Where(k => k.Tenure <= 12).Sum(k => k.Value);
                ViewBag.Asset = MyAsset.Sum(k => k.Value);
                return View(await _context.KopAssets.ToListAsync());
            }
        }
           

        // GET: KopAssets1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kopAsset = await _context.KopAssets
                .FirstOrDefaultAsync(m => m.id == id);
            if (kopAsset == null)
            {
                return NotFound();
            }

            return View(kopAsset);
        }

       // [AllowAnonymous]
        public JsonResult GenerateAssetNo(string Generate)
        {
            string StaffNo = "";
            var k = 0;
            var MyLast = _context.AssetNumber.ToList();
            if (MyLast.Count != 0)
            {
                var MyLastId = MyLast.Max(k => k.AssetId);
                k = MyLastId + 1;
            }
            else
            {
                k = 1;
            }

            StaffNo = Generate.TrimEnd() + "-" + k;
            // mySpList = (from MyLevel in _db.MyOrgList where MyLevel.TreeStatus == CategoryId.ToString() select MyLevel).ToList();

            return Json(StaffNo);
        }


        // GET: KopAssets1/Create
        public IActionResult Create(string CACRegno)
        {
            try
            {
                ViewBag.id = TempData["id"];
                string myid = TempData["id"].ToString();
                ViewBag.Type = _context.AssetTypes.Where(k => k.AL == "Asset").ToList();
                var myCorpDetail = _context.corpRegs.FirstOrDefault(K => K.Name == myid);
                ViewBag.Name = myCorpDetail.Name;
                ViewBag.CorpRegno = myCorpDetail.CopRegNum;
                var MyAssetNo = _context.AssetNumber.ToList();
                if (MyAssetNo.Count == 0)
                {
                    ViewBag.AsstNo = myCorpDetail.AssetPrefix + "-"  + 1;
                }
                else
                {
                    ViewBag.AsstNo = myCorpDetail.AssetPrefix +  "-" + MyAssetNo.Max(K => K.AssetId) + 1;
                }
               
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View();
            }
        }

        // POST: KopAssets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Type,Tenure,Value,CorpNName,AssetRegNo,CopAssetRegNo,CorpId,AssetDate")] KopAsset kopAsset)
        {
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {

                            _context.Add(kopAsset);
                            await _context.SaveChangesAsync();

                            Assetno MyAsset = new Assetno();
                            MyAsset.Name = "Asset";
                            MyAsset.Cop = kopAsset.CopAssetRegNo;
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
               
                return RedirectToAction("Index", "CorpRegs");
            }
            return View(kopAsset);
        }

        // GET: KopAssets1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kopAsset = await _context.KopAssets.FindAsync(id);
            if (kopAsset == null)
            {
                return NotFound();
            }
            return View(kopAsset);
        }

        // POST: KopAssets1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Type,Tenure,Value,CorpNName,AssetRegNo,CopAssetRegNo,CorpId,AssetDate")] KopAsset kopAsset)
        {
            if (id != kopAsset.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kopAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KopAssetExists(kopAsset.id))
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
            return View(kopAsset);
        }

        // GET: KopAssets1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kopAsset = await _context.KopAssets
                .FirstOrDefaultAsync(m => m.id == id);
            if (kopAsset == null)
            {
                return NotFound();
            }

            return View(kopAsset);
        }

        // POST: KopAssets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kopAsset = await _context.KopAssets.FindAsync(id);
            _context.KopAssets.Remove(kopAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KopAssetExists(int id)
        {
            return _context.KopAssets.Any(e => e.id == id);
        }
    }
}
