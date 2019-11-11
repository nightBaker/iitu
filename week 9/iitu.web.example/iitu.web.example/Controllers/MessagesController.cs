using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iitu.web.example.Controllers
{
    [Route("Say")]
    public class MessagesController : Controller
    {
        [Route("{**message}")]
        public IActionResult ShowMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                ViewData["Message"] = "Message is empty";
            }
            else
            {
                ViewData["Message"] = message;
            }
            
            return View();
        }
    }
}