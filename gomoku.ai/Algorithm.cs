using gomoku.core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gomoku.ai
{
    public class Algorithm
    {
        private const int Infinity = 10000000;
        
        //Minimax was used in early versions
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

        // Improved minimax algorithm
        public static int AlphaBetaPruning(Board board, int depth, int a = -Infinity, int b = Infinity)
        {
            int alpha = a;
            int beta = b;
            
            if (depth == 0)
            {
                return Evaluation.Evaluate(board);
            }

            var moves = board.CellsToMove;
            var value = Infinity;

            if (board.Turn == Color.Black)
            {
                value *= -1;
                foreach (var move in moves)
                {
                    var newBoard = new Board(board);
                    newBoard.AddStone(move.x, move.y);
                    var evaluation = AlphaBetaPruning(newBoard, depth - 1, alpha, beta);
                    value = Math.Max(evaluation, value);
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }

            } else
            {
                foreach (var move in moves)
                {
                    var newBoard = new Board(board);
                    newBoard.AddStone(move.x, move.y);
                    var evaluation = AlphaBetaPruning(newBoard, depth - 1, alpha, beta);
                    value = Math.Min(evaluation, value);
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }

            }
            
            return value;
        }
    }
}
