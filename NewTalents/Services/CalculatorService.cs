namespace NewTalents.Services;

public class CalculatorService
{
    private readonly List<Operation> _lastOperations = [];

    public int Sum(int x, int y)
    {
        var z = x + y;
        SaveOperation(Operation.Summation(x, y, z));
        return z;
    }

    public int Subtract(int x, int y)
    {
        var z = x - y;
        SaveOperation(Operation.Subtraction(x, y, z));
        return z;
    }

    public int Multiply(int x, int y)
    {
        var z = x * y;
        SaveOperation(Operation.Multiplication(x, y, z));
        return z;
    }

    public int Divide(int x, int y)
    {
        var z = x / y;
        SaveOperation(Operation.Division(x, y, z));
        return z;
    }

    private void SaveOperation(Operation operation)
    {
        if (_lastOperations.Count == 3)
        {
            _lastOperations.RemoveAt(2);
        }
        _lastOperations.Insert(0, operation);
    }

    public List<string> LastOperations()
    {
        return _lastOperations.Select(x => x.Format()).ToList();
    }
}

public readonly struct Operation
{
    public OperationType Type { get; init; }
    public int Lhs { get; init; }
    public int Rhs { get; init; }
    public int Result { get; init; }

    public string Format()
    {
        return Type switch
        {
            OperationType.Summation => $"{Lhs} + {Rhs} = {Result}",
            OperationType.Subtraction => $"{Lhs} - {Rhs} = {Result}",
            OperationType.Multiplication => $"{Lhs} * {Rhs} = {Result}",
            OperationType.Division => $"{Lhs} / {Rhs} = {Result}",
        };
    }

    public static Operation Summation(int lhs, int rhs, int result)
        => Create(OperationType.Summation, lhs, rhs, result);

    public static Operation Subtraction(int lhs, int rhs, int result)
        => Create(OperationType.Subtraction, lhs, rhs, result);

    public static Operation Multiplication(int lhs, int rhs, int result)
        => Create(OperationType.Multiplication, lhs, rhs, result);

    public static Operation Division(int lhs, int rhs, int result)
        => Create(OperationType.Division, lhs, rhs, result);

    public static Operation Create(OperationType type, int lhs, int rhs, int result)
    {
        return new Operation
        {
            Type = type,
            Lhs = lhs,
            Rhs = rhs,
            Result = result,
        };
    }
}

public enum OperationType
{
    Summation,
    Subtraction,
    Multiplication,
    Division,
}
