using gomoku.core;
using System;

namespace gomoku.app
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int x, y, size = 15;
            var board = new Board(size);
            board.Print();

            while (true)
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
                if (x > size - 1 || x < 0 || y < 0 || y > size - 1) 
                {
                    Console.WriteLine("Input out of range");
                    continue;
                }

                board.AddStone(x, y);
                board.Print();
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
