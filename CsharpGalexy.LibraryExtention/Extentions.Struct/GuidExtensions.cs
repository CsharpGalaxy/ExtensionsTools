using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Text;


    /// <summary>
    /// Provides extension methods for the <see cref="Guid"/> structure to enhance functionality and usability.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Converts the <see cref="Guid"/> to a shorter, URL-friendly string using Base64 encoding (with URL-safe characters).
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> instance to convert.</param>
        /// <returns>A URL-safe Base64 string representation of the <see cref="Guid"/>.</returns>
        /// <example>
        /// <code>
        /// var guid = Guid.NewGuid();
        /// string shortGuid = guid.ToShortString(); // e.g., "XyZ12-AbC34dEfG56H"
        /// </code>
        /// </example>
        public static string ToShortString(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            var base64 = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
            return base64;
        }

        /// <summary>
        /// Converts a URL-safe Base64 string back to a <see cref="Guid"/>.
        /// </summary>
        /// <param name="shortString">The URL-safe Base64 string to convert.</param>
        /// <returns>The reconstructed <see cref="Guid"/> instance.</returns>
        /// <exception cref="ArgumentException">Thrown when the input string is not a valid short GUID format.</exception>
        /// <example>
        /// <code>
        /// string shortGuid = "XyZ12-AbC34dEfG56H";
        /// Guid guid = shortGuid.ToGuid(); // Reconstructs original Guid
        /// </code>
        /// </example>
        public static Guid ToGuid(this string shortString)
        {
            if (string.IsNullOrWhiteSpace(shortString))
                throw new ArgumentException("Short string cannot be null or empty.", nameof(shortString));

            var base64 = shortString
                .Replace('-', '+')
                .Replace('_', '/');

            // Pad with '=' to make length multiple of 4
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            try
            {
                var bytes = Convert.FromBase64String(base64);
                return new Guid(bytes);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid short GUID format.", nameof(shortString), ex);
            }
        }

        /// <summary>
        /// Checks whether the <see cref="Guid"/> is empty (i.e., <see cref="Guid.Empty"/>).
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> instance to check.</param>
        /// <returns><c>true</c> if the <see cref="Guid"/> is empty; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var guid = Guid.Empty;
        /// bool isEmpty = guid.IsEmpty(); // returns true
        /// </code>
        /// </example>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// Returns a human-readable string representation of the <see cref="Guid"/> without hyphens.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> instance to format.</param>
        /// <returns>A 32-character lowercase hexadecimal string without hyphens.</returns>
        /// <example>
        /// <code>
        /// var guid = new Guid("123e4567-e89b-12d3-a456-426614174000");
        /// string clean = guid.ToCleanString(); // "123e4567e89b12d3a456426614174000"
        /// </code>
        /// </example>
        public static string ToCleanString(this Guid guid)
        {
            return guid.ToString("N");
        }

        /// <summary>
        /// Generates a new <see cref="Guid"/> based on the current <see cref="Guid"/> and a given string (deterministic).
        /// Useful for generating consistent child GUIDs.
        /// </summary>
        /// <param name="guid">The parent <see cref="Guid"/>.</param>
        /// <param name="salt">A string to combine with the parent GUID for deterministic generation.</param>
        /// <returns>A new deterministic <see cref="Guid"/> based on the parent and salt.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="salt"/> is null.</exception>
        /// <example>
        /// <code>
        /// var parent = Guid.NewGuid();
        /// var child = parent.DeriveGuid("UserProfile");
        /// </code>
        /// </example>
        public static Guid DeriveGuid(this Guid guid, string salt)
        {
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));

            using var sha1 = System.Security.Cryptography.SHA1.Create();
            var guidBytes = guid.ToByteArray();
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var combined = new byte[guidBytes.Length + saltBytes.Length];

            Buffer.BlockCopy(guidBytes, 0, combined, 0, guidBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combined, guidBytes.Length, saltBytes.Length);

            var hash = sha1.ComputeHash(combined);
            var guidHash = new byte[16];
            Buffer.BlockCopy(hash, 0, guidHash, 0, 16); // Take first 16 bytes

            return new Guid(guidHash);
        }

        /// <summary>
        /// Attempts to parse a string into a <see cref="Guid"/>, returning <c>false</c> if parsing fails.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="Guid"/> if successful; otherwise, <see cref="Guid.Empty"/>.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// string input = "123e4567-e89b-12d3-a456-426614174000";
        /// if (input.TryParseGuid(out var guid))
        /// {
        ///     Console.WriteLine($"Parsed: {guid}");
        /// }
        /// </code>
        /// </example>
        public static bool TryParseGuid(this string input, out Guid result)
        {
            return Guid.TryParse(input, out result);
        }

    /// <summary>
    /// Returns the current <see cref="Guid"/> if it is not empty; otherwise, returns <see cref="Guid.Empty"/>.
    /// Useful in LINQ or conditional assignments to avoid empty GUIDs.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> instance to check.</param>
    /// <returns>The original <see cref="Guid"/> if not empty; otherwise, <see cref="Guid.Empty"/>.</returns>
    /// <example>
    /// <code>
    /// Guid? userInput = null;
    /// Guid safeGuid = userInput.GetValueOrDefault().OrDefault(); // returns Guid.Empty
    /// </code>
    /// </example>
    public static Guid OrDefault(this Guid guid)
    {
        return guid.IsEmpty() ? default : guid;
    }

    /// <summary>
    /// Returns the current <see cref="Guid"/> if it is not empty; otherwise, returns the specified default value.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> instance to check.</param>
    /// <param name="defaultValue">The value to return if the <see cref="Guid"/> is empty.</param>
    /// <returns>The original <see cref="Guid"/> if not empty; otherwise, <paramref name="defaultValue"/>.</returns>
    /// <example>
    /// <code>
    /// Guid userProvided = Guid.Empty;
    /// Guid fallback = new Guid("12345678-1234-1234-1234-123456789012");
    /// Guid result = userProvided.OrDefault(fallback); // returns fallback GUID
    /// </code>
    /// </example>
    public static Guid OrDefault(this Guid guid, Guid defaultValue)
    {
        return guid.IsEmpty() ? defaultValue : guid;
    }

    /// <summary>
    /// Extension for <see cref="Guid?"/> (Nullable Guid) that returns the value if has value and is not empty; 
    /// otherwise, returns <see cref="Guid.Empty"/>.
    /// </summary>
    /// <param name="guid">The nullable <see cref="Guid"/> to check.</param>
    /// <returns>The unwrapped <see cref="Guid"/> if it has value and is not empty; otherwise, <see cref="Guid.Empty"/>.</returns>
    /// <example>
    /// <code>
    /// Guid? possibleNull = null;
    /// Guid safe = possibleNull.OrDefault(); // returns Guid.Empty
    /// </code>
    /// </example>
    public static Guid OrDefault(this Guid? guid)
    {
        return guid.HasValue && !guid.Value.IsEmpty() ? guid.Value : default;
    }

    /// <summary>
    /// Extension for <see cref="Guid?"/> (Nullable Guid) that returns the value if has value and is not empty; 
    /// otherwise, returns the specified default value.
    /// </summary>
    /// <param name="guid">The nullable <see cref="Guid"/> to check.</param>
    /// <param name="defaultValue">The value to return if <see cref="Guid"/> is null or empty.</param>
    /// <returns>The unwrapped <see cref="Guid"/> if valid; otherwise, <paramref name="defaultValue"/>.</returns>
    /// <example>
    /// <code>
    /// Guid? userInput = Guid.Empty;
    /// Guid fallback = Guid.NewGuid();
    /// Guid result = userInput.OrDefault(fallback); // returns fallback
    /// </code>
    /// </example>
    public static Guid OrDefault(this Guid? guid, Guid defaultValue)
    {
        return guid.HasValue && !guid.Value.IsEmpty() ? guid.Value : defaultValue;
    }
}
