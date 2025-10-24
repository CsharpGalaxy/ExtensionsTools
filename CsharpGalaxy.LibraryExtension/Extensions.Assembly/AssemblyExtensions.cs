using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


/// <summary>
/// Provides extension methods for working with assemblies and types.
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Returns the directory path of the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to extract the folder path from.</param>
    /// <returns>The directory path of the assembly file.</returns>
    public static string GetDirectoryPathX(this System.Reflection.Assembly assembly)
    {
        string filePath = new Uri(assembly.Location).LocalPath;
        return Path.GetDirectoryName(filePath);
    }

    /// <summary>
    /// Gets all types from loaded assemblies that contain the given namespace
    /// and define a method named "Configure".  
    /// Typically used for EntityTypeConfiguration classes.
    /// </summary>
    /// <param name="namespace">The namespace filter.</param>
    /// <returns>A list of matching types.</returns>
    public static List<Type> GetAllIEntityTypeConfigurationAssembliesByNamespaceContains(string @namespace)
    {
        if (string.IsNullOrEmpty(@namespace)) return new List<Type>();

        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x =>
                x.Namespace != null &&
                x.Namespace.Contains(@namespace) &&
                x.GetMethods().FirstOrDefault(m => m.Name == "Configure") != null &&
                !x.IsInterface &&
                !x.IsAbstract)
            .ToList();
    }

    /// <summary>
    /// Gets all non-abstract, non-interface types from loaded assemblies
    /// that implement the given interface <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The interface type to search for.</typeparam>
    /// <returns>A list of matching types.</returns>
    public static List<Type> GetAllAssembliesByInterface<T>()
    {
        if (!typeof(T).IsInterface) return new List<Type>();

        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();
    }

    /// <summary>
    /// Gets all non-abstract, non-interface types from loaded assemblies
    /// that implement the given interface <typeparamref name="T"/>.  
    /// (Same as <see cref="GetAllAssembliesByInterface{T}"/>).
    /// </summary>
    /// <typeparam name="T">The interface type to search for.</typeparam>
    /// <returns>A list of matching types.</returns>
    public static List<Type> GetAllIEntityTypeConfigurationAssembliesInterface<T>()
    {
        if (!typeof(T).IsInterface) return new List<Type>();

        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();
    }

    /// <summary>
    /// Finds a class type in the executing assembly by its name.
    /// </summary>
    /// <typeparam name="T">The base type to compare against.</typeparam>
    /// <param name="name">The class name (case-insensitive).</param>
    /// <returns>The matching type if found.</returns>
    /// <exception cref="Exception">Thrown if no such type exists.</exception>
    public static Type GetTypeOf<T>(string name)
    {
        var type = typeof(T);
        var types = System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name.ToLowerInvariant() == name.ToLowerInvariant() && t.IsClass && !t.IsInterface);

        if (type == null) throw new Exception("No such type");

        return types as Type;
    }

    /// <summary>
    /// Gets all class types in the executing assembly
    /// that are assignable from <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base type to compare against.</typeparam>
    /// <returns>An enumerable of matching types.</returns>
    /// <exception cref="Exception">Thrown if no matching types exist.</exception>
    public static IEnumerable<Type> GetClassOfType<T>()
    {
        var type = typeof(T);
        IEnumerable<Type> types = System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => type.IsAssignableFrom(t) && t.IsClass);

        if (types == null) throw new Exception("No such type");

        return types;
    }

    /// <summary>
    /// Gets all types in the given assembly that are assignable from <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base type to compare against.</typeparam>
    /// <param name="assembly">The assembly to search.</param>
    /// <returns>A list of matching types.</returns>
    public static List<Type> GetTypesAssignableFrom<T>(this System.Reflection.Assembly assembly) =>
        assembly.GetTypesAssignableFrom(typeof(T));

    /// <summary>
    /// Gets all types in the given assembly that are assignable from the specified type.
    /// </summary>
    /// <param name="assembly">The assembly to search.</param>
    /// <param name="compareType">The base type to compare against.</param>
    /// <returns>A list of matching types.</returns>
    public static List<Type> GetTypesAssignableFrom(this System.Reflection.Assembly assembly, Type compareType)
    {
        List<Type> ret = new List<Type>();
        foreach (var type in assembly.DefinedTypes)
        {
            if (compareType.IsAssignableFrom(type) && compareType != type)
            {
                ret.Add(type);
            }
        }
        return ret;
    }

    /// <summary>
    /// Gets the value of a property from an object by its name.
    /// </summary>
    /// <typeparam name="T">The expected property type.</typeparam>
    /// <param name="obj">The target object.</param>
    /// <param name="propName">The property name.</param>
    /// <returns>The value of the property cast to <typeparamref name="T"/>.</returns>
    public static T GetPropertyValue<T>(this object obj, string propName) =>
        (T)obj.GetType().GetProperty(propName)?.GetValue(obj, null);
}
