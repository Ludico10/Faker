namespace FakerLib
{
    public interface IFaker
    {
        public T Create<T>();

        public object Create(Type t);
    }
}
