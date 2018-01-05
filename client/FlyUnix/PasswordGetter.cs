using System;
using System.Security;

namespace FlyUnix
{
    public static class PasswordGetter
    {
        public static SecureString GetPassword()
        {
            AskForPassword();

            var password = new SecureString();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return password;
        }

        private static void AskForPassword()
        {
            Console.Write("Password: ");
        }
    }
}
