using DiscussionZone.Application.Enums;

namespace DiscussionZone.Application.DTOs
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }

        public BaseResponse()
        {

        }

        public BaseResponse(T? data, string status, string? message)
        {
            Data = data;
            Status = status;
            Message = message;
        }

        public static BaseResponse<T> Success(T data, string? message = "")
        {
            return new BaseResponse<T>(data, ResponseStatus.Success.ToString(), message);
        }
        public static BaseResponse<T> Error(string message)
        {
            return new BaseResponse<T>(default, ResponseStatus.Fail.ToString(), message);
        }
        public static BaseResponse<T> Warning(T data,string message)
        {
            return new BaseResponse<T>(data,ResponseStatus.Warning.ToString(), message);
        }
        public static BaseResponse<T> Info(T data, string message)
        {
            return new BaseResponse<T>(data, ResponseStatus.Info.ToString(), message);
        }

    }
}
