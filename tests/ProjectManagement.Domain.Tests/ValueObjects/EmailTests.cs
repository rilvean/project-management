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
    [InlineData("test")]
    [InlineData("@mail.com")]
    [InlineData("test@")]
    [InlineData("test@mail")]
    [InlineData("test@mail.c")]
    [InlineData("test@ma.com")]
    [InlineData("te@mail.com")]
    public void Create_Should_Throw_When_Email_Has_Invalid_Format(string value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Theory]
    [InlineData("test..test@mail.com")]
    [InlineData("test__test@mail.com")]
    [InlineData("test++test@mail.com")]
    [InlineData("test--test@mail.com")]
    public void Create_Should_Throw_When_Email_Has_Consecutive_Special_Chars(string value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Theory]
    [InlineData(".test@mail.com")]
    [InlineData("_test@mail.com")]
    [InlineData("+test@mail.com")]
    [InlineData("-test@mail.com")]
    public void Create_Should_Throw_When_Email_Starts_On_Special_Chars(string value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Theory]
    [InlineData("te$t@mail.com")]
    [InlineData("te/st@mail.com")]
    [InlineData("te*st@mail.com")]
    [InlineData("te!st@mail.com")]
    public void Create_Should_Throw_When_Email_Contains_Invalid_Chars(string value)
    {
        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Too_Long()
    {
        var value = $"{new string('a', Email.MaxLenght)}@mail.com";

        Assert.Throws<EmailException>(() => Email.Create(value));
    }

    [Fact]
    public void ToString_Should_Return_Email_Value()
    {
        var email = Email.Create("test@mail.com");

        Assert.Equal("test@mail.com", email.ToString());
    }

    [Fact]
    public void Implicit_Conversion_Should_Return_Value()
    {
        var email = Email.Create("test@mail.com");

        string value = email;

        Assert.Equal("test@mail.com", value);
    }
}