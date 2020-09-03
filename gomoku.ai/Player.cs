using gomoku.core;
using System;
using System.Collections.Generic;
using System.Linq;


namespace gomoku.ai
{
    public class Player
    {
        private const int Depth = 1;
        
        public Move GetMove(Board board)
        {
            if (board.FreeCells.Count == 0)
            {
                throw new ArgumentOutOfRangeException("No empty cell");
            }
            
            if (board.IsEmpty())
            {
                return new Move(board.Size / 2, board.Size / 2);
            }

            //Looking for winning move
            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell.x, cell.y);
                if (board.HasWinningCombitation())
                {
                    return new Move(cell.x, cell.y);
                }
            }

            var values = new Dictionary<Move, int>();
            
            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell.x, cell.y);
                var value = Algorithm.AlphaBetaPruning(newBoard, Depth);
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
