﻿using FurnitureShop.Backend.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Backend.WebApi.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var error = string.Join('\n', context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                
            throw new ValidationException(error);
        }

        await next.Invoke();
    }
}
