using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.ViewModels;
using Asset.Web.Models;

namespace Asset.Web.Controllers
{
    public class AssetLiabilityViewsController : Controller
    {
        private readonly AssetLiabilityRun  _assetLiabilityRun;

        public AssetLiabilityViewsController(AssetLiabilityRun assetLiabilityRun)
        {
            _assetLiabilityRun = assetLiabilityRun;
        }

        // GET: AssetLiabilityViews
        public async Task<IActionResult> Index(string Corp)
        {
            var Asset = _assetLiabilityRun.CorpAsset(Corp).Sum(k=>k.Value);
            var Laibilites = _assetLiabilityRun.CorPLiability(Corp).Sum(k => k.Value);
            var NetWorth = Asset - Laibilites;
            ViewBag.NetWorth = NetWorth;
            ViewBag.Name = Corp;
            ViewBag.Asset = Asset;
            ViewBag.TotalLibaility = Laibilites;
            return View( _assetLiabilityRun.PerCopAssetLiability(Corp));
        }

        // GET: AssetLiabilityViews/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var assetLiabilityView = await _context.AssetLiabilityView
    //            .FirstOrDefaultAsync(m => m.id == id);
    //        if (assetLiabilityView == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(assetLiabilityView);
    //    }

    //    // GET: AssetLiabilityViews/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: AssetLiabilityViews/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("id,Name,Type,Tenure,Value,CopAssetRegNo,CorpNName,TenureName,CorpId,AssetDate")] AssetLiabilityView assetLiabilityView)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(assetLiabilityView);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(assetLiabilityView);
    //    }

    //    // GET: AssetLiabilityViews/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var assetLiabilityView = await _context.AssetLiabilityView.FindAsync(id);
    //        if (assetLiabilityView == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(assetLiabilityView);
    //    }

    //    // POST: AssetLiabilityViews/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("id,Name,Type,Tenure,Value,CopAssetRegNo,CorpNName,TenureName,CorpId,AssetDate")] AssetLiabilityView assetLiabilityView)
    //    {
    //        if (id != assetLiabilityView.id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(assetLiabilityView);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!AssetLiabilityViewExists(assetLiabilityView.id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(assetLiabilityView);
    //    }

    //    // GET: AssetLiabilityViews/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var assetLiabilityView = await _context.AssetLiabilityView
    //            .FirstOrDefaultAsync(m => m.id == id);
    //        if (assetLiabilityView == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(assetLiabilityView);
    //    }

    //    // POST: AssetLiabilityViews/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var assetLiabilityView = await _context.AssetLiabilityView.FindAsync(id);
    //        _context.AssetLiabilityView.Remove(assetLiabilityView);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool AssetLiabilityViewExists(int id)
    //    {
    //        return _context.AssetLiabilityView.Any(e => e.id == id);
    //    }
    }
}
