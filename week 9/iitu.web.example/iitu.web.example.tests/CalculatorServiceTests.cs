using iitu.web.example.Services;
using System;
using Xunit;

namespace iitu.web.example.tests
{
    public class CalculatorServiceTests
    {
        [Theory]
        [InlineData(1,2,3)]
        [InlineData(15, 33, 48)]
        public void SumTest(int firstNumber, int secondNumber, int expectedResult)
        {
            var calService = new CalculatorService();
            var resultSum = calService.Sum(firstNumber, secondNumber);
            Assert.Equal(expectedResult, resultSum);
        }

        [Fact]
        public void DevideTest()
        {
            var calService = new CalculatorService();
            var resultSum = calService.Divide(9, 3);
            Assert.Equal(3, resultSum);
        }

        [Fact]
        public void DevideExceptionTest()
        {
            var calService = new CalculatorService();
            Assert.Throws<ArgumentException>(() => calService.Divide(9, 0));
            
        }

    }
}
