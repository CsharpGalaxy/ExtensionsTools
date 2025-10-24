using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CsharpGalexy.LibraryExtention.Helpers.Json;

public static class JsonHelper
{

    #region 📦 JSON Serialization/Deserialization (System.Text.Json)

    /// <summary>
    /// تبدیل شیء به رشته JSON با استفاده از System.Text.Json.
    /// در صورت خطا، null برمی‌گرداند.
    /// </summary>
    public static string ToJson(this object value, JsonSerializerOptions options = null)
    {
        if (value == null) return null;
        try
        {
            return JsonSerializer.Serialize(value, options ?? new JsonSerializerOptions());
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// تبدیل رشته JSON به شیء از نوع T با استفاده از System.Text.Json.
    /// در صورت خطا، مقدار پیش‌فرض T برگردانده می‌شود.
    /// </summary>
    public static T ParseTo<T>(this string str, JsonSerializerOptions options = null)
    {
        if (string.IsNullOrEmpty(str)) return default(T);
        try
        {
            return JsonSerializer.Deserialize<T>(str, options);
        }
        catch
        {
            return default(T);
        }
    }

    #endregion

    #region 📦 JSON Serialization/Deserialization (Newtonsoft.Json)





    /// <summary>
    /// تبدیل رشته JSON به شیء از نوع T با استفاده از System.Text.Json.
    /// در صورت خطا، مقدار پیش‌فرض T برگردانده می‌شود و خطا در کنسول چاپ می‌شود.
    /// </summary>
    public static T ParseTo<T>(this string str)
    {
        if (string.IsNullOrEmpty(str)) return default(T);
        try
        {
            return JsonSerializer.Deserialize<T>(str);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ParseTo Error: {ex.Message}");
            return default(T);
        }
    }

    /// <summary>
    /// اعتبارسنجی ساختار JSON (آیا رشته یک JSON معتبر است؟)
    /// با استفاده از System.Text.Json.JsonDocument
    /// </summary>
    public static bool IsValidJson(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        text = text.Trim();
        if ((text.StartsWith("{") && text.EndsWith("}")) ||
            (text.StartsWith("[") && text.EndsWith("]")))
        {
            try
            {
                using var doc = JsonDocument.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    #endregion



    // پیکربندی پیش‌فرض برای خوانایی (Pretty Print)
    private static readonly JsonSerializerOptions PrettyPrintOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // برای نمایش بهتر کاراکترهای فارسی
        // تنظیمات دیگر مانند CamelCase را در اینجا می‌توان اعمال کرد
    };

    // پیکربندی پیش‌فرض برای فشرده‌سازی (Minify)
    private static readonly JsonSerializerOptions MinifyOptions = new()
    {
        WriteIndented = false
    };

    // --- متدهای کمکی داخلی ---



    /// <summary>
    /// دسترسی عمیق به یک مقدار بر اساس مسیر (Dot Notation) با استفاده از JsonNode.
    /// </summary>
    private static JsonNode DeepGetNode(JsonNode root, string path)
    {
        if (root == null) return null;
        var parts = path.Split('.');
        JsonNode currentNode = root;

        foreach (var part in parts)
        {
            if (currentNode is JsonObject obj)
            {
                if (!obj.TryGetPropertyValue(part, out currentNode))
                {
                    return null;
                }
            }
            else
            {
                return null; // ساختار JSON object نیست
            }
        }
        return currentNode;
    }

    // ----------------------------------------------------------------------
    // ## توابع تبدیل پایه (Base Conversion)
    // ----------------------------------------------------------------------

    /// <summary>
    /// تبدیل Array/List به رشته JSON. (arrayToJson)
    /// </summary>
    public static string ArrayToJson<T>(IEnumerable<T> list, bool pretty = false)
    {
        return JsonSerializer.Serialize(list, pretty ? PrettyPrintOptions : MinifyOptions);
    }

    /// <summary>
    /// پارس رشته JSON به Array/List. (jsonToArray)
    /// </summary>
    public static List<T> JsonToArray<T>(string jsonString)
    {
        try
        {
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
        catch { return new List<T>(); }
    }

    /// <summary>
    /// پارس رشته JSON به Map/Dictionary. (jsonToMap)
    /// </summary>
    public static Dictionary<string, JsonNode> JsonToMap(string jsonString)
    {
        try
        {
            var node = JsonNode.Parse(jsonString);
            return node?.AsObject().ToDictionary(p => p.Key, p => p.Value);
        }
        catch { return new Dictionary<string, JsonNode>(); }
    }

    // ----------------------------------------------------------------------
    // ## فرمت‌بندی و فشرده‌سازی (Formatting & Compression)
    // ----------------------------------------------------------------------

    /// <summary>
    /// تبدیل آبجکت/Map به رشته JSON قابل خواندن انسان. (toPrettyJson)
    /// </summary>
    public static string ToPrettyJson<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, PrettyPrintOptions);
    }

    /// <summary>
    /// فرمت‌بندی زیبا (Pretty Print) رشته JSON. (formatJson)
    /// </summary>
    public static string FormatJson(string jsonString)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            var node = JsonNode.Parse(jsonString);
            return node.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    /// <summary>
    /// فشرده‌سازی رشته JSON با حذف فضاهای اضافی. (compressJson / minifyJson)
    /// </summary>
    public static string MinifyJson(string jsonString)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            // Serialize دوباره با WriteIndented = false
            var node = JsonNode.Parse(jsonString);
            return node.ToJsonString(MinifyOptions);
        }
        catch { return jsonString; }
    }

    /// <summary>
    /// اسکیپ کردن کاراکترهای خاص جهت قرارگیری در رشته JSON. (escapeJsonString)
    /// </summary>
    public static string EscapeJsonString(string rawString)
    {
        // با استفاده از JsonSerializer یک رشته را اسکیپ می‌کنیم
        return JsonSerializer.Serialize(rawString);
    }

    // ----------------------------------------------------------------------
    // ## دسترسی و استخراج (Access & Extraction)
    // ----------------------------------------------------------------------

    /// <summary>
    /// دسترسی به مقدار درون‌رفته با استفاده از مسیر dot separated. (deepGet)
    /// </summary>
    public static JsonNode DeepGet(string jsonString, string path)
    {
        if (!IsValidJson(jsonString)) return null;
        try
        {
            var root = JsonNode.Parse(jsonString);
            return DeepGetNode(root, path);
        }
        catch { return null; }
    }

    /// <summary>
    /// استخراج رشته با مسیر مشخص و مقدار پیش‌فرض. (getString)
    /// </summary>
    public static string GetString(string jsonString, string path, string defaultValue = null)
    {
        var node = DeepGet(jsonString, path);
        return node?.GetValueKind() == JsonValueKind.String ? node.GetValue<string>() : defaultValue;
    }

    /// <summary>
    /// استخراج عدد صحیح با مسیر مشخص و مقدار پیش‌فرض. (getInt)
    /// </summary>
    public static int GetInt(string jsonString, string path, int defaultValue = 0)
    {
        var node = DeepGet(jsonString, path);
        return node?.GetValueKind() == JsonValueKind.Number ? node.GetValue<int>() : defaultValue;
    }

    /// <summary>
    /// استخراج مقدار اعشاری با مسیر مشخص و مقدار پیش‌فرض. (getDouble)
    /// </summary>
    public static double GetDouble(string jsonString, string path, double defaultValue = 0.0)
    {
        var node = DeepGet(jsonString, path);
        return node?.GetValueKind() == JsonValueKind.Number ? node.GetValue<double>() : defaultValue;
    }

    /// <summary>
    /// استخراج مقدار بولین با مسیر مشخص و مقدار پیش‌فرض. (getBoolean)
    /// </summary>
    public static bool GetBoolean(string jsonString, string path, bool defaultValue = false)
    {
        var node = DeepGet(jsonString, path);
        return node?.GetValueKind() == JsonValueKind.True || node?.GetValueKind() == JsonValueKind.False ? node.GetValue<bool>() : defaultValue;
    }

    /// <summary>
    /// بررسی وجود کلید مشخص در JSON. (hasKey)
    /// </summary>
    public static bool HasKey(string jsonString, string path)
    {
        return DeepGet(jsonString, path) != null;
    }

    /// <summary>
    /// استخراج آرایه JSON با کلید مشخص یا برگرداندن آرایه خالی. (getJsonArray)
    /// </summary>
    public static JsonArray GetJsonArray(string jsonString, string path)
    {
        var node = DeepGet(jsonString, path);
        return node?.AsArray() ?? new JsonArray();
    }

    /// <summary>
    /// استخراج آبجکت JSON با کلید مشخص یا برگرداندن آبجکت خالی. (getJsonObject)
    /// </summary>
    public static JsonObject GetJsonObject(string jsonString, string path)
    {
        var node = DeepGet(jsonString, path);
        return node?.AsObject() ?? new JsonObject();
    }

    // ----------------------------------------------------------------------
    // ## دستکاری و تغییر (Manipulation & Modification)
    // ----------------------------------------------------------------------

    /// <summary>
    /// ایجاد کپی عمیق یک آبجکت JSON. (cloneJson)
    /// </summary>
    public static T CloneJson<T>(T obj)
    {
        // سریعترین راهکار بومی: سریالایز و سپس دی‌سریالایز
        var jsonString = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    /// <summary>
    /// تنظیم مقدار درون‌رفته با استفاده از مسیر dot separated. (deepSet)
    /// </summary>
    public static JsonObject DeepSet(JsonObject jObject, string path, JsonNode value)
    {
        if (jObject == null) return null;
        var parts = path.Split('.');
        JsonObject current = jObject;

        for (int i = 0; i < parts.Length; i++)
        {
            string key = parts[i];

            if (i == parts.Length - 1)
            {
                // آخرین قسمت مسیر: مقدار را تنظیم کنید
                current[key] = value;
            }
            else
            {
                // بخش میانی مسیر: مطمئن شوید که یک JsonObject وجود دارد
                if (current[key] is not JsonObject nextObj)
                {
                    nextObj = new JsonObject();
                    current[key] = nextObj;
                }
                current = nextObj;
            }
        }
        return jObject;
    }

    /// <summary>
    /// بروزرسانی یا افزودن مقدار برای کلید مشخص (پشتیبانی از مسیر dot separated). (updateKey)
    /// </summary>
    public static string UpdateKey(string jsonString, string path, object newValue)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            var root = JsonNode.Parse(jsonString);
            if (root is not JsonObject jObject) return jsonString;

            // تبدیل مقدار جدید به JsonNode
            var newNodeValue = JsonSerializer.SerializeToNode(newValue);

            DeepSet(jObject, path, newNodeValue);
            return jObject.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    /// <summary>
    /// حذف یک کلید مشخص از آبجکت JSON (پشتیبانی از مسیر dot separated). (removeKey)
    /// </summary>
    public static string RemoveKey(string jsonString, string path)
    {
        if (!IsValidJson(jsonString)) return jsonString;

        try
        {
            var root = JsonNode.Parse(jsonString);
            if (root is not JsonObject jObject) return jsonString;

            var parts = path.Split('.');
            if (parts.Length == 1)
            {
                // حذف در سطح اول
                jObject.Remove(path);
            }
            else
            {
                // حذف در سطح عمیق
                var parentPath = string.Join('.', parts.Take(parts.Length - 1));
                var keyToRemove = parts.Last();

                var parentNode = DeepGetNode(jObject, parentPath);
                if (parentNode is JsonObject parentObj)
                {
                    parentObj.Remove(keyToRemove);
                }
            }
            return jObject.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    /// <summary>
    /// حذف تمام کلیدهایی که مقدار null دارند به‌صورت بازگشتی. (removeNulls)
    /// </summary>
    public static string RemoveNulls(string jsonString)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            var root = JsonNode.Parse(jsonString);
            RemoveNullsRecursive(root);
            return root.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    private static void RemoveNullsRecursive(JsonNode node)
    {
        if (node is JsonObject obj)
        {
            var keysToRemove = obj.Where(p => p.Value == null || p.Value.GetValueKind() == JsonValueKind.Null)
                                  .Select(p => p.Key)
                                  .ToList();

            foreach (var key in keysToRemove)
            {
                obj.Remove(key);
            }

            foreach (var prop in obj)
            {
                RemoveNullsRecursive(prop.Value);
            }
        }
        else if (node is JsonArray arr)
        {
            foreach (var item in arr)
            {
                RemoveNullsRecursive(item);
            }
        }
    }

    // ----------------------------------------------------------------------
    // ## توابع تبدیل Case (Case Conversion)
    // ----------------------------------------------------------------------

    /// <summary>
    /// تغییر کلیدهای JSON به قالب camelCase یکپارچه. (convertKeysToCamelCase)
    /// </summary>
    public static string ConvertKeysToCamelCase(string jsonString)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            var root = JsonNode.Parse(jsonString);
            ConvertKeysRecursive(root, s => char.ToLowerInvariant(s[0]) + s.Substring(1));
            return root.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    /// <summary>
    /// تغییر کلیدهای JSON به قالب snake_case یکپارچه. (convertKeysToSnakeCase)
    /// </summary>
    public static string ConvertKeysToSnakeCase(string jsonString)
    {
        if (!IsValidJson(jsonString)) return jsonString;
        try
        {
            var root = JsonNode.Parse(jsonString);
            ConvertKeysRecursive(root, s => Regex.Replace(s, "([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant());
            return root.ToJsonString(PrettyPrintOptions);
        }
        catch { return jsonString; }
    }

    private static void ConvertKeysRecursive(JsonNode node, Func<string, string> keyConverter)
    {
        if (node is JsonObject obj)
        {
            // از لیست موقت برای جلوگیری از تغییر حین پیمایش استفاده می‌کنیم
            var properties = obj.ToList();
            obj.Clear(); // آبجکت را خالی می‌کنیم تا با کلیدهای جدید پر شود

            foreach (var property in properties)
            {
                var newName = keyConverter(property.Key);
                ConvertKeysRecursive(property.Value, keyConverter); // پیمایش بازگشتی روی مقدار
                obj.Add(newName, property.Value); // افزودن با نام جدید
            }
        }
        else if (node is JsonArray arr)
        {
            foreach (var item in arr)
            {
                ConvertKeysRecursive(item, keyConverter);
            }
        }
    }

    // ----------------------------------------------------------------------
    // ## توابع تبدیل ساختار (Structural Transformation)
    // ----------------------------------------------------------------------

    /// <summary>
    /// تبدیل JSON تو در تو به ساختار تکی (dot notation). (flattenJson)
    /// </summary>
    public static Dictionary<string, JsonNode> FlattenJson(string jsonString)
    {
        if (!IsValidJson(jsonString)) return new Dictionary<string, JsonNode>();
        var token = JsonNode.Parse(jsonString);
        return FlattenRecursive(token, string.Empty);
    }

    private static Dictionary<string, JsonNode> FlattenRecursive(JsonNode node, string path)
    {
        var result = new Dictionary<string, JsonNode>();

        if (node is JsonObject obj)
        {
            foreach (var property in obj)
            {
                var newPath = string.IsNullOrEmpty(path) ? property.Key : path + "." + property.Key;
                foreach (var item in FlattenRecursive(property.Value, newPath))
                {
                    result.Add(item.Key, item.Value);
                }
            }
        }
        else if (node is JsonArray arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                var newPath = $"{path}[{i}]";
                // نادیده گرفتن آرایه‌های تو در تو در سطح Flattening اصلی
                if (arr[i] is JsonValue)
                {
                    result.Add(newPath, arr[i]);
                }
                else
                {
                    foreach (var item in FlattenRecursive(arr[i], newPath))
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
            }
        }
        else if (node is JsonValue)
        {
            result.Add(path, node);
        }

        return result;
    }

    /// <summary>
    /// بازگرداندن ساختار dot notation به JSON تو در تو. (unflattenJson)
    /// </summary>
    public static string UnflattenJson(Dictionary<string, JsonNode> flattenedMap)
    {
        var root = new JsonObject();

        foreach (var kvp in flattenedMap)
        {
            DeepSet(root, kvp.Key, kvp.Value);
        }

        return root.ToJsonString(PrettyPrintOptions);
    }

    // ----------------------------------------------------------------------
    // ## توابع XML/JSON
    // ----------------------------------------------------------------------

    /// <summary>
    /// تبدیل رشته XML به JSON معادل. (xmlToJson)
    /// </summary>
    public static string XmlToJson(string xmlString)
    {
        try
        {
            var doc = XDocument.Parse(xmlString);
            // استفاده از کلاس XNode برای تبدیل به Json
            return System.Text.Json.JsonSerializer.Serialize(
                doc, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            return $"{{ \"Error\": \"XML conversion failed: {ex.Message}\" }}";
        }
    }

    /// <summary>
    /// تبدیل رشته JSON به XML معادل. (jsonToXml)
    /// </summary>
    public static string JsonToXml(string jsonString, string rootNodeName = "root")
    {
        // **نکته:** تبدیل مستقیم JSON بومی (System.Text.Json) به XML بومی 
        // در .NET وجود ندارد و باید از کتابخانه Newtonsoft.Json یا پیاده‌سازی 
        // پیچیده‌ای از طریق XML DOM استفاده کرد. در اینجا به‌دلیل محدودیت بومی‌سازی
        // این متد صرفاً به‌عنوان Placeholder باقی می‌ماند.
        return $"";
    }

    // ----------------------------------------------------------------------
    // ## توابع پیشرفته (Advanced - نیاز به پیاده‌سازی کامل)
    // ----------------------------------------------------------------------

    // **نکته:** متدهای زیر مانند `diffJson`، `patchJson`، `sortKeysAlphabetically`، 
    // `convertDateFormat`، `encryptJsonFields` و `countKeys` به‌طور کامل پیاده‌سازی نشدند، 
    // زیرا همگی نیاز به منطق پیچیده‌ی بازگشتی و تخصصی دارند که از دامنه یک Helper 
    // ساده بومی خارج است و بهتر است از کتابخانه‌های متناسب استفاده شود.
}

