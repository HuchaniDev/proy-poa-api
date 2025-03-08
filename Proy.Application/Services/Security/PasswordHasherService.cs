using Isopoh.Cryptography.Argon2;

namespace Proy.Application.Services.Security;

public class PasswordHasherService
{
    public string HashPassword(string password)
    {
        return Argon2.Hash(password);
    }

    public bool VerifyPassword(string hash, string password)
    {
        return Argon2.Verify(hash, password);
    }
}