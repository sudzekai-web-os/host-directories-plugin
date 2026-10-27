using Application;
using Application.Types.Exceptions;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;
using System.Net;
using System.Text.Json.Serialization.Metadata;

namespace Presentation.Services
{
    internal class HandlerFuncExecutor
    {
        public async Task<HandlerResult> SafeExecute<TResult>(
            ILogger logger,
            Func<Task<TResult>> func,
            JsonTypeInfo jsonTypeInfo,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            try
            {
                var result = await func();

                return HandlerResult.Create(statusCode).WithData(result).AsJson(jsonTypeInfo);
            }
            catch (BusinessException ex)
            {
                logger.LogError("{ex}", ex.Message);
                return HandlerResult.Create((HttpStatusCode)ex.StatusCode).WithData(ex.BusinessMessage).AsJson(PluginJsonContext.Default.String);
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);
                return HandlerResult.Create(HttpStatusCode.InternalServerError).WithData("Внутренняя ошибка сервера").AsJson(PluginJsonContext.Default.String);
            }
        }

        public async Task<HandlerResult> SafeExecute(
            ILogger logger,
            Func<Task<Unit>> action)
        {
            try
            {
                await action();

                return HandlerResult.Create(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                logger.LogError("{ex}", ex.Message);
                return HandlerResult.Create((HttpStatusCode)ex.StatusCode).WithData(ex.BusinessMessage).AsJson(PluginJsonContext.Default.String);
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);
                return HandlerResult.Create(HttpStatusCode.InternalServerError).WithData("Внутренняя ошибка сервера").AsJson(PluginJsonContext.Default.String);
            }
        }
    }
}
