namespace PasswordGeneratorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nHello, this is a password generator program.\n");

            int length = PasswordGenerator.ChooseLength();
            string password = PasswordGenerator.GeneratePassword(length);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYour password is: {password}");

            Console.ResetColor();
            Console.WriteLine("\nDo you want to copy this password to the clipboard? y/n:");
            ConsoleKeyInfo input = Console.ReadKey();
            if (input.KeyChar.ToString() == "y")
            {
                PasswordGenerator.CopyPasswordToClipboard(password);

                Console.WriteLine("\nDo you want to clear the clipboard? y/n:");
                input = Console.ReadKey();
                if (input.KeyChar.ToString() == "y")
                {
                    PasswordGenerator.ClearClipboard();
                }
                else if (input.KeyChar.ToString() == "n")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nYou didn't clear the clipboard.\n");
                }
                else
                {
                    PasswordGenerator.WrongButtonMessage();
                }
            }
            else if (input.KeyChar.ToString() == "n")
            {
                Console.WriteLine("\nYou didn't copy this password to the clipboard.\n");
            }
            else
            {
                PasswordGenerator.WrongButtonMessage();
            }

            PasswordGenerator.QuitProgram();
        }
    }
}
