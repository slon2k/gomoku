using System;
using System.Text.RegularExpressions;

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

        public void MakeMove(Move move) => MakeMove(move.x, move.y);

        public void MakeMove(int x, int y)
        {
            if (IsFinished)
            {
                Console.WriteLine("Game is over");
                return;
            }
            
            try
            {
                board.AddStone(x, y);
                CheckWinner();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } 

        private Regex WinningCombination(char c) => new Regex($@"[^{c}]{c}{c}{c}{c}{c}[^{c}]");
        
        private void CheckWinner()
        {
            string allStrings = string.Join(string.Empty, board.AllStrings);
            var black = Color.Black.ToChar();
            var white = Color.White.ToChar();

            if (WinningCombination(black).IsMatch(allStrings))
            {
                IsFinished = true;
                Winner = Color.Black;
                return;
            }

            if (WinningCombination(white).IsMatch(allStrings))
            {
                IsFinished = true;
                Winner = Color.White;
                return;
            }

            if (board.FreeCells.Count == 0)
            {
                IsFinished = true;
            }
        }
    }
    
    public struct Move
    {
        public int x;
        public int y;

        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
