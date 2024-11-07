using Moq;
using TechTalk.SpecFlow;
using FeatureTestsFramework.Extensions;
using FluentAssertions;
using System.Collections.Specialized;

namespace FeatureTestFramework.UnitTests.Extensions
{
    public class ScenarioContextExtensionsTests
    {
        private readonly Mock<IScenarioContext> _scenarioContext = new();

        [Fact]
        public void GetTableArg_Should_Return_Null_When_Column_Not_Found()
        {
            // Arrange
            var dictionary = new OrderedDictionary();
            var scenarioInfo = new ScenarioInfo("", "", [], dictionary, []);
            _scenarioContext.SetupGet(x => x.ScenarioInfo).Returns(scenarioInfo);
            var scenarioContext = _scenarioContext.Object;

            // Act
            var result = scenarioContext.GetTableArg("NonExistingColumn");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetTableArg_Should_Return_Value_When_Column_Found()
        {
            // Arrange
            var expectedValue = Any<string>();
            var columnName = Any<string>();
            var dictionary = new OrderedDictionary
            {
                { columnName, expectedValue }
            };
            var scenarioInfo = new ScenarioInfo("", "", [], dictionary, []);
            _scenarioContext.SetupGet(x => x.ScenarioInfo).Returns(scenarioInfo);
            var scenarioContext = _scenarioContext.Object;

            // Act
            var result = scenarioContext.GetTableArg(columnName);

            // Assert
            result.Should().Be(expectedValue);
        }
    }
}
