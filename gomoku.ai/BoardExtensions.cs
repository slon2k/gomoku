using gomoku.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace gomoku.ai
{
    public static class BoardExtensions
    {
        public static Move? FindWinningMove(this Board board)
        {
            foreach (var move in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(move.x, move.y);
                if (newBoard.HasWinningCombitation())
                {
                    return new Move(move.x, move.y);
                }
            }
            return null;
        }

        private static Regex WiningCombination(char c) => new Regex($@"[^{c}]{c}{c}{c}{c}{c}[^{c}]");

        public static bool HasWinningCombitation(this Board board)
        {
            string allStrings = string.Join(string.Empty, board.AllStrings);
            var black = Color.Black.ToChar();
            var white = Color.White.ToChar();

            if (WiningCombination(black).IsMatch(allStrings) || WiningCombination(white).IsMatch(allStrings))
            {
                return true;
            }

            return false;
        }

        public static bool IsEmpty(this Board board) => board.FreeCells.Count == board.Cells.Count; 
    }
}
