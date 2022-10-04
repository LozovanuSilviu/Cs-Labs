namespace Ciphers.Algorithms;
using Common;
public class CaesarWithTwoKeys : ICipher
{
    private static Dictionary<int, char> alphabetLetters = new ();
    private static string letters = "abcdefghijklmnopqrstuvwxyz";

    private const int Key = 5;
    private const string textKey = "time is tickin";
    
    public static string RemoveDuplicates(string input)
    {
        var result = "";
        var trimmedKey = input.Replace(" ", string.Empty);
        for (int i = 0; i < trimmedKey.Length; i++)
        {
            if (!result.Contains(trimmedKey[i]))
            {
                result+= trimmedKey[i];
            }
        }
        return result;
    }

    public  void CreateAlphabet()
    {
        var counter = 0;
        letters = RemoveDuplicates(textKey) + letters;
        for (int i = 0; i < letters.Length; i++)
        {
            if (!alphabetLetters.ContainsValue(letters[i]))
            {
                alphabetLetters.Add(i-counter,letters[i]);
            }
            else
            {
                counter++;
            }
        }
    }
    
    public string Encrypt(string message)
    {
        var messageLower = message.ToLower();
        var crypted = "";
        CreateAlphabet();
        for (int i=0; i<messageLower.Length; i++)
        {
            if (messageLower[i]==32)
            {
                crypted+=" ";
                continue;
            }
            var index = alphabetLetters.FirstOrDefault(x => x.Value == messageLower[i]).Key;
            var cryptedIndex = (index + Key) % alphabetLetters.Count;
            var cryptedLetter = alphabetLetters[cryptedIndex];
            crypted += cryptedLetter;
        }

        return crypted;
    }

    public string Decrypt(string message)
    {
        var deCrypted = "";
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i]==32)
            {
                deCrypted+=" ";
                continue;
            }
            var index = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var deCryptedIndex = (index - Key) % alphabetLetters.Count;
            if (deCryptedIndex<0)
            {
                deCryptedIndex += alphabetLetters.Count;
            }
            var deCryptedLetter = alphabetLetters[deCryptedIndex];
            deCrypted += deCryptedLetter;
        }

        return deCrypted;
    }
}