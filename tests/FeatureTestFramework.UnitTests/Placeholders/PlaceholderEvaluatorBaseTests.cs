using FeatureTestsFramework.Placeholders;
using FeatureTestsFramework.Placeholders.Evaluators;
using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;

namespace FeatureTestFramework.UnitTests.Placeholders
{
    public class PlaceholderEvaluatorBaseTests
    {
        private readonly PlaceholderEvaluatorBase _placeholderEvaluatorBase;
        private readonly Mock<IScenarioContext> _scenarioContext;

        public PlaceholderEvaluatorBaseTests()
        {
            _scenarioContext = new();
            _placeholderEvaluatorBase = new PlaceholderEvaluatorBase([]);
        }

        [Fact]
        public void GetValueOfKey_ShouldReturnConstantValue()
        {
            // Arrange
            var key = "ConstantKey";
            var expectedResult = "ConstantValue";

            _placeholderEvaluatorBase.AddEvaluation(key, expectedResult);

            // Act
            var result = _placeholderEvaluatorBase.GetValueOfKey(key, _scenarioContext.Object);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GetValueOfKey_ShouldReturnValueFromCustomEvaluator()
        {
            // Arrange
            var key = "ConstantKey";
            var expectedResult = "ConstantValue";

            var evaluation = (IScenarioContext scenarioContext) => expectedResult;

            _placeholderEvaluatorBase.AddEvaluation(key, evaluation);

            // Act
            var result = _placeholderEvaluatorBase.GetValueOfKey(key, _scenarioContext.Object);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GetValueOfKey_ShouldReturnValueFromDynamicEvaluator()
        {
            // Arrange
            var key = "ConstantKey";
            var expectedResult = "ConstantValue";

            var evaluation = (string key, IScenarioContext scenarioContext) => expectedResult;

            _placeholderEvaluatorBase.AddEvaluation(evaluation, "Error message");

            // Act
            var result = _placeholderEvaluatorBase.GetValueOfKey(key, _scenarioContext.Object);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GetValueOfKey_Should_Throw_Exception_When_Key_Not_Found()
        {
            // Arrange
            var key = "ConstantKey";

            // Act
            var action = () => _placeholderEvaluatorBase.GetValueOfKey(key, _scenarioContext.Object);

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}
