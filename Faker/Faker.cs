using FakerLib.Generators;

namespace FakerLib
{
    public class Faker
    {
        private readonly List<IValueGenerator> generators;
        private GeneratorContext generatorContext;

        Faker()
        {
            generators = GetGenerators();
            generatorContext = new GeneratorContext(new Random(), this);
        }

        private List<IValueGenerator> GetGenerators()
        {
            return new List<IValueGenerator>() {
                new BoolGenerator(),
                new IntGenerator(),
                new StringGenerator(),
                new DateTimeGenerator(),
                new ByteGenerator(),
                new CharGenerator(),
                new DecimalGenerator(),
                new DoubleGenerator(),
                new FloatGenerator(),
                new ListGenerator(),
                new LongGenerator(),
                new ShortGenerator() };
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        // Может быть вызван изнутри Faker, из IValueGenerator (см. ниже) или пользователем
        public object Create(Type t)
        {
            foreach (var generator in generators)
            {
                if (generator.CanGenerate(t))
                {
                    return generator.Generate(t, generatorContext);    
                }
            }
            throw new Exception($"Can not generate for type {t.Name}");
        }

        private static object? GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                // Для типов-значений вызов конструктора по умолчанию даст default(T).
                return Activator.CreateInstance(t);
            else
                // Для ссылочных типов значение по умолчанию всегда null.
                return null;
        }
    }
}