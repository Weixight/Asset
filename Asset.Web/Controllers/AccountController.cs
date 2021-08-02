using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OurHr.Models;
using OurHr.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using MimeKit;
using Asset.Web.Controllers;
using Asset.Web.Models;
using Asset.Web.Models.AccountViewModels;
using Asset.Web.Models.TamaViewModel;
using Asset.Web.Data;
using Asset.Web.Services;

namespace OurHr.Controllers
{
    //[Authorize]
    // [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSenderm _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnv;
        // private readonly IHostingEnvironment _hostingEnvironment;
        // private HRM.UTL.SMSMd sdsms = new SMSMd();
        //  private UtilityCls UtMail = new UtilityCls();
        private readonly RoleManager<IdentityRole> _roleManager;



        // UTL.= new SMSMd();
        //ok


        // private HRM.UTL.SMSMd Sdsms = new SMSMd();
        // private readonly UTL.UtilityCls utMail = new UtilityCls();
        [Obsolete]
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSenderm emailSender,
            ILogger<AccountController> logger, ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IHostingEnvironment hostingEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            //  _hostingEnv = hostingEnv;
            _logger = logger;
            _db = db;
            _roleManager = roleManager;
            _hostingEnv = hostingEnv;
            // UCL = UCL;
        }

        //[TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            if (TempData["Disable"]?.ToString() != null)
            {
                ViewBag.LogError = TempData["Disable"].ToString();
            }
            //ViewBag.LogError = TempData["Invalid"]?.ToString();
            //ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> NewLogin()
        {

            return View();
        }
        // [Authorize(Roles = SD.AdminEndUser)]
        //public IActionResult StaffListProfile()
        //{
        //    try

        //    {
        //        HrCllection HrStaffList = new HrCllection()
        //        {
        //            GetEmpTbls = _db.EmpTbls.ToList(),
        //        };
        //        return View(HrStaffList);
        //    }
        //    catch (Exception Ex)
        //    {
        //        HrCllection HrStaffList = new HrCllection()
        //        {
        //            GetEmpTbls = _db.EmpTbls.ToList(),
        //        };
        //        return View(HrStaffList);
        //        // return View();
        //    }
        //}
        //[Authorize(Roles = SD.AdminEndUser)]
        //public IActionResult StaffProfile(string id)
        //{
        //    {

        //        HrCllection HrempDetail = new HrCllection()
        //        {
        //            GetEmDegrees = _db.EmDegrees.Where(x => x.EmpEmail == HttpContext.Session.GetString("LogSession")).ToList(),
        //            // NewUserr = _db.NewUsers.SingleOrDefault(x => x.EmpId == id),
        //            GetEmpTbls = _db.EmpTbls.Where(m => m.Email == id).ToList(),
        //            GetEmpPros = _db.empPros.Where(x => x.EmpEmail == id).ToList(),
        //            GetWorks = _db.EmpExp.Where(x => x.EmpEmail == id).ToList(),
        //            GetEmpChildren = _db.EmpChildren.Where(x => x.EmpEmail == id).ToList(),
        //        };
        //        return View(HrempDetail);
        //    }
        //}

       // [HttpPost]
       //// [Authorize(Roles = SD.AdminEndUser)]
       // //  [ValidateAntiForgeryToken]
       // public async Task<IActionResult> StaffProfile(HrCllection HrmAuth, string MyEmail, string returnUrl = null)
       // {

       //     try
       //     {
       //         string Passm = HRM.UTL.UtilityCls.GenerateRandomPassword();




       //         HrmAuth.EmpTbls = await _db.EmpTbls.FirstOrDefaultAsync(x => x.Email == MyEmail);


       //         var user = new ApplicationUser
       //         {
       //             UserName = HrmAuth.EmpTbls.Email,
       //             Email = HrmAuth.EmpTbls.Email,
       //             PasswordHash = Passm,
       //             FirstName = HrmAuth.EmpTbls.Firstname,
       //             LastName = HrmAuth.EmpTbls.LastName,
       //             PhoneNumber = HrmAuth.EmpTbls.Phone,

       //         };

       //         var result = await _userManager.CreateAsync(user);
       //         if (result.Succeeded)
       //         {

       //             if (!await _roleManager.RoleExistsAsync(SD.CustomerEndUser))
       //             {
       //                 await _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser));

       //                 var resultAdmin = await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);

       //                 var claim = await _userManager.AddClaimAsync(user, new Claim("IsAdmin", "true"));
       //             }


       //             if (!await _roleManager.RoleExistsAsync(SD.CustmerHOU))
       //             {
       //                 await _roleManager.CreateAsync(new IdentityRole(SD.CustmerHOU));
       //             }

       //             if (!await _roleManager.RoleExistsAsync(SD.CustmerHOS))
       //             {
       //                 await _roleManager.CreateAsync(new IdentityRole(SD.CustmerHOS));
       //             }

       //             if (!await _roleManager.RoleExistsAsync(SD.CustmerHOD))
       //             {
       //                 await _roleManager.CreateAsync(new IdentityRole(SD.CustmerHOD));
       //             }

       //             string description = "Good name";
       //             string body = string.Empty;
       //             var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
       //             var callbackUrl = Url.EmailConfirmationLink(HrmAuth.EmpTbls.Email, code, Request.Scheme);
       //             var webRoot = _hostingEnv.WebRootPath;
       //             var file = System.IO.Path.Combine(webRoot, "profile.html");
       //             using (StreamReader reader = new StreamReader(file))
       //             {
       //                 body = reader.ReadToEnd();
       //             }

       //             body = body.Replace("{UserName}", user.UserName);
       //             body = body.Replace("{Title}", "");
       //             body = body.Replace("{Email}", user.Email);
       //             body = body.Replace("{Pass}", Passm);
       //             body = body.Replace("{callbackUrl}", callbackUrl);


       //             var ZSubjet = "Good";
       //             //  UtilityCls sender = new UtilityCls();
       //             // sender.SendHtmlFormattedEmail(HrmAuth.EmpTbls.Email, ZSubjet, body);

       //             HrCllection HrempDetail = new HrCllection()
       //             {
       //                 GetEmDegrees = _db.EmDegrees.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 // NewUserr = _db.NewUsers.SingleOrDefault(x => x.EmpId == HrmAuth.EmpTbls.EmpId),
       //                 GetEmpTbls = _db.EmpTbls.Where(m => m.EmpId == HrmAuth.EmpTbls.EmpId).ToList(),
       //                 GetEmpPros = _db.empPros.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 GetWorks = _db.EmpExp.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 GetEmpChildren = _db.EmpChildren.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),

       //             };

       //             ViewBag.Suceed = "The User has been Created Sussfully, and initial Password send to user email";
       //             return View(HrempDetail);

       //         }


       //         else
       //         {
       //             HrCllection HrempDetail = new HrCllection()
       //             {
       //                 GetEmDegrees = _db.EmDegrees.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 // NewUserr = _db.NewUsers.SingleOrDefault(x => x.EmpId == HrmAuth.EmpTbls.EmpId),
       //                 GetEmpTbls = _db.EmpTbls.Where(m => m.EmpId == HrmAuth.EmpTbls.EmpId).ToList(),
       //                 GetEmpPros = _db.empPros.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 GetWorks = _db.EmpExp.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),
       //                 GetEmpChildren = _db.EmpChildren.Where(x => x.EmpEmail == HrmAuth.EmpTbls.Email).ToList(),

       //             };

       //             ViewBag.Suceed = "The User has already been profile, please request for password reset";
       //             return View(HrempDetail);
       //         }




       //     }
       //     catch (Exception Ex)
       //     {

       //     }


       //     return View();
       // }

       [HttpGet]

        public IActionResult ChangePassword(string userid, string email, string token, string pass)
        {

            ViewBag.Message = "Email confirmed successfully!";
            ViewBag.Email = TempData["Email"]?.ToString();
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(HrCllection NewPass, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser User = new ApplicationUser();
                    var user = await _userManager.FindByEmailAsync(NewPass.changePasswordViewModel.Email);
                    if (user == null)
                    {
                        return RedirectToAction("");
                    }

                    else
                    {
                        NewPass.changePasswordViewModel.OldPassword = "Password2$";
                        var result = await _userManager.ChangePasswordAsync(user, NewPass.changePasswordViewModel.OldPassword, NewPass.changePasswordViewModel.NewPassword);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            TempData["Email"] = email;
                            return RedirectToAction("ChangePassword", "account");
                        }

                        else
                        {
                            var usermail = await _userManager.FindByIdAsync(user.Id);
                            if (usermail != null)
                            {
                                // var firm = await _userManager.ConfirmEmailAsync(user, NewPass.MyPass.Token);
                                return RedirectToAction("Login", "account");
                            }

                            else
                            {
                                ViewBag.Error = $"The userID {NewPass.MyPass.UserId} is invalid";
                                return View("Not Found");
                            }


                        }
                    }

                    // Don't reveal that the user does not exist
                    // return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                //else
                //{
                //    var user = await _userManager.FindByEmailAsync(User);
                //    if (user == null)
                //    {

                //    }

                //    else
                //    {

                //        var newPassword = _userManager.PasswordHasher.HashPassword(user, User.Password);
                //        user.PasswordHash = newPassword;
                //        var res = await _userManager.UpdateAsync(user);
                //        //var CurrPassCrpt = UTL.UtilityCls.EncodeBase64(ResetChange.CurrentPassword, "");
                //        // byte[] byt = System.Text.Encoding.UTF8.GetBytes(ResetChange.CurrentPassword);
                //        //byte[] bytm = System.Text.Encoding.UTF8.GetBytes(ResetChange.CurrentPassword);

                //        // var ConfPassCrypt = UTL.UtilityCls.FunForEncrypt(ResetChange.ConfirmPassword, "");
                //        // var result = await _userManager.ChangePasswordAsync(user, "", "");
                //        if (res.Succeeded)
                //        {
                //            return RedirectToAction(nameof(ResetPasswordConfirmation));
                //        }

                //        else
                //        {

                //        }

                //    }


                //}




            }

            catch (Exception Ex)
            {

            }

            return View();
        }




        [HttpGet]
        // [Authorize(Policy = "IsAdminClaimAccess")]
        public IActionResult TestMethod1()
        {
            return View("MyPage");
        }

        [HttpGet]
        //[Authorize(Policy = "IsAdminClaimAccess")]
        //[Authorize(Policy = "NonAdminAccess")]
        public IActionResult TestMethod2()
        {
            return View("MyPage");
        }

        public IActionResult MyPage()
        {
            return View();
        }

        public IActionResult ChangeUserPassword()
        {
            ViewBag.User = HttpContext.Session.GetString("LogSession");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ChangeUserPasswordAsync(HrCllection NewPass, string email)
        {
            ApplicationUser User = new ApplicationUser();
            var user = await _userManager.FindByEmailAsync(NewPass.changePasswordViewModel.Email);
            var result = await _userManager.ChangePasswordAsync(user, NewPass.changePasswordViewModel.OldPassword, NewPass.changePasswordViewModel.NewPassword);
            if (result.Succeeded)
            {
                TempData["Success"] = "Password Changed Successfully";
                return RedirectToAction("Success", "Account");
            }
            else
            {
                ViewBag.Error = "Password Fail";
                return View(NewPass);
            }
        }
        public async Task<IActionResult> Success()
        {
            string Message = TempData["Success"].ToString();
            ViewBag.Message = Message;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {

                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    var loginUser = await _userManager.FindByEmailAsync(model.Email);
                    if (loginUser == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username/password.");
                        return View();
                    }
                    if (result.Succeeded == true)
                    {

                        HttpContext.Session.SetString("LogSession", model.Email);
                        try
                        {
                            HttpContext.Session.SetString("FirstName", loginUser.FirstName);
                        }
                        catch
                        {

                        }
                        try
                        {
                            HttpContext.Session.SetString("LastName", loginUser.LastName);
                        }
                        catch
                        {

                        }
                        try
                        {
                            HttpContext.Session.SetString("OtherName", loginUser.OtherName);
                        }
                        catch
                        {

                        }


                      
                        return RedirectToAction("Index", "Home");
                    }

                    else if (result.Succeeded == false)
                    {
                        ViewBag.Error = "Password fail";
                        TempData["Invalid"] = "Invalid user or password";
                        return View(model);
                    }


                    else
                    {
                        ViewBag.Error = "Password fail";
                        TempData["Invalid"] = "Invalid user or password";
                        return RedirectToAction("Login", "Account");
                        // ModelState.AddModelError(nameof(model.Email), "Invalid user or password");
                    }

                    // If we got this far, something failed, redisplay form
                    //return View(model);
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View(model);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(HrCllection model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                model.RegisterViewModel.Password = "Password2$";

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.RegisterViewModel.Email, Email = model.RegisterViewModel.Email };
                    var result = await _userManager.CreateAsync(user, model.RegisterViewModel.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.EmailConfirmationLink(model.RegisterViewModel.Email, code, Request.Scheme);
                        //await _emailSender.SendEmailConfirmationAsync(model.RegisterViewModel.Email, callbackUrl);
                        string Fullname = user.FirstName + "" + user.LastName;
                        //var pathToFile = _hostingEnvironment.WebRootPath
                        //        + Path.DirectorySeparatorChar.ToString()
                        //        + "Templates"
                        //        + Path.DirectorySeparatorChar.ToString()
                        //        + "EmailTemplate"
                        //        + Path.DirectorySeparatorChar.ToString()
                        //        + "Confirm_Account_Registration.html";
                        var subject = "Confirm Account Registration";
                        string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                        string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = confirmationToken, email = model.RegisterViewModel.Email }, protocol: HttpContext.Request.Scheme);
                        //  string Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";

                        //var builder = new BodyBuilder();
                        //using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                        //{
                        //    builder.HtmlBody = SourceReader.ReadToEnd();
                        //}

                        // string messageBody = string.Format("{0}/{1}/{2}/{3}/{4}/{5}",
                        //builder.HtmlBody,

                        // subject,
                        // model.RegisterViewModel.Email,
                        // model.RegisterViewModel.Email,
                        // model.RegisterViewModel.Password,
                        // Message,
                        // callbackUrl

                        //);
                        await _emailSender.SendEmailAsync(model.RegisterViewModel.Email, subject, confirmationLink);
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created a new account with password.");
                        // return RedirectToLocal(returnUrl);
                        return RedirectToAction("Index", "HRM");
                    }
                    AddErrors(result);
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }

            // If we got this far, something failed, redisplay form

        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("LogSession");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        //  [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (userid == null || token == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = _userManager.FindByIdAsync(userid).Result;
            IdentityResult result = _userManager.ConfirmEmailAsync(user, token).Result;
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{user.Email}'.");
            }
            // var result = await _userManager.ConfirmEmailAsync(user, code);

            //return View(result.Succeeded ? "ChangePassword" : "Error");
            //return RedirectToAction("ChangePassword", "account", new { email =user.Email });
            TempData["Email"] = user.UserName;
            return RedirectToAction("ChangePassword", "account", new { email = user.Email });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = code, email = user.Email }, protocol: HttpContext.Request.Scheme);
                //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                // var callbackUrl = Url.Action("ResetPassword", "Account", user.Id, code, Request.Scheme);
                string confirmationLink = Url.Action("ResetPassword", "Account", new { userid = user.Id, token = code, email = user.Email }, protocol: HttpContext.Request.Scheme);
                string subject = "Reset Password";
                var webRoot = _hostingEnv.WebRootPath; //get wwwroot Folder  

                //Get TemplateFile located at wwwroot/Templates/EmailTemplate/Register_EmailTemplate.html  
                var pathToFile = _hostingEnv.WebRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + "Template"
                        + Path.DirectorySeparatorChar.ToString()
                        + "EmailTemplate"
                        + Path.DirectorySeparatorChar.ToString()
                        + "ActivationLink.html";
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                string messageBody = string.Format(builder.HtmlBody,
                     subject,
                     String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                     model.Email,
                     "",
                    "",
                    "",
                    confirmationLink
                    );
                await _emailSender.SendEmailAsync(model.Email, subject, messageBody);
                // await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //  $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> ResetStaffPassword(string id)
        {
            var UserMail = await _userManager.FindByIdAsync(id);
            ViewBag.UserMail = UserMail.Email;
            return View();
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> ResetStaffPassword(ApplicationUser user)
        {
            try
            {
                var UserMail = await _userManager.FindByEmailAsync(user.Email);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);
                var task = _userManager.ResetPasswordAsync(user, code, "Password2$");
                // var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                var webRoot = _hostingEnv.WebRootPath; //get wwwroot Folder  
                string subject = "Password Reset";
                var pathToFile = _hostingEnv.WebRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + "Template"
                        + Path.DirectorySeparatorChar.ToString()
                        + "EmailTemplate"
                        + Path.DirectorySeparatorChar.ToString()
                        + "ActivationLink.html";
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                string messageBody = string.Format(builder.HtmlBody,
                     subject,
                     String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                     user.FirstName + " " + user.LastName,
                     "",
                    "",
                    "",
                    callbackUrl
                    ); ;

                await _emailSender.SendEmailAsync(user.Email, subject, messageBody);
                return RedirectToAction("Users", "RoleAdmin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
                return View(user);
            }
        }
        private string GeneratePasswordResetToken(ApplicationUser user)
        {
            var task = _userManager.GeneratePasswordResetTokenAsync(user);
            task.Wait();
            var token = task.Result;
            return token;
        }
        private void ChangeUserPassword(ApplicationUser user, string newPassword)
        {
            var token = GeneratePasswordResetToken(user);
            var task = _userManager.ResetPasswordAsync(user, token, newPassword);
            task.Wait();
            var result = task.Result;
        }



        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
