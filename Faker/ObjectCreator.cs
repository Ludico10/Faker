namespace FakerLib
{
    public class ObjectCreator : IValueGenerator
    {
        private readonly RecursionKiller checker;

        public ObjectCreator(IFaker Faker)
        {
            checker = new RecursionKiller(3);
        }

        public bool CanGenerate(Type type)
        {
            return type.IsClass || (type.IsValueType && !type.IsEnum);
        }

        public Object Generate(Type type, GeneratorContext generatorContext)
        {
            Object? obj = null;
            if (checker.Add(type))
            {
                obj = Create(type, generatorContext);
                FillFields(type, obj, generatorContext);
                FillProperties(type, obj, generatorContext);
                checker.Clean(type);
            }
            if (obj != null) return obj;
            throw new FakerException($"Can not generate for type {type.Name}");
        }

        public Object Create(Type type, GeneratorContext context)
        {
            var constructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                try
                {
                    var args = constructor.GetParameters().Select(x => context.Faker.Create(x.ParameterType)).ToArray();
                    return constructor.Invoke(args);
                }
                catch { }
            }
            try
            {
                Object? obj = GetDefaultValue(type);
                if (obj != null) return obj;
            }
            catch { }
            throw new FakerException($"Can not create object of type { type }");
        }

        public void FillFields(Type type, Object obj, GeneratorContext context)
        {
            var fields = type.GetFields().Where(f => f.IsPublic);
            foreach (var field in fields)
            {
                try
                {
                    if (Equals(field.GetValue(obj), GetDefaultValue(field.FieldType)))
                    {
                        field.SetValue(obj, context.Faker.Create(field.FieldType));
                    }
                }
                catch { }
            }
        }

        public void FillProperties(Type type, Object obj, GeneratorContext context)
        {
            var properties = type.GetProperties().Where(p => p.CanWrite);
            foreach (var property in properties)
            {
                try
                {
                    if (Equals(property.GetValue(obj), GetDefaultValue(property.PropertyType)))
                    {
                        property.SetValue(obj, context.Faker.Create(property.PropertyType));
                    }
                }
                catch { }
            }
        }

        public static object? GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }
}
