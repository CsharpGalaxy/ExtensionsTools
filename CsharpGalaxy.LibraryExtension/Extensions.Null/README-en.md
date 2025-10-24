# 🚫 Null Pattern Extensions Documentation

## 📋 **Available Methods**
| Method | Description | Parameters | Return Type | Constraints |
|--------|-------------|------------|-------------|-------------|
| `Create<T>` | Creates a new instance of type T | None | `T` | T must have a default constructor |
| `NothingIfNull<T>` | Returns new instance if null, otherwise returns the object itself | `obj: T` | `T` | T must have a default constructor |

## 🔍 **Usage Examples**

### Create<T>
```csharp
// Create a new instance of a class
var person = NullPatternExtentions.Create<Person>();

// Create a new list
var list = NullPatternExtentions.Create<List<string>>();
```

### NothingIfNull<T>
```csharp
public class Person
{
    public string Name { get; set; }
}

// Example 1: Object is null
Person person = null;
person = person.NothingIfNull(); // Creates new Person instance

// Example 2: Object is not null
var person2 = new Person { Name = "John" };
person2 = person2.NothingIfNull(); // Returns the same instance
```

## ⚠️ **Important Notes**

1. Type Constraints:
   - Both methods require the generic type `T` to have a default constructor
   - Works with classes that have a parameterless constructor
   - Cannot be used with interfaces or abstract classes

2. Thread Safety:
   - Methods are thread-safe as they don't maintain any state
   - New instance creation is atomic

3. Performance Considerations:
   - `Create<T>` uses the `new()` constraint for compile-time safety
   - `NothingIfNull<T>` performs a null check before creating a new instance

4. Best Practices:
   - Use `NothingIfNull<T>` to ensure non-null objects in your code
   - Prefer this over repetitive null checks
   - Consider memory implications when creating new instances