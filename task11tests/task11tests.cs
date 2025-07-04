using task11;

public class TestClass
{
    private ICalculator _calculator;

    public TestClass()
    {
        string code = @"
        using task11;

        public class Calculator : ICalculator
        {
            public int Add(int a, int b) => a + b;
            public int Minus(int a, int b) => a - b;
            public int Mul(int a, int b) => a * b;
            public int Div(int a, int b) 
            {
                if (b == 0)
                    throw new System.DivideByZeroException(""Нельзя делить на 0."");
                return a / b;   
            }
        }";

        _calculator = CalculatorBuilder.CreateCalculator(code);
    }

    [Fact]
    public void TestAdd()
    {
        Assert.Equal(20, _calculator.Add(10, 10));
    }

    [Fact]
    public void TestMinus()
    {
        Assert.Equal(17, _calculator.Minus(19, 2));
    }

    [Fact]
    public void TestMul()
    {
        Assert.Equal(240, _calculator.Mul(60, 4));
    }

    [Fact]
    public void TestDiv() 
    {
        Assert.Equal(7, _calculator.Div(28, 4));
    }

    [Fact]
    public void TestDivByZero()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Div(1, 0));
    }
}
