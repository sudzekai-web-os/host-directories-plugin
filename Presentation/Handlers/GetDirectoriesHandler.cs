using Application.Dispatching.Commands;
using Application.Dispatching.Dispatchers;
using Application.Dto.Directory.Collections;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Presentation.Services;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;

namespace Presentation.Handlers
{
    internal class GetDirectoriesHandler(
        ICommandDispatcher dispatcher,
        ILogger<GetDirectoriesHandler> logger,
        HandlerFuncExecutor executor) : IEndpointHandler
    {
        public string Pattern => "directory/directories";

        public HandlerHttpMethod HttpMethod => HandlerHttpMethod.Get;

        public async Task<HandlerResult> HandleAsync(HandlerRequest request)
        {
            var path = request.GetQuery()["path"];

            if (string.IsNullOrWhiteSpace(path))
                return HandlerResult.BadRequest().WithData("Параметр path должен быть указан").AsJson(PluginJsonContext.Default.String);

            return await executor.SafeExecute(
                logger,
                async () => await dispatcher.DispatchAsync<GetDirectoryDirectoriesCommand, DirectoryDirectoriesDto>(new(path)),
                PluginJsonContext.Default.DirectoryDirectoriesDto
            );
        }

        public HandlerOpenApiInfo? OpenApiInfo { get; } = new(
            "directory-directories",
            "directory-directories",
            ["directory"],
            "Список директорий в директории",
            "v1"
        );
    
        public HandlerFilteringInfo? FilteringInfo { get; } = new(true);
    }
}
