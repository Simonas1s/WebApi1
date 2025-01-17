using Microsoft.AspNetCore.Mvc;
using OrderingNumbers.Controllers;
using OrderingNumbers.Services;
using System.IO;
using Xunit;

namespace OrderingNumbers.Tests
{
    public class SortingControllerTests
    {
        private readonly SortingController _controller;
        private readonly string _filePath = "result.txt";

        public SortingControllerTests()
        {
            var sortingService = new Sorting();
            _controller = new SortingController(sortingService);
        }

        [Fact]
        public void QuickSort_InvalidInput_ReturnsBadRequest()
        {
            var input = new SortingController.NumbersInput { numbers = null };

            var result = _controller.QuickSort(input);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Input cannot be null or empty.", badRequestResult.Value);
            Console.WriteLine("QuickSort_InvalidInput_ReturnsBadRequest: Test Passed");
        }

        [Fact]
        public void QuickSort_EmptyArray_ReturnsBadRequest()
        {
            var input = new SortingController.NumbersInput { numbers = new int[] { } };

            var result = _controller.QuickSort(input);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Input cannot be null or empty.", badRequestResult.Value);
            Console.WriteLine("QuickSort_EmptyArray_ReturnsBadRequest: Test Passed");
        }

        [Fact]
        public void QuickSort_ValidInput_ReturnsSortedNumbers()
        {
            var input = new SortingController.NumbersInput { numbers = new int[] { 3, -1, 2, 0, 5 } };

            var result = _controller.QuickSort(input);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            var sortedNumbers = Assert.IsAssignableFrom<int[]>(((dynamic)okResult.Value).sortedNumbers);
            Assert.Equal(new int[] { -1, 0, 2, 3, 5 }, sortedNumbers);
            Console.WriteLine("QuickSort_ValidInput_ReturnsSortedNumbers: Test Passed");
        }

        [Fact]
        public void QuickSort_NegativeAndPositiveNumbers_ReturnsSortedNumbers()
        {
            var input = new SortingController.NumbersInput { numbers = new int[] { -5, 3, -1, 2, 0, 5 } };

            var result = _controller.QuickSort(input);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            var sortedNumbers = Assert.IsAssignableFrom<int[]>(((dynamic)okResult.Value).sortedNumbers);
            Assert.Equal(new int[] { -5, -1, 0, 2, 3, 5 }, sortedNumbers);
            Console.WriteLine("QuickSort_NegativeAndPositiveNumbers_ReturnsSortedNumbers: Test Passed");
        }
    }
}
