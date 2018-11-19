using System;
using Microsoft.AspNetCore.Mvc;
using Optional;

namespace Optional.Extensions.AspNetCore
{
    public static class OptionToActionResultExtensions
    {
        public static IActionResult ToOkOrError<T>(this Option<T, ErrorCode> option) where T : class
        {
            return option.Match(
                some: value => new OkObjectResult(value) as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError<T>(this Option<T, ErrorCode> option) where T : class
        {
            return option.Match(
                some: value => new NoContentResult() as IActionResult,
                none: error => ToErrorResponse(error));
        }

        public static IActionResult ToNoContentOrError(this Option<ErrorCode> option)
        {
            return option.Match(
                some: error => ToErrorResponse(error),
                none: () => new NoContentResult() as IActionResult);
        }

        private static IActionResult ToErrorResponse(ErrorCode error)
        {
            switch (error)
            {
                case ErrorCode.NotFound:
                    return new NotFoundResult();
                case ErrorCode.BadRequest:
                    return new BadRequestResult();
                default:
                    throw new ArgumentOutOfRangeException(nameof(error), error, null);
            }

            ;
        }

        public static IActionResult ToCreatedOrError<T>(this Option<T, ErrorCode> option,
            string routeNameToGetAt, Func<T, object> getRouteData) where T : class
        {
            return option.Match(
                some: value => new CreatedAtRouteResult(routeNameToGetAt, getRouteData(value), value) as IActionResult,
                none: error => ToErrorResponse(error));
        }
    }
}