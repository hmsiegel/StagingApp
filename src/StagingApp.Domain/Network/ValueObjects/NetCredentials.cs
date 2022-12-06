namespace StagingApp.Domain.Network.ValueObjects;
public class NetCredentials : ValueObject
{
    public string? UserName { get; }
    public string? Password { get; }
    public string? PasswordSalt { get; }

    private NetCredentials(
        string? userName,
        string? password,
        string? passwordSalt)
    {
        UserName = userName;
        Password = password;
        PasswordSalt = passwordSalt;
    }

    public static NetworkCredential Create(
        string? userName,
        string? password,
        string? passwordSalt)
    {
        string decryptedPassword = DecryptPassword(password, passwordSalt);
        NetworkCredential cred = new(userName, decryptedPassword);

        return cred;
    }

    private static string DecryptPassword(string password, string passwordSalt) =>
        SecureStringService.DecryptString(password, passwordSalt);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserName!;
        yield return Password!;
        yield return PasswordSalt!;
    }
}
