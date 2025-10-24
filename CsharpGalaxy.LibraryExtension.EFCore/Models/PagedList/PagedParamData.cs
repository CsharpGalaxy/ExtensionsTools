using System.ComponentModel.DataAnnotations;

namespace CsharpGalaxy.LibraryExtension.EFCore.Models.PagedList;

public abstract class PagedParamData
{
  
    private int _pageSize = 50;
    private int _pageNumber = 0;

    [Range(0, int.MaxValue)] 
    public int? PageNumber {
        set
        {
            if (value.HasValue)
                _pageNumber = value.Value;
            else
                _pageNumber = 1;
        }
        get => _pageNumber;
    }
    [Range(1, int.MaxValue)]
    public int? PageSize
    {
        set
        {
            if (value.HasValue && value < _pageSize)
                _pageSize = value.Value;
            else
                _pageSize = 50;
        }
        get => _pageSize;
    }
}
