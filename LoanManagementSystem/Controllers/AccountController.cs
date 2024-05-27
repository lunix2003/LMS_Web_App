using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LoanManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register(string? returnUrl=null)
        {
            returnUrl ??= Url.Content("/");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model,string? returnUrl=null)
        {
            returnUrl ??= Url.Content("/");
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
            
            if (!(await roleManager.RoleExistsAsync(model.RoleName)))
            {
                var role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var roleResult = await roleManager.CreateAsync(role);   
                if (!roleResult.Succeeded)
                {
                    foreach(var item in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);

                    }
                    return View("Register",model);
                }
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password!);
           
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            var identityResult = await userManager.AddToRoleAsync(user, model.RoleName);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View("Register", model);

            }
            await signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("/");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model,string? returnUrl= null)
        {
            returnUrl ??= Url.Content("/");
            ViewBag.ReturnUrl = returnUrl;
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "There is no user register with this email");
            }
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password!, model.RememberMe,lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
            
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Admin");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
