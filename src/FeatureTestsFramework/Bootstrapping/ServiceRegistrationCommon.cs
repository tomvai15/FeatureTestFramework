using FeatureTestsFramework.Client;
using FeatureTestsFramework.HttpRequest;
using FeatureTestsFramework.Placeholders.Replacers;
using FeatureTestsFramework.Placeholders.Request;
using FeatureTestsFramework.Placeholders.Response;
using FeatureTestsFramework.Placeholders.Uri;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FeatureTestsFramework.Bootstrapping
{
    public static class ServiceRegistrationCommon
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonPlaceholders(configuration);
            services.AddCommonHttpClient(configuration);

            return services;
        }

        public static IServiceCollection AddCommonPlaceholders(this IServiceCollection services, IConfiguration configuration) 
        {
            services.TryAddTransient<IPlaceholderReplacer<IResponsePlaceholderEvaluator>, PlaceholderReplacer<IResponsePlaceholderEvaluator>>();
            services.TryAddTransient<IPlaceholderReplacer<IRequestPlaceholderEvaluator>, PlaceholderReplacer<IRequestPlaceholderEvaluator>>();
            services.TryAddTransient<IPlaceholderReplacer<IUriPlaceholderEvaluator>, PlaceholderReplacer<IUriPlaceholderEvaluator>>();

            services.TryAddTransient<IResponsePlaceholderEvaluator, ResponsePlaceholderEvaluator>();
            services.TryAddTransient<IRequestPlaceholderEvaluator, RequestPlaceholderEvaluator>();
            services.TryAddTransient<IUriPlaceholderEvaluator, UriPlaceholderEvaluator>();

            services.AddScoped<IRequestStore, RequestStore>();
            services.AddScoped<IFeatureTestRequestBuilder, FeatureTestRequestBuilder>();

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
}
