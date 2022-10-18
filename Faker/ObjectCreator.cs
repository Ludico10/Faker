namespace FakerLib
{
    public class ObjectCreator
    {
        private readonly IFaker faker;
        private readonly GeneratorContext context;
        private readonly RecursionKiller checker;


        public ObjectCreator(IFaker Faker, GeneratorContext generatorContext)
        {
            faker = Faker;
            context = generatorContext;
            checker = new RecursionKiller(3);
        }

        public Object? Generate(Type type)
        {
            Object? obj = null;
            if (checker.Add(type))
            {
                obj = Create(type);
                FillFields(type, obj);
                FillProperties(type, obj);
                checker.Clean(type);
            }
            return obj;
        }

        public Object Create(Type type)
        {
            var constructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                try
                {
                    var args = constructor.GetParameters().Select(x => faker.Create(x.ParameterType)).ToArray();
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

        public void FillFields(Type type, Object obj)
        {
            var fields = type.GetFields().Where(f => f.IsPublic);
            foreach (var field in fields)
            {
                try
                {
                    if (Equals(field.GetValue(obj), GetDefaultValue(field.FieldType)))
                    {
                        field.SetValue(obj, faker.Create(field.FieldType));
                    }
                }
                catch { }
            }
        }

        public void FillProperties(Type type, Object obj)
        {
            var properties = type.GetProperties().Where(p => p.CanWrite);
            foreach (var property in properties)
            {
                try
                {
                    if (Equals(property.GetValue(obj), GetDefaultValue(property.PropertyType)))
                    {
                        property.SetValue(obj, faker.Create(property.PropertyType));
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
