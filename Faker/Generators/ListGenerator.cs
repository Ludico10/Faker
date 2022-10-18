using System.Collections;

namespace FakerLib.Generators
{
    public class ListGenerator : IValueGenerator
    {
        private int limit = 10;

        public void ChangeLimit(int Lim)
        {
            limit = Lim;
        }

        bool IValueGenerator.CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        object IValueGenerator.Generate(Type typeToGenerate, GeneratorContext context)
        {
            int len = context.Random.Next(1, limit);
            IList? list = (IList?)Activator.CreateInstance(typeToGenerate, len);
            if (list != null)
            {
                for (int i = 0; i < len; i++)
                    list.Add(context.Faker.Create(typeToGenerate.GetGenericArguments()[0]));
                return list;
            }
            else throw new FakerException($"Unable to create list of type { typeToGenerate }");
        }
    }
}
