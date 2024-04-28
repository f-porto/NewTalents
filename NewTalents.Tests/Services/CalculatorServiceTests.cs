using NewTalents.Services;

namespace NewTalents.Tests.Services;

public class CalculatorServiceTests
{
    private static CalculatorService CreateService()
    {
        return new CalculatorService();
    }

    [Theory]
    [InlineData(4, 7, 11)]
    [InlineData(9, 34, 43)]
    [InlineData(0, 69, 69)]
    public void Sum_Given2Numbers_ReturnTheirSummation(int x, int y, int expected)
    {
        var service = CreateService();

        var z = service.Sum(x, y);

        Assert.Equal(expected, z);
    }

    [Theory]
    [InlineData(4, 7, -3)]
    [InlineData(52, 34, 18)]
    [InlineData(420, 0, 420)]
    public void Subtract_Given2Numbers_ReturnTheirSubtraction(int x, int y, int expected)
    {
        var service = CreateService();

        var z = service.Subtract(x, y);

        Assert.Equal(expected, z);
    }

    [Theory]
    [InlineData(4, 7, 28)]
    [InlineData(5, 9, 45)]
    [InlineData(1, 69, 69)]
    public void Multiply_Given2Numbers_ReturnTheirMultiplication(int x, int y, int expected)
    {
        var service = CreateService();

        var z = service.Multiply(x, y);

        Assert.Equal(expected, z);
    }

    [Theory]
    [InlineData(7, 4, 1)]
    [InlineData(95, 5, 19)]
    [InlineData(1337, 1, 1337)]
    public void Divide_Given2Numbers_ReturnTheirDivision(int x, int y, int expected)
    {
        var service = CreateService();

        var z = service.Divide(x, y);

        Assert.Equal(expected, z);
    }

    [Fact]
    public void Divide_Given0Divisor_ShouldThrowException()
    {
        var service = CreateService();
        Assert.Throws<DivideByZeroException>(() => service.Divide(1, 0));
    }

    [Fact]
    public void History_GetHistory_ReturnsHistory()
    {
        var service = CreateService();

        var sum = service.Sum(10, 4);
        var mul = service.Multiply(34, 1);
        var div = service.Divide(12, 3);
        var sub = service.Subtract(33, 99);

        var history = service.LastOperations();

        List<string> expected =
        [
            Operation.Subtraction(33, 99, sub).Format(),
            Operation.Division(12, 3, div).Format(),
            Operation.Multiplication(34, 1, mul).Format(),
        ];

        Assert.Equal(expected, history);
    }
}
