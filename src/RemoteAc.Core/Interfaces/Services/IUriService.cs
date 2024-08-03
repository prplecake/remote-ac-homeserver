using RemoteAc.Core.Filters;

namespace RemoteAc.Core.Interfaces.Services;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}
