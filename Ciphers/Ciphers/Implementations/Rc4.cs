﻿namespace Ciphers.Ciphers.Implementations
{
    public class Rc4
    {
        byte[] S = new byte[256];  

        int x = 0;
        int y = 0;

        public Rc4(byte[] key)
        {
            init(key);
        }

        // For initial initialization of the vector permutation key,
        // key-scheduling algorithm is used.
        private void init(byte[] key)
        {
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
                S[i] = (byte)i;

            int j = 0;
            for (int i = 0; i < 256; i++)
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
            byte[] data = dataB.Take(size).ToArray();

            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
                cipher[m] = (byte)(data[m] ^ keyItem());

            return cipher;
        }

        public byte[] Decrypt(byte[] dataB, int size)
        {
            return Encrypt(dataB, size);
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
    
}