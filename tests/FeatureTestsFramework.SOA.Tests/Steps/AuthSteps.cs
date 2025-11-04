using FeatureTestsFramework.Extensions;
using FeatureTestsFramework.HttpRequest;
using FeatureTestsFramework.SOA.Tests.Logic.Token;
using Reqnroll;

namespace FeatureTestsFramework.SOA.Tests.Steps
{
    [Binding]
    public class AuthSteps
    {
        private readonly IFakeJwtTokenGenerator _fakeJwtTokenGenerator;
        private readonly IFeatureTestRequestBuilder _featureTestRequestBuilder;

        public AuthSteps(ScenarioContext context)
        {
            _fakeJwtTokenGenerator = context.GetRequiredService<IFakeJwtTokenGenerator>();
            _featureTestRequestBuilder = context.GetRequiredService<IFeatureTestRequestBuilder>();
        }

        [Given(@"I have a valid auth token")]
        public void GivenIHaveValidAuthToken()
        {
            var token = _fakeJwtTokenGenerator.GenerateToken();
            _featureTestRequestBuilder.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}
