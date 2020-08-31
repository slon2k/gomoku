using gomoku.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gomoku.ai
{
    public static class Algorithm
    {
        public static int Minimax(Board board, int depth)
        {
            if (depth == 0)
            {
                return Evaluation.Evaluate(board);
            }

            var moves = board.CellsToMove;
            var values = new List<int>();
            
            foreach (var move in moves)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(move.x, move.y);
                var value = Minimax(newBoard, depth - 1);
                values.Add(value);
            }
            
            if (board.Turn == Color.Black)
            {
                return values.Max();
            } else
            {
                return values.Min();
            }
        }
    }
}
