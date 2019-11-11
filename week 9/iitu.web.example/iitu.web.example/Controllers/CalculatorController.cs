using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iitu.web.example.Services;
using Microsoft.AspNetCore.Mvc;

namespace iitu.web.example.Controllers
{
   
    [Route("[controller]/[action]")]
    public class CalculatorController : Controller
    {

        private CalculatorService _calcService;

        public CalculatorController(CalculatorService calcService)
        {
            _calcService = calcService;
        }

        [Route("{firstNumber:int}/{secondNumber:int}")]
        public IActionResult Sum(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '+';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = _calcService.Sum(firstNumber, secondNumber);

            return View("Result");
        }

        [Route("{firstNumber:int}/{secondNumber:int:min(1)}")]
        public IActionResult Divide(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '/';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = _calcService.Divide(firstNumber,secondNumber);

            return View("Result");
        }


    }
}