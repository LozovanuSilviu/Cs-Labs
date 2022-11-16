# Vigenere Cipher

## Theory

&ensp;&ensp;&ensp; The Vigenère cipher is a polyalphabetic substitution cipher that is a natural evolution of the Caesar
cipher. The Caesar cipher encrypts by shifting each letter in the plaintext up or down a certain number of places in the
alphabet. If the message was right shifted by 4, each A would become E, and each S would become W.

&ensp;&ensp;&ensp; In the Vigenère cipher, a message is encrypted using a secret key, as well as an encryption table (
called a Vigenere square, Vigenere table, or tabula recta). The tabula recta typically contains the 26 letters of the
Latin alphabet from A to Z along the top of each column, and repeated along the left side at the beginning of each row.
Each row of the square has the 26 letters of the Latin alphabet, shifted one position to the right in a cyclic way as
the rows progress downwards. Once B moves to the front, A moves down to the end. This continues for the entire square.

## Implementation

### Initial Step

&ensp;&ensp;&ensp; Make an initial mapping using dictionaries between letters in alphabet and corresponding indexes.

```
private void CreateAlphabet() {
  for (var i = 0; i < letters.Length; i++) alphabetLetters.Add(i, letters[i]);
}
```

Also create a method which will return you the composed key

```
private string GetComposedKey(int length) {
  var keyLength = textKey.Length;
  var intParts = length / keyLength;
  var composedKey = "";
  for (var i = 0; i < intParts; i++) composedKey += textKey;
  var rest = length % keyLength;
  var tail = textKey.Substring(0, rest);
  composedKey += tail;
  return composedKey;
}
```

#### Encryption

&ensp;&ensp;&ensp; In encryption and decryption we use the same mapping between letters and indexes.

```
for (var i = 0; i < message.Length; i++) {
  if (message[i] == 32) continue;
  var messageIndex =
      alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
  var keyIndex = alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
  var cryptedLetter = (messageIndex + keyIndex) % 26;
  encryptedMessage += alphabetLetters[cryptedLetter];
}
```

### Decryption

```
for (var i = 0; i < message.Length; i++) {
  if (message[i] == 32) continue;
  var messageIndex =
      alphabetLetters.FirstOrDefault(x => x.Value == message[i]).Key;
  var keyIndex = alphabetLetters.FirstOrDefault(x => x.Value == key[i]).Key;
  var deCryptedLetter = (messageIndex - keyIndex) % 26;
  if (deCryptedLetter < 0) deCryptedLetter += alphabetLetters.Count;
  decryptedMessage += alphabetLetters[deCryptedLetter];
}```
