namespace EnkaSharp.Exceptions;

public class ApiBrokenException : Exception
{
    public ApiBrokenException()
    {
    }

    public ApiBrokenException(string message) : base(message)
    {
    }

    public ApiBrokenException(string message, Exception inner) : base(message, inner)
    {
    }
}