namespace AdventOfCode2022.Utilities
{
    public static class DictionaryExtensions
    {
        public static void AddOrPrependAtKey<K, V>(this Dictionary<K, IEnumerable<V>> dictionary, K key, V value) where K : notnull
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = dictionary[key].Prepend(value);
            }
            else
            {
                dictionary.Add(key, new List<V>() { value });
            }
        }
    }
}
