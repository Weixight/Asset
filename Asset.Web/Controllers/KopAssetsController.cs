using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web;
using Asset.Web.Data;

namespace Asset.Web.Controllers
{
    public class KopAssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KopAssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KopAssets
        public async Task<IActionResult> Index()
        {
            return View(await _context.KopAssets.ToListAsync());
        }

        // GET: KopAssets/Details/5
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

        // GET: KopAssets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KopAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Type,Tenure,Value,CorpNName,CorpId,AssetDate")] KopAsset kopAsset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kopAsset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kopAsset);
        }

        // GET: KopAssets/Edit/5
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

        // POST: KopAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Type,Tenure,Value,CorpNName,CorpId,AssetDate")] KopAsset kopAsset)
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

        // GET: KopAssets/Delete/5
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

        // POST: KopAssets/Delete/5
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
