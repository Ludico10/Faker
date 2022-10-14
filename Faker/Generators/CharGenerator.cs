using System.Text;

namespace FakerLib.Generators
{
    public class CharGenerator : IValueGenerator
    {
        private char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(char);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            return chars[context.Random.Next(chars.Length)];
        }
    }
}
