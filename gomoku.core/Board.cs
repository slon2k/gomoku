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
        private readonly Status[,] Position;
        public Status Turn { get; private set; } = Status.Black;

        public Board(int size)
        {
            Size = size;
            Position = new Status[Size, Size];
            
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Position[i, j] = Status.Free;
                }
            }
        }

        // For making a copy of the board
        public Board(Board board)
        {
            Size = board.Size;
            Position = new Status[Size, Size];
            Turn = board.Turn;
            
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Position[i, j] = board.Cell(i, j);
                }
            }
        }

        public void AddStone(Cell cell) => AddStone(cell.x, cell.y);

        public void AddStone(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist");
            }

            if (Position[x, y] != Status.Free)
            {
                throw new Exception("The cell is not empty");
            }

            Position[x, y] = Turn;
            Turn = Turn.Inverse();
        }

        public Status Cell(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist");
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
            get => Cells.Where(c => c.color == Status.Free).ToList();
        }

        // Assuming that the best cells are in a short distance from stones on the board.
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
                            if (Cell(x + i, y + j) != Status.Free)
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

        // Row as a string like "-----XX00-----"
        private string Row(int index)
        {
            if (IsOutOfRange(index))
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }
            
            var str = new StringBuilder();
            
            for (int i = 0; i < Size; i++)
            {
                str.Append(Cell(index, i).ToChar());
            }
            
            return StringWithBorder(str.ToString());
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

        // Column as a string
        private string Column(int index)
        {
            if (IsOutOfRange(index))
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }

            var str = new StringBuilder();
            
            for (int i = 0; i < Size; i++)
            {
                str.Append(Cell(i, index).ToChar());
            }
            
            return StringWithBorder(str.ToString());
        }

        // Diagonal from down-left to up-rigth as a string 
        private string DiagonalDownUp(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist");
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

            return StringWithBorder(str.ToString());
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

        // Diagonal from up-left to down-right as a string 
        private string DiagonalUpDown(int x, int y)
        {
            if (IsOutOfRange(x) || IsOutOfRange(y))
            {
                throw new ArgumentOutOfRangeException("Position does not exist");
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

            return StringWithBorder(str.ToString());
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

        public IList<Cell> Neighbors(int x, int y)
        {
            var neighbors = new List<Cell>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (IsOutOfRange(x + i) || IsOutOfRange(y + j))
                    {
                        continue;
                    }
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    neighbors.Add(new Cell(x + i, y + j, Cell(x + i, y + j)));

                }
            }
            return neighbors;
        }

        // Adding borders to a string representing row, column, or diagonal 
        private string StringWithBorder(string str) => $"{Status.Border.ToChar()}{str}{Status.Border.ToChar()}";

        private bool IsOutOfRange(int i) => i < 0 || i >= Size;

    }

    public struct Cell
    {
        public int x;
        public int y;
        public Status color;

        public Cell(int x, int y, Status color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }

    public enum Status : byte
    {
        Free,
        Black,
        White,
        Border
    }
}
