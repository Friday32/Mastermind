using System;

namespace mastermind
{
    /// <summary>
    /// This class is to presents game information in console window.
    /// </summary>
    public class Display: IDisplay
    {
        private int top;
        private int left;

        public void Initialize()
        {
            Console.Clear();
            top = Console.CursorTop;
            left = Console.CursorLeft;
        }

        public string ReadInput()
        {
            string value = "";
            var keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                value += keyInfo.KeyChar;
                if (keyInfo.Key == ConsoleKey.Backspace && value.Length>=2)
                {
                    value = value.Remove(value.Length - 2);
                    Console.Write(" \b");
                }
                keyInfo = Console.ReadKey();
            }

            return value;
        }

        /// <summary>
        /// Writes data a specified cursor position
        /// </summary>
        /// <param name="message">Data to display</param>
        /// <param name="x">Cursor column position</param>
        /// <param name="y">Cursor row position</param>
        protected void Write(string message, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(left + x, top + y);
                Console.Write(message);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Sets the console cursor position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void SetCursorPos(int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
            }
            catch (Exception)
            {
            }
        }

        public int Header()
        {
            int row = 0;
                     //         10        20        30        40        50        60        70
                     //01234567890123456789012345678901234567890123456789012345678901234567890123456789
            Write("---------------------------------------------------------------------", 0, row++);
            Write("! Mastermind - The Simple Numeric Version                           !", 0, row++);
            Write("!                                                                   !", 0, row++);
            Write("! Enter a 4 digit number with digit values between 1 and 6.         !", 0, row++);
            Write("---------------------------------------------------------------------", 0, row++);
            return row;
        }

        public int Attempt(int row, int guessNumber)
        {     
            SetCursorPos(0, row);
            Console.Write(new string(' ', Console.WindowWidth));

                      //         10        20        30        40        50        60        70
                     //01234567890123456789012345678901234567890123456789012345678901234567890123456789
            Write("! Attempt    | Enter Number :                                       !", 0, row++);
            Write($"{guessNumber,2:##}", 10, row - 1);

            SetCursorPos(30,  row -1);
            return row;
        }

        public int Result(int row, int guessNumber, string number, string message)
        {
            SetCursorPos(0, row);
            Console.Write(new string(' ', Console.WindowWidth));

                     //         10        20        30        40        50        60        70
                     //01234567890123456789012345678901234567890123456789012345678901234567890123456789
            Write("! Attempt    | Enter Number :                                       !", 0, row);
            Write($"{guessNumber,2:##}", 10, row);
            Write(number, 30, row);
            Write(message, 38, row);

            SetCursorPos(30, row);
            return row;
        }

        public void YouWin(string value)
        {
            Console.WriteLine();
            Console.WriteLine($"You found the correct number {value}.");
        }

        public void YouLose(string value)
        {
            Console.WriteLine();
            Console.WriteLine($"Sorry you lose. The number was {value}. Try again.");
        }
    }
}
