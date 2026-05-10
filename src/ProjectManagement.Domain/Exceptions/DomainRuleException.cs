namespace ProjectManagement.Domain.Exceptions;

public class DomainRuleException(string message) : Exception(message) { }