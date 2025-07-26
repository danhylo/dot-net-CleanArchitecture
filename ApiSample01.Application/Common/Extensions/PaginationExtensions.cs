namespace ApiSample01.Application.Common.Extensions;

public static class PaginationExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int start, int limit)
    {
        var skip = (start - 1) * limit;
        var data = source.Skip(skip).Take(limit);
        return data;
    }
    
    public static (IEnumerable<T> data, int total) PaginateWithTotal<T>(this IEnumerable<T> source, int start, int limit)
    {
        var total = source.Count();
        var skip = (start - 1) * limit;
        var data = source.Skip(skip).Take(limit);
        return (data, total);
    }
}