using System.Security.Cryptography;

namespace StagingApp.Domain.Services;
public static class SecureStringService
{
    //TODO: Add Salt to secrets.json
    private readonly static byte[] _salt = Encoding.ASCII.GetBytes("c;fr,u+$VDNMEhGvg)3F7lrK+HHA1vN+");

    public static string EncryptString(string plainText, string sharedSecret)
    {
        string result = null;
        Aes aesAlg = null;

        try
        {
            Rfc2898DeriveBytes key = new(sharedSecret, _salt);
            aesAlg = Aes.Create("AesManaged");
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
            msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plainText);
            }
            result = Convert.ToBase64String(msEncrypt.ToArray());
        }
        finally
        {
            if (aesAlg != null)
            {
                aesAlg.Clear();
            }
        }

        return result;
    }

    public static string DecryptString(string cipherText, string sharedSecret)
    {
        Aes aesAlg = null;
        string result = null;

        try
        {
            Rfc2898DeriveBytes key = new(sharedSecret, _salt);
            byte[] bytes = Convert.FromBase64String(cipherText);
            using MemoryStream msDecrypt = new(bytes);
            aesAlg = Aes.Create("AesManaged");
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = ReadByteArray(msDecrypt);
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            result = srDecrypt.ReadToEnd();
        }
        finally
        {
            if (aesAlg != null)
            {
                aesAlg.Clear();
            }
        }

        return result;
    }

    private static byte[] ReadByteArray(Stream s)
    {
        byte[] rawLength = new byte[sizeof(int)];
        if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
        {
            throw new SystemException("Stream did not contain properly formatted byte array");
        }

        byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
        if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
        {
            throw new SystemException("Did not read byte array properly");
        }

        return buffer;
    }
}
