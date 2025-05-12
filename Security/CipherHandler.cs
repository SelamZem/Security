using System;
using Security.Algorithms;

namespace Security
{
    public class CipherHandler
    {
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();
                int choice = GetMenuChoice();

                if (choice == 0)
                    break;

                Console.WriteLine("\nEnter the text:");
                string input = Console.ReadLine();

                string encryptedText = string.Empty;
                string decryptedText = string.Empty;

                int caesarShift, a, b;
                string keyword;

                switch (choice)
                {
                    case 1: // Caesar + Rail Fence
                        Console.Write("Enter the Caesar shift value: ");
                        caesarShift = GetValidIntegerInput();

                        encryptedText = CaesarRail.Encrypt(input, caesarShift, 3); // Using key=3 for Rail Fence
                        decryptedText = CaesarRail.Decrypt(encryptedText, caesarShift, 3);
                        break;

                    case 2: // Affine + Columnar
                        Console.Write("Enter the 'a' parameter for Affine Cipher (must be coprime with 26): ");
                        a = GetValidIntegerInput();
                        while (GCD(a, 26) != 1)
                        {
                            Console.Write("Invalid 'a'. It must be coprime with 26. Enter again: ");
                            a = GetValidIntegerInput();
                        }

                        Console.Write("Enter the 'b' parameter for Affine Cipher: ");
                        b = GetValidIntegerInput();

                        Console.Write("Enter the keyword for Columnar Transposition: ");
                        keyword = GetNonEmptyString();

                        encryptedText = AffineColumnar.Encrypt(input, a, b, keyword);
                        decryptedText = AffineColumnar.Decrypt(encryptedText, a, b, keyword);
                        break;

                    case 3: // Atbash + Route Cipher
                        encryptedText = AtbashRoute.Encrypt(input);
                        decryptedText = AtbashRoute.Decrypt(encryptedText);
                        break;

                    case 4: // ROT13 + Zigzag Matrix
                        encryptedText = Rot13Zigzag.Encrypt(input);
                        decryptedText = Rot13Zigzag.Decrypt(encryptedText);
                        break;

                    case 5: // Keyword Substitution + Scytale
                        Console.Write("Enter the keyword for Keyword Substitution + Scytale: ");
                        keyword = GetNonEmptyString();

                        encryptedText = KeywordScytale.Encrypt(input, keyword);
                        decryptedText = KeywordScytale.Decrypt(encryptedText, keyword);
                        break;

                    case 6: // Monoalphabetic + Spiral
                        encryptedText = MonoSpiral.Encrypt(input);
                        decryptedText = MonoSpiral.Decrypt(encryptedText);
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        continue;
                }

                Console.WriteLine($"\nEncrypted Text: {encryptedText}");
                Console.WriteLine($"Decrypted Text: {decryptedText}");

                Console.WriteLine("\nWould you like to try another algorithm? (y/n)");
                char retry = Console.ReadKey().KeyChar;
                if (retry != 'y' && retry != 'Y')
                {
                    break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("Select a cipher algorithm:");
            Console.WriteLine("1. Caesar + Rail Fence");
            Console.WriteLine("2. Affine + Columnar");
            Console.WriteLine("3. Atbash + Route Cipher");
            Console.WriteLine("4. ROT13 + Zigzag Matrix");
            Console.WriteLine("5. Keyword Substitution + Scytale");
            Console.WriteLine("6. Monoalphabetic + Spiral");
            Console.WriteLine("0. Exit");
        }

        private int GetMenuChoice()
        {
            int choice;
            while (true)
            {
                Console.Write("\nEnter your choice (0-6): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= 6)
                    return choice;

                Console.WriteLine("Invalid input. Please enter a number between 0 and 6.");
            }
        }

        private int GetValidIntegerInput()
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                    return result;

                Console.WriteLine("Invalid input. Please enter a valid integer:");
            }
        }

        private string GetNonEmptyString()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input cannot be empty. Try again:");
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
