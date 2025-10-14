# کلاس JsonHelper

این مستندات شامل کلاس استاتیک `JsonHelper` است که قابلیت‌های جامعی برای دستکاری، تجزیه و تبدیل JSON با استفاده از System.Text.Json ارائه می‌دهد.

## سریالایز/دی‌سریالایز JSON

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToJson | object value, JsonSerializerOptions options | string | شیء را به رشته JSON تبدیل می‌کند. در صورت خطا null برمی‌گرداند. |
| ParseTo<T> | string str, JsonSerializerOptions options | T | رشته JSON را به نوع T دی‌سریالایز می‌کند. در صورت خطا مقدار پیش‌فرض T را برمی‌گرداند. |
| IsValidJson | string text | bool | اعتبارسنجی می‌کند که آیا یک رشته JSON معتبر است. |

## تبدیل پایه

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ArrayToJson<T> | IEnumerable<T> list, bool pretty | string | آرایه/لیست را به رشته JSON تبدیل می‌کند با قابلیت فرمت‌بندی زیبا. |
| JsonToArray<T> | string jsonString | List<T> | رشته JSON را به List<T> تجزیه می‌کند. در صورت خطا لیست خالی برمی‌گرداند. |
| JsonToMap | string jsonString | Dictionary<string, JsonNode> | رشته JSON را به دیکشنری تبدیل می‌کند. در صورت خطا دیکشنری خالی برمی‌گرداند. |

## فرمت‌بندی و فشرده‌سازی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToPrettyJson<T> | T obj | string | شیء را به رشته JSON قابل خواندن برای انسان تبدیل می‌کند. |
| FormatJson | string jsonString | string | رشته JSON را با تورفتگی مناسب زیباسازی می‌کند. |
| MinifyJson | string jsonString | string | JSON را با حذف فضاهای خالی غیرضروری فشرده می‌کند. |
| EscapeJsonString | string rawString | string | کاراکترهای خاص را برای رشته JSON اسکیپ می‌کند. |

## دسترسی و استخراج

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| DeepGet | string jsonString, string path | JsonNode | مقدار تو در تو را با استفاده از مسیر نقطه‌گذاری شده دریافت می‌کند. |
| GetString | string jsonString, string path, string defaultValue | string | مقدار رشته‌ای را با مسیر مشخص و مقدار پیش‌فرض استخراج می‌کند. |
| GetInt | string jsonString, string path, int defaultValue | int | مقدار عددی صحیح را با مسیر مشخص و مقدار پیش‌فرض استخراج می‌کند. |
| GetDouble | string jsonString, string path, double defaultValue | double | مقدار اعشاری را با مسیر مشخص و مقدار پیش‌فرض استخراج می‌کند. |
| GetBoolean | string jsonString, string path, bool defaultValue | bool | مقدار بولین را با مسیر مشخص و مقدار پیش‌فرض استخراج می‌کند. |
| HasKey | string jsonString, string path | bool | وجود کلید در مسیر مشخص را بررسی می‌کند. |
| GetJsonArray | string jsonString, string path | JsonArray | آرایه JSON را در مسیر مشخص استخراج می‌کند. در صورت خطا آرایه خالی برمی‌گرداند. |
| GetJsonObject | string jsonString, string path | JsonObject | شیء JSON را در مسیر مشخص استخراج می‌کند. در صورت خطا شیء خالی برمی‌گرداند. |

## دستکاری و تغییر

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| CloneJson<T> | T obj | T | یک کپی عمیق از شیء JSON ایجاد می‌کند. |
| DeepSet | JsonObject jObject, string path, JsonNode value | JsonObject | مقدار تو در تو را با استفاده از مسیر نقطه‌گذاری شده تنظیم می‌کند. |
| UpdateKey | string jsonString, string path, object newValue | string | مقدار را در مسیر مشخص به‌روزرسانی یا اضافه می‌کند. |
| RemoveKey | string jsonString, string path | string | کلید را در مسیر مشخص حذف می‌کند. |
| RemoveNulls | string jsonString | string | تمام مقادیر null را به صورت بازگشتی حذف می‌کند. |

## تبدیل حالت نگارش

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ConvertKeysToCamelCase | string jsonString | string | تمام کلیدهای JSON را به camelCase تبدیل می‌کند. |
| ConvertKeysToSnakeCase | string jsonString | string | تمام کلیدهای JSON را به snake_case تبدیل می‌کند. |

## تبدیل ساختاری

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| FlattenJson | string jsonString | Dictionary<string, JsonNode> | JSON تو در تو را به ساختار مسطح با نقطه‌گذاری تبدیل می‌کند. |
| UnflattenJson | Dictionary<string, JsonNode> flattenedMap | string | ساختار مسطح را به JSON تو در تو تبدیل می‌کند. |

## تبدیل XML/JSON

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| XmlToJson | string xmlString | string | رشته XML را به JSON معادل تبدیل می‌کند. |
| JsonToXml | string jsonString, string rootNodeName | string | رشته JSON را به XML معادل تبدیل می‌کند. |

### مثال‌های استفاده

```csharp
// سریالایز کردن
var json = someObject.ToJson();

// فرمت‌بندی زیبا
var formatted = JsonHelper.ToPrettyJson(someObject);

// دسترسی عمیق
var value = JsonHelper.GetString(json, "user.address.city");

// تغییر
var updated = JsonHelper.UpdateKey(json, "user.email", "new@email.com");

// تبدیل حالت نگارش
var camelCase = JsonHelper.ConvertKeysToCamelCase(json);

// مسطح/تو در تو کردن
var flat = JsonHelper.FlattenJson(json);
var nested = JsonHelper.UnflattenJson(flat);
```

### مدیریت خطا
- اکثر متدها در صورت خطا null، مجموعه‌های خالی یا ورودی اصلی را برمی‌گردانند
- متدهای اعتبارسنجی برای ورودی نامعتبر false برمی‌گردانند
- متدها طوری طراحی شده‌اند که بدون پرتاب استثنا با خطا مواجه شوند

### نکات کارایی
- از System.Text.Json برای کارایی بهتر استفاده می‌کند
- تنظیمات JsonSerializerOptions را برای عملیات رایج در حافظه نگه می‌دارد
- از تجزیه غیرضروری هنگامی که ورودی نامعتبر است جلوگیری می‌کند