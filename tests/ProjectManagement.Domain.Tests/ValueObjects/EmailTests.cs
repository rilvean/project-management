using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Domain.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_Should_Normalize_Email()
    {
        var email = Email.Create("  TEST1@MAIL.COM ");

        Assert.Equal("test1@mail.com", email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_Should_Throw_When_Email_Is_Empty(string? value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value!));
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("@mail.com")]
    [InlineData("test@")]
    [InlineData("test@mail.c")]
    [InlineData("test..test@mail.com")]
    [InlineData(".test@mail.com")]
    [InlineData("te$t@mail.com")]
    public void Create_Should_Throw_When_Email_Has_Invalid_Format(string value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Too_Long()
    {
        var value = $"{new string('a', Email.MaxLenght)}@mail.com";

        Assert.Throws<EmailException>(() => Email.Create(value));
    }
}