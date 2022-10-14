namespace FakerLib.Generators
{
    public class LongGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextInt64(1, long.MaxValue);
        }
    }
}
