namespace FakerLib.Generators
{
    public class BoolGenerator : IValueGenerator
    {
        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return true;
        }
    }
}
