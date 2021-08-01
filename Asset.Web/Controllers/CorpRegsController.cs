using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asset.Web.Data;
using Asset.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Newtonsoft.Json;

namespace Asset.Web.Controllers
{
    public class CorpRegsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorpRegsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CorpRegs
        public async Task<IActionResult> Index()
        {
            ViewBag.State = _context.State.ToList();
            var MyAsset = _context.KopAssets.ToList();
            var MyLiability = _context.Liabilities.ToList();
            ViewBag.MyAsset = MyAsset;
            ViewBag.MyLiability = MyLiability;
            var TotalAsset = MyAsset.Sum(K => K.Value);
            var TotalLiability = MyLiability.Sum(k => k.Value);
            var Networth = TotalAsset - TotalLiability;
            ViewBag.Asset = TotalAsset;
            ViewBag.TotalLibaility = TotalLiability;
            ViewBag.Networth = Networth;
            return View(await _context.corpRegs.ToListAsync());
        }

        // GET: CorpRegs/Details/5k

        [AllowAnonymous]
        public JsonResult GenerateStaffNo(string jsonInput)
        {
           // JsonResult result = new JsonResult();
            DataTable data = new  DataTable();
            
            // Deserialize Input JSON String into target Object Mapper.  
            CorpReg reqObj = JsonConvert.DeserializeObject<CorpReg>(jsonInput);
            _context.corpRegs.Add(reqObj);
            _context.SaveChanges();
            var CorpList = _context.corpRegs.ToList();

            var result = this.Json(JsonConvert.SerializeObject(reqObj));

            return Json(result);
        }
        public async Task<IActionResult> Details(string Name)
        {
            if (Name == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs
                .FirstOrDefaultAsync(m => m.Name == Name);
            corpReg.State = _context.State.FirstOrDefaultAsync(K => K.state_id == Convert.ToInt32(corpReg.State)).Result.state_name;
            corpReg.LGA = _context.LGA.FirstOrDefaultAsync(K => K.lga_id == Convert.ToInt32(corpReg.LGA)).Result.lga_name;

            if (corpReg == null)
            {
                return NotFound();
            }

            return View(corpReg);
        }


        public JsonResult GetLga(int CategoryID)
        {

            var MyStep = _context.LGA.Where(m => m.state_id == CategoryID).ToList(); //call repository
            return Json(new SelectList(MyStep, "lga_id", "lga_name"));
            //return Json(MyStep);
        }

        // GET: CorpRegs/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.State = _context.State.ToList();
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message.ToString();
                return View();
            }
        }

        // POST: CorpRegs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CopRegNum,Name,Address,State,LGA,Phone,Email,Website,RegState,RegLGA,CACRegno")] CorpReg corpReg)
        {
            if (ModelState.IsValid)
            {
                var Exist = _context.corpRegs.FirstOrDefault(K => K.CACRegno == corpReg.CACRegno);
                if (Exist == null)
                {
                    _context.Add(corpReg);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Exist = "Corperative Exist";
                    ViewBag.State = _context.State.ToList();
                    return View(corpReg);
                }
            }
            return View(corpReg);
        }

        // GET: CorpRegs/Edit/5
        public async Task<IActionResult> Edit(string Name)
        {
            if (Name == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs.FirstOrDefaultAsync(k=>k.Name==Name);
            if (corpReg == null)
            {
                return NotFound();
            }
            ViewBag.State = _context.State.ToList();
            
            var lga = _context.LGA.ToList();
            var MyLga = lga.FirstOrDefault(K => K.lga_id ==Convert.ToInt32(corpReg.LGA));
            ViewBag.Lga = lga;
            ViewBag.MyLga = MyLga;
            return View(corpReg);
        }

        // POST: CorpRegs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CopRegNum,Name,Address,State,LGA,Phone,Email,Website,RegState,RegLGA,CACRegno")] CorpReg corpReg)
        {
            if (id != corpReg.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corpReg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorpRegExists(corpReg.id))
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
            return View(corpReg);
        }

        // GET: CorpRegs/Delete/5
        public async Task<IActionResult> Delete(string Name)
        {
            if (Name == null)
            {
                return NotFound();
            }

            var corpReg = await _context.corpRegs
                .FirstOrDefaultAsync(m => m.Name == Name);
            if (corpReg == null)
            {
                return NotFound();
            }

            return View(corpReg);
        }

        // POST: CorpRegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Name)
        {
            var corpReg = await _context.corpRegs.FirstOrDefaultAsync(k=>k.Name ==Name);
             _context.corpRegs.Remove(corpReg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorpRegExists(int id)
        {
            return _context.corpRegs.Any(e => e.id == id);
        }
    }
}
