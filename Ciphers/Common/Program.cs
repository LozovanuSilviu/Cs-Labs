using System.Text;
using Ciphers.Ciphers.Implementations.Asymmetric;
using Ciphers.Ciphers.Implementations.Classic;
using Ciphers.Ciphers.Implementations.Symmetric;

namespace Ciphers.Common;

public class Program
{
    public static void Main(string[] args)
    {
        var phrase = "Security";
        var byteMessage = Encoding.ASCII.GetBytes(phrase);
        
        var key_phrase = "Publicity";
        
        var key = Encoding.ASCII.GetBytes(key_phrase);
        
        var messageToEncrypt = "The bad cop handed me a speeding ticket";
        var messsageVigenere = "its not good to submit assignments after deadline";
        
        var encryptedSingleKeyCaesar = new CaesarWithOneKey().Encrypt(messageToEncrypt);
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
        
        var rc4Encrypted = new Rc4(key).Encrypt(byteMessage, byteMessage.Length);
        var textRc4 = Convert.ToBase64String(rc4Encrypted);
        var rc4Decrypted = new Rc4(key).Decrypt(rc4Encrypted, rc4Encrypted.Length);
        var decryptedText = Encoding.ASCII.GetString(rc4Decrypted);
        Console.WriteLine($"The encrypted message using rc4 cipher is : '{textRc4}'");
        Console.WriteLine($"The decrypted message using rc4 is : '{decryptedText}'");
        
        var blowfishCrypted = new BlowFish(key_phrase).Encrypt(phrase);
        var blowfishDecrypted = new BlowFish(key_phrase).Decrypt(blowfishCrypted);
        Console.WriteLine($"The encrypted message using blowfish cipher is : '{blowfishCrypted}'");
        Console.WriteLine($"The decrypted message using blowfisch is : '{blowfishDecrypted}'");
        
        
        var text = Encoding.Unicode.GetBytes("Rsa isn't funny");
        var Rsakey = RSA.GenerateKeyPair(2048);
        var enc = RSA.Encrypt(text, Rsakey.Public);
        
        Console.WriteLine("[Encrypted]");
        Console.WriteLine(Convert.ToBase64String(enc) + "\n");
        
        var dec = RSA.Decrypt(enc, Rsakey.Private);
        
        Console.WriteLine("[Decrypted]");
        Console.WriteLine(Encoding.Unicode.GetString(dec) + "\n");
        var hash = new HashRsa("Hash message");
        hash.Encrypt();
        hash.Decrypt();
    }
}