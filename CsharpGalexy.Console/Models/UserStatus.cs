using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
[Flags]
enum FlagEnum { A = 1, B = 2, C = 4 }
public enum SampleEnum
{
    None = 0,

    [Description("این مقدار اول است")]
    First = 1,

    [Description("این مقدار دوم است")]
    [Display( Name ="مقدار دوم")]
    Second = 2,

    Third = 3
}