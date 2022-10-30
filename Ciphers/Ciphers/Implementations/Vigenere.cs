namespace Ciphers.Ciphers.Implementations;

public class Vigenere : ICipher
{
    private const string textKey = "omega";
    private static readonly Dictionary<int, char> alphabetLetters = new();
    private static readonly string letters = "abcdefghijklmnopqrstuvwxyz";

    public string Encrypt(string message)
    {
        CreateAlphabet();
        var key = GetComposedKey(message.Length);
        var encryptedMessage = "";

        for (var i = 0; i < message.Length; i++)
        {
            if (message[i] == 32) continue;
            var messageIndex = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var keyIndex = alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
            var cryptedLetter = (messageIndex + keyIndex) % 26;
            encryptedMessage += alphabetLetters[cryptedLetter];
        }

        return encryptedMessage;
    }

    public string Decrypt(string message)
    {
        var decryptedMessage = "";
        var key = GetComposedKey(message.Length);

        for (var i = 0; i < message.Length; i++)
        {
            if (message[i] == 32) continue;
            var messageIndex = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var keyIndex = alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
            var deCryptedLetter = (messageIndex - keyIndex) % 26;
            if (deCryptedLetter < 0) deCryptedLetter += alphabetLetters.Count;
            decryptedMessage += alphabetLetters[deCryptedLetter];
        }

        return decryptedMessage;
    }

    private void CreateAlphabet()
    {
        for (var i = 0; i < letters.Length; i++) alphabetLetters.Add(i, letters[i]);
    }

    private string GetComposedKey(int length)
    {
        var keyLength = textKey.Length;
        var intParts = length / keyLength;
        var composedKey = "";
        for (var i = 0; i < intParts; i++) composedKey += textKey;
        var rest = length % keyLength;
        var tail = textKey.Substring(0, rest);
        composedKey += tail;
        return composedKey;
    }
}