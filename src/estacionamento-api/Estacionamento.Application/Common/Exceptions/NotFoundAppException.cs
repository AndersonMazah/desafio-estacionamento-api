namespace Estacionamento.Application.Common.Exceptions;

public class NotFoundAppException : AppException
{
    public NotFoundAppException(string message) : base(message, 404)
    {
    }
}
