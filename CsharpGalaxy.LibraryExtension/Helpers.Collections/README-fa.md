# 📚 مستندات کلاس ListHelper

## 📋 **عملیات پایه**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `IsNullOrEmpty<T>` | بررسی خالی یا null بودن لیست | `list: IList<T>` | `bool` |
| `IsNotEmpty<T>` | بررسی وجود آیتم در لیست | `list: IList<T>` | `bool` |
| `SizeOf<T>` | دریافت اندازه لیست (به صورت امن) | `list: IList<T>` | `int` |
| `GetFirst<T>` | دریافت اولین آیتم یا مقدار پیش‌فرض | `list: IList<T>` | `T` |
| `GetLast<T>` | دریافت آخرین آیتم یا مقدار پیش‌فرض | `list: IList<T>` | `T` |
| `GetOrElse<T>` | دریافت آیتم در ایندکس مشخص یا مقدار پیش‌فرض | `list: IList<T>, index: int, defaultValue: T` | `T` |
| `GetRandom<T>` | دریافت یک آیتم تصادفی از لیست | `list: IList<T>` | `T` |

## 🔄 **عملیات تغییر لیست**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `SetSafe<T>` | تنظیم مقدار در ایندکس با گسترش خودکار لیست | `list: List<T>, index: int, value: T` | `void` |
| `AddIfAbsent<T>` | افزودن آیتم در صورت عدم وجود | `list: List<T>, item: T` | `bool` |
| `AddAllIfAbsent<T>` | افزودن چند آیتم در صورت عدم وجود | `list: List<T>, items: IEnumerable<T>` | `void` |
| `RemoveFirst<T>` | حذف و بازگرداندن اولین آیتم | `list: List<T>` | `T` |
| `RemoveLast<T>` | حذف و بازگرداندن آخرین آیتم | `list: List<T>` | `T` |
| `RemoveNulls<T>` | حذف تمام آیتم‌های null | `list: List<T>` | `void` |
| `RemoveDuplicates<T>` | حذف آیتم‌های تکراری | `list: List<T>` | `void` |
| `RemoveByIndex<T>` | حذف آیتم‌ها در ایندکس‌های مشخص | `list: List<T>, indices: int[]` | `void` |

## 🔀 **تغییر ساختار لیست**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Swap<T>` | جابجایی دو عنصر | `list: IList<T>, i: int, j: int` | `void` |
| `Reverse<T>` | معکوس کردن ترتیب لیست | `list: IList<T>` | `void` |
| `Shuffle<T>` | بر هم زدن تصادفی ترتیب | `list: IList<T>` | `void` |
| `RotateLeft<T>` | چرخش به چپ به تعداد n خانه | `list: IList<T>, n: int` | `void` |
| `RotateRight<T>` | چرخش به راست به تعداد n خانه | `list: IList<T>, n: int` | `void` |

## 📊 **عملیات مرتب‌سازی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `SortNatural<T>` | مرتب‌سازی طبیعی | `list: IList<T>` where T : IComparable<T> | `void` |
| `SortReverse<T>` | مرتب‌سازی معکوس | `list: IList<T>` where T : IComparable<T> | `void` |
| `SortBy<T>` | مرتب‌سازی با تابع مقایسه سفارشی | `list: IList<T>, comparison: Comparison<T>` | `void` |

## 📈 **عملیات تجمعی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Min<T>` | یافتن کمترین مقدار | `source: IEnumerable<T>` where T : IComparable<T> | `T` |
| `Max<T>` | یافتن بیشترین مقدار | `source: IEnumerable<T>` where T : IComparable<T> | `T` |
| `SumInt` | جمع اعداد صحیح | `source: IEnumerable<int>` | `int` |
| `SumLong` | جمع اعداد long | `source: IEnumerable<long>` | `long` |
| `SumDouble` | جمع اعداد اعشاری | `source: IEnumerable<double>` | `double` |
| `Average` | محاسبه میانگین | `source: IEnumerable<double>` | `double` |

## 🔍 **عملیات فیلتر**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Filter<T>` | فیلتر آیتم‌ها با شرط | `source: IEnumerable<T>, predicate: Func<T,bool>` | `List<T>` |
| `FilterNot<T>` | فیلتر آیتم‌هایی که شرط را نقض می‌کنند | `source: IEnumerable<T>, predicate: Func<T,bool>` | `List<T>` |
| `FilterNotNull<T>` | حذف آیتم‌های null | `source: IEnumerable<T>` | `List<T>` |

## 🎯 **عملیات نگاشت**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Map<T,TResult>` | نگاشت آیتم‌ها با تابع تبدیل | `source: IEnumerable<T>, selector: Func<T,TResult>` | `List<TResult>` |
| `MapIndexed<T,TResult>` | نگاشت آیتم‌ها به همراه ایندکس | `source: IEnumerable<T>, selector: Func<int,T,TResult>` | `List<TResult>` |
| `MapNotNull<T,TResult>` | نگاشت و فیلتر نتایج null | `source: IEnumerable<T>, selector: Func<T,TResult>` | `List<TResult>` |
| `FlatMap<T,TResult>` | تخت کردن کالکشن‌های تو در تو | `source: IEnumerable<T>, selector: Func<T,IEnumerable<TResult>>` | `List<TResult>` |

## 🎭 **عملیات مجموعه‌ای**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Intersect<T>` | اشتراک دو مجموعه | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `Union<T>` | اجتماع دو مجموعه | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `Subtract<T>` | تفاضل دو مجموعه | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |
| `SymmetricDiff<T>` | تفاضل متقارن دو مجموعه | `first: IEnumerable<T>, second: IEnumerable<T>` | `List<T>` |

## 🔄 **تبدیل نوع**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `ToArray<T>` | تبدیل به آرایه | `source: IEnumerable<T>` | `T[]` |
| `ToSet<T>` | تبدیل به HashSet | `source: IEnumerable<T>` | `HashSet<T>` |
| `ToMap<T,TKey>` | تبدیل به Dictionary | `source: IEnumerable<T>, keySelector: Func<T,TKey>` | `Dictionary<TKey,T>` |
| `ToImmutableList<T>` | ایجاد لیست فقط‌خواندنی | `source: IEnumerable<T>` | `IReadOnlyList<T>` |

## 📦 **تقسیم‌بندی و پنجره‌سازی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Chunked<T>` | تقسیم به قطعات با اندازه ثابت | `source: IEnumerable<T>, size: int` | `List<List<T>>` |
| `Windowed<T>` | ایجاد پنجره‌های متحرک | `source: IEnumerable<T>, size: int, step: int` | `List<List<T>>` |
| `Slice<T>` | دریافت بخشی از لیست | `source: IList<T>, start: int, end: int?` | `List<T>` |

## 🔒 **امنیت چندنخی**
| متد | توضیحات | پارامترها | نوع خروجی |
|-----|----------|-----------|------------|
| `Synchronize<T>` | ایجاد پوشش thread-safe | `list: IList<T>` | `IList<T>` |

نکته: برای امنیت چندنخی بهتر، استفاده از `ConcurrentBag<T>` یا مکانیزم‌های قفل‌گذاری صریح توصیه می‌شود.