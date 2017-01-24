using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    public partial class SapperForm : Form
    {        
        private GameLevelEnum gameLevel = GameLevelEnum.Normal;
        public static System.Drawing.Graphics g;
        public static DateTime myTimer = new DateTime(0, 0);
        static Cell[,] Cells;
        static GameStatus gameStatus = GameStatus.StartGame;
        Field GameField = new Field(Cells, 0,0, gameStatus);
        
        public SapperForm()
        {
            InitializeComponent();
            DrawField(gameLevel);
        }
        
        private void DrawField(GameLevelEnum gameLevel)
        {
            GameField.GemeLevelOptions = Game.SetLevel(gameLevel);
            int horNum = GameField.GemeLevelOptions.HorNum;
            int vertNum = GameField.GemeLevelOptions.VertNum;
            GameField.Cells= ServiceCell.CreateCells(vertNum, horNum);

            ClientSize = new Size(Cell.cellWidh * horNum + 20, Cell.cellHeight * vertNum + menuStrip1.Height + label1.Height + 20);
            panel1.Size = new Size(Cell.cellWidh * horNum + 20, Cell.cellHeight * vertNum + menuStrip1.Height + 20);

            Game.NewGame(GameField);

            g = panel1.CreateGraphics();
        }        

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Game.PlayGame(e, GameField, timer1);
            if (GameField.gameStatus==GameStatus.EndOfTheGame)
            {
                panel1.Invalidate();
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawField(gameLevel);
            ServiceField.ShowField(GameField);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ServiceField.ShowField(GameField);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameLevel = GameLevelEnum.Normal;
            DrawField(gameLevel);
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameLevel = GameLevelEnum.Hard;
            DrawField(gameLevel);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameLevel = GameLevelEnum.Easy;
            DrawField(gameLevel);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            myTimer = myTimer.AddSeconds(1);
            label1.Text = myTimer.ToString("mm:ss");
        }
    }
}