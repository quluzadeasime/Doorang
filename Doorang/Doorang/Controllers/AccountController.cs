using Core.Models;
using Doorang.DTO_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Doorang.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser()
            {
                Name = registerDTO.FirstName,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName
            };
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login");

        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole rol1 = new IdentityRole("Admin");
            IdentityRole rol2 = new IdentityRole("Moderator");
            IdentityRole rol3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(rol1);
            await _roleManager.CreateAsync(rol2);
            await _roleManager.CreateAsync(rol3);

            return Ok();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPatch]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser;

            if (loginDTO.UsernameOrEmail.Contains("@"))
            {
                appUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            }
            else
            {
                appUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            }
            if (appUser == null)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sehvdir");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sehvdir!");
                return View();
            }
            await _signInManager.SignInAsync(appUser, loginDTO.RememberMe);



            return RedirectToAction("Index", "Dashboard");
        }
    }
}
