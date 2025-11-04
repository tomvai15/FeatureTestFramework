using System;
using System.Collections.Generic;
using System.Net.Http;
using FeatureTestsFramework.HttpRequest;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace FeatureTestFramework.UnitTests.HttpRequest
{
    public class FeatureTestRequestTests
    {
        [Fact]
        public void ToHttpRequestMessage_ReturnsCorrectRequest_NoBody_NoAdditionalHeaders_NoSubUri()
        {
            // Arrange
            var request = new FeatureTestRequest
            {
                HttpMethod = HttpMethod.Get,
                EndpointRelativeUri = new Uri("/api/test", UriKind.Relative),
                RequestTimestampUtc = DateTime.UtcNow
            };

            // Act
            var httpRequest = request.ToHttpRequestMessage();

            // Assert
            httpRequest.Method.Should().Be(HttpMethod.Get);
            httpRequest.RequestUri.Should().Be(new Uri("/api/test", UriKind.Relative));
            httpRequest.Content.Should().BeNull();
            httpRequest.Headers.Should().BeEmpty();
        }

        [Fact]
        public void ToHttpRequestMessage_ReturnsCorrectRequest_WithBody_AndAdditionalHeaders_AndSubUri()
        {
            // Arrange
            var request = new FeatureTestRequest
            {
                HttpMethod = HttpMethod.Post,
                EndpointRelativeUri = new Uri("/api/test", UriKind.Relative),
                RequestTimestampUtc = DateTime.UtcNow,
                Body = "{\"name\": \"John\"}",
                AdditionalHeaders = new Dictionary<string, StringValues>
                {
                    {"Authorization", "Bearer Token"},
                    {"Accept", "application/json"}
                },
                SubUri = "/sub"
            };

            // Act
            var httpRequest = request.ToHttpRequestMessage();

            // Assert
            httpRequest.RequestUri.Should().Be(new Uri("/sub/api/test", UriKind.Relative));
            httpRequest.Content.Should().NotBeNull();
            httpRequest.Content.ReadAsStringAsync().Result.Should().Be("{\"name\": \"John\"}");
            httpRequest.Headers.Should().HaveCount(2);
            httpRequest.Headers.Should().ContainKeys("Authorization", "Accept");
        }
    }
}
