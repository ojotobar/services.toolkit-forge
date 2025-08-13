﻿using API.Common.Response.Model.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Services.Toolkit.Forge.Logging.Contracts;
using System.Net;
using System.Text.Json;

namespace Services.Toolkit.Forge.Middlewares
{
    public class UnifiedErrorHandlerMIddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public UnifiedErrorHandlerMIddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleAsync(context, ex);
            }
        }

        private async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                context.Response.StatusCode = contextFeature.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    ForbiddenException => StatusCodes.Status403Forbidden,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                _logger.LogError($"Something went wrong: {contextFeature.Error}");
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = contextFeature.Error?.Message
                }));
            }
        }
    }
}