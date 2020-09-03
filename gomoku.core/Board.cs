using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Position[i, j] = board.Cell(i, j);
                }
            }
        }

        public void AddStone(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist 1");
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
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist 2");
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
            get => Cells.Where(c => c.color == Color.Undefined).ToList();
        }

        public IList<Cell> CellsToMove
        {
            get
            {
                int x, y, neighbors;
                var cells = new List<Cell>();
                foreach (var cell in FreeCells)
                {
                    x = cell.x;
                    y = cell.y;
                    neighbors = 0;
                    for (int i = -2; i <= 2; i++)
                    {
                        for (int j = -2; j <= 2; j++)
                        {
                            if (IsOutOfRange(x + i) || IsOutOfRange(y + j))
                            {
                                continue;
                            }
                            if (Cell(x + i, y + j) != Color.Undefined)
                            {
                                neighbors++;
                            }
                        }
                    }
                    if (neighbors > 0)
                    {
                        cells.Add(cell);
                    }
                }

                return cells;
            }
        }

        private string Row(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }
            
            var str = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                str.Append(Cell(index, i).ToChar());
            }
            return $"+{str}+";
        }

        public IList<string> Rows
        {
            get
            {
                var rows = new List<string>();
                for (int i = 0; i < Size; i++)
                {
                    rows.Add(Row(i));
                }
                return rows;
            }
        }

        private string Column(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }

            var str = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                str.Append(Cell(i, index).ToChar());
            }
            return $"+{str}+";
        }

        private string DiagonalDownUp(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist 3");
            }
            if (y !=0  && x != Size - 1)
            {
                throw new ArgumentOutOfRangeException("The cell is not at the edge");
            }
            
            var str = new StringBuilder();
            int i = x;
            int j = y;
            
            while (!IsOutOfRange(i) && !IsOutOfRange(j))
            {
                str.Append(Cell(i--, j++).ToChar());
            }
            
            return $"+{str}+";
        }

        public IList<string> Diagonals
        {
            get
            {
                var diagonals = new List<string>();
                for (int i = 0; i < Size; i++)
                {
                    diagonals.Add(DiagonalDownUp(i, 0));
                }
                
                for (int i = 1; i < Size; i++)
                {
                    diagonals.Add(DiagonalDownUp(Size - 1, i));
                }
                
                for (int i = 0; i < Size; i++)
                {
                    diagonals.Add(DiagonalUpDown(i, 0));
                }

                for (int i = 1; i < Size; i++)
                {
                    diagonals.Add(DiagonalUpDown(0, i));
                }

                return diagonals.Where(x => x.Length > 6 ).ToList();
            }
        }

        private string DiagonalUpDown(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist 4");
            }
            if (x != 0 && y != 0)
            {
                throw new ArgumentOutOfRangeException("The cell is not at the edge");
            }

            var str = new StringBuilder();
            int i = x;
            int j = y;

            while (!IsOutOfRange(i) && !IsOutOfRange(j))
            {
                str.Append(Cell(i++, j++).ToChar());
            }

            return $"+{str}+";
        }

        public IList<string> Columns
        {
            get
            {
                var columns = new List<string>();
                for (int i = 0; i < Size; i++)
                {
                    columns.Add(Column(i));
                }
                return columns;
            }
        }

        public IList<string> AllStrings
        {
            get
            {
                var result = new List<string>();
                result.AddRange(Rows);
                result.AddRange(Columns);
                result.AddRange(Diagonals);
                
                return result;
            }
        }

        private bool IsOutOfRange(int i) => i < 0 || i >= Size;

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
