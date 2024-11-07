using Newtonsoft.Json.Linq;

namespace FeatureTestsFramework.Extensions
{
    public static class JTokenExtensions
    {
        public static Dictionary<string, object?> FlattenJson(this JToken jToken)
        {
            var properties = new Dictionary<string, object?>();
            var token = jToken;
            while (token != null && token.HasValues)
            {
                FlattenJsonImpl(token, properties);
                token = token.Next;
            }
            return properties;
        }

        private static void FlattenJsonImpl(JToken jToken, Dictionary<string, object?> properties)
        {
            foreach (var child in jToken.Children())
            { 
                if (!child.HasValues)
                {
                    properties.Add(child.Path, child.ConvertToNativeType());
                }
                else
                {
                    FlattenJsonImpl(child, properties);
                }
            }
        }

        public static object? ConvertToNativeType(this JToken token)
        {
            var a = token.ToString();

            return token.Type switch
            {
                JTokenType.String => token.ToString(),
                JTokenType.Date => token.ToObject<DateTime>().ToString("O"),
                JTokenType.Boolean => token.ToObject<bool>(),
                JTokenType.Float => token.ToObject<float>(),
                JTokenType.Integer => token.ToObject<long>(),
                JTokenType.Null => null,
                _ => token
            };
        }
    }
}
