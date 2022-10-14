namespace FakerLib.Generators
{
    public class ByteGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(byte);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(1, byte.MaxValue);
        }
    }
}
