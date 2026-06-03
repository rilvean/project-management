namespace ProjectManagement.Domain.Exceptions;

public sealed class EmailException(string message) : Exception(message)
{
}