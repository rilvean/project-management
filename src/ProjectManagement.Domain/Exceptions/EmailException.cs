namespace ProjectManagement.Domain.Exceptions;

public class EmailException(string message) : Exception(message)
{
}