using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Helpers.Xml;


using System.Collections.Generic;
using System.Xml;

using System.Xml.Xsl;

using System.Linq;

public static class XmlHelper
{
    // 1. Parse string to XmlDocument
    public static XmlDocument Parse(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);
        return doc;
    }

    // 2. Convert XmlDocument to string
    public static string ToXml(XmlDocument doc)
    {
        return doc.OuterXml;
    }

    // 3. Pretty XML
    public static string ToPrettyXml(XmlDocument doc)
    {
        using var stringWriter = new System.IO.StringWriter();
        using var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
        doc.WriteTo(xmlTextWriter);
        return stringWriter.ToString();
    }

    // 4. Minify XML
    public static string Minify(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);
        using var stringWriter = new System.IO.StringWriter();
        using var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.None };
        doc.WriteTo(xmlWriter);
        return stringWriter.ToString();
    }

    // 5. Validate against XSD
    public static bool ValidateXsd(XmlDocument doc, string xsdPath)
    {
        bool isValid = true;
        doc.Schemas.Add(null, xsdPath);
        doc.Validate((s, e) => { isValid = false; });
        return isValid;
    }

    // 6. Validate against DTD
    public static bool ValidateDtd(XmlDocument doc)
    {
        bool isValid = true;
        doc.Validate((s, e) => { isValid = false; });
        return isValid;
    }

    // 7. Get root element
    public static XmlElement GetRoot(XmlDocument doc) => doc.DocumentElement;

    // 8. Get single element by XPath
    public static XmlElement GetElement(XmlDocument doc, string xpath)
        => (XmlElement)doc.SelectSingleNode(xpath);

    // 9. Get elements list
    public static List<XmlElement> GetElements(XmlDocument doc, string xpath)
        => doc.SelectNodes(xpath)?.OfType<XmlElement>().ToList() ?? new List<XmlElement>();

    // 10. Get attribute value
    public static string GetAttribute(XmlElement el, string attrName) => el.GetAttribute(attrName);

    // 11. Set/Add attribute
    public static void SetAttribute(XmlElement el, string attrName, string value) => el.SetAttribute(attrName, value);

    // 12. Remove attribute
    public static void RemoveAttribute(XmlElement el, string attrName) => el.RemoveAttribute(attrName);

    // 13. Get inner text
    public static string GetText(XmlElement el) => el.InnerText;

    // 14. Set inner text
    public static void SetText(XmlElement el, string value) => el.InnerText = value;

    // 15. Add child element
    public static XmlElement AddElement(XmlElement parent, string name, string innerText = null)
    {
        var child = parent.OwnerDocument.CreateElement(name);
        if (!string.IsNullOrEmpty(innerText))
            child.InnerText = innerText;
        parent.AppendChild(child);
        return child;
    }

    // 16. Remove child element by name
    public static void RemoveElement(XmlElement parent, string childName)
    {
        var child = parent[childName];
        if (child != null) parent.RemoveChild(child);
    }

    // 17. Update element name or text
    public static void UpdateElement(XmlElement el, string newName = null, string newText = null)
    {
        if (!string.IsNullOrEmpty(newName))
        {
            var doc = el.OwnerDocument;
            var newEl = doc.CreateElement(newName);
            newEl.InnerXml = el.InnerXml;
            foreach (XmlAttribute attr in el.Attributes)
                newEl.SetAttribute(attr.Name, attr.Value);
            el.ParentNode.ReplaceChild(newEl, el);
            el = newEl;
        }
        if (newText != null) el.InnerText = newText;
    }

    // 18. Deep copy of element
    public static XmlElement CopyElement(XmlElement el)
    {
        return (XmlElement)el.CloneNode(true);
    }

    // 19. Move element to new parent
    public static void MoveElement(XmlElement el, XmlElement newParent)
    {
        var copy = (XmlElement)el.CloneNode(true);
        newParent.AppendChild(copy);
        el.ParentNode.RemoveChild(el);
    }

    // 20. Rename element
    public static void RenameElement(XmlElement el, string newName)
    {
        var doc = el.OwnerDocument;
        var newEl = doc.CreateElement(newName);
        newEl.InnerXml = el.InnerXml;
        foreach (XmlAttribute attr in el.Attributes)
            newEl.SetAttribute(attr.Name, attr.Value);
        el.ParentNode.ReplaceChild(newEl, el);
    }

    // 21. Get namespace URI
    public static string GetNamespace(XmlElement el) => el.NamespaceURI;

    // 22. Set namespace
    public static void SetNamespace(XmlElement el, string ns)
    {
        var doc = el.OwnerDocument;
        var newEl = doc.CreateElement(el.LocalName, ns);
        newEl.InnerXml = el.InnerXml;
        foreach (XmlAttribute attr in el.Attributes)
            newEl.SetAttribute(attr.Name, attr.Value);
        el.ParentNode.ReplaceChild(newEl, el);
    }

 

    // 24. Add namespace to root
    public static void AddNamespace(XmlDocument doc, string prefix, string ns)
    {
        doc.DocumentElement.SetAttribute($"xmlns:{prefix}", ns);
    }

    // 25. Check if XPath exists
    public static bool XPathExists(XmlDocument doc, string xpath) => doc.SelectSingleNode(xpath) != null;

    // 26. Select single node by XPath
    public static XmlNode XPathSelect(XmlDocument doc, string xpath) => doc.SelectSingleNode(xpath);

    // 27. Select all nodes by XPath
    public static List<XmlNode> XPathSelectAll(XmlDocument doc, string xpath)
        => doc.SelectNodes(xpath)?.Cast<XmlNode>().ToList() ?? new List<XmlNode>();

    // 28. Evaluate number from XPath
    public static double XPathEvaluateNumber(XmlDocument doc, string xpath)
    {
        var nav = doc.CreateNavigator();
        return (double)nav.Evaluate(xpath);
    }

    // 29. Evaluate string from XPath
    public static string XPathEvaluateString(XmlDocument doc, string xpath)
    {
        var nav = doc.CreateNavigator();
        return (string)nav.Evaluate(xpath);
    }

    // 30. Transform XML via XSLT
    public static string TransformXslt(XmlDocument doc, string xsltPath)
    {
        var xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        using var sw = new System.IO.StringWriter();
        xslt.Transform(doc, null, sw);
        return sw.ToString();
    }

    // 31. Transform XML to HTML
    public static string TransformToHtml(XmlDocument doc, string xsltPath) => TransformXslt(doc, xsltPath);

    // 32. Canonicalize XML (simple)
    public static string Canonicalize(XmlDocument doc)
    {
        using var ms = new System.IO.MemoryStream();
        doc.Save(ms);
        return System.Text.Encoding.UTF8.GetString(ms.ToArray());
    }

    // 37. Convert XML to Map (Dictionary<string, object>)
    public static Dictionary<string, object> ToMap(XmlElement el)
    {
        var dict = new Dictionary<string, object>();
        foreach (XmlAttribute attr in el.Attributes)
            dict[$"@{attr.Name}"] = attr.Value;

        foreach (XmlNode node in el.ChildNodes)
        {
            if (node is XmlElement child)
            {
                var value = ToMap(child);
                if (dict.ContainsKey(child.Name))
                {
                    if (dict[child.Name] is List<Dictionary<string, object>> list)
                        list.Add(value);
                    else
                        dict[child.Name] = new List<Dictionary<string, object>> { (Dictionary<string, object>)dict[child.Name], value };
                }
                else dict[child.Name] = value;
            }
            else if (node is XmlText text)
                dict["#text"] = text.Value;
        }
        return dict;
    }

    //// 39. Convert XML to JSON
    //public static string ToJson(XmlDocument doc) => JsonConvert.SerializeXmlNode(doc);

    //// 40. Convert JSON to XML
    //public static XmlDocument FromJson(string json)
    //{
    //    var doc = JsonConvert.DeserializeXmlNode(json);
    //    return doc;
    //}
}
