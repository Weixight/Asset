using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asset.Web.Models;
using Asset.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurHr.Models;
using OurHr.ViewModels;

namespace OurHr.Controllers
{
   // [Authorize("Authorization")]
    public class UserAccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public UserAccountController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(login.Email);

                if (user != null)
                {
                    // Cancel existing session
                    await signInManager.SignOutAsync();

                    // Perform the authentication
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, login.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                else // user is null
                {
                    ModelState.AddModelError(nameof(LoginVm.Email), "Invalid user or password");
                }
            }

            return View(login); // user is null or fail authentication (result.Succeeded = false)
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()  // Fired when a user cannot access an action
        {
            return View();
        }
    }
}
