using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



public class HashingHelper
{
    // تابع تولید Salt تصادفی
    public static string GenerateSalt(int size = 16)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }

    // تابع هش‌کردن پسورد + Salt با SHA256
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string saltedPassword = password + salt;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2")); // تبدیل بایت‌ها به رشته‌ی hex
            return builder.ToString();
        }
    }

    public static bool ComparePassword(string password, string newPassword, string sult)
    {
        var newHash = HashPassword(newPassword, sult);
        return newHash.Equals(password);
    }


}
