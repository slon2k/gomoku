using gomoku.ai;
using gomoku.core;
using System;
using System.Collections.Generic;

namespace gomoku.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var player = new Player();
            Move move = new Move(-1, -1);
            var players = new Dictionary<Color, string>();
            players[Color.Black] = "human";
            players[Color.White] = "computer";

            game.PrintBoard();

            while (!game.IsFinished)
            {
                if (players[game.Turn] == "human")
                {
                    try
                    {
                        move = InputMove();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                    
                } else
                {
                    try
                    {
                        move = player.GetMove(game.board);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }
               
                try
                {
                    game.MakeMove(move);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }             
                game.PrintBoard();
            }

            game.AnnounceResult();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static Move InputMove()
        {
            var input = Console.ReadLine().Split(" ");
            var move = new Move();
            
            if (input.Length < 2)
            {
                throw new ArgumentNullException("Invalid input");
            }
            
            if (!int.TryParse(input[0], out move.x) || !int.TryParse(input[1], out move.y))
            {
                throw new ArgumentNullException("Invalid input");
                
            };
            
            return move;
        }
    }
}
