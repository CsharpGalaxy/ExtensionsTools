namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class ImageGenerator
{
    /// <summary>
    /// تصویر پلیسهولدر ۱۰۰x۱۰۰ (پیکسل) را به صورت Base64 برمی‌گرداند
    /// رنگ تصادفی
    /// </summary>
    public static string PlaceholderBase64(int width = 100, int height = 100)
    {
        var random = new Random();
        byte r = (byte)random.Next(256);
        byte g = (byte)random.Next(256);
        byte b = (byte)random.Next(256);

        // ساده‌ترین تصویر BMP ممکن
        return GenerateSimpleBitmapBase64(width, height, r, g, b);
    }

    /// <summary>
    /// تصویر پروفایل مرد (Avatar) را به صورت Base64 برمی‌گرداند
    /// </summary>
    public static string MaleAvatarBase64()
    {
        // یک SVG ساده برای مرد
        string svg = @"
<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'>
  <circle cx='50' cy='30' r='20' fill='#8B4513'/>
  <path d='M 20 60 Q 20 50 30 45 L 70 45 Q 80 50 80 60 L 80 85 Q 80 95 70 95 L 30 95 Q 20 95 20 85 Z' fill='#4169E1'/>
</svg>";
        return SvgToBase64(svg);
    }

    /// <summary>
    /// تصویر پروفایل زن (Avatar) را به صورت Base64 برمی‌گرداند
    /// </summary>
    public static string FemaleAvatarBase64()
    {
        // یک SVG ساده برای زن
        string svg = @"
<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'>
  <circle cx='50' cy='30' r='20' fill='#CD853F'/>
  <path d='M 20 60 Q 20 50 30 45 L 70 45 Q 80 50 80 60 L 80 85 Q 80 95 70 95 L 30 95 Q 20 95 20 85 Z' fill='#FF69B4'/>
</svg>";
        return SvgToBase64(svg);
    }

    /// <summary>
    /// تصویر QR Code ساده را به صورت Base64 برمی‌گرداند
    /// نوت: این یک نقطه‌بندی ساده است، نه QR واقعی
    /// </summary>
    public static string SimpleQRCodeBase64(string text)
    {
        // تولید الگوی ساده بر اساس متن
        int hash = text.GetHashCode();
        string pattern = Convert.ToString(Math.Abs(hash), 2).PadLeft(32, '0');

        var svg = new System.Text.StringBuilder();
        svg.AppendLine(@"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'>");
        svg.AppendLine("<rect width='160' height='160' fill='white'/>");

        int size = 8;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == '1')
            {
                int x = (i % 8) * size;
                int y = (i / 8) * size;
                svg.AppendLine($"<rect x='{x}' y='{y}' width='{size}' height='{size}' fill='black'/>");
            }
        }

        svg.AppendLine("</svg>");
        return SvgToBase64(svg.ToString());
    }

    /// <summary>
    /// نمودار ستونی ساده را به صورت Base64 برمی‌گرداند
    /// </summary>
    public static string SimpleChartBase64(int[] values, string[] labels)
    {
        if (values.Length == 0 || values.Length != labels.Length)
            return "";

        int maxValue = values.Max();
        int width = 200 + (values.Length * 30);
        int height = 200;

        var svg = new System.Text.StringBuilder();
        svg.AppendLine($@"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {width} {height}'>");
        svg.AppendLine("<rect width='" + width + "' height='" + height + "' fill='white' stroke='black'/>");

        // محور‌ها
        svg.AppendLine("<line x1='30' y1='150' x2='" + (width - 10) + "' y2='150' stroke='black' stroke-width='2'/>");
        svg.AppendLine("<line x1='30' y1='20' x2='30' y2='150' stroke='black' stroke-width='2'/>");

        // ستون‌ها
        int barWidth = 20;
        int spacing = 30;

        for (int i = 0; i < values.Length; i++)
        {
            int barHeight = (int)((values[i] / (double)maxValue) * 120);
            int x = 40 + (i * spacing);
            int y = 150 - barHeight;

            // رنگ‌های مختلف برای ستون‌های مختلف
            string color = GetColorForIndex(i);

            svg.AppendLine($"<rect x='{x}' y='{y}' width='{barWidth}' height='{barHeight}' fill='{color}'/>");
            svg.AppendLine($"<text x='{x + barWidth / 2}' y='165' text-anchor='middle' font-size='10'>{labels[i]}</text>");
        }

        svg.AppendLine("</svg>");
        return SvgToBase64(svg.ToString());
    }

    /// <summary>
    /// تصویر شطرنجی تصادفی را برمی‌گرداند
    /// </summary>
    public static string RandomCheckerboardBase64(int gridSize = 8)
    {
        var random = new Random();
        int cellSize = 20;
        int size = gridSize * cellSize;

        var svg = new System.Text.StringBuilder();
        svg.AppendLine($@"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {size} {size}'>");

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                string color = random.Next(2) == 0 ? "black" : "white";
                int x = col * cellSize;
                int y = row * cellSize;

                svg.AppendLine($"<rect x='{x}' y='{y}' width='{cellSize}' height='{cellSize}' fill='{color}' stroke='gray'/>");
            }
        }

        svg.AppendLine("</svg>");
        return SvgToBase64(svg.ToString());
    }

    // متدهای کمکی
    private static string SvgToBase64(string svg)
    {
        byte[] svgBytes = System.Text.Encoding.UTF8.GetBytes(svg);
        return "data:image/svg+xml;base64," + Convert.ToBase64String(svgBytes);
    }

    private static string GenerateSimpleBitmapBase64(int width, int height, byte r, byte g, byte b)
    {
        // تولید یک BMP ساده
        using (var memoryStream = new System.IO.MemoryStream())
        {
            // BMP Header (ساده‌شده)
            var bmpData = new byte[width * height * 3];

            for (int i = 0; i < bmpData.Length; i += 3)
            {
                bmpData[i] = b;
                bmpData[i + 1] = g;
                bmpData[i + 2] = r;
            }

            memoryStream.Write(bmpData, 0, bmpData.Length);
            byte[] imageData = memoryStream.ToArray();

            return "data:image/bmp;base64," + Convert.ToBase64String(imageData);
        }
    }

    private static string GetColorForIndex(int index)
    {
        string[] colors = { "#FF6B6B", "#4ECDC4", "#45B7D1", "#FFA07A", "#98D8C8", "#F7DC6F" };
        return colors[index % colors.Length];
    }
}
