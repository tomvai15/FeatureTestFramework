namespace FeatureTestsFramework.HttpRequest
{
    public class FeatureTestClientConfiguration
    {
        public const string SectionName = "FeatureTestClient";

        public string BaseUri { get; set; }
        public string UserAgent { get; set; }
    }
}
