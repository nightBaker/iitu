using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iitu.web.example.Controllers
{
    public class HelloController : Controller
    {        
        public IActionResult Index()
        {

            if (ModelState.IsValid)
            {
                //Do some works
            }

            return View();
        }
    }

}