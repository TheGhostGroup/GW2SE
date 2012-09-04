using System;

namespace GW2SE.Base
{
    public class Logger
    {
        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void WriteLineColored(string Message, ConsoleColor Color)
        {
            var tmpColor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.WriteLine(Message);
            Console.ForegroundColor = tmpColor;
        }

        public static void WriteColored(string Message, ConsoleColor Color)
        {
            var tmpColor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.Write(Message);
            Console.ForegroundColor = tmpColor;
        }

        public static void SetTitle(string Title)
        {
            Console.Title = Title;
        }
    }
}
