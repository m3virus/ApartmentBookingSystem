﻿using System.Runtime.InteropServices;
using ABS.Back.Application.Abstractions.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ABS.Back.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        
        try
        {
            logger.LogInformation("Executing command {Command}", name);
            var result = await next();

            logger.LogInformation("Command {Command} Processed successfully", name);
            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Command {Command} processing failed", name);

            throw;
        }
    }
}