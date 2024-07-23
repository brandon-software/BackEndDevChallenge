using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BackEndDevChallenge.Models;
using Microsoft.Extensions.Logging;

namespace BackEndDevChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiReportController : ControllerBase
    {

        private readonly CalculatorContext _context;
        private readonly ILogger<CalculatorController> _logger;

        public ApiReportController(ILogger<CalculatorController> logger, CalculatorContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            try
            {
                var mathProblems = await _context.MathProblems.AsNoTracking().ToListAsync();

                var usersReport = mathProblems
                    .GroupBy(mp => mp.Username)
                    .Select(g => new
                    {
                        Username = g.Key,
                        MathProblemsAttempted = g.Count(),
                        ErrorsCaused = g.Count(mp => mp.ResultType != MathResultType.Ok),
                        resultTypes = g.GroupBy(mp => mp.ResultType)
                            .ToDictionary(g => g.Key.ToString(), g => g.Count())
                    })
                    .ToList();

                var resultTypes = mathProblems
                    .Where(mp => mp.ResultType != MathResultType.Ok)
                    .GroupBy(mp => mp.ResultType)
                    .ToDictionary(g => g.Key.ToString(), g => g.Count());

                var mostCommonAnswer = mathProblems
                    .Where(mp => mp.ResultType == MathResultType.Ok)
                    .GroupBy(mp => mp.Result)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key.ToString())
                    .FirstOrDefault();

                var report = new
                {
                    UsersReport = usersReport,
                    ResultTypes = resultTypes,
                    MostCommonAnswer = mostCommonAnswer
                };
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the report.");
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }
    }
}