using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CsharpGalexy.Console.Models;

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