namespace FakerLib.Generators
{
    public class DoubleGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextDouble() + context.Random.NextInt64();
        }
    }
}
