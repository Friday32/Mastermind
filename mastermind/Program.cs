namespace mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(new Display(), 1, 6, 4, 10).Play(); ;
        }
    }
}
