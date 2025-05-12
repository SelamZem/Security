using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Algorithms
{
    internal class AffineColumnar
    {
        public static string Encrypt(string input, int a, int b, string key)
        {
            string affineEncrypted = AffineEncrypt(input, a, b);
            return ColumnarEncrypt(affineEncrypted, key);
        }

        public static string Decrypt(string input, int a, int b, string key)
        {
            string columnarDecrypted = ColumnarDecrypt(input, key);
            return AffineDecrypt(columnarDecrypted, a, b);
        }

        // Affine Cipher: Encrypts using the formula E(x) = (ax + b) % 26
        private static string AffineEncrypt(string text, int a, int b)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    int x = c - 'A';
                    char encryptedChar = (char)((a * x + b) % 26 + 'A');
                    result.Append(encryptedChar);
                }
                else
                {
                    result.Append(c); // Non-alphabet characters remain unchanged
                }
            }
            return result.ToString();
        }

        private static string AffineDecrypt(string text, int a, int b)
        {
            // Calculate modular inverse of 'a' modulo 26
            int aInv = ModInverse(a, 26);
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    int x = c - 'A';
                    char decryptedChar = (char)((aInv * (x - b + 26) % 26) + 'A');
                    result.Append(decryptedChar);
                }
                else
                {
                    result.Append(c); // Non-alphabet characters remain unchanged
                }
            }
            return result.ToString();
        }

        // Modulo inverse of a number under modulo m
        private static int ModInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return -1; // If no modular inverse exists
        }

        // Columnar Transposition Cipher: Reorder the characters based on a key
        private static string ColumnarEncrypt(string text, string key)
        {
            int numCols = key.Length;
            int numRows = (int)Math.Ceiling((double)text.Length / numCols);
            char[,] grid = new char[numRows, numCols];

            // Fill the grid with the plaintext
            int index = 0;
            for (int col = 0; col < numCols; col++)
                for (int row = 0; row < numRows && index < text.Length; row++)
                    grid[row, col] = text[index++];

            // Read the grid by columns according to the key
            StringBuilder result = new StringBuilder();
            foreach (char c in SortKey(key))
            {
                int colIndex = key.IndexOf(c);
                for (int row = 0; row < numRows; row++)
                {
                    if (grid[row, colIndex] != '\0') result.Append(grid[row, colIndex]);
                }
            }
            return result.ToString();
        }

        private static string ColumnarDecrypt(string text, string key)
        {
            int numCols = key.Length;
            int numRows = (int)Math.Ceiling((double)text.Length / numCols);
            char[,] grid = new char[numRows, numCols];

            // Fill the grid with ciphertext column by column according to the sorted key
            int index = 0;
            foreach (char c in SortKey(key))
            {
                int colIndex = key.IndexOf(c);
                for (int row = 0; row < numRows && index < text.Length; row++)
                {
                    grid[row, colIndex] = text[index++];
                }
            }

            // Read the grid by rows to form the plaintext
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < numRows; row++)
                for (int col = 0; col < numCols; col++)
                    if (grid[row, col] != '\0') result.Append(grid[row, col]);

            return result.ToString();
        }

        // Sort the key to decide the order of reading columns
        private static string SortKey(string key)
        {
            var sorted = new List<char>(key);
            sorted.Sort();
            return new string(sorted.ToArray());
        }
    }
}
