using System;
using System.Net.Http;
using FeatureTestsFramework.HttpRequest;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace FeatureTestFramework.UnitTests.HttpRequest
{
    public class FeatureTestRequestBuilderTests
    {
        [Fact]
        public void Build_ReturnsCorrectRequest_NoQueryParameters()
        {
            // Arrange
            var builder = new FeatureTestRequestBuilder()
                .SetMethod(HttpMethod.Get)
                .SetRelativeUrl(new Uri("/api/test", UriKind.Relative))
                .SetUriApiVersion("v1")
                .AddHeader("Authorization", new StringValues("Bearer Token"))
                .SetSubUri("/sub");

            // Act
            var request = builder.Build();

            // Assert
            request.HttpMethod.Should().Be(HttpMethod.Get);
            request.EndpointRelativeUri.Should().Be(new Uri("/api/test", UriKind.Relative));
            request.ApiVersion.Should().Be("v1");
            request.AdditionalHeaders.Should().ContainKey("Authorization");
            request.AdditionalHeaders["Authorization"].Should().Contain("Bearer Token");
            request.SubUri.Should().Be("/sub");
        }

        [Fact]
        public void Build_ReturnsCorrectRequest_WithQueryParameters()
        {
            // Arrange
            var builder = new FeatureTestRequestBuilder()
                .SetMethod(HttpMethod.Get)
                .SetRelativeUrl(new Uri("/api/test", UriKind.Relative))
                .AddQueryParameter("page", "1")
                .AddQueryParameter("pageSize", "10");

            // Act
            var request = builder.Build();

            // Assert
            request.HttpMethod.Should().Be(HttpMethod.Get);
            request.EndpointRelativeUri.Should().Be(new Uri("/api/test?page=1&pageSize=10", UriKind.Relative));
        }

        [Fact]
        public void Build_ReturnsCorrectRequest_WithBody()
        {
            // Arrange
            var builder = new FeatureTestRequestBuilder()
                .SetMethod(HttpMethod.Post)
                .SetRelativeUrl(new Uri("/api/test", UriKind.Relative))
                .SetBody("{\"name\": \"John\"}");

            // Act
            var request = builder.Build();

            // Assert
            request.HttpMethod.Should().Be(HttpMethod.Post);
            request.Body.Should().Be("{\"name\": \"John\"}");
        }

        [Fact]
        public void Build_ReturnsCorrectRequest_WithRemovedHeader()
        {
            // Arrange
            var builder = new FeatureTestRequestBuilder()
                .SetRelativeUrl(new Uri("/uri", UriKind.Relative))
                .AddHeader("Authorization", new StringValues("Bearer Token"))
                .RemoveHeader("Authorization");

            // Act
            var request = builder.Build();

            // Assert
            request.AdditionalHeaders.Should().NotContainKey("Authorization");
        }
    }
}
