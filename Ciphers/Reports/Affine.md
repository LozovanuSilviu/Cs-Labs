# Affine Cipher

## Theory
&ensp;&ensp;&ensp;
The affine cipher is a type of monoalphabetic substitution cipher, where each letter in an alphabet is mapped to its numeric equivalent, encrypted using a simple mathematical function, and converted back to a letter. The formula used means that each letter encrypts to one other letter, and back again, meaning the cipher is essentially a standard substitution cipher with a rule governing which letter goes to which. As such, it has the weaknesses of all substitution ciphers. Each letter is enciphered with the function (ax + b) mod 26, where b is the magnitude of the shift.
## Implementation
### Initial Steps

1. Create the alphabet
2. Create a method which will compute modularMultiplicative



```
  private static int GetModularMultiplicative()
    {
        var counter = 26 % firstkey;
        if (counter % 2 == 0)
            counter++;
        else
            counter += 2;
        while (!(firstkey * counter % 26).Equals(1)) counter += 2;

        return counter;
    }

    private void CreateAlphabet()
    {
        for (var i = 0; i < letters.Length; i++) alphabetLetters.Add(i, letters[i]);
    }
```

## Encryption
&ensp;&ensp;&ensp;  Encryption is done using the formula:
```
E(x)=(ax+b) mod (m)
```
Here is the encryption method logic
```
 foreach (var letter in message)
        {
            if (letter == 32) continue;
            var index = alphabetLetters.FirstOrDefault(x => x.Value == letter).Key;
            var encryptedLetter = (firstkey * index + secondkey) % alphabetLetters.Count;
            encrypted += alphabetLetters[encryptedLetter];
        }
```

## Decryption
&ensp;&ensp;&ensp; The decryption is done using formula 
```
D(x)=a^-1*(x-b) mod (m)
```
Here is the decryption method logic
```
 foreach (var letter in message)
        {
            var decryptedLetter = 0;
            if (letter == 32) continue;
            var index = alphabetLetters.FirstOrDefault(x => x.Value == letter).Key;

            if (index - secondkey > 0)
                decryptedLetter = modularMultiplicative * (index - secondkey) % alphabetLetters.Count;
            else
                decryptedLetter = modularMultiplicative * (index - secondkey + alphabetLetters.Count) %
                                  alphabetLetters.Count;
            decrypted += alphabetLetters[decryptedLetter];
        }

```
