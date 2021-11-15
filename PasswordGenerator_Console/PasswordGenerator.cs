using System;

namespace PasswordGeneratorConsoleApp
{
    class PasswordGenerator
    {
        public static int length;
        public static string password;
        public static int minimumLength = 8;
        public static int maximumLength = 30;

        const string lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        const string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        const string specialCharacters = "@%+/'!#$^?:(){}[]~-_.";

        public static int ChooseLength()
        {
            Console.WriteLine($"Please choose the length of your password between {minimumLength} and {maximumLength} characters:");
            string input = Console.ReadLine();

            bool successfulConversionToInteger = int.TryParse(input, out length);

            if (successfulConversionToInteger)
            {
                if (length < minimumLength ^ length > maximumLength)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError: Your chosen length is too short or too long.");
                    Console.WriteLine($"The password has to have at least {minimumLength} and not more than {maximumLength} characters.\n");
                    QuitProgram();
                }
                else
                {
                    Console.WriteLine($"\nYou chose a password length of {length} characters.\n");
                }                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: You didn't enter a valid whole number.\n");
                QuitProgram();
            }

            return length;
        }

        public static string GeneratePassword(int length)
        {
            length = PasswordGenerator.length;

            string validPasswordCharacters = lowerCaseLetters + upperCaseLetters + numbers + specialCharacters;
            Random random = new Random();

            char[] characters = new char[length];
            for (int i = 0; i < length; i++)
            {
                characters[i] = validPasswordCharacters[random.Next(minimumLength, validPasswordCharacters.Length)];
            }

            password = new string(characters);

            bool containsLowerCaseLetters = password.IndexOfAny(lowerCaseLetters.ToCharArray()) != -1;
            bool containsUpperCaseLetters = password.IndexOfAny(upperCaseLetters.ToCharArray()) != -1;
            bool containsNumbers = password.IndexOfAny(numbers.ToCharArray()) != -1;
            bool containsSpecialCharacters = password.IndexOfAny(specialCharacters.ToCharArray()) != -1;

            if (!containsLowerCaseLetters | !containsUpperCaseLetters | !containsNumbers | !containsSpecialCharacters)
            {
                GeneratePassword(length);
            }     

            return password;   
        }

        public static void CopyPasswordToClipboard(string password)
        {
            password = PasswordGenerator.password;

            TextCopy.ClipboardService.SetText(password);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nThe password {password} was copied to the clipboard.\n"); 
            Console.ResetColor();
            Console.WriteLine("\nFirst, please paste your password somewhere to save it.\n");
        }

        public static void ClearClipboard()
        {
            TextCopy.ClipboardService.SetText(string.Empty);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nThe clipboard was cleared.\n");
        }

        public static void WrongButtonMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYou pressed a wrong button.");
            Console.WriteLine("Please press y for yes or n for no.\n");
            QuitProgram();
        }

        public static void QuitProgram()
        {
            Console.ResetColor();
            Console.WriteLine("\nYour password wasn't saved anywhere else than in the clipboard, if you chose so.");
            Console.WriteLine("\nPlease press any key to quit the program.\n");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}