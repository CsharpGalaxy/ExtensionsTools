using System.ComponentModel;

namespace CsharpGalexy.Console.Models;

public enum UserStatus
{
    [Description("کاربر عادی")]
    Regular = 1,

    [Description("مدیر سیستم")]
    Admin = 2,

    //[DisplayName("غیرفعال")]
    [Description("کاربر مسدود شده")]
    Blocked = 3
}
