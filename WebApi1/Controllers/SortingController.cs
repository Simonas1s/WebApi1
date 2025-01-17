using Microsoft.AspNetCore.Mvc;
using OrderingNumbers.Services;
using System.IO;
using System.Linq;

namespace OrderingNumbers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SortingController : ControllerBase
    {
        private readonly Sorting _sortingService;
        private const string FilePath = "result.txt";

        public SortingController(Sorting sortingService)
        {
            _sortingService = sortingService;
        }

        [HttpPost("quicksort")]
        public IActionResult QuickSort([FromBody] NumbersInput input)
        {
            if (input == null || input.numbers == null || input.numbers.Length == 0)
            {
                return BadRequest("Input cannot be null or empty.");
            }
            var sortedNumbers = _sortingService.QuickSort(input.numbers);
            SaveToFile(sortedNumbers);

            return Ok(new { sortedNumbers });
        }

        [HttpGet("latest")]
        public IActionResult GetLatestFile()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("No saved file found.");
            }

            var num = System.IO.File.ReadAllText(FilePath);
            return Ok(new { num });
        }

        private void SaveToFile(int[] numbers)
        {
            var num = string.Join(" ", numbers);
            System.IO.File.WriteAllText(FilePath, num);
        }

        public class NumbersInput
        {
            public int[] numbers { get; set; }
        }
    }
}
