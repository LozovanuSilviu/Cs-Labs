# Caesar Permutation Cipher

## Theory

&ensp;&ensp;&ensp; It is a type of mono-alphabetic permutation cipher where the letters of the alphabet are arranged based on a given key.


## Implementation


### Alphabet Generation

&ensp;&ensp;&ensp; Add at start only unique characters from the message, afterwards all the remaining alphabet letters.
```
 private void CreateAlphabet()
    {
        var counter = 0;
        letters = RemoveDuplicates(textKey) + letters;
        for (var i = 0; i < letters.Length; i++)
            if (!alphabetLetters.ContainsValue(letters[i]))
                alphabetLetters.Add(i - counter, letters[i]);
            else
                counter++;
    } 
```
### Encryption
&ensp;&ensp;&ensp;  At the encryption part we use dictionaries which are mapping letters in the alphabet and their indexes.
In this way, when we make a shift we simply deduce the letter by checking it new index and replacing it with the new corresponding letter.
```
 for (var i = 0; i < messageLower.Length; i++)
        {
            if (messageLower[i] == 32) continue;

            var index = alphabetLetters.FirstOrDefault(x => x.Value == messageLower[i]).Key;
            var cryptedIndex = (index + Key) % alphabetLetters.Count;
            var cryptedLetter = alphabetLetters[cryptedIndex];
            crypted += cryptedLetter;
        }
```
### Decryption
&ensp;&ensp;&ensp; Same dictionaries are used but this time we perform a shift back.

```
 for (var i = 0; i < message.Length; i++)
        {
            if (message[i] == 32) continue;

            var index = alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
            var deCryptedIndex = (index - Key) % alphabetLetters.Count;
            if (deCryptedIndex < 0) deCryptedIndex += alphabetLetters.Count;

            var deCryptedLetter = alphabetLetters[deCryptedIndex];
            deCrypted += deCryptedLetter;
        }
```