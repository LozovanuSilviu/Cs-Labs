using System.Diagnostics.Contracts;
using Ciphers.Common;

namespace Ciphers.Algorithms;

public class Affine : ICipher
{
    private static Dictionary<int, char> alphabetLetters = new ();
    private static string letters = "abcdefghijklmnopqrstuvwxyz";
    public const int firstkey = 11;
    public const int secondkey = 7;

    public static int GetModularMultiplicative()
    {
        var counter = 26 % firstkey;
        if (counter % 2 ==0)
        {
            counter++;
        }
        else
        {
            counter += 2;
        }
        while (!(firstkey*counter %26).Equals(1))
        {
            counter += 2;
        }

        return counter;
    }
    
    public  void CreateAlphabet()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            alphabetLetters.Add(i, letters[i]);
        }
    }
    
    public string Encrypt(string message)
    {
        CreateAlphabet();
        var encrypted = "";
        foreach (var letter in message)
        {
            if (letter==32)
            {
                encrypted += " ";
                continue;
            }
            var index = alphabetLetters.FirstOrDefault(x => x.Value == letter).Key;
            var encryptedLetter = (firstkey * index + secondkey) % alphabetLetters.Count;
            encrypted += alphabetLetters[encryptedLetter];
        }

        return encrypted;
    }

    public string Decrypt(string message)
    {
        var modularMultiplicative = GetModularMultiplicative();
        var decrypted = "";
        foreach (var letter in message)
        {
            var decryptedLetter = 0;
            if (letter==32)
            {
                decrypted += " ";
                continue;
            }
            var index =alphabetLetters.FirstOrDefault(x => x.Value == letter).Key;
            
            if (index - secondkey >0)
            {
                 decryptedLetter = modularMultiplicative * (index - secondkey) % alphabetLetters.Count;
            }
            else
            {
                 decryptedLetter = modularMultiplicative * (index - secondkey + alphabetLetters.Count) % alphabetLetters.Count;

            }
            decrypted += alphabetLetters[decryptedLetter];
        }

        return decrypted;
    }
}