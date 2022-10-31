using AutoMapper;
using Microsoft.Data.SqlClient;

namespace PVI.GQKN.Infrastructure.Contracts;

public class PagedList<T>
{
    // Tổng số trang
    public int TotalPage { get; private set; }
    // Phần tử / trang
    public int PageSize { get; private set; }
    // Tổng số phần tử
    public int TotalCount { get; private set; }
    //
    public int? NextPageId { get; private set; }

    public List<T> Data { get; private set; }

    public int? PageId { get; private set; }

    public PagedList(List<T> items, 
        int totalCount, 
        int pageSize, 
        int? pageId,
        int? nextPageId)
    {
        Data = items;
        TotalCount = totalCount;
        PageSize = pageSize;
        TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);

        NextPageId = nextPageId;
        PageId = pageId;
    }

    public dynamic ToPage<E>(IMapper mapper)
    {
        return new { 
            TotalCount,
            PageSize,
            TotalPage,
            NextPageId,
            PageId,
            Data = mapper.Map<IEnumerable<E>>(Data)
        };
    }
}