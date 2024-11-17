namespace PersonClientService.Core.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string message, IDictionary<string, string[]> errors)
            : base(message)
        {
            Errors = errors;
        }

        public ValidationException(string fieldName, string errorMessage)
            : base($"Validation error on field '{fieldName}': {errorMessage}")
        {
            Errors = new Dictionary<string, string[]>
            {
                { fieldName, new[] { errorMessage } }
            };
        }
    }
}
