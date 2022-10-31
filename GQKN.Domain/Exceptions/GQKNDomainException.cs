namespace PVI.GQKN.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class GQKNDomainException : Exception
{
    public string Code { get; private set; } = "9999";

    public GQKNDomainException()
    { }

    public GQKNDomainException(string message)
        : base(message)
    { }

    public GQKNDomainException(string code, string message)
       : base(message)
    {
        this.Code = code;
    }

    public GQKNDomainException(int code, string message)
       : base(message)
    {
        this.Code = code.ToString();
    }

    public GQKNDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public GQKNDomainException(string code, string message, Exception innerException)
        : base(message, innerException)
    {
        this.Code = code;
    }

    public GQKNDomainException(int code, string message, Exception innerException)
       : base(message, innerException)
    {
        this.Code = code.ToString();
    }
}
