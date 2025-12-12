namespace CsharpGalaxy.LibraryExtension.EFCore.Models.PagedList
{

    public class PagedListInfo
    {
        private int _pageSize = 100;
        private int _pageNumber = 1;

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int? PageNumber
        {
            get => _pageNumber;
            set
            {
                if (value != null)
                {
                    _pageNumber = value.Value;
                }
                else
                {
                    _pageNumber = 1;
                }

            }
        }

        public int? PageSize
        {
            get => _pageSize;
            set
            {
                if (value != null)
                {
                    _pageSize = value.Value;
                }
                else
                {
                    _pageSize = 8;
                }

            }
        }



        /// <summary>
        /// پارامترهای جستجو و فیلتر (کلید-مقدار)
        /// </summary>
        public Dictionary<string, object> Filters { get; set; } = new();

        /// <summary>
        /// اضافه کردن یا تغییر یک فیلتر
        /// </summary>
        public void AddFilter(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            Filters[key] = value;
        }

        /// <summary>
        /// گرفتن مقدار فیلتر
        /// </summary>
        public object GetFilter(string key)
        {
            return Filters.TryGetValue(key, out var value) ? value : null;

        }
    }
}