using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Domain.Models;

public class User : IAuditable
{
    public const int NameMaxLength = 50;

    private string _name = null!;
    private Email _email = null!;
    private string _passwordHash = null!;


    public User(string name, Email email, string passwordHash, UserRole role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    private User()
    {
    }


    public Guid Id { get; private init; } = Guid.CreateVersion7();

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(Name));
            if (value.Length > NameMaxLength) throw new ArgumentOutOfRangeException(nameof(Name));
            _name = value;
        }
    }

    public Email Email
    {
        get => _email;
        private set => _email = value ?? throw new ArgumentNullException(nameof(Email));
    }

    public string PasswordHash
    {
        get => _passwordHash;
        private set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(PasswordHash));
            _passwordHash = value;
        }
    }

    public UserRole Role { get; private set; }


    public void Rename(string newName) => Name = newName;
    public void ChangeEmail(Email newEmail) => Email = newEmail;
    public void ChangePassword(string newPasswordHash) => PasswordHash = newPasswordHash;
    public void ChangeRole(UserRole newRole) => Role = newRole;
}