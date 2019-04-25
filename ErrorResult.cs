using System.Net;

namespace Optional.Extensions.AspNetCore
{
    public class ErrorResult
    {
        protected ErrorResult(HttpStatusCode  statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public static ErrorResult Unauthorized() => new ErrorResult(HttpStatusCode.Unauthorized);
    }

    public class ErrorResult<T> : ErrorResult
    {
        public T Data { get; }

        private ErrorResult(HttpStatusCode statusCode, T data)
            : base(statusCode)
        {
            Data = data;
        }

        public static ErrorResult<T> NotFound(T data) => new ErrorResult<T>(HttpStatusCode.NotFound, data);

        public static ErrorResult<T> BadRequest(T data) => new ErrorResult<T>(HttpStatusCode.BadRequest, data);

        public static ErrorResult<T> Conflict(T data) => new ErrorResult<T>(HttpStatusCode.Conflict, data);

        public static ErrorResult<T> ServerError(T data) => new ErrorResult<T>(HttpStatusCode.InternalServerError, data);

        public static ErrorResult<T> UnprocessableEntity(T data) => new ErrorResult<T>(HttpStatusCode.UnprocessableEntity, data);
    }

    public static class ErrorTExtensions
    {
        public static ErrorResult<T> ToNotFound<T>(this T data) => ErrorResult<T>.NotFound(data);
        public static ErrorResult<T> ToBadRequest<T>(this T data) => ErrorResult<T>.BadRequest(data);
        public static ErrorResult<T> ToConflict<T>(this T data) => ErrorResult<T>.Conflict(data);
        public static ErrorResult<T> ToServerError<T>(this T data) => ErrorResult<T>.ServerError(data);
        public static ErrorResult<T> ToUnprocessableEntity<T>(this T data) => ErrorResult<T>.UnprocessableEntity(data);
    }
}