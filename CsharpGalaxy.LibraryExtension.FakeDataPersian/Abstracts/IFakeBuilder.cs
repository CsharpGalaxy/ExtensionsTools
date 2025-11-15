using System.Linq.Expressions;
using System.Reflection;

namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;

/// <summary>
/// پترن Builder برای تولید داده‌های تصادفی با Fluent API
/// </summary>
public class FakeBuilder<T> where T : new()
{
    private readonly Dictionary<string, Func<object>> _rules = new();

    /// <summary>
    /// قانون تولید برای یک Property را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<object> generator)
    {
        var propName = GetPropertyName(property);
        _rules[propName] = generator;
        return this;
    }

    /// <summary>
    /// قانون تولید برای تمام Properties از نوع مشخص را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllProperties(Type propertyType, Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType == propertyType);
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// قانون تولید برای تمام string properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllStrings(Func<object> generator)
    {
        return RuleForAllProperties(typeof(string), generator);
    }

    /// <summary>
    /// قانون تولید برای تمام int properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllInts(Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(int) || p.PropertyType == typeof(int?));
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// قانون تولید برای تمام bool properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllBools(Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?));
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// قانون تولید برای تمام decimal properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllDecimals(Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?));
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// قانون تولید برای تمام DateTime properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllDateTimes(Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// یک نمونه تکی را ایجاد می‌کند
    /// </summary>
    public T Build()
    {
        var entity = new T();
        foreach (var rule in _rules)
        {
            var prop = typeof(T).GetProperty(rule.Key);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, rule.Value());
            }
        }
        return entity;
    }

    /// <summary>
    /// چندین نمونه را ایجاد می‌کند
    /// </summary>
    public List<T> BuildList(int count)
    {
        var list = new List<T>();
        for (int i = 0; i < count; i++)
        {
            list.Add(Build());
        }
        return list;
    }

    /// <summary>
    /// نام property را از expression استخراج می‌کند
    /// </summary>
    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> property)
    {
        if (property.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        if (property.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression innerMember)
        {
            return innerMember.Member.Name;
        }

        throw new ArgumentException("Invalid property expression");
    }
}
