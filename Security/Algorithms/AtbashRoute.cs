using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Algorithms
{
    internal class AtbashRoute
    {
        public static string Encrypt(string input)
        {
            string atbash = AtbashEncrypt(input);
            return RouteCipherEncrypt(atbash, 5); // Example: using 5 as the number of rows
        }

        public static string Decrypt(string input)
        {
            string route = RouteCipherDecrypt(input, 5); // Example: using 5 as the number of rows
            return AtbashDecrypt(route);
        }

        // Atbash Cipher: Reverse the alphabet (A->Z, B->Y, C->X, etc.)
        private static string AtbashEncrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                    result.Append((char)('Z' - (c - 'A')));
            }
            return result.ToString();
        }

        private static string AtbashDecrypt(string text)
        {
            return AtbashEncrypt(text); // Atbash encryption and decryption are the same
        }

        // Route Cipher: Arrange text in a matrix and read it in a specific pattern
        private static string RouteCipherEncrypt(string text, int rows)
        {
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Fill the matrix row by row
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (index < text.Length)
                        matrix[i, j] = text[index++];

            // Read the matrix in a spiral order (Route Cipher)
            StringBuilder result = new StringBuilder();
            int top = 0, left = 0, bottom = rows - 1, right = cols - 1;
            while (top <= bottom && left <= right)
            {
                for (int i = left; i <= right; i++) result.Append(matrix[top, i]);
                top++;
                for (int i = top; i <= bottom; i++) result.Append(matrix[i, right]);
                right--;
                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--) result.Append(matrix[bottom, i]);
                    bottom--;
                }
                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--) result.Append(matrix[i, left]);
                    left++;
                }
            }

            return result.ToString();
        }

        private static string RouteCipherDecrypt(string text, int rows)
        {
            int len = text.Length;
            int cols = (int)Math.Ceiling((double)len / rows);
            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Rebuild the matrix by reading the spiral order
            int top = 0, left = 0, bottom = rows - 1, right = cols - 1;
            while (top <= bottom && left <= right)
            {
                for (int i = left; i <= right; i++) matrix[top, i] = text[index++];
                top++;
                for (int i = top; i <= bottom; i++) matrix[i, right] = text[index++];
                right--;
                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--) matrix[bottom, i] = text[index++];
                    bottom--;
                }
                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--) matrix[i, left] = text[index++];
                    left++;
                }
            }

            // Rebuild the string from the matrix row by row
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (matrix[i, j] != '\0') result.Append(matrix[i, j]);

            return result.ToString();
        }
    }
}
