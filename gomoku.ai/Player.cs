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
            if (board.FreeCells.Count >= board.Cells.Count - 1)
            {
                if (board.Cell(board.Size / 2, board.Size / 2) == Color.Undefined)
                {
                    return new Move(board.Size / 2, board.Size / 2);
                } else
                {
                    return new Move(board.Size / 2, board.Size / 2 + 1);
                }
            }

            var values = new Dictionary<Move, int>();
            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell.x, cell.y);
                var value = Evaluation.Evaluate(newBoard);
                values.Add(new Move(cell.x, cell.y), value);
            }
            if (board.Turn == Color.Black)
            {
                return values.OrderByDescending(x => x.Value).First().Key;
            }
            return values.OrderBy(x => x.Value).First().Key;
        }
    }
}
