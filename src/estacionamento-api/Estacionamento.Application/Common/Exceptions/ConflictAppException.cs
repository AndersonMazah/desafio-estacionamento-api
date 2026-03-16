namespace Estacionamento.Application.Common.Exceptions;

public class ConflictAppException : AppException
{
    public ConflictAppException(string message) : base(message, 409)
    {
    }
}
