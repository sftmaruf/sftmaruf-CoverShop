using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace CoverShop.Api.Settings;

public static class AufacConfiguration
{
    public static WebApplicationBuilder AddAutofac(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => 
            containerBuilder.RegisterModule<ApiModule>());

        return builder;
    } 
}
