using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }
    }
}
