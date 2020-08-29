using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace gomoku.core
{
    public class Board
    {
        public readonly int Size;
        private readonly Color[,] Position;
        public Color Turn { get; private set; } = Color.Black;

        public Board(int size)
        {
            Size = size;
            Position = new Color[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Position[i, j] = Color.Undefined;
                }
            }
        }

        public Board(Board board)
        {
            Size = board.Size;
            Position = new Color[Size, Size];
            Turn = board.Turn;
        }

        public void AddStone(int x, int y)
        {
            if (x < 0 || y < 0 || x > Size - 1 || y > Size - 1 )
            {
                throw new ArgumentOutOfRangeException("Position does not exist");
            }

            if (Position[x, y] != Color.Undefined)
            {
                throw new Exception("The cell is not empty");
            }

            Position[x, y] = Turn;
            Turn = Turn.Inverse();
        }

        public Color Cell(int x, int y)
        {
            if (x < 0 || y < 0 || x > Size - 1 || y > Size - 1)
            {
                throw new ArgumentOutOfRangeException("");
            }
            
            return Position[x, y];
        }

        public IList<Cell> Cells
        {
            get
            {
                var cells = new List<Cell>();
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        cells.Add(new Cell(i, j, Position[i, j]));
                    }
                }
                return cells;
            }           
        }

        public IList<Cell> FreeCells
        {
            get
            {
                return Cells.Where(c => c.color == Color.Undefined).ToList();
            }
        }

        public void Print()
        {
            var head = new StringBuilder();
            head.Append("   ");
            for (int j = 0; j < Size; j++)
            {
                head.Append($"{j / 10}{j % 10} ");
            }
            Console.WriteLine(head);

            for (int i = 0; i < Size; i++)
            {
                var str = new StringBuilder();
                str.Append($"{i / 10}{i % 10} ");
                for (int j = 0; j < Size; j++)
                {
                    str.Append(Cell(i, j).PrintColor());
                }
                Console.WriteLine(str);
            }
        }
    }

    public struct Cell
    {
        public int x;
        public int y;
        public Color color;

        public Cell(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }

    public enum Color : byte
    {
        Undefined,
        Black,
        White
    }
}
