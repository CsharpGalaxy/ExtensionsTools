namespace CsharpGalexy.LibraryExtention.Extentions.Dictionary
{
    public static class DefaultableDictionaryExtentions
    {
        public static IDictionary<TKey, TValue> WithDefaultValue<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue) => new DefaultableDictionary<TKey, TValue>(dictionary, defaultValue);
    }
}
