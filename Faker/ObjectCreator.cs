namespace FakerLib
{
    public class ObjectCreator
    {
        private readonly IFaker faker;
        private readonly GeneratorContext context;
        private readonly RecursionKiller checker;


        public ObjectCreator(IFaker Faker, GeneratorContext generatorContext, RecursionKiller recursion)
        {
            faker = Faker;
            context = generatorContext;
            checker = recursion;
        }

        public Object Generate(Type type)
        {
            Object? obj = null;
            if (checker.Add(type))
            {
                obj = Create(type);
                FillFields(type, obj);
                FillProperties(type, obj);
                checker.Clean(type);
            }
            return obj!;
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
            throw new Exception($"Can not create object of type { type }");
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
                        faker.Create(field.FieldType);
                    }
                }
                catch { }
            }
        }

        public void FillProperties(Type type, Object obj)
        {
            var properties = type.GetProperties().Where(p => p.CanWrite);
            foreach (var properrty in properties)
            {
                try
                {
                    if (Equals(properrty.GetValue(obj), GetDefaultValue(properrty.PropertyType)))
                    {
                        faker.Create(properrty.PropertyType);
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
