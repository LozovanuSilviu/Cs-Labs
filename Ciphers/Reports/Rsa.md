# RSA Cipher

## Theory

&ensp;&ensp;&ensp; RSA algorithm is an asymmetric cryptography algorithm. Asymmetric actually means that it works on two
different keys i.e. Public Key and Private Key. As the name describes that the Public Key is given to everyone and the
Private key is kept private.

## Implementation

&ensp;&ensp;&ensp; This is an example of RSA algorithm which follows all the required steps:

1. generating two numbers and checking if they are prime (for that I use isprime from sympy)
2. determining the greatest common divisor (imported gcd from math)
3. determining the multiplicative inverse
4. perform encryption and decryption

### Initial Steps:

&ensp;&ensp;&ensp; For the start is generation of key pairs from generated prime numbers.

```
public static RsaKeyPair GenerateKeyPair(int keySize) {
  BigInteger p = GetRandomPrime(keySize / 2);
  BigInteger q = GetRandomPrime(keySize / 2);
  BigInteger n = p * q;
  BigInteger pi = (p - 1) * (q - 1);
  BigInteger e = 65537;
  BigInteger d = ModInverse(e, pi);

  return new RsaKeyPair(new Rsa(n, e), new RsaPrivate(n, d));
}
```

### Encryption

&ensp;&ensp;&ensp; At the encryption part we determine the unicode of each character in plaintext, afterwards compute
ciphertext using public key.

```
public static byte[] Encrypt(byte[] plain, Rsa pub) {
  BigInteger plainInt = new BigInteger(plain);
  BigInteger cipherInt = BigInteger.ModPow(plainInt, pub.E, pub.N);
  return cipherInt.ToByteArray();
}
```

### Decryption

&ensp;&ensp;&ensp; In the decryption I reverse the padding scheme, using the private key I determine the unicode of each
character in the string and find the matching char for it.

```
 public static byte[] Decrypt(byte[] cipher, RsaPrivate pri) {
  BigInteger cipherInt = new BigInteger(cipher);
  BigInteger plainInt = BigInteger.ModPow(cipherInt, pri.D, pri.N);
  return plainInt.ToByteArray();
}
```
