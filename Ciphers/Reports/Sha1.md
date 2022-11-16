# SHA256 Hash Function

## Theory
In cryptography, SHA-1 (Secure Hash Algorithm 1) is a cryptographically broken but still widely used hash function which takes an input and produces a 160-bit (20-byte) hash value known as a message digest – typically rendered as 40 hexadecimal digits. It was designed by the United States National Security Agency, and is a U.S. Federal Information Processing Standard

## Implementation
&ensp;&ensp;&ensp; This is an implementation of SHA256 which includes the basic steps:
1. Pre-processing of the original message
2. Hashing original message
3. Performing encryption using rsa algorithm
4. Saving crypted message in local database this case in memeory one.
5. Perform decryption
### Main Steps:
1. Pre-process the original message
```
Byte[] Buffer = Encoding.ASCII.GetBytes(message);
```
2. Hashing original message
```
Byte[] Hash = Crypto.ComputeHash(Buffer);
```
3.Performing encryption using rsa algorithm
```
key = RSA.GenerateKeyPair(2048);
var enc = RSA.Encrypt(bytesToEncrypt, key.Public);
```
4. Saving crypted message in local database
```
localDb.Add("crypted", enc); 
```
5. Perform decryption

```
var toDecrypt = localDb["crypted"];
var decrypted = RSA.Decrypt(toDecrypt, key.Private);
```