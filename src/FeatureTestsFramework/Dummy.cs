using AutoFixture;

namespace FeatureTestsFramework
{
    public static class Dummy
    {
        private static readonly Fixture Fixture = new Fixture();
        public static T Any<T>() => Fixture.Create<T>();
        public static IEnumerable<T> AnyMany<T>() => Fixture.CreateMany<T>();
        public static DateTime Future => DateTime.UtcNow.AddDays(1);
        public static DateTime Past => DateTime.UtcNow.AddDays(-1);
        public static string Text(int length) => new('a', length);
    }
}
