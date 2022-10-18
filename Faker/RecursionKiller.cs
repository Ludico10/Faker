namespace FakerLib
{
    public class RecursionKiller
    {
        private int limit;
        private Dictionary<Type, int> counters;

        public RecursionKiller(int max)
        {
            limit = max;
            counters = new Dictionary<Type, int>();
        }

        public bool Add(Type type)
        {
            if (counters.ContainsKey(type)) counters[type]++;
            else counters.Add(type, 1);
            return counters[type] < limit;
        }

        public void Clean(Type type)
        {
            if (counters.ContainsKey(type)) counters[type]--;
            if (counters[type] == 0) counters.Remove(type);
        }
    }
}
