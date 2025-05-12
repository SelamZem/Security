using System;

namespace Security.Algorithms
{
    internal class CaesarRail
    {
        // Caesar Cipher Encryption
        public static string Encrypt(string text, int caesarShift, int railFenceKey)
        {
            string caesarEncrypted = CaesarEncrypt(text, caesarShift);
            string railFenceEncrypted = RailFenceEncrypt(caesarEncrypted, railFenceKey);
            return railFenceEncrypted;
        }

        // Caesar Cipher Decryption
        public static string Decrypt(string text, int caesarShift, int railFenceKey)
        {
            string railFenceDecrypted = RailFenceDecrypt(text, railFenceKey);
            string caesarDecrypted = CaesarDecrypt(railFenceDecrypted, caesarShift);
            return caesarDecrypted;
        }

        // Caesar Cipher Encryption Helper
        private static string CaesarEncrypt(string text, int shift)
        {
            char[] buffer = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    buffer[i] = (char)((((letter + shift) - offset) % 26 + 26) % 26 + offset);
                }
                else
                {
                    buffer[i] = letter;
                }
            }
            return new string(buffer);
        }

        // Caesar Cipher Decryption Helper
        private static string CaesarDecrypt(string text, int shift)
        {
            return CaesarEncrypt(text, -shift); // Decrypt by shifting in the opposite direction
        }

        // Rail Fence Cipher Encryption Helper
        private static string RailFenceEncrypt(string text, int key)
        {
            if (key == 1) return text;

            char[] railFence = new char[text.Length];
            int[] rail = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                rail[i] = (i % (2 * key - 2) < key) ? i % key : key - (i % key) - 1;
            }

            int pos = 0;
            for (int r = 0; r < key; r++)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (rail[i] == r)
                    {
                        railFence[pos++] = text[i];
                    }
                }
            }

            return new string(railFence);
        }

        // Rail Fence Cipher Decryption Helper
        private static string RailFenceDecrypt(string cipherText, int key)
        {
            if (key == 1) return cipherText;

            char[] railFence = new char[cipherText.Length];
            int[] rail = new int[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                rail[i] = (i % (2 * key - 2) < key) ? i % key : key - (i % key) - 1;
            }

            int pos = 0;
            for (int r = 0; r < key; r++)
            {
                for (int i = 0; i < cipherText.Length; i++)
                {
                    if (rail[i] == r)
                    {
                        railFence[i] = cipherText[pos++];
                    }
                }
            }

            return new string(railFence);
        }
    }
}
