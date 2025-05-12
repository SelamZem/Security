using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Algorithms
{
    internal class MonoSpiral
    {
        private static readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] shuffledKey = "QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray(); // fixed substitution

        public static string Encrypt(string input)
        {
            string cleaned = CleanText(input);
            string substituted = MonoEncrypt(cleaned);
            return SpiralEncrypt(substituted);
        }

        public static string Decrypt(string input)
        {
            string spiralOut = SpiralDecrypt(input);
            return MonoDecrypt(spiralOut);
        }

        private static string MonoEncrypt(string text)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (c == alphabet[i])
                    {
                        result.Append(shuffledKey[i]);
                        break;
                    }
                }
            }

            return result.ToString();
        }

        private static string MonoDecrypt(string text)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                for (int i = 0; i < shuffledKey.Length; i++)
                {
                    if (c == shuffledKey[i])
                    {
                        result.Append(alphabet[i]);
                        break;
                    }
                }
            }

            return result.ToString();
        }

        private static string SpiralEncrypt(string text)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(text.Length));
            char[,] matrix = new char[size, size];

            // Fill matrix with padding 'X'
            for (int i = 0; i < size * size; i++)
            {
                int row = i / size;
                int col = i % size;
                matrix[row, col] = i < text.Length ? text[i] : 'X';
            }

            // Spiral traversal
            StringBuilder result = new StringBuilder();
            int top = 0, bottom = size - 1;
            int left = 0, right = size - 1;

            while (top <= bottom && left <= right)
            {
                for (int i = left; i <= right; i++) result.Append(matrix[top, i]);
                top++;
                for (int i = top; i <= bottom; i++) result.Append(matrix[i, right]);
                right--;
                for (int i = right; i >= left; i--) result.Append(matrix[bottom, i]);
                bottom--;
                for (int i = bottom; i >= top; i--) result.Append(matrix[i, left]);
                left++;
            }

            return result.ToString();
        }

        private static string SpiralDecrypt(string text)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(text.Length));
            char[,] matrix = new char[size, size];

            // Spiral fill
            int index = 0;
            int top = 0, bottom = size - 1;
            int left = 0, right = size - 1;

            while (top <= bottom && left <= right)
            {
                for (int i = left; i <= right && index < text.Length; i++) matrix[top, i] = text[index++];
                top++;
                for (int i = top; i <= bottom && index < text.Length; i++) matrix[i, right] = text[index++];
                right--;
                for (int i = right; i >= left && index < text.Length; i--) matrix[bottom, i] = text[index++];
                bottom--;
                for (int i = bottom; i >= top && index < text.Length; i--) matrix[i, left] = text[index++];
                left++;
            }

            // Read matrix row by row
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result.Append(matrix[i, j]);

            return result.ToString().TrimEnd('X'); // remove padding
        }

        private static string CleanText(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                    result.Append(c);
            }

            return result.ToString();
        }
    }
}
