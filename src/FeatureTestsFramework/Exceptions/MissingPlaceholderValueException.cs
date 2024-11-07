using System.Runtime.Serialization;

namespace FeatureTestsFramework.Exceptions
{
    public class MissingPlaceholderValueException : Exception
    {
        public MissingPlaceholderValueException()
        {
        }

        public MissingPlaceholderValueException(string message) : base(message)
        {
        }

        public MissingPlaceholderValueException(string message, Exception inner) : base(message, inner)
        {
        }

        public MissingPlaceholderValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
