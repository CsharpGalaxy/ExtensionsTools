using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGalexy.LibraryExtention.Models;

public class Person
{
    public string Name { get; set; } = "Unknown";
    public int Age { get; set; }
    // کانسترکتور پیش‌فرض وجود دارد → ✅ کار می‌کند
}
