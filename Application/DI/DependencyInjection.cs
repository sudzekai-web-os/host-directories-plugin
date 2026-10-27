using Application.Dispatching.Commands;
using Application.Dispatching.Dispatchers;
using Application.Dispatching.Handlers;
using Application.Dto.Directory.Collections;
using Application.Dto.Directory.Info;
using Application.Dto.Directory.Security;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static class DependencyInjection
    {
        extension (IServiceCollection services)
        {
            public IServiceCollection AddApplicationServices()
                => services.AddSingleton<ICommandDispatcher, CommandHandlersDispatcher>()
                           .AddServices()
                           .AddHandlers();
            private IServiceCollection AddHandlers()
                => services.AddScoped<
                                ICommandHandler<GetDirectoryFilesCommand, DirectoryFilesDto>, 
                                GetDirectoryFilesCommandHandler>()
                            .AddScoped<
                                ICommandHandler<GetDirectoryDirectoriesCommand, DirectoryDirectoriesDto>, 
                                GetDirectoryDirectoriesCommandHandler>()
                            .AddScoped<
                                ICommandHandler<GetDirectoryGeneralInfoCommand, DirectoryGeneralInfoDto>, 
                                GetDirectoryGeneralInfoCommandHandler>()
                            .AddScoped<
                                ICommandHandler<GetDirectorySecurityInfoCommand, DirectorySecurityInfoDto>, 
                                GetDirectorySecurityInfoCommandHandler>()
                            .AddScoped<
                                ICommandHandler<CreateDirectoryCommand, DirectoryGeneralInfoDto>, 
                                CreateDirectoryCommandHandler>()
                            .AddScoped<
                                ICommandHandler<DeleteDirectoryCommand, Unit>, 
                                DeleteDirectoryCommandHandler>();
            private IServiceCollection AddServices()
                => services.AddSingleton<DirectoryPathValidator>();
        }
    }
}
