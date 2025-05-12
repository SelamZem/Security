using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Algorithms
{
    internal class Rot13Zigzag
    {
        public static string Encrypt(string input)
        {
            string rot13 = Rot13Encrypt(input);
            return ZigzagEncrypt(rot13, 3); // Example: using 3 rows for Zigzag
        }

        public static string Decrypt(string input)
        {
            string zigzag = ZigzagDecrypt(input, 3); // Example: using 3 rows for Zigzag
            return Rot13Decrypt(zigzag);
        }

        // ROT13 Cipher: Shift each letter by 13 positions in the alphabet
        private static string Rot13Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (c >= 'A' && c <= 'Z')
                    result.Append((char)('A' + ((c - 'A' + 13) % 26)));
                else if (c >= 'a' && c <= 'z')
                    result.Append((char)('a' + ((c - 'a' + 13) % 26)));
                else
                    result.Append(c); // Non-alphabet characters remain unchanged
            }
            return result.ToString();
        }

        private static string Rot13Decrypt(string text)
        {
            return Rot13Encrypt(text); // ROT13 encryption and decryption are the same
        }

        // Zigzag Cipher: Arrange the text in a zigzag pattern with multiple rows
        private static string ZigzagEncrypt(string text, int rows)
        {
            if (rows == 1) return text;

            StringBuilder[] zigzag = new StringBuilder[rows];
            for (int i = 0; i < rows; i++)
                zigzag[i] = new StringBuilder();

            int currentRow = 0;
            bool goingDown = false;

            foreach (char c in text)
            {
                zigzag[currentRow].Append(c);
                if (currentRow == 0 || currentRow == rows - 1)
                    goingDown = !goingDown;
                currentRow += goingDown ? 1 : -1;
            }

            StringBuilder result = new StringBuilder();
            foreach (var row in zigzag)
                result.Append(row.ToString());

            return result.ToString();
        }

        private static string ZigzagDecrypt(string text, int rows)
        {
            if (rows == 1) return text;

            char[] arr = text.ToCharArray();
            char[,] zigzag = new char[rows, text.Length / rows + (text.Length % rows == 0 ? 0 : 1)];

            bool goingDown = false;
            int currentRow = 0;
            int currentCol = 0;

            // Create the zigzag matrix pattern
            for (int i = 0; i < arr.Length; i++)
            {
                zigzag[currentRow, currentCol] = arr[i];
                currentCol++;

                if (currentRow == 0 || currentRow == rows - 1)
                    goingDown = !goingDown;

                currentRow += goingDown ? 1 : -1;

                if (currentRow == 0 || currentRow == rows - 1)
                    currentCol = 0;
            }

            // Rebuild the string from the zigzag pattern
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < zigzag.GetLength(1); col++)
                    if (zigzag[row, col] != '\0')
                        result.Append(zigzag[row, col]);

            return result.ToString();
        }
    }
}
