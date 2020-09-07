using gomoku.ai;
using gomoku.core;
using System;
using System.Collections.Generic;

namespace gomoku.app
{
    class Program
    {
        const string ComputerPlayer = "Computer";
        const string HumanPlayer = "Human";
        
        static void Main(string[] args)
        {
            var game = new Game();
            var player = new Player();
            Move move = new Move(-1, -1);
            var players = new Dictionary<Status, string>
            {
                [Status.Black] = ComputerPlayer,
                //[Status.Black] = HumanPlayer,
                [Status.White] = ComputerPlayer
            };

            game.PrintBoard();

            while (!game.IsFinished)
            {
                if (players[game.Turn] == HumanPlayer)
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
                    }
                }
               
                try
                {
                    game.MakeMove(move);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }             
                game.PrintBoard();
            }

            game.AnnounceResult();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        // Input new move from console
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
