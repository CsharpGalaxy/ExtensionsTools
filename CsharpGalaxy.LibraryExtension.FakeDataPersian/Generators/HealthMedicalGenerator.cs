namespace CsharpGalaxy.LibraryExtension.FakeDataPersian.Generators;

public static class HealthMedicalGenerator
{
    private static readonly Random Random = new();

    private static readonly string[] BloodTypes = new[] { "O+", "O-", "A+", "A-", "B+", "B-", "AB+", "AB-" };

    private static readonly string[] CommonDiseases = new[]
    {
        "دیابت", "فشار خون بالا", "کلسترول بالا", "بیماری قلبی", "آسم", "اختلال تیروئید",
        "بیماری کبدی", "بیماری کلیوی", "بیماری ریوی", "ضایعات خاموشی"
    };

    private static readonly string[] Medications = new[]
    {
        "متفورمین", "آملودیپین", "سیمواستاتین", "اسپیرین", "ورساتیون", "لاووکسیتین",
        "سالبوتامول", "لاوتیروکسین", "فوروسمید", "کاپتوپریل"
    };

    private static readonly string[] Allergies = new[]
    {
        "آلرژی به پنی‌سیلین", "آلرژی به آفتاب", "آلرژی به گوجه‌فرنگی", "آلرژی به آجیل",
        "آلرژی به گلوتن", "آلرژی به شیر", "آلرژی به تخم‌مرغ", "آلرژی به میگو"
    };

    private static readonly string[] Specialties = new[]
    {
        "پزشک عمومی", "متخصص قلب", "متخصص مغز و اعصاب", "متخصص انکولوژی",
        "متخصص ارتوپدی", "متخصص چشم", "متخصص گوش و حلق و بینی", "دندان‌پزشک"
    };

    /// <summary>
    /// گروه خونی تصادفی را برمی‌گرداند
    /// </summary>
    public static string BloodType() => BloodTypes[Random.Next(BloodTypes.Length)];

    /// <summary>
    /// قد (بر حسب سانتی‌متر) را برمی‌گرداند
    /// </summary>
    public static int Height() => Random.Next(150, 200);

    /// <summary>
    /// وزن (بر حسب کیلوگرم) را برمی‌گرداند
    /// </summary>
    public static int Weight() => Random.Next(45, 120);

    /// <summary>
    /// شاخص توده‌بدن (BMI) را محاسبه می‌کند
    /// </summary>
    public static decimal CalculateBMI(int heightCm, int weightKg)
    {
        double heightM = heightCm / 100.0;
        return Math.Round((decimal)(weightKg / (heightM * heightM)), 2);
    }

    /// <summary>
    /// سطح فشار خون را برمی‌گرداند (Systolic/Diastolic)
    /// </summary>
    public static string BloodPressure()
    {
        int systolic = Random.Next(90, 180); // فشار بالا
        int diastolic = Random.Next(60, 110); // فشار پایین
        return $"{systolic}/{diastolic}";
    }

    /// <summary>
    /// ضربان قلب را برمی‌گرداند (BPM)
    /// </summary>
    public static int HeartRate() => Random.Next(60, 100);

    /// <summary>
    /// سطح اکسیژن خون را برمی‌گرداند
    /// </summary>
    public static int BloodOxygenLevel() => Random.Next(95, 100);

    /// <summary>
    /// درجهٔ حرارت بدن را برمی‌گرداند (سلسیوس)
    /// </summary>
    public static decimal BodyTemperature()
    {
        return Math.Round(36.5m + (decimal)(Random.NextDouble() * 2.5 - 1.25), 1);
    }

    /// <summary>
    /// بیماری شایع تصادفی را برمی‌گرداند
    /// </summary>
    public static string CommonDisease() => CommonDiseases[Random.Next(CommonDiseases.Length)];

    /// <summary>
    /// دارو تصادفی را برمی‌گرداند
    /// </summary>
    public static string Medication() => Medications[Random.Next(Medications.Length)];

    /// <summary>
    /// آلرژی تصادفی را برمی‌گرداند
    /// </summary>
    public static string Allergy() => Allergies[Random.Next(Allergies.Length)];

    /// <summary>
    /// تخصص پزشک تصادفی را برمی‌گرداند
    /// </summary>
    public static string DoctorSpecialty() => Specialties[Random.Next(Specialties.Length)];

    /// <summary>
    /// شماره پروانهٔ پزشک را برمی‌گرداند
    /// </summary>
    public static string DoctorLicenseNumber()
    {
        return $"MED-{Random.Next(100000, 999999)}";
    }

    /// <summary>
    /// شماره پرونده‌های بیمار را برمی‌گرداند
    /// </summary>
    public static string PatientFileNumber()
    {
        return $"PAT-{DateTime.Now.Year}-{Random.Next(100000, 999999)}";
    }

    /// <summary>
    /// شماره بیمهٔ سلامت را برمی‌گرداند
    /// </summary>
    public static string HealthInsuranceNumber()
    {
        return string.Concat(Enumerable.Range(0, 13).Select(_ => Random.Next(0, 10)));
    }

    /// <summary>
    /// شماره نسخهٔ پزشکی را برمی‌گرداند
    /// </summary>
    public static string PrescriptionNumber()
    {
        return $"RX-{DateTime.Now:yyyyMMdd}-{Random.Next(10000, 99999)}";
    }

    /// <summary>
    /// دوز دارو را برمی‌گرداند (mg)
    /// </summary>
    public static int MedicationDose()
    {
        int[] commonDoses = { 125, 250, 500, 1000, 2000 };
        return commonDoses[Random.Next(commonDoses.Length)];
    }

    /// <summary>
    /// فرکانس مصرف دارو را برمی‌گرداند
    /// </summary>
    public static string MedicationFrequency()
    {
        string[] frequencies = { "یک‌بار روزی", "دو‌بار روزی", "سه‌بار روزی", "هر ۶ ساعت", "هر ۸ ساعت" };
        return frequencies[Random.Next(frequencies.Length)];
    }

    /// <summary>
    /// مدت درمان را برمی‌گرداند (روز)
    /// </summary>
    public static int TreatmentDuration() => Random.Next(3, 90);

    /// <summary>
    /// شماره بخش بیمارستان را برمی‌گرداند
    /// </summary>
    public static string HospitalDepartmentCode()
    {
        return $"DEPT-{Random.Next(100, 999)}";
    }
}
