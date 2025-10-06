using System;
using System.Reflection;



/// <summary>
/// Provides information about the calling assembly,
/// such as version, title, product name, description,
/// copyright, and company.
/// </summary>
public static class ApplicationInfo
{
    /// <summary>
    /// Gets the version of the calling assembly.
    /// </summary>
    public static Version Version =>
        System.Reflection.Assembly.GetCallingAssembly().GetName().Version;

    /// <summary>
    /// Gets the title of the calling assembly.
    /// If no <see cref="AssemblyTitleAttribute"/> is defined,
    /// returns the file name of the executing assembly without extension.
    /// </summary>
    public static string Title
    {
        get
        {
            object[] attributes = System.Reflection.Assembly
                .GetCallingAssembly()
                .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title.Length > 0)
                    return titleAttribute.Title;
            }

            return Path.GetFileNameWithoutExtension(
                System.Reflection.Assembly.GetExecutingAssembly().Location
            );
        }
    }

    /// <summary>
    /// Gets the product name defined in the calling assembly.
    /// Returns an empty string if not defined.
    /// </summary>
    public static string ProductName
    {
        get
        {
            object[] attributes = System.Reflection.Assembly
                .GetCallingAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute), false);

            return attributes.Length == 0
                ? ""
                : ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    /// <summary>
    /// Gets the description defined in the calling assembly.
    /// Returns an empty string if not defined.
    /// </summary>
    public static string Description
    {
        get
        {
            object[] attributes = System.Reflection.Assembly
                .GetCallingAssembly()
                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

            return attributes.Length == 0
                ? ""
                : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    /// <summary>
    /// Gets the copyright information defined in the calling assembly.
    /// Returns an empty string if not defined.
    /// </summary>
    public static string CopyrightHolder
    {
        get
        {
            object[] attributes = System.Reflection.Assembly
                .GetCallingAssembly()
                .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

            return attributes.Length == 0
                ? ""
                : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    /// <summary>
    /// Gets the company name defined in the calling assembly.
    /// Returns an empty string if not defined.
    /// </summary>
    public static string CompanyName
    {
        get
        {
            object[] attributes = System.Reflection.Assembly
                .GetCallingAssembly()
                .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

            return attributes.Length == 0
                ? ""
                : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }
}
