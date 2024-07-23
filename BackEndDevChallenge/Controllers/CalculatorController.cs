using BackEndDevChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndDevChallenge.Controllers
{
    internal class TruncatedException : Exception
    {
        public TruncatedException()
        {
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly CalculatorContext _context;

        public CalculatorController(ILogger<CalculatorController> logger, CalculatorContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Add")]
        public async Task<ActionResult> Add(int input1, int input2, string? username = null)
        {
            return await PerformMathOperation(input1, input2, MathOperationType.Addition, username);
        }

        [HttpGet("Subtract")]
        public async Task<ActionResult> Subtract(int input1, int input2, string? username = null)
        {
            return await PerformMathOperation(input1, input2, MathOperationType.Subtraction, username);
        }

        [HttpGet("Multiply")]
        public async Task<ActionResult> Multiply(int input1, int input2, string? username = null)
        {
            return await PerformMathOperation(input1, input2, MathOperationType.Multiplication, username);
        }

        [HttpGet("Divide")]
        public async Task<ActionResult> Divide(int input1, int input2, string? username = null)
        {
            return await PerformMathOperation(input1, input2, MathOperationType.Division, username);
        }

        private async Task<ActionResult> PerformMathOperation(int input1, int input2, MathOperationType operationType, string? username)
        {
            try
            {
                checked
                {
                    int result = 0;
                    switch (operationType)
                    {
                        case MathOperationType.Addition:
                            result = input1 + input2;
                            break;
                        case MathOperationType.Subtraction:
                            result = input1 - input2;
                            break;
                        case MathOperationType.Multiplication:
                            result = input1 * input2;
                            break;
                        case MathOperationType.Division:
                            result = input1 / input2;
                            int remainder = input1 % input2;
                            if (remainder != 0)
                            {
                                throw new TruncatedException();
                            }
                            break;
                    }

                    await SaveMathProblemAsync(input1, input2, result, operationType, MathResultType.Ok, username);
                    return Ok(result);
                }
            }
            catch (TruncatedException)
            {
                await SaveMathProblemAsync(input1, input2, null, operationType, MathResultType.Truncation, username);
                return BadRequest("Division truncated.");
            }
            catch (DivideByZeroException)
            {
                await SaveMathProblemAsync(input1, input2, null, operationType, MathResultType.DivideByZero, username);
                return BadRequest("Cannot divide by zero.");
            }
            catch (OverflowException)
            {
                await SaveMathProblemAsync(input1, input2, null, operationType, MathResultType.Overflow, username);
                return BadRequest("Result is an overflow.");
            }
            catch (Exception ex)
            {
                await SaveMathProblemAsync(input1, input2, null, operationType, MathResultType.Unknown, username);
                _logger.LogError(ex, "An error occurred while performing the math operation.");
                return StatusCode(500, "An error occurred while performing the math operation.");
            }
        }
        private async Task SaveMathProblemAsync(int input1, int input2, int? result, MathOperationType operationType, MathResultType resultType, string? username)
        {
            try
            {
                var mathProblem = new MathProblem
                {
                    Username = username ?? "Legacy",
                    Input1 = input1,
                    Input2 = input2,
                    Result = result,
                    OperationType = operationType,
                    ResultType = resultType
                };

                await _context.MathProblems.AddAsync(mathProblem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the math problem.");
                throw; // rethrow the exception to be handled by the caller
            }
        }
    }
}
