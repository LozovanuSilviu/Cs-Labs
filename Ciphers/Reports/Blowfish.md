# Block Cipher

## Theory

&ensp;&ensp;&ensp; This is an implementation of the Blowfish cipher, which is a symmetric key cipher implementing permutation and
substitution boxes. These boxes are used to scramble a message during encryption and then unscramble it during
decryption.

## Implementation
The functions used are __init__, encrypt, decrypt, __round_func, and cipher. __init__ is used to initialize the
permutation and substitution boxes using a given key. Encrypt will scramble a given 8 byte string. Decrypt will
unscramble the given encrypted string. __round_func is used as the round robin XOR on integers pertaining to the boxes.
Cipher is used to initialize the key made from the original key used for scrambling and unscrambling the message given.

*accepts only 8 byte blocks, but you can introduce two at the same time if you wish

### Initial Steps:
&ensp;&ensp;&ensp; As for start we use the constructor which is being basically used to create an instance of blowfish and use key as encryption key.

Cipher function being used for encrypting data, having two blocks (upper and lower) of 32-bits.

Round_func working with the same blocks of data and returning 32-bit result as a long integer

```
 public BlowFish(String key1)
        {

            byte[] key = Encoding.Unicode.GetBytes(key1);
            subs = (uint[,]) SUBS.Clone();
            pi = (uint[]) PI.Clone();
            uint temp, left, right;
            left = 0x00000000;
            right = 0x00000000;
            int keyLen = key.Length;
            int k = 0;
            for (int i = 0; i < N16bit + 2; i++)
            {
                temp = 0x00000000;
                for (int j = 0; j < 4; j++, k++)
                    temp = (temp << 8) | key[k % keyLen];
                pi[i] ^= temp;
            }

            for (int i = 0; i < N16bit + 2; i += 2)
            {
                Encrypt(ref left, ref right);
                pi[i] = left;
                pi[i + 1] = right;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 256; j += 2)
                {
                    Encrypt(ref left, ref right);
                    subs[i, j] = left;
                    subs[i, j + 1] = right;
                }
            }
        }   
```

### Encryption
&ensp;&ensp;&ensp;  At the encryption part we encrypt a 8-byte block of text, which should be 8 byte string and return encrypted string. For this, I use big endianness for the blocks of data xl and xr, which means I store the most significant byte of a word at the smallest memory address and the least significant byte at the largest.
```
  public String Encrypt(String text)
        {
            _end = (short) (4 - (text.Length) % 4);
            int ij = -1;
            while (++ij < _end)
                text = text.Insert(text.Length, "0");
            byte[] stringInBytes = Encoding.Unicode.GetBytes(text);
            uint xl, xr;
            int length = stringInBytes.Length;
            byte[] data = stringInBytes;
            for (int i = 0; i < length; i += 8)
            {
                xl = (uint) ((data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8) | data[i + 3]);
                xr = (uint) ((data[i + 4] << 24) | (data[i + 5] << 16) | (data[i + 6] << 8) | data[i + 7]);
                Encrypt(ref xl, ref xr);
                data[i] = (byte) (xl >> 24);
                data[i + 1] = (byte) (xl >> 16);
                data[i + 2] = (byte) (xl >> 8);
                data[i + 3] = (byte) (xl);
                data[i + 4] = (byte) (xr >> 24);
                data[i + 5] = (byte) (xr >> 16);
                data[i + 6] = (byte) (xr >> 8);
                data[i + 7] = (byte) (xr);
            }

            return (Convert.ToBase64String(data));
        }
```

### Decryption
&ensp;&ensp;&ensp; In the decryption I use the same method as in encryption, this time only in opposite 'direction'.
```
 public String Decrypt(String source)
        {
            
            byte[] data = Convert.FromBase64String(source);
            uint xl, xr;
            int length = data.Length;
            string text;
            for (int i = 0; i < length; i += 8)
            {
                // Encode the data in 8 byte blocks.
                xl = (uint) ((data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8) | data[i + 3]);
                xr = (uint) ((data[i + 4] << 24) | (data[i + 5] << 16) | (data[i + 6] << 8) | data[i + 7]);
                Decrypt(ref xl, ref xr);
                // Now Replace the data.
                data[i] = (byte) (xl >> 24);
                data[i + 1] = (byte) (xl >> 16);
                data[i + 2] = (byte) (xl >> 8);
                data[i + 3] = (byte) (xl);
                data[i + 4] = (byte) (xr >> 24);
                data[i + 5] = (byte) (xr >> 16);
                data[i + 6] = (byte) (xr >> 8);
                data[i + 7] = (byte) (xr);
            }

            text = Encoding.Unicode.GetString(data);
            return (text.Remove(text.Length - _end ));
        }
```