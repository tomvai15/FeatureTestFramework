using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace FeatureTestsFramework.SOA.Tests.Logic.Bootstrapping
{
    public static class WireMockRegistration
    {
        public static IServiceCollection AddWireMock(this IServiceCollection services)
        {
            const int port = 5999;// TODO: move somewhere
            var stubServer = WireMockServer.Start(port);
            services.AddSingleton(stubServer);

            stubServer.Given(Request.Create().WithPath("/healthcheck").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200));

            var a = stubServer.Mappings;

            return services;
        }
    }
}
