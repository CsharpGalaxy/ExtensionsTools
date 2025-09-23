using System;
using System.Collections.Generic;
using System.Linq;
using CsharpGalexy.LibraryExtention.Models.IO;
using Microsoft.Win32;

namespace CsharpGalexy.LibraryExtention.IO;

public static class RegistryExtentions
{
  
    public static List<AppInfo> GetInstalledSoftware()
    {
        string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        List<AppInfo> installedApps = new List<AppInfo>();
        using RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);
        
        key?.GetSubKeyNames()
                .Select(a => new { a, r = key?.OpenSubKey(a) })
                .Select(@t => new AppInfo
                {
                    Application = @t?.r?.GetValue("DisplayName")?.ToString(),
                    InstallLocation = @t?.r?.GetValue("InstallLocation")?.ToString()
                })
            .ToList()
            .FindAll(c => c.Application != null)
            .ForEach(c => installedApps.Add(c));

        return installedApps;
    }
    
}