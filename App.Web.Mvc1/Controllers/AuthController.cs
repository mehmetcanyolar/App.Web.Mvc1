using App.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.Web.Mvc1.Controllers
{
	public class AuthController : Controller
	{
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var kullanici = await _context.Users.FirstOrDefaultAsync(k => k.Email == email && k.Password == password);
                if (kullanici != null)
                {
                    var kullaniciHaklari = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, kullanici.Name),
                        //new Claim("Role", kullanici.IsAdmin ? "Admin" : "User"),
                        new Claim("UserId", kullanici.Id.ToString())
                    };
                    var kullaniciKimligi = new ClaimsIdentity(kullaniciHaklari, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new(kullaniciKimligi);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Home/Index");
                }
                else TempData["Mesaj"] = "Giriş Başarısız!";
            }
            catch (Exception hata)
            {
                // hata.Message
                // todo : hatalar db ye loglanacak
                TempData["Mesaj"] = "Hata Oluştu!";
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
