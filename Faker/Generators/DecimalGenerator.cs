namespace FakerLib.Generators
{
    public class DecimalGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(decimal);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextSingle() + context.Random.NextInt64();
        }
    }
}
