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
        
        Game()
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
                //checkWinner();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } 
        
        private void checkWinner()
        {
            throw new NotImplementedException();
        }

        public void PrintBoard()
        {
            board.Print();
        }

    }
}
