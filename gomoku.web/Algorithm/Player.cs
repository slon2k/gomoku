using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace gomoku.web
{
    public class Player
    {
        private const int Depth = 1;

        private static readonly Random random = new Random();
        
        public Move GetMove(Board board)
        {
            if (board.FreeCells.Count == 0)
            {
                throw new ArgumentOutOfRangeException("No empty cell");
            }
            
            // Placing the first stone in the middle
            if (board.IsEmpty())
            {
                return new Move(board.Size / 2, board.Size / 2);
            }

            // Placing the second stone in a random cell close to the first stone 
            if (board.Cells.Count - board.FreeCells.Count == 1)
            {
                var firstCell = board.Cells.First(c => c.color != Status.Free);
                var cells = board.Neighbors(firstCell.x, firstCell.y);
                var cell = cells[random.Next(cells.Count)];
                return new Move(cell);
            }

            // Looking for winning move
            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell);
                if (newBoard.HasWinningCombination())
                {
                    return new Move(cell);
                }
            }

            var values = new ConcurrentDictionary<Move, int>();

            var watch = new Stopwatch();
            watch.Start();

            // Evaluating cells 
            //Parallel.ForEach(board.CellsToMove, cell =>
            //{
            //    var newBoard = new Board(board);
            //    newBoard.AddStone(cell);
            //    var value = Algorithm.AlphaBetaPruning(newBoard, Depth);
            //    values.TryAdd(new Move(cell), value);
            //});

            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell);
                var value = Algorithm.AlphaBetaPruning(newBoard, Depth);
                values.TryAdd(new Move(cell), value);
            }

            watch.Stop();
            Console.WriteLine($"Elapsed: {watch.Elapsed.TotalSeconds} seconds");          

            if (board.Turn == Status.Black)
            {
                return values.OrderByDescending(x => x.Value).First().Key;
            }
            
            return values.OrderBy(x => x.Value).First().Key;
        }
    }
}
