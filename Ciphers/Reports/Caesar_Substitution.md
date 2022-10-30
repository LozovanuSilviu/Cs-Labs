# Caesar Substitution Cipher

## Theory

&ensp;&ensp;&ensp; It's one of the simplest and most widely known encryption techniques. It is a type of substitution cipher in which each letter in the plaintext is replaced by a letter some fixed number of positions down the alphabet.

&ensp;&ensp;&ensp;  For example, with a left shift of 3, D would be replaced by A, E would become B, and so on. The method is named after Julius Caesar, who used it in his private correspondence.

## Implementation

### Initial Step
&ensp;&ensp; For this specific cipher implementation we dont need any setup like mapping of the alphabet elements to a specific index, we are just using the ASCII code of the needed characters.

### Encryption

&ensp;&ensp;&ensp;  This specific implementation of caesar cipher uses ASCII code of characters, so we are encrypting using the formula :
```
E(x)=(x+n) mod 26.
```
Here we have the encryption method logic:
```
foreach (var letter in lowerCaseMessage)
        {
            if (letter == 32) continue;
            var cryptedLetter = (letter - 97 + Key) % 26;
            var stringLetter = char.ConvertFromUtf32(cryptedLetter + 97);
            finalMessage = finalMessage + stringLetter;
        }
```

### Decryption

&ensp;&ensp;&ensp; Same logic used as for the encryption and using the decryption formula:

```
D(x)=(x-n) mod 26.
```
The decryption method logic:
```
 foreach (var letter in message)
        {
            if (letter == 32) continue;

            var decryptedLetter = (letter - 97 - Key) % 26;
            var stringLetter = char.ConvertFromUtf32(decryptedLetter + 97);
            finalMessage = finalMessage + stringLetter;
        }
```
