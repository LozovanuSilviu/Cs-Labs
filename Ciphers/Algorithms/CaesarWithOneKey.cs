using Ciphers.Common;

namespace Ciphers.Algorithms;

public class CaesarWithOneKey : ICipher
{
    private const int Key = 5;

    public string Encrypt(string messageLower)
    {
        var lowerCaseMessage = messageLower.ToLower();
        var finalMessage = "";
        foreach (var letter in lowerCaseMessage)
        {
            if (letter==32)
            {
                finalMessage=finalMessage+" ";
                continue;
            }
            var cryptedLetter =(letter-97+Key)%26;
            var stringLetter = char.ConvertFromUtf32(cryptedLetter + 97);
            finalMessage =  finalMessage+stringLetter;
        }

        return finalMessage;
    }

    public string Decrypt(string message)
    {
        var finalMessage = "";
        foreach (var letter in message)
        {
            if (letter==32)
            {
                finalMessage = finalMessage + " ";
                continue;
            }

            var decryptedLetter = (letter - 97 - Key) % 26;
            var stringLetter = char.ConvertFromUtf32(decryptedLetter + 97);
            finalMessage = finalMessage + stringLetter;
        }

        return finalMessage;
    }
}