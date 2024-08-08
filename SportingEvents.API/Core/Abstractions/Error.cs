namespace SportingEvents.API.Core.Abstractions
{
    public sealed class Error
    {
        private readonly string _code;
        private readonly string? _message;
        private readonly ErrorType _type;

        public Error(string code, ErrorType type, string? message = null)
        {
            _code = code;
            _message = message;
            _type = type;
        }

        public static readonly Error None = new(string.Empty, ErrorType.Failure);

        public string Code => _code;

        public string? Message => _message;

        public ErrorType Type => _type;

        public static Error Failure(string code, string? message = null) => new(code, ErrorType.Failure, message);
        public static Error Validation(string code, string? message = null) => new(code, ErrorType.Validation, message);
        public static Error NotFound(string code, string? message = null) => new(code, ErrorType.NotFound, message);
        public static Error Conflict(string code, string? message = null) => new(code, ErrorType.Conflict, message);
    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3
    }
}
