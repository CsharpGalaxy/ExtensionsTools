# XmlHelper Class

This documentation covers the `XmlHelper` class which provides a comprehensive set of static methods for XML manipulation and processing.

## Methods

### XML Document Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| Parse | string xml | XmlDocument | Parses a string into an XmlDocument. |
| ToXml | XmlDocument doc | string | Converts an XmlDocument to its string representation. |
| ToPrettyXml | XmlDocument doc | string | Converts an XmlDocument to a formatted string with proper indentation. |
| Minify | string xml | string | Removes unnecessary whitespace from XML string. |

### Validation

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ValidateXsd | XmlDocument doc, string xsdPath | bool | Validates XML against an XSD schema. |
| ValidateDtd | XmlDocument doc | bool | Validates XML against its DTD. |

### Element Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetRoot | XmlDocument doc | XmlElement | Gets the root element of the document. |
| GetElement | XmlDocument doc, string xpath | XmlElement | Gets a single element using XPath. |
| GetElements | XmlDocument doc, string xpath | List<XmlElement> | Gets all elements matching an XPath expression. |
| AddElement | XmlElement parent, string name, string innerText | XmlElement | Adds a new child element. |
| RemoveElement | XmlElement parent, string childName | void | Removes a child element by name. |
| UpdateElement | XmlElement el, string newName, string newText | void | Updates element name and/or text. |
| CopyElement | XmlElement el | XmlElement | Creates a deep copy of an element. |
| MoveElement | XmlElement el, XmlElement newParent | void | Moves an element to a new parent. |
| RenameElement | XmlElement el, string newName | void | Renames an element. |

### Attribute Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetAttribute | XmlElement el, string attrName | string | Gets an attribute value. |
| SetAttribute | XmlElement el, string attrName, string value | void | Sets/adds an attribute. |
| RemoveAttribute | XmlElement el, string attrName | void | Removes an attribute. |

### Text Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetText | XmlElement el | string | Gets element's inner text. |
| SetText | XmlElement el, string value | void | Sets element's inner text. |

### Namespace Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| GetNamespace | XmlElement el | string | Gets element's namespace URI. |
| SetNamespace | XmlElement el, string ns | void | Sets element's namespace. |
| AddNamespace | XmlDocument doc, string prefix, string ns | void | Adds a namespace to root element. |

### XPath Operations

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| XPathExists | XmlDocument doc, string xpath | bool | Checks if XPath exists. |
| XPathSelect | XmlDocument doc, string xpath | XmlNode | Selects single node by XPath. |
| XPathSelectAll | XmlDocument doc, string xpath | List<XmlNode> | Selects all nodes by XPath. |
| XPathEvaluateNumber | XmlDocument doc, string xpath | double | Evaluates XPath to number. |
| XPathEvaluateString | XmlDocument doc, string xpath | string | Evaluates XPath to string. |

### Transformation

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| TransformXslt | XmlDocument doc, string xsltPath | string | Transforms XML using XSLT. |
| TransformToHtml | XmlDocument doc, string xsltPath | string | Transforms XML to HTML. |
| Canonicalize | XmlDocument doc | string | Canonicalizes XML document. |

### Conversion

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| ToMap | XmlElement el | Dictionary<string, object> | Converts XML to dictionary structure. |

### Usage Examples

```csharp
// Parse XML string
var doc = XmlHelper.Parse("<root><child>value</child></root>");

// Pretty print
string formatted = XmlHelper.ToPrettyXml(doc);

// Get element by XPath
var element = XmlHelper.GetElement(doc, "//child");

// Add new element
var parent = XmlHelper.GetRoot(doc);
XmlHelper.AddElement(parent, "newChild", "text");

// Validate against XSD
bool isValid = XmlHelper.ValidateXsd(doc, "schema.xsd");

// Transform using XSLT
string html = XmlHelper.TransformToHtml(doc, "template.xslt");
```

### Error Handling

- Most methods may throw `XmlException` for invalid XML
- XPath methods return null or empty collections if no matches found
- Validation methods return boolean instead of throwing exceptions

### Thread Safety

The class contains only static methods and is thread-safe as long as the input XmlDocument instances are not modified concurrently.