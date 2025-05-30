﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters;

public class ApiExceptionFiler : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFiler> _logger;

    public ApiExceptionFiler(ILogger<ApiExceptionFiler> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu uma execeção não tratada: Status code 500");
        
        context.Result = new ObjectResult("Ocorreu um problema ao traar a sua solicitação: Status Code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

    }
}

