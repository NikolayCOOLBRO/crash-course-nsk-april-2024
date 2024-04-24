namespace Market.UseCases.Exceptions
{
    public class InvalidDataRequestException : Exception
    {
        public ErrorDetails Details { get; }

        public InvalidDataRequestException(ErrorDetails errorDetails) : base(errorDetails.ConvertToOneMessage())
        {
            Details = errorDetails;
        }
    }
}
