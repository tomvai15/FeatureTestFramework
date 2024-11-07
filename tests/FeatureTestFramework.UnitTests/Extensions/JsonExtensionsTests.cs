using FeatureTestsFramework.Extensions;
using FluentAssertions;

namespace FeatureTestFramework.UnitTests.Extensions
{
    public class JsonExtensionsTests
    {
        [Fact]
        public void FormatJsonWithPlaceholders_WhenNoPlaceholders_ShouldReturnFormatedJson()
        {
            //Arrange
            var json = @"{""property"": ""value""}";

            //Act
            var result = json.FormatJsonWithPlaceholders();

            //Assert
            var expectedJson =
@"{
  ""property"": ""value""
}";
            result.Should().Be(expectedJson);
        }

        [Fact]
        public void FormatJsonWithPlaceholders_WhenPlaceholders_ShouldReturnFormatedJson()
        {
            //Arrange
            var json = @"{""property"": {{placeholder}}}";

            //Act
            var result = json.FormatJsonWithPlaceholders();

            //Assert
            var expectedJson =
@"{
  ""property"": {{placeholder}}
}";
            result.Should().Be(expectedJson);
        }
    }
}
