using System.Diagnostics.Contracts;

namespace Ciphers.Algorithms;
using Common;
public class Vigenere : ICipher
{
    private static Dictionary<int, char> alphabetLetters = new ();
    private static string letters = "abcdefghijklmnopqrstuvwxyz";
    private const string textKey = "omega";

    public  void CreateAlphabet()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            alphabetLetters.Add(i, letters[i]);
        }
    }

    public string GetComposedKey(int length)
    {
        var keyLength = textKey.Length;
        var intParts = length / keyLength;
        var composedKey = "";
        for (int i = 0; i < intParts; i++)
        {
            composedKey += textKey;
        }
        var rest = length % keyLength;
        var tail = textKey.Substring(0, rest);
        composedKey += tail;
        return composedKey;
    }
    
    public string Encrypt(string message)
    {
        CreateAlphabet();
        var key = GetComposedKey(message.Length);
        var encryptedMessage = "";

        for (int i = 0; i < message.Length; i++)
        {
            if (message[i]==32)
            {
                encryptedMessage += " ";
                continue;
            }
            var messageIndex = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var keyIndex=  alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
            var cryptedLetter = (messageIndex + keyIndex) % 26;
            encryptedMessage+=alphabetLetters[cryptedLetter];
        }
        return encryptedMessage;
    }

    public string Decrypt(string message)
    {
        var decryptedMessage = "";
        var key = GetComposedKey(message.Length);

        for (int i = 0; i < message.Length; i++)
        {
            if (message[i]==32)
            {
                decryptedMessage += " ";
                continue;
            }
            var messageIndex = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var keyIndex=  alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
            var deCryptedLetter = (messageIndex - keyIndex) % 26;
            if (deCryptedLetter<0)
            {
                deCryptedLetter += alphabetLetters.Count;
            }
            decryptedMessage+=alphabetLetters[deCryptedLetter];
        }

        return decryptedMessage;
    }
}