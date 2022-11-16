using System.Security.Cryptography;
using System.Text;

namespace Ciphers.Ciphers.Implementations.Asymmetric;

public class HashRsa
{
    private byte[] bytesToEncrypt { get; set; }
    public RsaKeyPair key { get; set; }
    
    public Dictionary<string, byte[]> localDb { get; set; }
    public bool compare { get; set; }

    public HashRsa(string message)
    {
        SHA1CryptoServiceProvider Crypto =  new SHA1CryptoServiceProvider();
        Byte[] Buffer = Encoding.ASCII.GetBytes(message);
        Byte[] Hash = Crypto.ComputeHash(Buffer);
        bytesToEncrypt = Hash;
        var hashedToPrint = Convert.ToBase64String(Hash);
        Console.WriteLine($"Hashed message: {hashedToPrint}");
        MD5 Md52 = MD5.Create();
        localDb = new();
    }


    public void Encrypt()
    {
        key = RSA.GenerateKeyPair(2048);
        var enc = RSA.Encrypt(bytesToEncrypt, key.Public);
        localDb.Add("crypted", enc);
        Console.WriteLine($"Encrypted message: {Convert.ToBase64String(enc)}");
    }

    public void Decrypt()
    {
        var toDecrypt = localDb["crypted"];
        var decrypted = RSA.Decrypt(toDecrypt, key.Private);
        Console.WriteLine($"The decrypted message is : {Convert.ToBase64String(decrypted)}");
        CompareBytes(bytesToEncrypt,decrypted);
        if (compare)
        {
            Console.WriteLine("Digital signature check passed successfully");
        }
        else
        {
            Console.WriteLine("Digital signature check failed");
        }
    }

    public void CompareBytes(byte[] actual, byte[] current)
    {   compare = false;
        if (actual.Length == current.Length)
        {
            int i=0;
            while ((i < actual.Length) && (actual[i] == current[i]))
            {
                i += 1;
            }
            if (i == actual.Length)
            {
                compare = true;
            }
        }
    }
    
}