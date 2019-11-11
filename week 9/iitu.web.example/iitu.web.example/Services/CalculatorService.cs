using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iitu.web.example.Services
{
    public class CalculatorService
    {
        public int Sum(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public double Divide(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0) throw new ArgumentException("Second number can't be 0");

            return firstNumber / secondNumber;
        }
    }
}
