namespace BackEndDevChallenge.Models
{
    public class MathProblem
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int? Result { get; set; }
        public MathOperationType OperationType { get; set; }
        public MathResultType ResultType { get; set; }

    }
    public enum MathOperationType
    {
        Addition,
        Subtraction,

        Multiplication,

        Division
    }

    public enum MathResultType
    {
        Ok,
        Truncation,
        DivideByZero,
        Overflow,
        Unknown
    }
}
