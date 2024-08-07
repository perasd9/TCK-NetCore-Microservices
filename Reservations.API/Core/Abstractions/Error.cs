namespace Reservations.API.Core.Abstractions
{
    public sealed class Error
    {
        private readonly string _code;
        private readonly string? _message;

        public Error(string code, string? message = null)
        {
            _code = code;
            _message = message;
        }

        public static readonly Error None = new(string.Empty);

        public string Code => _code;

        public string? Message => _message;
    }
}
