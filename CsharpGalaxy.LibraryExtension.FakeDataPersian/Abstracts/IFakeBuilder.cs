using System.Linq.Expressions;
using System.Reflection;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Attributes;
using CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Abstracts;

/// <summary>
/// پترن Builder برای تولید داده‌های تصادفی با Fluent API
/// </summary>
public class FakeBuilder<T> where T : new()
{
    private readonly Dictionary<string, Func<object>> _rules = new();
    private readonly Dictionary<string, (Type, Delegate)> _foreignKeyRules = new();
    private readonly Random _random = new();

    /// <summary>
    /// قانون تولید برای یک Property را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<object> generator)
    {
        var propName = GetPropertyName(property);
        _rules[propName] = generator;
        _foreignKeyRules.Remove(propName);
        return this;
    }

    /// <summary>
    /// قانون تولید برای یک کلید خارجی را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForForeignKey<TRelated>(Expression<Func<T, TRelated>> property, Func<TRelated> generator) 
        where TRelated : class, new()
    {
        var propName = GetPropertyName(property);
        _rules.Remove(propName);
        _foreignKeyRules[propName] = (typeof(TRelated), new Func<object>(() => generator()));
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
    /// قانون تولید برای تمام Enum properties را تعریف می‌کند
    /// </summary>
    public FakeBuilder<T> RuleForAllEnums(Func<object> generator)
    {
        var props = typeof(T).GetProperties().Where(p => p.PropertyType.IsEnum);
        foreach (var prop in props)
        {
            _rules[prop.Name] = generator;
        }
        return this;
    }

    /// <summary>
    /// قانون تولید برای یک Enum property را تعریف می‌کند (نوع-آگاه)
    /// </summary>
    public FakeBuilder<T> RuleForEnum<TEnum>(Expression<Func<T, TEnum>> property, Func<TEnum> generator) 
        where TEnum : Enum
    {
        var propName = GetPropertyName(property);
        _rules[propName] = () => generator();
        return this;
    }

    /// <summary>
    /// قانون تولید برای انتخاب رندوم از یک لیست
    /// </summary>
    public FakeBuilder<T> RuleForListSelection<TProperty>(Expression<Func<T, TProperty>> property, params TProperty[] items)
    {
        var propName = GetPropertyName(property);
        if (items == null || items.Length == 0)
            throw new ArgumentException("Items list cannot be empty");
        
        _rules[propName] = () => items[_random.Next(items.Length)];
        return this;
    }

    /// <summary>
    /// قانون تولید برای انتخاب رندوم از یک لیست (IEnumerable)
    /// </summary>
    public FakeBuilder<T> RuleForListSelection<TProperty>(Expression<Func<T, TProperty>> property, IEnumerable<TProperty> items)
    {
        var propName = GetPropertyName(property);
        var itemArray = items?.ToArray() ?? throw new ArgumentException("Items list cannot be null or empty");
        if (itemArray.Length == 0)
            throw new ArgumentException("Items list cannot be empty");
        
        _rules[propName] = () => itemArray[_random.Next(itemArray.Length)];
        return this;
    }

    /// <summary>
    /// یک نمونه تکی را ایجاد می‌کند
    /// </summary>
    public T Build()
    {
        var entity = new T();
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            if (!prop.CanWrite)
                continue;

            // بررسی Ignore Attribute
            if (prop.GetCustomAttribute<IgnoreAttribute>() != null)
                continue;

            // بررسی Constant Attribute
            var constantAttr = prop.GetCustomAttribute<ConstantAttribute>();
            if (constantAttr != null)
            {
                prop.SetValue(entity, constantAttr.Value);
                continue;
            }

            // بررسی Enum Attribute
            var enumAttr = prop.GetCustomAttribute<EnumAttribute>();
            if (enumAttr != null && !_rules.ContainsKey(prop.Name))
            {
                if (enumAttr.AllowedValues != null && enumAttr.AllowedValues.Length > 0)
                {
                    var randomIndex = _random.Next(enumAttr.AllowedValues.Length);
                    prop.SetValue(entity, enumAttr.AllowedValues[randomIndex]);
                }
                else
                {
                    prop.SetValue(entity, EnumGenerator.GetRandomEnumValue(enumAttr.EnumType));
                }
                continue;
            }

            // اگر property نوع Enum است
            if (prop.PropertyType.IsEnum && !_rules.ContainsKey(prop.Name))
            {
                prop.SetValue(entity, EnumGenerator.GetRandomEnumValue(prop.PropertyType));
                continue;
            }

            // بررسی ForeignKey Attribute
            var foreignKeyAttr = prop.GetCustomAttribute<ForeignKeyAttribute>();
            
            // اگر قانون کلید خارجی وجود دارد
            if (_foreignKeyRules.TryGetValue(prop.Name, out var fkRule))
            {
                // اگر کلید خارجی اختیاری است، احتمال null
                if (foreignKeyAttr?.IsOptional == true)
                {
                    int probability = foreignKeyAttr.NullProbability;
                    if (_random.Next(0, 100) < probability)
                    {
                        prop.SetValue(entity, null);
                        continue;
                    }
                }
                var value = fkRule.Item2.DynamicInvoke();
                prop.SetValue(entity, value);
            }
            // اگر قانون شخصی وجود دارد
            else if (_rules.TryGetValue(prop.Name, out var rule))
            {
                prop.SetValue(entity, rule());
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
