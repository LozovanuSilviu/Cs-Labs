namespace Ciphers.Common;

public interface ICipher
{
    public string Encrypt(string messageLower);
    
    public string Decrypt(string message);
}