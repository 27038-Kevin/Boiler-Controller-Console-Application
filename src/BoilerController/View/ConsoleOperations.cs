using BoilerController.Models;
using BoilerController.Utils;

namespace BoilerController.View
{
    internal delegate void UIRenderer();

    internal class ConsoleOperations
    {
        internal void ClearScreen()
        {
            const string clearScreenAnsiSequence = "\u001b[2J";
            const string clearScrollbackAnsiSequence = "\u001b[3J";
            const string moveCursorToTopLeftAnsiSequence = "\u001b[H";

            Console.Write(
                clearScreenAnsiSequence +
                clearScrollbackAnsiSequence +
                moveCursorToTopLeftAnsiSequence);
        }

        internal UIRenderer DisplayBoilerStatus(Boiler boiler)
        {
            return () =>
            {
                string text = $"Boiler System Status: {boiler.SystemStatus}";
                var originalPosition = Console.GetCursorPosition();
                Console.SetCursorPosition(Console.WindowWidth - text.Length, 0);
                Console.Write(text);
                Console.SetCursorPosition(originalPosition.Left, originalPosition.Top);
            };
        }

        internal UIRenderer DisplayEventLogs(IEnumerable<BoilerEvent> eventLogs)
        {
            return () =>
            {
                Console.WriteLine("Press Enter to see the next line, or Esc to exit.");
                Console.WriteLine();
                Console.WriteLine("Timestamp            Event Title          Event Description");
                foreach (var eventInfo in eventLogs)
                {
                    Console.WriteLine($"{eventInfo.Timestamp,-20} {eventInfo.Title,-20} {eventInfo.Description}");
                    while (true)
                    {
                        ConsoleKeyInfo key = GetKeyPress(string.Empty);

                        if (key.Key == ConsoleKey.Escape)
                            return;

                        if (key.Key == ConsoleKey.Enter)
                            break;
                    }
                }
            };
        }

        internal UIRenderer DisplayMenu<T>() where T : struct, Enum
        {
            string[] menu = [.. Enum.GetNames<T>().Select(CaseConverter.PascalToTitle)];
            return () =>
            {
                for (int i = 0; i < menu.Length; ++i)
                {
                    Console.WriteLine($"{i + 1}. {menu[i]}");
                }
            };
        }

        internal UIRenderer DisplayMessage(string message)
        {
            return () => Console.WriteLine(message);
        }

        internal ConsoleKeyInfo GetKeyPress(string userPrompt)
        {
            //var originalPosition = Console.GetCursorPosition();
            //Console.SetCursorPosition(0, Console.WindowHeight);
            Console.Write(userPrompt);
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            //Console.SetCursorPosition(originalPosition.Left, originalPosition.Top);
            return key;
        }

        internal T GetMenuOption<T>() where T : struct, Enum
        {
            var choices = Enum.GetValues<T>();
            while (true)
            {
                ConsoleKeyInfo digit = GetKeyPress("Enter the number to avail the corresponding option:");
                if (!char.IsDigit(digit.KeyChar))
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a valid number!");
                    continue;
                }

                int index = digit.KeyChar - '1';
                if (index < 0 || index >= choices.Length)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a valid number!");
                    continue;
                }

                return choices[index];
            }
        }
    }
}
