namespace Pro.Api.Model.Concrete;

public class BaseResponse<T>
{
    public int StatusCode { get; }
    public string Message { get; }
    public T Data { get; set; }
    public int TotalRecords { get; set; }

    public BaseResponse(int statusCode, string message, dynamic? data = null)
    {
        StatusCode = statusCode;
        Message = message;
        if (data != null)
        {
            Data = data;
        }
    }
}