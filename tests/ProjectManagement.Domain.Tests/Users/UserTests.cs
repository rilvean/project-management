using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Models;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Domain.Tests.Users;

public class UserTests
{
    [Fact]
    public void Constructor_Should_Create_User_With_Valid_Data()
    {
        var user = CreateUser();

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("test_name", user.Name);
        Assert.Equal("test@mail.com", user.Email.Value);
        Assert.Equal("password_hash", user.PasswordHash);
        Assert.Equal(UserRole.Employee, user.Role);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Rename_Should_Throw_When_Name_Is_Empty(string? name)
    {
        var user = CreateUser();

        Assert.Throws<ArgumentNullException>(() => user.Rename(name!));
    }

    [Fact]
    public void Rename_Should_Throw_When_Name_Is_Too_Long()
    {
        var user = CreateUser();

        var longName = new string('a', User.NameMaxLength + 1);

        Assert.Throws<ArgumentOutOfRangeException>(() => user.Rename(longName));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ChangePassword_Should_Throw_When_PasswordHash_Is_Empty(string? password)
    {
        var user = CreateUser();

        Assert.Throws<ArgumentNullException>(() => user.ChangePassword(password!));
    }

    [Fact]
    public void ChangeEmail_Should_Throw_When_Email_Is_Null()
    {
        var user = CreateUser();

        Assert.Throws<ArgumentNullException>(() => user.ChangeEmail(null!));
    }

    private static User CreateUser()
    {
        return new User(
            "test_name",
            Email.Create("test@mail.com"),
            "password_hash",
            UserRole.Employee
        );
    }
}