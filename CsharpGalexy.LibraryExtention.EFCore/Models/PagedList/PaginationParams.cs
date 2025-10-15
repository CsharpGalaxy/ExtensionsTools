namespace CsharpGalexy.LibraryExtention.EFCore.Models.PagedList
{

    public class PagedListInfo
    {
        private int _pageSize = 100;
        private int _pageNumber=1;

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
    }
}
