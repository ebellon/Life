using System;
using System.Text;

namespace Life
{
    class LifeManager
    {
        private Cell[,] cellsBoard;
        private Cell[,] nextCellsBoard;
        private readonly int maxX;
        private readonly int maxY;

        private Random randomGenerator = new Random();

        public LifeManager(int boardLines, int boardColumns)
        {
            this.maxX = boardColumns;
            this.maxY = boardLines;
            this.cellsBoard = CellBoardInitializer();
            this.nextCellsBoard = new Cell[this.maxX, this.maxY];
        }

        public void PrintCellBoard()
        {
            for (int y = 0; y < this.maxY; y++)
            {
                var lineBuffer = new StringBuilder();
                for (int x = 0; x < this.maxX; x++)
                {
                    if (cellsBoard[x, y].IsAlive)
                    {
                        lineBuffer.Append("X");
                        continue;
                    }

                    lineBuffer.Append(" ");
                }
                Console.WriteLine(lineBuffer);
            }
        }

        public void ApplyLifeRules()
        {
            /*
                Any live cell with two or three live neighbours survives.
                Any dead cell with three live neighbours becomes a live cell.
                All other live cells die in the next generation. Similarly, all other dead cells stay dead.
             */

            for (int y = 0; y < this.maxY; y++)
                for (int x = 0; x < this.maxX; x++)
                {
                    var neighbours = CalculateAliveNeighbours(cellsBoard[x, y]);
                    var isAlive = cellsBoard[x, y].IsAlive;

                    // Keep living
                    if (isAlive && (neighbours == 2 || neighbours == 3))
                    {
                        nextCellsBoard[x, y] = new Cell(x, y, true);
                        continue;
                    }

                    // Rise from the Dead!
                    if (!isAlive && neighbours == 3)
                    {
                        nextCellsBoard[x, y] = new Cell(x, y, true);
                        continue;
                    }

                    // Die!
                    nextCellsBoard[x, y] = new Cell(x, y, false);

                }

            // Deep clone 
            this.DeepClone(nextCellsBoard, cellsBoard);
            this.nextCellsBoard = new Cell[this.maxX, this.maxY];
        }


        private int CalculateAliveNeighbours(Cell cell)
        {
            return this.CalculateAliveTopNeighbours(cell) + this.CalculateAliveBottomNeighbours(cell) + this.CalculateAliveSidesNeighbours(cell);
        }

        private int CalculateAliveSidesNeighbours(Cell cell)
        {
            var result = 0;

            if (cell.X + 1 < maxX)
            {
                if (cellsBoard[cell.X + 1, cell.Y].IsAlive)
                {
                    result++;
                }
            }

            if (cell.X - 1 >= 0)
            {
                if (cellsBoard[cell.X - 1, cell.Y].IsAlive)
                {
                    result++;
                }
            }

            return result;
        }

        private int CalculateAliveTopNeighbours(Cell cell)
        {
            var result = 0;

            if (cell.Y - 1 >= 0)
            {
                if (cell.X + 1 < maxX)
                {
                    if (cellsBoard[cell.X + 1, cell.Y - 1].IsAlive)
                    {
                        result++;
                    }
                }

                if (cell.X - 1 >= 0)
                {
                    if (cellsBoard[cell.X - 1, cell.Y - 1].IsAlive)
                    {
                        result++;
                    }
                }

                if (cell.X >= 0)
                {
                    if (cellsBoard[cell.X, cell.Y - 1].IsAlive)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private int CalculateAliveBottomNeighbours(Cell cell)
        {
            var result = 0;

            if (cell.Y + 1 < maxY)
            {
                if (cell.X + 1 < maxX)
                {
                    if (cellsBoard[cell.X + 1, cell.Y + 1].IsAlive)
                    {
                        result++;
                    }
                }

                if (cell.X - 1 >= 0)
                {
                    if (cellsBoard[cell.X - 1, cell.Y + 1].IsAlive)
                    {
                        result++;
                    }
                }

                if (cell.X >= 0)
                {
                    if (cellsBoard[cell.X, cell.Y + 1].IsAlive)
                    {
                        result++;
                    }
                }
            }

            return result;
        }


        private Cell[,] CellBoardInitializer()
        {
            var result = new Cell[this.maxX, this.maxY];

            for (int y = 0; y < this.maxY; y++)
                for (int x = 0; x < this.maxX; x++)
                    result[x, y] = new Cell(x, y, randomGenerator.NextDouble() < 0.5);

            return result;
        }


        private void DeepClone(Cell[,] origin, Cell[,] destination)
        {
            for (int y = 0; y < this.maxY; y++)
                for (int x = 0; x < this.maxX; x++)
                {
                    var originalCell = origin[x, y];
                    destination[x, y] = new Cell(originalCell.X, originalCell.Y, originalCell.IsAlive);
                }
        }

    }
}
