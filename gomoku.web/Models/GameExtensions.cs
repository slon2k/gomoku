using System;
using System.Text;

namespace gomoku.web
{
    public static class GameExtensions
    {
        public static void AnnounceResult(this Game game)
        {
            if (!game.IsFinished)
            {
                Console.WriteLine("The game was not finished");
                return;
            }

            if (game.Winner == Status.Free)
            {
                Console.WriteLine("The game ended in a draw");
                return;
            }

            Console.WriteLine($"{game.Winner} won.");
        }

        public static void PrintBoard(this Game game)
        {
            var board = game.board;
            var size = game.Size;
            var head = new StringBuilder();
            head.Append("   ");
            for (int j = 0; j < size; j++)
            {
                head.Append($"{j / 10}{j % 10} ");
            }
            Console.WriteLine(head);

            for (int i = 0; i < size; i++)
            {
                var str = new StringBuilder();
                str.Append($"{i / 10}{i % 10} ");
                for (int j = 0; j < size; j++)
                {
                    str.Append(board.Cell(i, j).PrintColor());
                }
                Console.WriteLine(str);
            }
        }

        //For diagnostic purposes
        public static void PrintLines(this Game game)
        {
            var board = game.board;
            Console.WriteLine("Rows:");
            foreach (var item in board.Rows)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Columns:");
            foreach (var item in board.Columns)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Diagonals:");
            foreach (var item in board.Diagonals)
            {
                Console.WriteLine(item);
            }
        }
    }
}
