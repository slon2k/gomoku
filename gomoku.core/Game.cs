using System;
using System.Collections.Generic;
using System.Text;

namespace gomoku.core
{
    public class Game
    {
        public const int Size = 15;
        private readonly Board board;
        public bool IsFinished { get; private set; }
        public Color Winner { get; private set; } = Color.Undefined;
        public Color Turn { get => board.Turn; }
        public int MyProperty { get; set; }
        
        public Game()
        {
            board = new Board(Size);
        }

        public void Move(int x, int y)
        {
            if (IsFinished)
            {
                Console.WriteLine("Game is over");
                return;
            }
            
            try
            {
                board.AddStone(x, y);
                checkWinner();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } 
        
        private void checkWinner()
        {
            foreach (var item in board.AllStrings)
            {
                if (item.Contains("00000"))
                {
                    IsFinished = true;
                    Winner = Color.White;
                    return;
                }
                if (item.Contains("XXXXX"))
                {
                    IsFinished = true;
                    Winner = Color.Black;
                    return;
                }
            };

            if (board.FreeCells.Count == 0)
            {
                IsFinished = true;
            }
        }

        public void AnnounceResult()
        {
            if (!IsFinished)
            {
                Console.WriteLine("The game was not finished");
                return;
            }

            if (Winner == Color.Undefined) 
            {
                Console.WriteLine("The game ended in a draw");
                return;
            }

            Console.WriteLine($"{Winner} won.");
        }

        public void PrintBoard()
        {
            board.Print();
        }

        public void PrintLines()
        {
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
