using System;
using Microsoft.AspNetCore.Mvc;
using Optional;

namespace Optional.Extensions.AspNetCore
{
    public static class OptionToActionResultExtensionsWithTError
    {
        public static IActionResult ToOkOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new OkObjectResult(value) as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new NoContentResult() as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError<TError>(this Option<ErrorResult<TError>> option)
        {
            return option.Match(
                some: error => ToErrorResponse(error),
                none: () => new NoContentResult() as IActionResult);
        }

        private static IActionResult ToErrorResponse<TError>(ErrorResult<TError> errorResult)
        {
            switch (errorResult.Code)
            {
                case ErrorCode.NotFound:
                    return new NotFoundObjectResult(errorResult.Data);
                case ErrorCode.BadRequest:
                    return new BadRequestObjectResult(errorResult.Data);
                case ErrorCode.Conflict:
                    return new ConflictObjectResult(errorResult.Data);
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorResult), errorResult, null);
            }
        }

        public static IActionResult ToCreatedOrError<T, TError>(this Option<T, ErrorResult<TError>> option,
            string routeNameToGetAt, Func<T, object> getRouteData) where T : class
        {
            return option.Match(
                some: value => new CreatedAtRouteResult(routeNameToGetAt, getRouteData(value), value) as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToCreatedOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new ObjectResult(value) { StatusCode = 201 } as IActionResult,
                none: error => ToErrorResponse(error));
        }
    }

    public static class OptionToActionResultExtensions
    {
        public static IActionResult ToOkOrError<T>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new OkObjectResult(value) as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError<T, TError>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new NoContentResult() as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError<TError>(this Option<ErrorResult> option)
        {
            return option.Match(
                some: error => ToErrorResponse(error),
                none: () => new NoContentResult() as IActionResult);
        }

        private static IActionResult ToErrorResponse(ErrorResult errorResult)
        {
            switch (errorResult.Code)
            {
                case ErrorCode.NotFound:
                    return new NotFoundResult();
                case ErrorCode.BadRequest:
                    return new BadRequestResult();
                case ErrorCode.Conflict:
                    return new ConflictResult();
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorResult), errorResult, null);
            }
        }

        public static IActionResult ToCreatedOrError<T>(this Option<T, ErrorResult> option,
            string routeNameToGetAt, Func<T, object> getRouteData) where T : class
        {
            return option.Match(
                some: value => new CreatedAtRouteResult(routeNameToGetAt, getRouteData(value), value) as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToCreatedOrError<T>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new StatusCodeResult(201) as IActionResult,
                none: error => ToErrorResponse(error));
        }
    }
}