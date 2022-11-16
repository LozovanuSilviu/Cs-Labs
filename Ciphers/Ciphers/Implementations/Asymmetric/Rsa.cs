using System.Numerics;
using System.Security.Cryptography;

namespace Ciphers.Ciphers.Implementations.Asymmetric;

public class Rsa
{
    public Rsa(BigInteger n, BigInteger e)
    {
        N = n;
        E = e;
    }
    
    public BigInteger N { get; }
    public BigInteger E { get; }
}

public class RsaPrivate
{
    public RsaPrivate(BigInteger n, BigInteger d)
    {
        N = n;
        D = d;
    }

    public BigInteger N { get; }
    public BigInteger D { get; }
}

public class RsaKeyPair
{
    public RsaKeyPair(Rsa pub, RsaPrivate pri)
    {
        Public = pub;
        Private = pri;
    }

    public Rsa Public { get; }
    public RsaPrivate Private { get; }
}

public static class RSA
{
    public static RsaKeyPair GenerateKeyPair(int keySize)
    {
        var p = GetRandomPrime(keySize / 2);
        var q = GetRandomPrime(keySize / 2);
        var n = p * q;
        var pi = (p - 1) * (q - 1);
        BigInteger e = 65537;
        var d = ModInverse(e, pi);

        return new RsaKeyPair(new Rsa(n, e), new RsaPrivate(n, d));
    }

    public static byte[] Encrypt(byte[] plain, Rsa pub)
    {
        var plainInt = new BigInteger(plain);
        var cipherInt = BigInteger.ModPow(plainInt, pub.E, pub.N);
        return cipherInt.ToByteArray();
    }

    public static byte[] Decrypt(byte[] cipher, RsaPrivate pri)
    {
        var cipherInt = new BigInteger(cipher);
        var plainInt = BigInteger.ModPow(cipherInt, pri.D, pri.N);
        return plainInt.ToByteArray();
    }

    private static void ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
    {
        if (a % b == 0)
        {
            x = 0;
            y = 1;
        }
        else
        {
            BigInteger subX, subY;
            ExtendedGcd(b, a % b, out subX, out subY);
            x = subY;
            y = subX - subY * (a / b);
        }
    }

    private static BigInteger ModInverse(BigInteger value, BigInteger mod)
    {
        BigInteger x;
        ExtendedGcd(value, mod, out x, out _);
        if (x < 0)
            x += mod;

        return x % mod;
    }

    private static BigInteger GetRandomPrime(int bitCount)
    {
        BigInteger prime;
        var rng = RandomNumberGenerator.Create();
        var byteLength = bitCount / 8 + (bitCount % 8 > 0 ? 1 : 0);
        do
        {
            var bytes = new byte[byteLength];
            rng.GetBytes(bytes);
            prime = new BigInteger(bytes);
        } while (!IsProbablyPrime(prime, 40));

        rng.Dispose();
        return prime;
    }

    private static bool IsProbablyPrime(BigInteger target, int repeat)
    {
        if (target == 2 || target == 3)
            return true;
        if (target < 2 || target % 2 == 0)
            return false;

        var d = target - 1;
        var s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            ++s;
        }

        var rng = RandomNumberGenerator.Create();
        var bytes = new byte[target.ToByteArray().LongLength];
        BigInteger a;

        for (var i = 0; i < repeat; ++i)
        {
            do
            {
                rng.GetBytes(bytes);
                a = new BigInteger(bytes);
            } while (a < 2 || a >= target - 2);

            var x = BigInteger.ModPow(a, d, target);
            if (x == 1 || x == target - 1)
                continue;

            for (var r = 1; r < s; ++r)
            {
                x = BigInteger.ModPow(x, 2, target);
                if (x == 1)
                    return false;
                if (x == target - 1)
                    break;
            }

            if (x != target - 1)
                return false;
        }

        rng.Dispose();
        return true;
    }
}