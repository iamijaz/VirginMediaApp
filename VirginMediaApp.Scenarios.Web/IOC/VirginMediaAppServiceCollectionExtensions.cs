using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VirginMediaApp.Scenarios.Core.Cache;
using VirginMediaApp.Scenarios.Core.Services;

namespace VirginMediaApp.Scenarios.Web.IOC;

public static class VirginMediaAppServiceCollectionExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
    {
        services.TryAddSingleton<IXmlDataLoader, XmlDataLoader>();
        services.TryAddSingleton<IScenariosService, ScenariosService>();
        services.Decorate<IScenariosService, CachedScenariosService>();

        return services;
    }
}