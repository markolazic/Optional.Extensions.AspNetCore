using System;
using System.Net;
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
                none: ToErrorResponse);
        }

        public static IActionResult ToNoContentOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new NoContentResult() as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToNoContentOrError<TError>(this Option<ErrorResult<TError>> option)
        {
            return option.Match(
                some: ToErrorResponse,
                none: () => new NoContentResult() as IActionResult);
        }

        public static IActionResult ToCreatedOrError<T, TError>(this Option<T, ErrorResult<TError>> option,
            string routeNameToGetAt, Func<T, object> getRouteData) where T : class
        {
            return option.Match(
                some: value => new CreatedAtRouteResult(routeNameToGetAt, getRouteData(value), value) as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToCreatedOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new ObjectResult(value) { StatusCode = 201 } as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToAcceptedOrError<T, TError>(this Option<T, ErrorResult<TError>> option) where T : class
        {
            return option.Match(
                some: value => new ObjectResult(value) { StatusCode = 202 } as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToCreatedOrError<TError>(this Option<ErrorResult<TError>> option)
        {
            return option.Match(
                some: ToErrorResponse,
                none: () => new StatusCodeResult(201) as IActionResult);
        }

        public static IActionResult ToAcceptedOrError<TError>(this Option<ErrorResult<TError>> option)
        {
            return option.Match(
                some: ToErrorResponse,
                none: () => new StatusCodeResult(202) as IActionResult);
        }

        private static IActionResult ToErrorResponse<TError>(ErrorResult<TError> errorResult)
        {
            return new ObjectResult(errorResult.Data)
            {
                StatusCode = (int)errorResult.StatusCode
            };
        }
    }

    public static class OptionToActionResultExtensions
    {
        public static IActionResult ToOkOrError<T>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new OkObjectResult(value) as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToNoContentOrError<T, TError>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new NoContentResult() as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToNoContentOrError<TError>(this Option<ErrorResult> option)
        {
            return option.Match(
                some: ToErrorResponse,
                none: () => new NoContentResult() as IActionResult);
        }

        public static IActionResult ToCreatedOrError<T>(this Option<T, ErrorResult> option,
            string routeNameToGetAt, Func<T, object> getRouteData) where T : class
        {
            return option.Match(
                some: value => new CreatedAtRouteResult(routeNameToGetAt, getRouteData(value), value) as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToCreatedOrError<T>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new StatusCodeResult(201) as IActionResult,
                none: ToErrorResponse);
        }

        public static IActionResult ToAcceptedOrError<T>(this Option<T, ErrorResult> option) where T : class
        {
            return option.Match(
                some: value => new ObjectResult(value) { StatusCode = 202 } as IActionResult,
                none: ToErrorResponse);
        }

        private static IActionResult ToErrorResponse(ErrorResult errorResult)
        {
            return new ObjectResult(null)
            {
                StatusCode = (int)errorResult.StatusCode
            };
        }
    }
}