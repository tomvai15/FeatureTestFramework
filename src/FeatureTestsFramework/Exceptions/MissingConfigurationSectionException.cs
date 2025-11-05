using System.Runtime.Serialization;

namespace FeatureTestsFramework.Exceptions;

public class MissingConfigurationSectionException : Exception
{
    public MissingConfigurationSectionException()
    {
    }

    public MissingConfigurationSectionException(string configKey) : base($"Configuration section {configKey} was not found.")
    {
    }

    public MissingConfigurationSectionException(string message, Exception inner) : base(message, inner)
    {
    }

    public MissingConfigurationSectionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}