using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    class Game
    {
        public static void NewGame(Field GameField)
        {
            int horNum = GameField.GemeLevelOptions.HorNum;
            int vertNum = GameField.GemeLevelOptions.VertNum;

            for (int row = 1; row <= vertNum; row++)
            {
                for (int col = 1; col <= horNum; col++)
                {
                    GameField.Cells[row, col].Value = (int)NumberForCell.Empty;
                }
            }

            ServiceCell.PlacingMines(vertNum, horNum, GameField);
            ServiceCell.PlacingNumbers(vertNum, horNum, GameField);

            GameField.gameStatus = GameStatus.StartGame;
            GameField.MineFound = 0;
            GameField.FlagPlaced = 0;
        }

        public static void PlayGame(MouseEventArgs e, Field GameField, Timer timer1)
        {
            if (GameField.gameStatus == GameStatus.EndOfTheGame)
            {
                return;
            }

            if (GameField.gameStatus == GameStatus.StartGame)
            {
                GameField.gameStatus = GameStatus.Game;
                timer1.Enabled = true;
            }

            int row = e.Y / Cell.cellHeight + 1, col = e.X / Cell.cellWidh + 1;

            int x = (col - 1) * Cell.cellWidh + 1, y = (row - 1) * Cell.cellHeight + 1;

            if (e.Button == MouseButtons.Left)
            {
                if (ServiceCell.IsMineInCell(row, col, GameField))
                {
                    GameField.Cells[row, col].IsOpen = true;
                    GameField.gameStatus = GameStatus.EndOfTheGame;

                    MessageBox.Show("Oops, you lost!");
                    timer1.Enabled = false;
                    SapperForm.myTimer = new DateTime(0, 0);
                }
                else
                    if (!ServiceCell.IsMineInCell(row, col, GameField))
                    ServiceCell.OpenCells(row, col, GameField);
            }

            if (e.Button == MouseButtons.Right)
            {
                if (!GameField.Cells[row, col].HasFlag)
                {
                    GameField.FlagPlaced++;

                    if (ServiceCell.IsMineInCell(row, col, GameField))
                    {
                        GameField.MineFound++;
                    }

                    GameField.Cells[row, col].HasFlag = true;

                    if ((GameField.MineFound == GameField.GemeLevelOptions.MineNumber) && (GameField.FlagPlaced == GameField.GemeLevelOptions.MineNumber))
                    {
                        MessageBox.Show("You win!");
                        timer1.Enabled = false;
                        SapperForm.myTimer = new DateTime(0, 0);
                        GameField.gameStatus = GameStatus.EndOfTheGame;

                    }
                    else
                        ServiceCell.DrawCell(row, col, GameField);
                }
                else

                    if (GameField.Cells[row, col].HasFlag)
                {
                    GameField.FlagPlaced--;
                    GameField.Cells[row, col].HasFlag = false;

                    ServiceCell.DrawCell(row, col, GameField);
                }
            }
        }

        public static GameLevel SetLevel(GameLevelEnum gameLevel)
        {
            GameLevel LevelOptions = new GameLevel(GameLevelEnum.Easy, 9, 9, 10);
            switch (gameLevel)
            {
                case GameLevelEnum.Easy:
                    {
                        LevelOptions = new GameLevel(GameLevelEnum.Easy, 9, 9, 10);
                        break;
                    }
                case GameLevelEnum.Normal:
                    {
                        LevelOptions = new GameLevel(GameLevelEnum.Normal, 16, 16, 40);
                        break;
                    }
                case GameLevelEnum.Hard:
                    {
                        LevelOptions = new GameLevel(GameLevelEnum.Hard, 16, 30, 99);
                        break;
                    }
            }
            return LevelOptions;
        }
    }
}