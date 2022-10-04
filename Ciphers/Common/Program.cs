namespace Ciphers.Common;
using Algorithms;

public class Program
{
    public static void Main(string[] args)
    {
        var messageToEncrypt = "The bad cop handed me a speeding ticket";
        var messsageVigenere = "its not good to submit assignments after deadline";
        
        var encryptedSingleKeyCaesar=new CaesarWithOneKey().Encrypt(messageToEncrypt);
        var decryptedSingleKeyCaesar = new CaesarWithOneKey().Decrypt(encryptedSingleKeyCaesar);
        Console.WriteLine($"The encrypted message using Caesar cipher with one key  is: '{encryptedSingleKeyCaesar}'");
        Console.WriteLine($"The decrypted message using Caesar with one key is: '{decryptedSingleKeyCaesar}'");
        
        var encryptedDoubleKeyCaesar = new CaesarWithTwoKeys().Encrypt(messageToEncrypt);
        var deCryptedDoubleKeyCaesar = new CaesarWithTwoKeys().Decrypt(encryptedDoubleKeyCaesar);
        Console.WriteLine($"The encrypted message using double key caesar is : '{encryptedDoubleKeyCaesar}'");
        Console.WriteLine($"The decrypted message using double key caesar is : '{deCryptedDoubleKeyCaesar}'");
        
        var vigenereEncrypted = new Vigenere().Encrypt(messsageVigenere);
        var vigenereDecrypted = new Vigenere().Decrypt(vigenereEncrypted);
        Console.WriteLine($"The encrypted message using vigenere cipher is : '{vigenereEncrypted}'");
        Console.WriteLine($"The decrypted message using vigenere cipher is : '{vigenereDecrypted}'");

        var affineEncrypted = new Affine().Encrypt(messsageVigenere);
        var affineDecrypted = new Affine().Decrypt(affineEncrypted);
        Console.WriteLine($"The encrypted message using affine cipher is : '{affineEncrypted}'");
        Console.WriteLine($"The decrypted message using affine cipher is : '{affineDecrypted}'");

    }
}