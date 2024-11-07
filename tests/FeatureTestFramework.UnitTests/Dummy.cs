using AutoFixture.Dsl;
using AutoFixture;

namespace FeatureTestFramework.UnitTests
{
    public static class Dummy
    {
        private static Fixture fixture = new Fixture();

        public static T Any<T>() => fixture.Create<T>();
        public static ICustomizationComposer<T> Build<T>() => fixture.Build<T>();
        public static IEnumerable<T> AnyMany<T>(int count) => fixture.CreateMany<T>(count);
    }
}
