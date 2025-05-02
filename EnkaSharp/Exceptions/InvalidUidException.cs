namespace EnkaSharp.Exceptions;

public class InvalidUidException : Exception
{
    public InvalidUidException()
    {
    }

    public InvalidUidException(string message) : base(message)
    {
    }

    public InvalidUidException(string message, Exception inner) : base(message, inner)
    {
    }
}