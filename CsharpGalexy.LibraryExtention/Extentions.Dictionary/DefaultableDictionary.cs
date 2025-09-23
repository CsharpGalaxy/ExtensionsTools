using System.Collections;
using System.Collections.Generic;

namespace CsharpGalexy.LibraryExtention.Extentions.Dictionary;


/// <summary>
/// A dictionary wrapper that returns a default value when a key is not found,
/// instead of throwing a KeyNotFoundException.
/// </summary>
/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public class DefaultableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    #region Fields & Constructor

    private readonly IDictionary<TKey, TValue> _dictionary;
    private readonly TValue _defaultValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultableDictionary{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="dictionary">The underlying dictionary to wrap.</param>
    /// <param name="defaultValue">The value to return when a key is not found.</param>
    public DefaultableDictionary(IDictionary<TKey, TValue> dictionary, TValue defaultValue)
    {
        _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        _defaultValue = defaultValue;
    }

    #endregion

    #region IDictionary<TKey, TValue> Properties

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Gets a value indicating whether the dictionary is read-only.
    /// </summary>
    public bool IsReadOnly => _dictionary.IsReadOnly;

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// If the key does not exist on get, returns the default value.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>The value for the specified key, or default value if key not found.</returns>
    public TValue this[TKey key]
    {
        get
        {
            try
            {
                return _dictionary[key];
            }
            catch (KeyNotFoundException)
            {
                return _defaultValue;
            }
        }
        set => _dictionary[key] = value;
    }

    /// <summary>
    /// Gets an ICollection containing the keys of the dictionary.
    /// </summary>
    public ICollection<TKey> Keys => _dictionary.Keys;

    /// <summary>
    /// Gets an ICollection containing the values in the dictionary.
    /// ⚠️ Note: This includes the default value at the end (unusual behavior — preserved as in original).
    /// </summary>
    public ICollection<TValue> Values
    {
        get
        {
            var values = new List<TValue>(_dictionary.Values)
            {
                _defaultValue // Appends default value to collection (original behavior)
            };
            return values;
        }
    }

    #endregion

    #region IDictionary<TKey, TValue> Methods

    /// <summary>
    /// Adds an element with the provided key and value to the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);

    /// <summary>
    /// Adds an item to the dictionary.
    /// </summary>
    /// <param name="item">The object to add.</param>
    public void Add(KeyValuePair<TKey, TValue> item) => _dictionary.Add(item);

    /// <summary>
    /// Removes all items from the dictionary.
    /// </summary>
    public void Clear() => _dictionary.Clear();

    /// <summary>
    /// Determines whether the dictionary contains a specific key-value pair.
    /// </summary>
    /// <param name="item">The object to locate.</param>
    /// <returns>True if the item is found; otherwise, false.</returns>
    public bool Contains(KeyValuePair<TKey, TValue> item) => _dictionary.Contains(item);

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate.</param>
    /// <returns>True if the dictionary contains an element with the key; otherwise, false.</returns>
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    /// <summary>
    /// Copies the elements of the dictionary to an array, starting at a particular index.
    /// </summary>
    /// <param name="array">The array to copy to.</param>
    /// <param name="arrayIndex">The index in the array at which copying begins.</param>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _dictionary.CopyTo(array, arrayIndex);

    /// <summary>
    /// Removes the element with the specified key from the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>True if the element is successfully removed; otherwise, false.</returns>
    public bool Remove(TKey key) => _dictionary.Remove(key);

    /// <summary>
    /// Removes the first occurrence of a specific object from the dictionary.
    /// </summary>
    /// <param name="item">The object to remove.</param>
    /// <returns>True if item was successfully removed; otherwise, false.</returns>
    public bool Remove(KeyValuePair<TKey, TValue> item) => _dictionary.Remove(item);

    /// <summary>
    /// Gets the value associated with the specified key.
    /// If key is not found, assigns default value to 'value' and returns true.
    /// </summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key,
    /// or the default value if the key is not found.</param>
    /// <returns>Always returns true (to mimic successful retrieval).</returns>
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (!_dictionary.TryGetValue(key, out value))
        {
            value = _defaultValue;
        }
        return true; // Always returns true — consistent with "defaultable" behavior
    }

    #endregion

    #region IEnumerable Implementation

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>An enumerator for the dictionary.</returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>An enumerator for the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}