using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Algorithms
{
    internal class KeywordScytale
    {
        public static string Encrypt(string input, string keyword)
        {
            string keywordSubstitution = KeywordSubstitutionEncrypt(input.ToUpper(), keyword.ToUpper());
            return ScytaleEncrypt(keywordSubstitution, 4); // using 4 rows
        }

        public static string Decrypt(string input, string keyword)
        {
            string scytale = ScytaleDecrypt(input.ToUpper(), 4);
            return KeywordSubstitutionDecrypt(scytale, keyword.ToUpper());
        }

        private static string KeywordSubstitutionEncrypt(string text, string keyword)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string key = new string(keyword.Distinct().ToArray());
            string cipher = key + new string(alphabet.Except(key).ToArray());

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (alphabet.Contains(c))
                    result.Append(cipher[alphabet.IndexOf(c)]);
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        private static string KeywordSubstitutionDecrypt(string text, string keyword)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string key = new string(keyword.Distinct().ToArray());
            string cipher = key + new string(alphabet.Except(key).ToArray());

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (cipher.Contains(c))
                    result.Append(alphabet[cipher.IndexOf(c)]);
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        private static string ScytaleEncrypt(string text, int rows)
        {
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            int index = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = index < text.Length ? text[index++] : '_';  // Use padding

            StringBuilder result = new StringBuilder();
            for (int j = 0; j < cols; j++)
                for (int i = 0; i < rows; i++)
                    result.Append(matrix[i, j]);

            return result.ToString();
        }

        private static string ScytaleDecrypt(string text, int rows)
        {
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            int index = 0;

            for (int j = 0; j < cols; j++)
                for (int i = 0; i < rows; i++)
                    matrix[i, j] = index < text.Length ? text[index++] : '_';

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result.Append(matrix[i, j]);

            return result.ToString().Replace("_", "");
        }
    }
}
