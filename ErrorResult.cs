namespace Optional.Extensions.AspNetCore
{
    public enum ErrorCode
    {
        NotFound,
        BadRequest,
        Conflict,
        None
    }

    public class ErrorResult
    {
        public ErrorResult(ErrorCode code)
        {
            Code = code;
        }

        public ErrorCode Code { get; }
    }

    public class ErrorResult<T> : ErrorResult
    {
        public T Data { get; }

        public ErrorResult(ErrorCode code, T data)
            : base(code)
        {
            Data = data;
        }

        public static ErrorResult<T> NotFound(T data) => new ErrorResult<T>(ErrorCode.NotFound, data);

        public static ErrorResult<T> BadRequest(T data) => new ErrorResult<T>(ErrorCode.BadRequest, data);

        public static ErrorResult<T> Conflict(T data) => new ErrorResult<T>(ErrorCode.Conflict, data);
    }

    public static class ErrorTExtensions
    {
        public static ErrorResult<T> ToNotFound<T>(this T data) => ErrorResult<T>.NotFound(data);
        public static ErrorResult<T> ToBadRequest<T>(this T data) => ErrorResult<T>.BadRequest(data);
        public static ErrorResult<T> ToConflict<T>(this T data) => ErrorResult<T>.Conflict(data);
    }
}