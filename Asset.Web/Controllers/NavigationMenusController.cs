using Asset.Web.Data;
using Asset.Web.Models;
using Asset.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurHr.Controllers
{
    //  [Authorize("Authorization")]
    public class NavigationMenusController : Controller
    {
        private readonly ApplicationDbContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public NavigationMenusController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // [Authorize("Authorization")]


        // GET: NavigationMenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AspNetNavigationMenu.Include(n => n.ParentNavigationMenu);
            ViewBag.Exist = TempData["Exist"]?.ToString();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NavigationMenus/Details/5
        //  [Authorize("Authorization")]

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMenu = await _context.AspNetNavigationMenu
                .Include(n => n.ParentNavigationMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationMenu == null)
            {
                return NotFound();
            }

            return View(navigationMenu);
        }
        [AllowAnonymous]
        public JsonResult GetSubCategory(string CategoryId)
        {
            ReadDir readDir = new ReadDir(_hostingEnvironment);
            List<ControllerActions> theMethods = new List<ControllerActions>();
            var ActName = readDir.GetActions(CategoryId);
            List<FileModel> fileModelsList = new List<FileModel>();


            return Json(new SelectList(ActName, "Action", "Action"));
        }

        // GET: NavigationMenus/Create
        // [Authorize("Authorization")]

        public IActionResult Create()
        {

            try
            {
                string mystring = "";
                ReadDir readDir = new ReadDir(_hostingEnvironment);
                //var Myclass = readDir.ReadMyDir(mystring);
                var Myclass = readDir.MyController();

                ViewBag.Myclass = Myclass;
                ViewData["ParentMenuId"] = new SelectList(_context.AspNetNavigationMenu, "Id", "Name");
                ViewData["Service"] = new SelectList(_context.ServiceTbl, "ServiceCode", "ServiceName");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentMenuId,Area,ControllerName,ActionName,IsExternal,ExternalUrl,DisplayOrder,Visible,Klass", "Read", "Delete", "Update", "Create", "ServiceCode")] NavigationMenu navigationMenu)
        {
            if (navigationMenu.ControllerName == "Please select one")
            {
                navigationMenu.ControllerName = null;
            }
            var Exist = _context.AspNetNavigationMenu.FirstOrDefault(J => J.Name == navigationMenu.Name && J.ControllerName == navigationMenu.ControllerName && J.ActionName == navigationMenu.ActionName);
            if (Exist == null)
            {
                string Substring = "Controller";
                if (navigationMenu.ControllerName != null)
                {
                    navigationMenu.ControllerName = navigationMenu.ControllerName.Replace(Substring, "");
                }
                navigationMenu.Id = Guid.NewGuid();
                // navigationMenu.ActionName = navigationMenu.ControllerName;
                _context.Add(navigationMenu);
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["Exist"] = "Page already exsit";
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMenu = await _context.AspNetNavigationMenu.FindAsync(id);
            if (navigationMenu == null)
            {
                return NotFound();
            }
            ViewData["ParentMenuId"] = new SelectList(_context.AspNetNavigationMenu, "Id", "Name", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ParentMenuId,Area,ControllerName,ActionName,IsExternal,ExternalUrl,DisplayOrder,Visible")] NavigationMenu navigationMenu)
        {
            if (id != navigationMenu.Id)
            {
                return NotFound();
            }
            if (navigationMenu.ControllerName == "Please select one")
            {
                navigationMenu.ControllerName = null;
                navigationMenu.ParentMenuId = null;
            }

            if (ModelState.IsValid || navigationMenu.ParentMenuId == null)
            {
                try
                {
                   
                    _context.Update(navigationMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationMenuExists(navigationMenu.Id))
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
            ViewData["ParentMenuId"] = new SelectList(_context.AspNetNavigationMenu, "Id", "Id", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }
       // [Authorize("Authorization")]

        // GET: NavigationMenus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMenu = await _context.AspNetNavigationMenu
                .Include(n => n.ParentNavigationMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationMenu == null)
            {
                return NotFound();
            }

            return View(navigationMenu);
        }

        // POST: NavigationMenus/Delete/5
       // [Authorize("Authorization")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var navigationMenu = await _context.AspNetNavigationMenu.FindAsync(id);
            _context.AspNetNavigationMenu.Remove(navigationMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationMenuExists(Guid id)
        {
            return _context.AspNetNavigationMenu.Any(e => e.Id == id);
        }
    }
}