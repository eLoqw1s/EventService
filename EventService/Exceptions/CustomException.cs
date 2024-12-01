namespace EventService.Exceptions
{
	public class CustomException : Exception
	{
        public int ErrorCode { get; }
        public CustomException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
	}
}
