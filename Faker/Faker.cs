using FakerLib.Generators;

namespace FakerLib
{
    public class Faker : IFaker
    {
        private readonly List<IValueGenerator> generators;
        private GeneratorContext generatorContext;
        private ObjectCreator creator;

        public Faker()
        {
            generators = GetGenerators();
            generatorContext = new GeneratorContext(new Random(), this);
            creator = new ObjectCreator(this, generatorContext);
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

        public object Create(Type t)
        {
            foreach (var generator in generators)
            {
                if (generator.CanGenerate(t))
                {
                    return generator.Generate(t, generatorContext);    
                }
            }
            Object? obj = creator.Generate(t);
            if (obj != null) return obj;

            throw new FakerException($"Can not generate for type {t.Name}");
        }
    }
}