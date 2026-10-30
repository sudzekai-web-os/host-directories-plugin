using Application.Dispatching.Commands;
using Application.Dispatching.Dispatchers;
using Application.Dto.Directory.Security;
using Microsoft.Extensions.Logging;
using Presentation.Context;
using Presentation.Services;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;

namespace Presentation.Handlers
{
    internal class GetDirectorySecurityInfoHandler(
        ICommandDispatcher dispatcher,
        ILogger<GetDirectorySecurityInfoHandler> logger,
        HandlerFuncExecutor executor) : IEndpointHandler
    {
        public string Pattern => "directory/security";

        public HandlerHttpMethod HttpMethod => HandlerHttpMethod.Get;

        public async Task<HandlerResult> HandleAsync(HandlerRequest request)
        {
            var path = request.GetQuery()["path"];

            if (string.IsNullOrWhiteSpace(path))
                return HandlerResult.BadRequest().WithData("Параметр path должен быть указан").AsJson(PluginJsonContext.Default.String);

            return await executor.SafeExecute(
                logger,
                async () => await dispatcher.DispatchAsync<GetDirectorySecurityInfoCommand, DirectorySecurityInfoDto>(new(path)),
                PluginJsonContext.Default.WindowsDirectorySecurityInfo
            );
        }

        public HandlerOpenApiInfo? OpenApiInfo { get; } = new(
            "directory-securityInfo",
            "directory-securityInfo",
            ["directory"],
            "Инорфмация о безопасности директории"
        )
        {
            Responses = [
                new(200, PluginJsonContext.Default.WindowsDirectorySecurityInfo),
                new(400, PluginJsonContext.Default.String)
                ]
        };

        public HandlerFilteringInfo? FilteringInfo { get; } = new(true);
    }
}
