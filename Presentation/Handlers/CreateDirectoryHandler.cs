using Application.Dispatching.Commands;
using Application.Dispatching.Dispatchers;
using Application.Dto.Directory.Info;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Presentation.Dto.Requests;
using Presentation.Services;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;
using System.Text.Json;

namespace Presentation.Handlers
{
    internal class CreateDirectoryHandler(
        ICommandDispatcher dispatcher,
        ILogger<CreateDirectoryHandler> logger,
        HandlerFuncExecutor executor) : IEndpointHandler
    {
        public string Pattern => "directory";

        public HandlerHttpMethod HttpMethod => HandlerHttpMethod.Post;

        public async Task<HandlerResult> HandleAsync(HandlerRequest request)
        {
            CreateDirectoryRequest? body;

            try
            {
                body = JsonSerializer.Deserialize(request.Body, PluginJsonContext.Default.CreateDirectoryRequest);
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
                async () => await dispatcher.DispatchAsync<CreateDirectoryCommand, DirectoryGeneralInfoDto>(new(body.FullName)),
                PluginJsonContext.Default.DirectoryGeneralInfoDto
            );
        }

        public HandlerOpenApiInfo? OpenApiInfo { get; } = new(
            "directory-create",
            "directory-create",
            ["directory"],
            "Создание директории"
        )
        {
            RequestBody = new(PluginJsonContext.Default.CreateDirectoryRequest),
            Responses = [
                new(200, PluginJsonContext.Default.DirectoryGeneralInfoDto),
                new(400, PluginJsonContext.Default.String)
                ]
        };

        public HandlerFilteringInfo? FilteringInfo { get; } = new(true);
    }
}
