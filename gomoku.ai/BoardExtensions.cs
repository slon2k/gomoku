using gomoku.core;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace gomoku.ai
{
    public static class BoardExtensions
    {
        private static Regex WiningCombination(char c) => new Regex($@"[^{c}]{c}{c}{c}{c}{c}[^{c}]");

        public static bool HasWinningCombitation(this Board board)
        {
            string allStrings = string.Join(string.Empty, board.AllStrings);
            var black = Status.Black.ToChar();
            var white = Status.White.ToChar();

            if (WiningCombination(black).IsMatch(allStrings) || WiningCombination(white).IsMatch(allStrings))
            {
                return true;
            }

            return false;
        }

        public static bool IsEmpty(this Board board) => board.FreeCells.Count == board.Cells.Count; 
    }
}
