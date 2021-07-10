using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asset.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Asset.Web.Data;
using OurHr.Models;
using Asset.Web.Services;
using Asset.Web.ViewModels;
using Asset.Web.Controllers;

namespace OurHr.Controllers
{
  //  [Authorize("Authorization")]
    public class SuperAdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly ILogger _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSenderm _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnv;

        private ApplicationUser testUser = new ApplicationUser
        {
            UserName = "TestTestForPassword",
            Email = "testForPassword@test.test"
        };

        public SuperAdminController(UserManager<ApplicationUser> userMgr,
            IUserValidator<ApplicationUser> userValid, IPasswordValidator<ApplicationUser> passValid,
            IPasswordHasher<ApplicationUser> passHasher, ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager,
            IEmailSenderm emailSender,  RoleManager<IdentityRole> roleManager,

           ApplicationDbContext db,
           IHostingEnvironment hostingEnv)
        {
            userManager = userMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passHasher;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
            _hostingEnv = hostingEnv;
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            return View(userManager.Users);
        }

        public async Task<ViewResult> Create()
        {
            var roleViewModel = new List<RoleViewModel>();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var item in roles)
            {
                roleViewModel.Add(new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVm createVm)
        {
            if (ModelState.IsValid)
            {
                createVm.Password = "Password2$";
                User user = new User
                {
                    UserName = createVm.Email,
                    Email = createVm.Email,
                   
                };

                IdentityResult result = await userManager.CreateAsync(user, createVm.Password);
                
                if (result.Succeeded)
                {
                    ActivationMail activationMail = new ActivationMail();
                  // var MyRoleList = RoleList();
                    var result1 = await userManager.AddToRoleAsync(user, "NEW USER");
                    var subject = "Confirm Account Registration";
                    string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = confirmationToken, email = user.Email }, protocol: HttpContext.Request.Scheme);

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
                         createVm.Name,
                         "",
                        "",
                        "",
                        confirmationLink
                        );
                    // {0} : confirmationLink
                    //{1} : Current DateTime  
                    //{2} : Email  
                    //{3} : Username  
                    //{4} : Password  
                    //{5} : Message  
                    //{6} : callbackURL 
                    // var subject = "Confirm Account Registration";

                    var getLastMail = createVm.Email;
                    //_db.ActivationMail.Remove(getLastMail);
                    createVm.Email = "Weixight1@yahoo.com";
                    await _emailSender.SendEmailAsync(createVm.Email, subject, messageBody);
                    activationMail.Email = getLastMail;
                    activationMail.Message = messageBody;
                    activationMail.UserId = user.Id;
                    activationMail.MessageDate = System.DateTime.Now;

                    _db.ActivationMail.Add(activationMail);
                    _db.SaveChanges();
                    

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Users", "RoleAdmin");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(createVm);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.TryAddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View("Index", userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // the names of its parameters must be the same as the property of the User class if we use asp-for in the view
        // otherwise form values won't be passed properly
        public async Task<IActionResult> Edit(string id, string userName, string email, string password)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                // Validate UserName and Email 
                user.UserName = userName; // UserName won't be changed in the database until UpdateAsync is executed successfully
                user.Email = email;
                IdentityResult validUseResult = await userValidator.ValidateAsync(userManager, user);
                if (!validUseResult.Succeeded)
                {
                    AddErrors(validUseResult);
                }

                // Validate password
                // Step 1: using built in validations
                IdentityResult passwordResult = await userManager.CreateAsync(testUser, password);
                if (passwordResult.Succeeded)
                {
                    await userManager.DeleteAsync(testUser);
                }
                else
                {
                    AddErrors(passwordResult);
                }
                /* Step 2: Because of DI, IPasswordValidator<User> is injected into the custom password validator. 
                   So the built in password validation stop working here */
                IdentityResult validPasswordResult = await passwordValidator.ValidateAsync(userManager, user, password);
                if (validPasswordResult.Succeeded)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                }
                else
                {
                    AddErrors(validPasswordResult);
                }

                // Update user info
                if (validUseResult.Succeeded && passwordResult.Succeeded && validPasswordResult.Succeeded)
                {
                    // UpdateAsync validates user info such as UserName and Email except password since it's been hashed 
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            ;

            return View(user);
        }

        private List<RoleViewModel> RoleList()
        {

            var roleViewModel = new List<RoleViewModel>();
            var roles = _roleManager.Roles.ToList();
            foreach (var item in roles)
            {
                roleViewModel.Add(new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            //ViewBag.Roles = roles;
            return roleViewModel;
        }
    }
}
