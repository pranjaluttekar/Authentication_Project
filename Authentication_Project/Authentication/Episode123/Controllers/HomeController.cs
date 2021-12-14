using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        [Authorize(Policy ="Claim.DoB") ]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }


        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()

            {
                new Claim(ClaimTypes.Name, "BDAT"),
                new Claim(ClaimTypes.Email, "Student.georgian@gmail.com"),
                new Claim(ClaimTypes.DateOfBirth, "12/06/2000"),
                new Claim(ClaimTypes.Role,"Admin"),
                 new Claim("Grandma.Says", "Nice Boy!"),
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Kushal S"),
                new Claim("DrivingLicense", "A"),
            };
            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "GrandmaIdentity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");



            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });


            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index");
        }
    }
}