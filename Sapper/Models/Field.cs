using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    class Field
    {       
        public Cell[,] Cells { get; set; }
        public GameLevel GemeLevelOptions { get; set; }
        public int MineFound { get; set; }
        public int FlagPlaced { get; set; }
        public GameStatus gameStatus { get; set; }
        
        public Field(Cell[,] cells, int MineFound, int FlagPlaced, GameStatus gameStatus)
        {
            Cells = cells;
            this.MineFound = MineFound;
            this.FlagPlaced = FlagPlaced;
            this.gameStatus = gameStatus;
        }
    }
}
