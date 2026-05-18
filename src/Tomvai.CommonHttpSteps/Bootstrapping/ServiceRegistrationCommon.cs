using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tomvai.CommonHttpSteps.HttpRequest;
using Tomvai.CommonHttpSteps.Placeholders;
using WireMock.Server;

namespace Tomvai.CommonHttpSteps.Bootstrapping;

public static class ServiceRegistrationCommon
{
    public static IServiceCollection AddCommonServices<TProgram>(this IServiceCollection services, IConfiguration configuration)
    where TProgram : class
    {
        services.AddSingleton(configuration);
        services.AddCommonPlaceholders(configuration);
        services.AddCommonHttpClient(configuration);

        services.AddWebApplicationFactory<TProgram>(ConfigurationAccessor.Configuration);
        return services;
    }
    
    
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddCommonPlaceholders(configuration);
        services.AddCommonHttpClient(configuration);
        return services;
    }

    public static IServiceCollection AddCommonPlaceholders(this IServiceCollection services, IConfiguration configuration) 
    {
        services.TryAddTransient(typeof(IPlaceholderReplacer<>), typeof(PlaceholderReplacer<>));

        services.TryAddTransient<IResponsePlaceholderEvaluator, ResponsePlaceholderEvaluator>();
        services.TryAddTransient<IRequestPlaceholderEvaluator, RequestPlaceholderEvaluator>();
        services.TryAddTransient<IUriPlaceholderEvaluator, UriPlaceholderEvaluator>();

        services.AddScoped<IRequestStore, RequestStore>();
        services.AddScoped<IFeatureTestRequestBuilder, FeatureTestRequestBuilder>();
        
        services.AddSingleton(_ => WireMockServer.Start(5999));

        return services;
    }

    public static IServiceCollection AddCommonHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(FeatureTestClientConfiguration.SectionName);
        services.Configure<FeatureTestClientConfiguration>(configurationSection);
        var clientConfiguration = new FeatureTestClientConfiguration();
        configurationSection.Bind(clientConfiguration);

        services.AddHttpClient<IFeatureTestClient, FeatureTestClient>(client =>
        {
            client.BaseAddress = new Uri(clientConfiguration.BaseUri);
        });

        return services;
    }
}