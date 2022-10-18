namespace FakerLib.Generators
{
    public class ShortGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(short);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (short)context.Random.Next(1, short.MaxValue);
        }
    }
}
