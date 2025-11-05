using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FeatureTestsFramework.SOA.Tests.Logic;

public static class JObjectExtensions
{
    public static JObject MergeInto(this JObject valuesToMergeFrom, object objectToMergeTo)
    {
        var objJson = JsonSerializer.Serialize(objectToMergeTo, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var objectToMergeIntoJson = JObject.Parse(objJson);

        var settings = new JsonMergeSettings
        {
            MergeArrayHandling = MergeArrayHandling.Merge,
            MergeNullValueHandling = MergeNullValueHandling.Merge,
        };

        objectToMergeIntoJson.Merge(settings);

        return objectToMergeIntoJson;
        
    }
}