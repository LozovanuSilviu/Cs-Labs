namespace Ciphers.Ciphers.Implementations.Symmetric;

public class Rc4
{
    private readonly byte[] S = new byte[256];

    private int x;
    private int y;

    public Rc4(byte[] key)
    {
        init(key);
    }

    // For initial initialization of the vector permutation key,
    // key-scheduling algorithm is used.
    private void init(byte[] key)
    {
        var keyLength = key.Length;

        for (var i = 0; i < 256; i++)
            S[i] = (byte) i;

        var j = 0;
        for (var i = 0; i < 256; i++)
        {
            j = (j + S[i] + key[i % keyLength]) % 256;
            S.Swap(i, j);
        }
    }

    // Pseudo-Random Generation Algorithm.
    private byte keyItem()
    {
        x = (x + 1) % 256;
        y = (y + S[x]) % 256;

        S.Swap(x, y);

        return S[(S[x] + S[y]) % 256];
    }

    public byte[] Encrypt(byte[] dataB, int size)
    {
        var data = dataB.Take(size).ToArray();

        var encryptedText = new byte[data.Length];

        for (var m = 0; m < data.Length; m++)
            encryptedText[m] = (byte) (data[m] ^ keyItem());

        return encryptedText;
    }

    public byte[] Decrypt(byte[] dataB, int size)
    {
        return Encrypt(dataB, size);
    }
}

internal static class SwapExt
{
    public static void Swap<T>(this T[] array, int index1, int index2)
    {
        var temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }
}