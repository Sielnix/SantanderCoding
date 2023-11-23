using Santander.Coding.BestStories;

namespace Santander.Coding;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddSantanderCoding(this IServiceCollection services)
    {
        services.AddHttpClient<BestStoriesFetcher>();
        services.AddScoped<IBestStoriesFetcher, BestStoriesFetcher>();

        return services;
    }
}
