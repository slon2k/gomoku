using gomoku.core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace gomoku.ai
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
            
            if (board.IsEmpty())
            {
                return new Move(board.Size / 2, board.Size / 2);
            }

            if (board.Cells.Count - board.FreeCells.Count == 1)
            {
                var cells = board.CellsToMove;
                var cell = cells[random.Next(cells.Count)];
                return new Move(cell);
            }

            // Looking for winning move
            foreach (var cell in board.CellsToMove)
            {
                var newBoard = new Board(board);
                newBoard.AddStone(cell);
                if (newBoard.HasWinningCombitation())
                {
                    return new Move(cell);
                }
            }

            var values = new ConcurrentDictionary<Move, int>();

            var watch = new Stopwatch();
            watch.Start();

            Parallel.ForEach(board.CellsToMove, cell => { 
                var newBoard = new Board(board);
                newBoard.AddStone(cell);                
                var value = Algorithm.AlphaBetaPruning(newBoard, Depth);
                values.TryAdd(new Move(cell), value);
            });
                   
            //foreach (var cell in board.CellsToMove)
            //{
            //    var newBoard = new Board(board);
            //    newBoard.AddStone(cell);
            //    var value = Algorithm.AlphaBetaPruning(newBoard, Depth);
            //    values.TryAdd(new Move(cell), value);
            //}

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
