namespace mastermind
{
    /// <summary>
    /// Defines a interface to display data and acquire player input.
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        /// Initializes the display.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Reads user input from the display.
        /// </summary>
        /// <returns></returns>
        string ReadInput();

        /// <summary>
        /// Prints information about the game.
        /// </summary>
        /// <returns></returns>
        int Header();

        /// <summary>
        /// Prints a line on the console for the user to enter a guess.
        /// </summary>
        /// <param name="row">Display row.</param>
        /// <param name="guessNumber">Current guess attempt by the player.</param>
        /// <returns></returns>
        int Attempt(int row, int guessNumber);

        /// <summary>
        /// This method reports the result of the user's attempt.
        /// </summary>
        /// <param name="row">Display row.</param>
        /// <param name="guessNumber">Current guess attempt by the player.</param>
        /// <param name="number">The number that the player entered.</param>
        /// <param name="message">A message to display next to number.</param>
        /// <returns></returns>
        int Result(int row, int guessNumber, string number, string message);

        /// <summary>
        /// Displays a message that the player has won the game.
        /// </summary>
        /// <param name="value"></param>
        void YouWin(string value);

        /// <summary>
        /// Displays a message that the player failed to guess the game number.
        /// <param name="value"></param>
        /// </summary>
        void YouLose(string value);
    }
}
