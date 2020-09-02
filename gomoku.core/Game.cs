using System;
using System.Collections.Generic;
using System.Text;
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

        private Regex Win(char c) => new Regex($@"[^{c}]{c}{c}{c}{c}{c}[^{c}]");
        
        private void CheckWinner()
        {
            string allStrings = string.Join(string.Empty, board.AllStrings);
            var b = Color.Black.ToChar();
            var w = Color.White.ToChar();
            var blackWin = Win(b);
            var whiteWin = Win(w);

            if (blackWin.IsMatch(allStrings))
            {
                IsFinished = true;
                Winner = Color.Black;
                return;
            }

            if (whiteWin.IsMatch(allStrings))
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
