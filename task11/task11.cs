using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11;

public interface ICalculator
{
    int Add(int a, int b);
    int Minus(int a, int b);
    int Mul(int a, int b);
    int Div(int a, int b);
}

public class CalculatorBuilder
{
    public static ICalculator CreateCalculator(string code_to_compile)
    {
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
        };

        var compile = CSharpCompilation
            .Create
            (
                "Assembly",
                new[] { CSharpSyntaxTree.ParseText(code_to_compile) },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

        var memory_stream = new MemoryStream();
        var result = compile.Emit(memory_stream);

        if (!result.Success)
        {
            throw new Exception("Не удалось скомпилировать.");
        }

        memory_stream.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(memory_stream.ToArray());
        var calculator_type = assembly.GetType("Calculator");

        if (calculator_type == null)
            throw new Exception("Calculator не найден в сборке.");

        var member = Activator.CreateInstance(calculator_type);

        if (member == null)
            throw new Exception("Объект Calculator создать не получилось.");

        return (ICalculator)member;
        }
    }
