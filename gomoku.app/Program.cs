using gomoku.core;
using System;

namespace gomoku.app
{
    class Program
    {
        static void Main(string[] args)
        {
            int x, y;
            var game = new Game();
            game.PrintBoard();

            while (!game.IsFinished)
            {
                var input = Console.ReadLine().Split(" ");
                if (input.Length < 2)
                {
                    break;
                }
                if (!int.TryParse(input[0], out x) || !int.TryParse(input[1], out y)) 
                {
                    Console.WriteLine("Invalid input");
                    continue;
                };
                try
                {
                    game.Move(x, y);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }             
                game.PrintBoard();
            }

            game.AnnounceResult();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
