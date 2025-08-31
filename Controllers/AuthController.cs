using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using form_task_arda.Data;
using form_task_arda.Models;
using form_task_arda.DTOs;
using Microsoft.AspNetCore.Identity;



namespace form_task_arda.Controllers
{
    public class AuthController : Controller
    {

        // Bu değişkenleri önceden bildirip saadece bir kez tanımlanacağını belirtiyorum //
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;


        // Özel objelerimi identity kütüphanesinden alıp kendi değişkenlerime eşitliyorum. //
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Form_Done()
        {
            return View();
        }

        // Register Formunun ekrana getirilmesini Sağlıyorum //
        [HttpGet]
        public IActionResult Register_Form()
        {
            return View();
        }

        // Register_Form'dan kullanıcı verilerini çekiyorum //
        [HttpPost]
        public async Task<IActionResult> Register_Form(RegisterFormDto myFormResult)
        {
            // Bilgiler Modeldeki kurallara uymazsa... //
            if (!ModelState.IsValid)
            {
                return View("Process_Failed");
            }

            // kullanıcıyı oluşturuyorum (şifresi hariç)
            var _user = new AppUser
            {
                FullName = myFormResult.FullName,
                UserName = myFormResult.UserName,
                Email = myFormResult.Email
            };

            // şimdi asenkron şekilde şifreyi parametre olarak yollayıp kullanıcıyı oluşturuyorum// 
            var _registerResult = await _userManager.CreateAsync(_user, myFormResult.Password);

            if (_registerResult.Succeeded)
            {
                return RedirectToAction("Form_Done");
            }

            foreach (var err in _registerResult.Errors)
                ModelState.AddModelError("--- HATA : ", err.Description);

            return View();
        }


        [HttpGet]
        public IActionResult Login_Form()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login_Form(LoginFormDto _formResult)
        {
            var _user = await _userManager.FindByEmailAsync(_formResult.Email);

            if (_user == null)
            {
                return Unauthorized("Böyle bir kullanıcı yok");
            }

            var myLoginProcess = await _signInManager.PasswordSignInAsync(_user, _formResult.Password, false, false);

            if (myLoginProcess.Succeeded)
            {
                return RedirectToAction("Form_Done");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction("Bye_Page");
            }
            catch
            {
                return RedirectToAction("Process_Failed");
            }
        }

        public IActionResult Process_Failed()
        {
            return View();
        }

        public IActionResult Bye_Page()
        {
            return View();
        }
    }
}
