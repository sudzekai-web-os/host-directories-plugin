using Application;
using Application.Dispatching.Commands;
using Application.Dispatching.Dispatchers;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Presentation.Dto.Requests;
using Presentation.Services;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.Handlers
{
    internal class DeleteDirectoryHandler(
        ICommandDispatcher dispatcher,
        ILogger<CreateDirectoryHandler> logger,
        HandlerFuncExecutor executor) : IEndpointHandler
    {
        public string Pattern => "directory";

        public HandlerHttpMethod HttpMethod => HandlerHttpMethod.Delete;

        public async Task<HandlerResult> HandleAsync(HandlerRequest request)
        {
            DeleteDirectoryRequest? body;

            try
            {
                body = JsonSerializer.Deserialize(request.Body, PluginJsonContext.Default.DeleteDirectoryRequest);
            }
            catch
            {
                logger.LogError("Invalid request body");
                return HandlerResult.BadRequest().WithData("Некорректное тело запроса").AsJson(PluginJsonContext.Default.String);
            }

            if (body is null)
            {
                logger.LogError("Empty request body");
                return HandlerResult.BadRequest().WithData("Тело запроса не может быть пустым").AsJson(PluginJsonContext.Default.String);
            }

            if (string.IsNullOrWhiteSpace(body.FullName))
            {
                logger.LogError("FullName field is required");
                return HandlerResult.BadRequest().WithData("Свойство \"FullName\" должно быть указано").AsJson(PluginJsonContext.Default.String);
            }

            return await executor.SafeExecute(
                logger,
                async () => await dispatcher.DispatchAsync<DeleteDirectoryCommand, Unit>(new(body.FullName, body.IsRecursive))
            );
        }

        public HandlerOpenApiInfo? OpenApiInfo { get; } = new(
            "directory-delete",
            "directory-delete",
            ["directory"],
            "Удаление директории",
            "v1"
        );

        public HandlerFilteringInfo? FilteringInfo { get; } = new(true);
    }
}
