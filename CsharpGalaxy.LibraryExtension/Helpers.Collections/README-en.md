# 📚 List Helper Documentation

## 📋 **Basic Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `IsNullOrEmpty<T>` | Checks if list is null or empty | `list: IList<T>` | `bool` |
| `IsNotEmpty<T>` | Checks if list has items | `list: IList<T>` | `bool` |
| `SizeOf<T>` | Gets the size of list (safely) | `list: IList<T>` | `int` |
| `GetFirst<T>` | Gets first item or default | `list: IList<T>` | `T` |
| `GetLast<T>` | Gets last item or default | `list: IList<T>` | `T` |
| `GetOrElse<T>` | Gets item at index or default | `list: IList<T>, index: int, defaultValue: T` | `T` |
| `GetRandom<T>` | Gets random item from list | `list: IList<T>` | `T` |

## 🔄 **Modification Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `SetSafe<T>` | Sets value at index, expanding if needed | `list: List<T>, index: int, value: T` | `void` |
| `AddIfAbsent<T>` | Adds item if not present | `list: List<T>, item: T` | `bool` |
| `AddAllIfAbsent<T>` | Adds multiple items if not present | `list: List<T>, items: IEnumerable<T>` | `void` |
| `RemoveFirst<T>` | Removes and returns first item | `list: List<T>` | `T` |
| `RemoveLast<T>` | Removes and returns last item | `list: List<T>` | `T` |
| `RemoveNulls<T>` | Removes all null items | `list: List<T>` | `void` |
| `RemoveDuplicates<T>` | Removes duplicate items | `list: List<T>` | `void` |
| `RemoveByIndex<T>` | Removes items at specified indices | `list: List<T>, indices: int[]` | `void` |

## 🔀 **List Transformations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Swap<T>` | Swaps two elements | `list: IList<T>, i: int, j: int` | `void` |
| `Reverse<T>` | Reverses list in place | `list: IList<T>` | `void` |
| `Shuffle<T>` | Randomly shuffles list | `list: IList<T>` | `void` |
| `RotateLeft<T>` | Rotates list left by n positions | `list: IList<T>, n: int` | `void` |
| `RotateRight<T>` | Rotates list right by n positions | `list: IList<T>, n: int` | `void` |

## 📊 **Sorting Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `SortNatural<T>` | Sorts list naturally | `list: IList<T>` where T : IComparable<T> | `void` |
| `SortReverse<T>` | Sorts list in reverse | `list: IList<T>` where T : IComparable<T> | `void` |
| `SortBy<T>` | Sorts list using custom comparison | `list: IList<T>, comparison: Comparison<T>` | `void` |

## 📈 **Aggregation Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Min<T>` | Gets minimum value | `source: IEnumerable<T>` where T : IComparable<T> | `T` |
| `Max<T>` | Gets maximum value | `source: IEnumerable<T>` where T : IComparable<T> | `T` |
| `SumInt` | Sums integer values | `source: IEnumerable<int>` | `int` |
| `SumLong` | Sums long values | `source: IEnumerable<long>` | `long` |
| `SumDouble` | Sums double values | `source: IEnumerable<double>` | `double` |
| `Average` | Calculates average | `source: IEnumerable<double>` | `double` |

## 🔍 **Filtering Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Filter<T>` | Filters items by predicate | `source: IEnumerable<T>, predicate: Func<T,bool>` | `List<T>` |
| `FilterNot<T>` | Filters items NOT matching predicate | `source: IEnumerable<T>, predicate: Func<T,bool>` | `List<T>` |
| `FilterNotNull<T>` | Removes null items | `source: IEnumerable<T>` | `List<T>` |

## 🎯 **Mapping Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Map<T,TResult>` | Maps items using selector | `source: IEnumerable<T>, selector: Func<T,TResult>` | `List<TResult>` |
| `MapIndexed<T,TResult>` | Maps items with their indices | `source: IEnumerable<T>, selector: Func<int,T,TResult>` | `List<TResult>` |
| `MapNotNull<T,TResult>` | Maps and filters null results | `source: IEnumerable<T>, selector: Func<T,TResult>` | `List<TResult>` |
| `FlatMap<T,TResult>` | Flattens nested collections | `source: IEnumerable<T>, selector: Func<T,IEnumerable<TResult>>` | `List<TResult>` |

## 🎭 **Set Operations**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Intersect<T>` | Gets common elements | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `Union<T>` | Combines unique elements | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `Subtract<T>` | Removes elements in second from first | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `SymmetricDiff<T>` | Gets elements in either but not both | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |

## 🔄 **Type Conversion**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `ToArray<T>` | Converts to array | `source: IEnumerable<T>` | `T[]` |
| `ToSet<T>` | Converts to HashSet | `source: IEnumerable<T>` | `HashSet<T>` |
| `ToMap<T,TKey>` | Converts to Dictionary | `source: IEnumerable<T>, keySelector: Func<T,TKey>` | `Dictionary<TKey,T>` |
| `ToImmutableList<T>` | Creates read-only list | `source: IEnumerable<T>` | `IReadOnlyList<T>` |

## 📦 **Chunking & Windowing**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Chunked<T>` | Splits into fixed-size chunks | `source: IEnumerable<T>, size: int` | `List<List<T>>` |
| `Windowed<T>` | Creates sliding windows | `source: IEnumerable<T>, size: int, step: int` | `List<List<T>>` |
| `Slice<T>` | Gets a portion of list | `source: IList<T>, start: int, end: int?` | `List<T>` |

## 🔒 **Thread Safety**
| Method | Description | Parameters | Return Type |
|--------|-------------|------------|-------------|
| `Synchronize<T>` | Creates thread-safe wrapper | `list: IList<T>` | `IList<T>` |

Note: For better thread safety, consider using `ConcurrentBag<T>` or explicit locking mechanisms.