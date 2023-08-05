namespace Catalog.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new ValidationException(error);
        }
    }
}
