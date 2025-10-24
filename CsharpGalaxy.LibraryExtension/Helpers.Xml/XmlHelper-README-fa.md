# کلاس XmlHelper

این مستندات شامل کلاس `XmlHelper` است که مجموعه‌ای جامع از متدهای استاتیک برای دستکاری و پردازش XML ارائه می‌دهد.

## متدها

### عملیات سند XML

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| Parse | string xml | XmlDocument | یک رشته را به XmlDocument تبدیل می‌کند. |
| ToXml | XmlDocument doc | string | یک XmlDocument را به نمایش رشته‌ای آن تبدیل می‌کند. |
| ToPrettyXml | XmlDocument doc | string | یک XmlDocument را به رشته‌ای فرمت‌شده با تورفتگی مناسب تبدیل می‌کند. |
| Minify | string xml | string | فضاهای خالی غیرضروری را از رشته XML حذف می‌کند. |

### اعتبارسنجی

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ValidateXsd | XmlDocument doc, string xsdPath | bool | XML را در برابر یک طرح XSD اعتبارسنجی می‌کند. |
| ValidateDtd | XmlDocument doc | bool | XML را در برابر DTD آن اعتبارسنجی می‌کند. |

### عملیات المان‌ها

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetRoot | XmlDocument doc | XmlElement | المان ریشه سند را دریافت می‌کند. |
| GetElement | XmlDocument doc, string xpath | XmlElement | یک المان را با استفاده از XPath دریافت می‌کند. |
| GetElements | XmlDocument doc, string xpath | List<XmlElement> | تمام المان‌های منطبق با عبارت XPath را دریافت می‌کند. |
| AddElement | XmlElement parent, string name, string innerText | XmlElement | یک المان فرزند جدید اضافه می‌کند. |
| RemoveElement | XmlElement parent, string childName | void | یک المان فرزند را با نام حذف می‌کند. |
| UpdateElement | XmlElement el, string newName, string newText | void | نام و/یا متن المان را به‌روزرسانی می‌کند. |
| CopyElement | XmlElement el | XmlElement | یک کپی عمیق از المان ایجاد می‌کند. |
| MoveElement | XmlElement el, XmlElement newParent | void | یک المان را به والد جدید منتقل می‌کند. |
| RenameElement | XmlElement el, string newName | void | نام یک المان را تغییر می‌دهد. |

### عملیات صفات

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetAttribute | XmlElement el, string attrName | string | مقدار یک صفت را دریافت می‌کند. |
| SetAttribute | XmlElement el, string attrName, string value | void | یک صفت را تنظیم/اضافه می‌کند. |
| RemoveAttribute | XmlElement el, string attrName | void | یک صفت را حذف می‌کند. |

### عملیات متن

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetText | XmlElement el | string | متن داخلی المان را دریافت می‌کند. |
| SetText | XmlElement el, string value | void | متن داخلی المان را تنظیم می‌کند. |

### عملیات فضای نام

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| GetNamespace | XmlElement el | string | URI فضای نام المان را دریافت می‌کند. |
| SetNamespace | XmlElement el, string ns | void | فضای نام المان را تنظیم می‌کند. |
| AddNamespace | XmlDocument doc, string prefix, string ns | void | یک فضای نام به المان ریشه اضافه می‌کند. |

### عملیات XPath

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| XPathExists | XmlDocument doc, string xpath | bool | وجود XPath را بررسی می‌کند. |
| XPathSelect | XmlDocument doc, string xpath | XmlNode | یک گره را با XPath انتخاب می‌کند. |
| XPathSelectAll | XmlDocument doc, string xpath | List<XmlNode> | تمام گره‌ها را با XPath انتخاب می‌کند. |
| XPathEvaluateNumber | XmlDocument doc, string xpath | double | XPath را به عدد ارزیابی می‌کند. |
| XPathEvaluateString | XmlDocument doc, string xpath | string | XPath را به رشته ارزیابی می‌کند. |

### تبدیل

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| TransformXslt | XmlDocument doc, string xsltPath | string | XML را با استفاده از XSLT تبدیل می‌کند. |
| TransformToHtml | XmlDocument doc, string xsltPath | string | XML را به HTML تبدیل می‌کند. |
| Canonicalize | XmlDocument doc | string | سند XML را استاندارد می‌کند. |

### تبدیل ساختار

| متد | پارامترها | نوع خروجی | توضیحات |
|-----|-----------|------------|----------|
| ToMap | XmlElement el | Dictionary<string, object> | XML را به ساختار دیکشنری تبدیل می‌کند. |

### مثال‌های استفاده

```csharp
// تجزیه رشته XML
var doc = XmlHelper.Parse("<root><child>value</child></root>");

// چاپ زیبا
string formatted = XmlHelper.ToPrettyXml(doc);

// دریافت المان با XPath
var element = XmlHelper.GetElement(doc, "//child");

// افزودن المان جدید
var parent = XmlHelper.GetRoot(doc);
XmlHelper.AddElement(parent, "newChild", "text");

// اعتبارسنجی با XSD
bool isValid = XmlHelper.ValidateXsd(doc, "schema.xsd");

// تبدیل با استفاده از XSLT
string html = XmlHelper.TransformToHtml(doc, "template.xslt");
```

### مدیریت خطا

- اکثر متدها ممکن است برای XML نامعتبر `XmlException` پرتاب کنند
- متدهای XPath در صورت عدم تطابق null یا مجموعه‌های خالی برمی‌گردانند
- متدهای اعتبارسنجی به جای پرتاب استثنا، مقدار boolean برمی‌گردانند

### ایمنی در برابر چند نخی

این کلاس فقط شامل متدهای استاتیک است و تا زمانی که نمونه‌های XmlDocument ورودی به طور همزمان تغییر نکنند، thread-safe است.