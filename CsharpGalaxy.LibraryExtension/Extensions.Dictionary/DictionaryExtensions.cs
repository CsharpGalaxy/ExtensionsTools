using System.Collections.Generic;
using System.Linq;

namespace CsharpGalexy.LibraryExtention.Extensions.Dictionary
{
    /// <summary>
    /// Provides extension methods for working with dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the first key that matches the given value.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="value">The value to find.</param>
        /// <returns>The matching key if found; otherwise the default value of <typeparamref name="TKey"/>.</returns>
        public static TKey GetKeyFromValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value) =>
            dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key;

        /// <summary>
        /// Gets the value associated with the given key.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="key">The key to find.</param>
        /// <returns>The matching value if found; otherwise the default value of <typeparamref name="TValue"/>.</returns>
        public static TValue GetValueFromKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.FirstOrDefault(x => x.Key.Equals(key)).Value;

        /// <summary>
        /// Checks whether the dictionary instance is null.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary instance.</param>
        /// <returns><c>true</c> if the dictionary is null; otherwise <c>false</c>.</returns>
        public static bool CheckDictionaryIsNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary == null;

        /// <summary>
        /// Adds a new entry to the dictionary if the key does not already exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary to modify.</param>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        /// <returns><c>true</c> if the key was added; <c>false</c> if the key already exists or the dictionary is null.</returns>
        public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.CheckDictionaryIsNull() && dictionary.ContainsKey(key)) { return false; }

            dictionary.Add(key, value);
            return false;
        }

        /// <summary>
        /// Deletes an entry from the dictionary if the key exists.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary to modify.</param>
        /// <param name="key">The key to remove.</param>
        /// <returns><c>true</c> if the key was found and removed; otherwise <c>false</c>.</returns>
        public static bool DeleteIfExistsKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.CheckDictionaryIsNull() && !dictionary.ContainsKey(key)) { return false; }
            return dictionary.Remove(key);
        }

        /// <summary>
        /// Updates the value of the given key if it exists.  
        /// If the key or value is null, the update will not be applied.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="dictionary">The dictionary to modify.</param>
        /// <param name="key">The key to update.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns><c>true</c> if the value was updated; otherwise <c>false</c>.</returns>
        public static bool Update<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.CheckDictionaryIsNull() && CheckKeyValuePairIsNull(key, value)) { return false; }
            dictionary[key] = value;
            return true;
        }

        /// <summary>
        /// Checks whether a key-value pair is null (either key or value).
        /// </summary>
        private static bool CheckKeyValuePairIsNull<TKey, TValue>(TKey key, TValue value) => key == null || value == null;

        /// <summary>
        /// Checks whether a <see cref="KeyValuePair{TKey, TValue}"/> is null (either key or value).
        /// </summary>
        private static bool CheckKeyValuePairIsNull<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Key == null || pair.Value == null;
    }
}
