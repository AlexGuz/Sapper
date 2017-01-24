using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    class ServiceCell
    {
        //initialize an array and create a non-displayed borders
        public static Cell[,] CreateCells(int vertNum, int horNum)
        {
            Cell[,] Cells = new Cell[vertNum + 2, horNum + 2];
            for (int row = 0; row <= vertNum + 1; row++)
            {
                for (int col = 0; col <= horNum + 1; col++)
                {
                    //non-displayed borders
                    if (col == 0 || row == 0|| col == horNum + 1 || row == vertNum + 1)
                    {
                        Cells[row, col] = new Cell(false, false, false, -1);
                    }
                    else
                    {
                        Cells[row, col] = new Cell(false, false, false, 0);
                    }                    
                }
            }
            return Cells;
        }

        //drawing cells, depending on the type of
        public static void DrawCell(int row, int col, Field GameField)
        {
            Image FlagImage = ResourceForCell.flag;
            Image MineImage = ResourceForCell.mine;

            int x = (col - 1) * Cell.cellWidh + 1;
            int y = (row - 1) * Cell.cellHeight + 1;

            Point ulCorner = new Point(x, y);

            if (!GameField.Cells[row, col].IsOpen)
            {
                SapperForm.g.FillRectangle(Brushes.Gray, x - 1, y - 1, Cell.cellWidh, Cell.cellHeight);
            }

            if (GameField.Cells[row, col].IsOpen)
            {
                var brushColor = GameField.Cells[row, col].HasMine ? Brushes.Red : Brushes.White;
                SapperForm.g.FillRectangle(brushColor, x - 1, y - 1, Cell.cellWidh, Cell.cellHeight);

                if ((GameField.Cells[row, col].Value != 0))
                {
                    SapperForm.g.DrawString(GameField.Cells[row, col].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Blue, x + 3, y + 2);
                }
            }

            if (GameField.Cells[row, col].HasFlag)
            {                
                SapperForm.g.DrawImage(FlagImage, ulCorner);
            }

            SapperForm.g.DrawRectangle(Pens.Black, x - 1, y - 1, Cell.cellWidh, Cell.cellHeight);

            if (GameField.gameStatus == GameStatus.EndOfTheGame && GameField.Cells[row, col].HasMine)
            {
                SapperForm.g.DrawImage(MineImage, ulCorner);
            }
        }

        public static void OpenCells(int row, int col, Field GameField)
        {
            int x = (col - 1) * Cell.cellWidh + 1;
            int y = (row - 1) * Cell.cellHeight + 1;

            //check the value of cell
            if (GameField.Cells[row, col].Value == 0)
            {
                //if in cell "0", we to watch all the neighboring and also open
                while (!GameField.Cells[row, col].IsOpen)
                {
                    GameField.Cells[row, col].IsOpen = true;
                    DrawCell(row, col, GameField);

                    var pointsForCheck = new List<Point>
                        {
                            new Point(row - 1, col - 1),
                            new Point(row - 1, col),
                            new Point(row - 1, col + 1),
                            new Point(row, col - 1),
                            new Point(row, col + 1),
                            new Point(row + 1, col - 1),
                            new Point(row + 1, col),
                            new Point(row + 1, col + 1)
                        };
                    foreach (var point in pointsForCheck)
                    {
                        OpenCells(point.X, point.Y, GameField);
                    }
                }
            }

            //if in cell the number redraw its
            else if ((!GameField.Cells[row, col].IsOpen) && (GameField.Cells[row, col].Value != (int)NumberForCell.DontShow))
            {
                GameField.Cells[row, col].IsOpen = true;
                DrawCell(row, col, GameField);
            }
        }        

        public static int PlacingMines(int vertNum, int horNum, Field GameField)
        {
            Random rnd = new Random();

            int deployedMine = 0;
            do
            {
                int row = rnd.Next(vertNum) + 1;
                int col = rnd.Next(horNum) + 1;

                if (!GameField.Cells[row, col].HasMine)
                {
                    GameField.Cells[row, col].HasMine = true;
                    deployedMine++;
                }
            }
            while (deployedMine != GameField.GemeLevelOptions.MineNumber);
            return deployedMine;
        }

        public static void PlacingNumbers(int vertNum, int horNum, Field GameField)
        {
            for (int row = 1; row <= vertNum; row++)
            {
                for (int col = 1; col <= horNum; col++)
                {
                    GameField.Cells[row, col].IsOpen = false;
                    GameField.Cells[row, col].HasFlag = false;

                    if (!IsMineInCell(row, col, GameField))
                    {
                        var pointsForCheck = new List<Point>
                        {
                            new Point(row - 1, col - 1),
                            new Point(row - 1, col),
                            new Point(row - 1, col + 1),
                            new Point(row, col - 1),
                            new Point(row, col + 1),
                            new Point(row + 1, col - 1),
                            new Point(row + 1, col),
                            new Point(row + 1, col + 1)
                        };
                        GameField.Cells[row, col].Value = pointsForCheck.Count(point => IsMineInCell(point.X, point.Y, GameField));
                    }
                }
            }
        }

        public static bool IsMineInCell(int row, int col, Field GameField)
        {
            return GameField.Cells[row, col].HasMine == true;
        }

        private static bool IsZeroInCell(int row, int col, Field GameField)
        {
            return GameField.Cells[row, col].Value == 0;
        }
    }
}