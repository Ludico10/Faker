using System.Text;

namespace FakerLib.Generators
{
    public class StringGenerator : IValueGenerator
    {
        private char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private int limit = 100;

        public void ChangeLimit(int Lim)
        {
            limit = Lim;
        }

        bool IValueGenerator.CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            int len = context.Random.Next(1, limit);
            var result = new StringBuilder(len);

            for (int i = 0; i < len; i++) 
                result.Append(chars[context.Random.Next(chars.Length)]);
            return result.ToString();
        }
    }
}
