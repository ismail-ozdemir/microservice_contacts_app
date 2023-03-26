
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ValidationException : CommonBaseException
    {
        public List<ResponseItem> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new();
        }
        public ValidationException(List<ResponseItem> errors)
        {
            Errors = errors;
        }
        public ValidationException(List<ResponseItem> errors, string message) : this(message)
        {
            Errors = errors;
        }
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Errors = (info.GetValue(nameof(ValidationException.Errors), typeof(List<ResponseItem>)) as List<ResponseItem>) ?? new List<ResponseItem>();
        }



        public class ResponseItem
        {
            /// <summary>
            /// Creates a new validation failure.
            /// </summary>
            public ResponseItem(string propertyName, string errorMessage) : this(propertyName, errorMessage, null)
            {

            }
            /// <summary>
            /// Creates a new ValidationFailure.
            /// </summary>
            public ResponseItem(string propertyName, string errorMessage, object? attemptedValue)
            {
                PropertyName = propertyName;
                ErrorMessage = errorMessage;
                AttemptedValue = attemptedValue;
            }

            /// <summary>
            /// The name of the property.
            /// </summary>
            public string PropertyName { get; set; }

            /// <summary>
            /// The error message
            /// </summary>
            public string ErrorMessage { get; set; }
            /// <summary>
            /// The property value that caused the failure.
            /// </summary>
            public object? AttemptedValue { get; set; }
            /// <summary>
            /// Creates a textual representation of the failure.
            /// </summary>
            public override string ToString()
            {
                return ErrorMessage;
            }
        }

    }

}
