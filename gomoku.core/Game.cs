using System;
using System.Collections.Generic;
using System.Text;

namespace gomoku.core
{
    public class Game
    {
        public readonly int Size;
        public readonly Board board;
        public bool IsFinished { get; private set; }
        public Color Winner { get; private set; } = Color.Undefined;
        public Color Turn { get => board.Turn; }
        
        public Game(int size = 15)
        {
            Size = size;
            board = new Board(size);
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
    }
}
