using FeatureTestsFramework.Extensions;
using Newtonsoft.Json.Linq;

namespace FeatureTestsFramework.HttpRequest;

public interface IRequestStore
{
    void AddRequest(FeatureTestRequest request);
    object? GetValue(string key);
}

public class RequestStore : IRequestStore
{
    private readonly Dictionary<string, object?> requestProperties = new();

    public void AddRequest(FeatureTestRequest request)
    {
        if (request.Body == null)
        {
            return;
        }

        var jsonProperties = JObject.Parse(request.Body).FlattenJson();

        foreach (var key in jsonProperties.Keys)
        {
            if (requestProperties.ContainsKey(key))
            {
                requestProperties[key] = jsonProperties[key];
            }
            else
            {
                requestProperties.Add(key, jsonProperties[key]);
            }
        }
    }

    public object? GetValue(string key) => requestProperties.GetValueOrDefault(key);
}