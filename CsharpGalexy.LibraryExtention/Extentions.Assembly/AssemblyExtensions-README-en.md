# AssemblyExtensions Class

This documentation covers the `AssemblyExtensions` class which provides extension methods for working with assemblies and types using reflection.

## Extension Methods

### Assembly Path Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetDirectoryPathX | Assembly assembly | string | Returns the directory path of the specified assembly |

### Type Discovery Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetAllIEntityTypeConfigurationAssembliesByNamespaceContains | string namespace | List<Type> | Gets types with "Configure" method in specified namespace |
| GetAllAssembliesByInterface<T> | - | List<Type> | Gets types implementing interface T |
| GetAllIEntityTypeConfigurationAssembliesInterface<T> | - | List<Type> | Alternative method for getting types implementing interface T |
| GetTypeOf<T> | string name | Type | Finds class type by name in executing assembly |
| GetClassOfType<T> | - | IEnumerable<Type> | Gets all classes assignable from type T |
| GetTypesAssignableFrom<T> | Assembly assembly | List<Type> | Gets types assignable from T in specified assembly |
| GetTypesAssignableFrom | Assembly assembly, Type compareType | List<Type> | Gets types assignable from specified type |

### Property Access Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetPropertyValue<T> | object obj, string propName | T | Gets property value by name with type casting |

## Usage Examples

### Working with Assembly Paths
```csharp
var assembly = Assembly.GetExecutingAssembly();
string path = assembly.GetDirectoryPathX();
// Returns the directory containing the assembly
```

### Finding Types by Interface
```csharp
// Get all non-abstract classes implementing IMyInterface
var types = AssemblyExtensions.GetAllAssembliesByInterface<IMyInterface>();

// Find types in specific namespace with Configure method
var configTypes = AssemblyExtensions.GetAllIEntityTypeConfigurationAssembliesByNamespaceContains("MyNamespace");
```

### Type Discovery
```csharp
// Find specific class type
Type type = AssemblyExtensions.GetTypeOf<BaseClass>("TargetClassName");

// Get all classes inheriting from BaseClass
var derivedClasses = AssemblyExtensions.GetClassOfType<BaseClass>();

// Find assignable types in specific assembly
Assembly assembly = Assembly.GetExecutingAssembly();
var assignableTypes = assembly.GetTypesAssignableFrom<BaseClass>();
```

### Property Access
```csharp
var obj = new MyClass();
string value = obj.GetPropertyValue<string>("PropertyName");
```

## Important Notes

1. **Type Safety**:
   - Generic methods ensure type safety at compile time
   - Throws exceptions for invalid type requests
   - Handles null cases appropriately

2. **Performance Considerations**:
   - Uses reflection which can impact performance
   - Caches Assembly.GetExecutingAssembly() when possible
   - Efficient LINQ queries for type filtering

3. **Usage Context**:
   - Best for application startup/configuration
   - Useful for plugin architectures
   - Helpful for dynamic type loading

## Common Use Cases

1. **Entity Framework Configuration**:
   - Finding configuration classes
   - Automatic registration of entity configurations
   - Dynamic model building

2. **Plugin Systems**:
   - Discovering plugin implementations
   - Loading types dynamically
   - Finding compatible types

3. **Reflection Utilities**:
   - Property access by name
   - Type discovery
   - Assembly information

## Dependencies

- System namespace
- System.Collections.Generic namespace
- System.IO namespace
- System.Linq namespace
- System.Reflection namespace