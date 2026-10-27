using Application.DI;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Handlers;
using Presentation.Services;
using Sudzekai.WebOs.Core;
using Sudzekai.WebOs.Plugin;
using System.Runtime.InteropServices;

namespace Presentation
{
    public static class Plugin
    {
        private static WebOsPluginBuilder _builder = default!;
        private static WebOsPlugin _plugin = default!;


        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.Main)]
        public static int Main()
        {
            _builder = WebOsPluginBuilder.NewBuilder();

            _builder.SetInformation(new()
            {
                Name = "Directories plugin",
                Version = "0.6.0",
                Summary = "API for directory operations: create, delete, and query"
            });

            _builder.Services.AddApplicationServices();

            _builder.Services.AddScoped<HandlerFuncExecutor>();

            _builder.Services.AddScoped<GetFilesHandler>();
            _builder.Services.AddScoped<GetDirectoriesHandler>();
            _builder.Services.AddScoped<GetDirectoryGeneralInfoHandler>();
            _builder.Services.AddScoped<GetDirectorySecurityInfoHandler>();
            _builder.Services.AddScoped<CreateDirectoryHandler>();
            _builder.Services.AddScoped<DeleteDirectoryHandler>();

            return 0;
        }


        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.DelegateLogging)]
        public static int DelegateLogging(nint logAction)
            => _builder.DelegateLogging(logAction);


        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.Build)]
        public static int Build()
        {
            _plugin = _builder.Build();

            return 0;
        }

        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.GetHandlers)]
        public static unsafe EndpointHandlerDescriptor* GetHandlers(int* count)
            => _plugin.GetHandlers(count);


        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.GetInfo)]
        public static PluginInfoDescriptor GetPluginInfo()
            => _plugin.GetPluginInfo();


        [UnmanagedCallersOnly(EntryPoint = WebOsCoreContext.Exports.Dispose)]
        public static int Dispose()
            => _plugin.Dispose();
    }
}
