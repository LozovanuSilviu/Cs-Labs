namespace Ciphers.Ciphers;

public interface ICipher
{
    public string Encrypt(string messageLower);

    public string Decrypt(string message);
}