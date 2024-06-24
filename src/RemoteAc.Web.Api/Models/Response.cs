namespace RemoteAc.Web.Api.Models;

public class Response<T>
{
    public Response() { }
    public Response(T data)
    {
        Success = true;
        Message = string.Empty;
        Errors = null;
        Data = data;
    }
    public T Data { get; set; }
    public string[] Errors { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}
