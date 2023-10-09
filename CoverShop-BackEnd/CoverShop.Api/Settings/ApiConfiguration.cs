namespace CoverShop.Api.Settings;

public static class ApiConfiguration
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }
}
