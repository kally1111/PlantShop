using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlantShop.DataAccess;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly PlantShopDbContext db;


        public HomeController(PlantShopDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        [Authorize]
        public IActionResult SearchData()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Validate(string username, string password)
        {
            int count = db.Employees.Where(x => x.LastName == username).Where(x => x.Password == password).Count();
            if (count == 1)
            {
                string returnUr = "/Home/SearchData";
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUr);
            }
            // return StatusCode(StatusCodes.Status403Forbidden);
            TempData["Error"] = "User name or password is invalid";
            return View("login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
