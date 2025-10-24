using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ProvinceCapital
{
    public string ProvinceId { get; set; } = string.Empty;
    public string ProvinceName { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
}

public static class ProvinceCapitalHelper
{
    private static Task<List<ProvinceCapital>>? _provincesTask;

    private static string JsonFileUrl =>
        "https://raw.githubusercontent.com/CsharpGalexy/ExtensionsTools/main/CsharpGalexy.LibraryExtention.Data/Iran/Provinces/province-capitals.json";

    /// <summary>
    /// بارگذاری اولیه و کش کردن داده‌ها
    /// </summary>
    public static Task InitializeAsync()
    {
        if (_provincesTask == null)
        {
            _provincesTask = LoadFromJsonAsync();
        }

        return _provincesTask;
    }

    public static async Task<List<ProvinceCapital>> LoadFromJsonAsync()
    {
        using var httpClient = new HttpClient();

        try
        {
            var json = await httpClient.GetStringAsync(JsonFileUrl);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var provinces = JsonSerializer.Deserialize<List<ProvinceCapital>>(json, options);
            return provinces ?? new List<ProvinceCapital>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
        }
    }

    public static async Task<IReadOnlyList<ProvinceCapital>> GetAllProvincesAsync()
    {
        if (_provincesTask == null)
            await InitializeAsync();

        return (await _provincesTask!).AsReadOnly();
    }

    public static async Task<string?> GetCapitalByProvinceIdAsync(string provinceId)
    {
        var provinces = await GetAllProvincesAsync();
        return provinces.FirstOrDefault(p => p.ProvinceId == provinceId)?.Capital;
    }

    public static async Task<string?> GetCapitalByProvinceNameAsync(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            return null;

        var provinces = await GetAllProvincesAsync();
        return provinces.FirstOrDefault(p =>
            string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase))?.Capital;
    }

    public static async Task<string?> GetProvinceNameByCapitalAsync(string capitalName)
    {
        if (string.IsNullOrWhiteSpace(capitalName))
            return null;

        var provinces = await GetAllProvincesAsync();
        return provinces.FirstOrDefault(p =>
            string.Equals(p.Capital, capitalName, StringComparison.OrdinalIgnoreCase))?.ProvinceName;
    }

    public static async Task<bool> ExistsByProvinceIdAsync(string provinceId)
    {
        var provinces = await GetAllProvincesAsync();
        return provinces.Any(p => p.ProvinceId == provinceId);
    }

    public static async Task<bool> ExistsByProvinceNameAsync(string provinceName)
    {
        if (string.IsNullOrWhiteSpace(provinceName))
            return false;

        var provinces = await GetAllProvincesAsync();
        return provinces.Any(p =>
            string.Equals(p.ProvinceName, provinceName, StringComparison.OrdinalIgnoreCase));
    }
}
