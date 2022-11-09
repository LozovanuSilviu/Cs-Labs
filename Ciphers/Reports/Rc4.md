# Stream Cipher

## Theory

&ensp;&ensp;&ensp; A stream cipher is a symmetric key cipher where plaintext digits are combined with a pseudorandom cipher digit stream (keystream). In a stream cipher, each plaintext digit is encrypted one at a time with the corresponding digit of the keystream, to give a digit of the ciphertext stream. Since encryption of each digit is dependent on the current state of the cipher, it is also known as state cipher. In practice, a digit is typically a bit and the combining operation is an exclusive-or (XOR).

### Initial Steps:
&ensp;&ensp;&ensp; For start we use the random key generator.
```
 private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
```

### Encryption
&ensp;&ensp;&ensp; In the encryption part we use the new key and index position of each character. With that we add all characters together and do modulo the length of alphaCollection. Afterwards, just add the characters from alphacollection based on the new index.
```
public byte[] Encrypt(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();

            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
                cipher[m] = (byte)(data[m] ^ keyItem());

            return cipher;
        }
```

### Decryption
&ensp;&ensp;&ensp; Same as encryption. Only difference is that we perform a backwards operation.
```
 public byte[] Decrypt(byte[] dataB, int size)
        {
            return Encrypt(dataB, size);
        }
```
