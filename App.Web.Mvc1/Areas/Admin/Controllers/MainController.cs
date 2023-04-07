﻿using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
