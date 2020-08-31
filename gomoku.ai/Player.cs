using gomoku.core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace gomoku.ai
{
    public class Player
    {
        public Move GetMove(Board board)
        {
            if (board.FreeCells.Count == 0)
            {
                throw new ArgumentOutOfRangeException("No empty cell");
            }
            var values = new Dictionary<Move, int>();
            foreach (var cell in board.CellToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell.x, cell.y);
                var value = Evaluation.Evaluate(newBoard);
                values.Add(new Move() { x = cell.x, y = cell.y }, value);
            }
            if (board.Turn == Color.Black)
            {
                return values.OrderByDescending(x => x.Value).First().Key;
            }
            return values.OrderBy(x => x.Value).First().Key;
        }
    }

    public struct Move
    {
        public int x;
        public int y;
    }
}
