using RemoteAc.Core.Filters;
using RemoteAc.Core.Interfaces.Services;

namespace RemoteAc.Web.Api.Models;

public class PagedResponse<T> : Response<T>
{
    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Success = true;
        Message = string.Empty;
        Errors = null;
    }
    public Uri? FirstPage { get; set; }
    public Uri? LastPage { get; set; }
    public Uri? NextPage { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri? PreviousPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public static PagedResponse<T> Create(T data, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
    {
        var response = new PagedResponse<T>(data, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.NextPage = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
            : null;
        response.PreviousPage = validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
            : null;
        response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
        response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;
        return response;
    }
}
