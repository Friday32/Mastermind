using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace mastermind
{
    /// <summary>
    /// This class represents the Mastermind game.
    ///
    /// An instance of the game will include a randomly generated
    /// number that the player may guess against.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// A object to present information and to acquire player input.
        /// </summary>
        private IDisplay display;

        /// <summary>
        /// The game number.
        /// </summary>
        private IList<int> number;

        /// <summary>
        /// Lowest numeric digit than can appear in the game number.
        /// </summary>
        private int minDigit;

        /// <summary>
        /// Highest numeric digit  than can appear in the game number.
        /// </summary>
        private int maxDigit;

        /// <summary>
        /// Maximum number of attempts the player can guess before losing the game.
        /// </summary>
        private int maxPlayerGuesses;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="display">Object to present information to the user.</param>
        /// <param name="minDigit">Lowest numeric digit than can appear in the game number.</param>
        /// <param name="maxDigit">Highest numeric digit than can appear in the game number. </param>
        /// <param name="numberLength">The length of the game number.</param>
        /// <param name="maxPlayerGuesses">Maximum number of attempts the player can guess before losing the game.</param>
        public Game(IDisplay display, int minDigit, int maxDigit, int numberLength, int maxPlayerGuesses)
        {
            this.display = display;
            this.minDigit = minDigit;
            this.maxDigit = maxDigit;
            this.maxPlayerGuesses = maxPlayerGuesses;
            var temp = new List<int>(Enumerable.Range(minDigit, maxDigit));
            number = temp.OrderRandom().Take(numberLength).ToList();
        }

        /// <summary>
        /// Alternate constructor for unit tests.
        /// </summary>
        /// <param name="number"></param>
        public Game(IList<int> number)
        {
            this.number = number;
        }

        /// <summary>
        /// This method returns the game number as a string.
        /// </summary>
        public string Value
        {
            get { return number.Select(x => x.ToString()).Aggregate((a, b) => a + b); }
        }
 
        /// <summary>
        /// This method validates the number entered by the player.
        /// It checks the number length and for correct user input.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public bool ValidateInput(string value, out string errMessage)
        {
            try
            {
                errMessage = "";
                var length = number.Count;
                var pattern = $"^[{minDigit}-{maxDigit}]{{{length},{length}}}$$";
                if (!Regex.Match(value, pattern).Success)
                {
                    errMessage = "Invalid Number";
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                errMessage = "Internal validation error";
                return false;
            }
        }

        /// <summary>
        /// Test a player's guess against the game number
        /// and return a hint regarding the comparison.
        /// </summary>
        /// <param name="guess">Player input.</param>
        /// <param name="hint">This pattern is described in programming exercise.</param>
        /// <returns></returns>
        public bool Try(string guess, out string hint)
        {
            hint = "";
            var arr = guess.Select(x => (int) (x -'0')).ToArray();
            for(int i = 0; i < number.Count; i++)
            {
                if (number[i] == arr[i])
                {
                    hint += "+";
                }
                else if (number.Any(x => x == arr[i]))
                {
                    hint += "-";
                }
                else
                {
                    hint += " ";
                }
            }

            return hint.All(x => x == '+');
        }

        /// <summary>
        /// Executes the game.
        /// </summary>
        public void Play()
        {
            display.Initialize();
            int row = display.Header();

            for (int i = 0; i < maxPlayerGuesses; ++i)
            {
                row = display.Attempt(row, i + 1);
                var input = display.ReadInput();

                while (!ValidateInput(input, out string errorMessage))
                {
                    display.Result(row - 1, i + 1, "", errorMessage);
                    input = display.ReadInput();
                }

                if (Try(input, out string hint))
                {
                    display.YouWin(Value);
                    return;
                }

                row = display.Result(row - 1, i + 1, input, hint) + 1;
            }

            display.YouLose(Value);
        }
    }
}
