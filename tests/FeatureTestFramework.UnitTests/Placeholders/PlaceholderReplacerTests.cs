using FeatureTestsFramework.Exceptions;
using FeatureTestsFramework.Placeholders;
using FeatureTestsFramework.Placeholders.Evaluators;
using FluentAssertions;
using Moq;
using Reqnroll;
using Xunit;

namespace FeatureTestFramework.UnitTests.Placeholders;

public class PlaceholderReplacerTests
{
    private readonly PlaceholderReplacer<IPlaceholderEvaluator> _placeholderReplacer;
    private readonly Mock<IPlaceholderEvaluator> _placeholderEvaluator;
    private readonly Mock<IScenarioContext> _scenarioContext;

    public PlaceholderReplacerTests()
    {
        _placeholderEvaluator = new();
        _scenarioContext = new();
        _placeholderReplacer = new PlaceholderReplacer<IPlaceholderEvaluator>(_placeholderEvaluator.Object);
    }

    [Fact]
    public void Replace_WhenNoPlaceholder_ShouldReturnSameText()
    {
        //Arrange
        var text = "text with no placeholder";

        //Act
        var result = _placeholderReplacer.Replace(text, _scenarioContext.Object);

        //Assert
        result.Should().BeEquivalentTo(text);
    }

    [Theory]
    [InlineData("{{placeholder1}} {{placeholder2}}", "placeholderValue1 placeholderValue2")]
    [InlineData("{{placeholder1}} a {{placeholder2}} b {{placeholder1}}", "placeholderValue1 a placeholderValue2 b placeholderValue1")]
    [InlineData("{{placeholder1}}", "placeholderValue1")]
    public void Replace_WhenSinglePlaceholder_ShouldReplacePlaceholder(string text, string expected)
    {
        //Arrange
        _placeholderEvaluator.Setup(x => x.Evaluate("placeholder1", _scenarioContext.Object)).Returns("placeholderValue1");
        _placeholderEvaluator.Setup(x => x.Evaluate("placeholder2", _scenarioContext.Object)).Returns("placeholderValue2");

        //Act
        var result = _placeholderReplacer.Replace(text, _scenarioContext.Object);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Replace_WhenPlaceholderValueNotFound_ShouldThrowException()
    {
        //Arrange
        var text = "{{notExistingPlaceholder}}";

        //Act
        var replace = () => _placeholderReplacer.Replace(text, _scenarioContext.Object);

        //Assert
        replace.Should().ThrowExactly<MissingPlaceholderValueException>().WithMessage("Placeholder notExistingPlaceholder was not found");
    }
}