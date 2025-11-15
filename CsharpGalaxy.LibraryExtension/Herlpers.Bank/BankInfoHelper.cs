using System.Text.Json;


public class BankInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // دولتی یا خصوصی
    public bool IsActive { get; set; }
}

public static class BankInfoHelper
{
    private static Task<List<BankInfo>>? _banksTask;
    private static string JsonFileUrl =>
        "https://raw.githubusercontent.com/CsharpGalaxy/ExtensionsTools/refs/heads/main/CsharpGalaxy.LibraryExtension.Data/Iran/Provinces/banks.json";
 
    public static Task InitializeAsync()
    {
        if (_banksTask == null)
        {
            _banksTask = LoadFromJsonAsync();
        }

        return _banksTask;
    }

    public static async Task<List<BankInfo>> LoadFromJsonAsync()
    {
        using var httpClient = new HttpClient();

        try
        {
            var json = await httpClient.GetStringAsync(JsonFileUrl);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var banks = JsonSerializer.Deserialize<List<BankInfo>>(json, options);
            return banks ?? new List<BankInfo>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"خطا در بارگذاری JSON از {JsonFileUrl}: {ex.Message}");
        }
    }

    public static async Task<IReadOnlyList<BankInfo>> GetAllBanksAsync()
    {
        if (_banksTask == null)
            await InitializeAsync();

        return (await _banksTask!).AsReadOnly();
    }

    public static async Task<BankInfo?> GetBankByCodeAsync(string code)
    {
        var banks = await GetAllBanksAsync();
        return banks.FirstOrDefault(b => b.Code == code);
    }

    public static async Task<BankInfo?> GetBankByNameAsync(string name)
    {
        var banks = await GetAllBanksAsync();
        return banks.FirstOrDefault(b =>
            string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public static async Task<bool> ExistsByCodeAsync(string code)
    {
        var banks = await GetAllBanksAsync();
        return banks.Any(b => b.Code == code);
    }

    public static async Task<IReadOnlyList<BankInfo>> GetActiveBanksAsync()
    {
        var banks = await GetAllBanksAsync();
        return banks.Where(b => b.IsActive).ToList().AsReadOnly();
    }

    public static async Task<IReadOnlyList<BankInfo>> GetBanksByTypeAsync(string type)
    {
        var banks = await GetAllBanksAsync();
        return banks
            .Where(b => string.Equals(b.Type, type, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .AsReadOnly();
    }
}
