using System.Text.RegularExpressions;
using ProjectManagement.Domain.Exceptions;

namespace ProjectManagement.Domain.ValueObjects;

public sealed record Email
{
    public const int MaxLenght = 50;

    public static readonly Regex Regex = new(
        @"^(?!.*(\.\.|__|\+\+|--))[a-z0-9][a-z0-9._+-]+[a-z0-9]@[a-z][a-z0-9.-]+[a-z0-9]\.[a-z]{2,}$",
        RegexOptions.Compiled
    );

    private Email(string value) => Value = value;

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmailException("Email is empty.");

        value = value.Trim().ToLower();

        if (!Regex.IsMatch(value))
            throw new EmailException("Invalid email.");

        if (value.Length > MaxLenght)
            throw new EmailException("Email is too long.");

        return new Email(value);
    }

    public static implicit operator string(Email email) => email.Value;
    public override string ToString() => Value;
}