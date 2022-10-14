namespace FakerLib.Generators
{
    public class FloatGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextSingle() + context.Random.NextInt64();
        }
    }
}
