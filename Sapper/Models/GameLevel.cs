using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    class GameLevel
    {
        public GameLevelEnum level;
        public int VertNum { get; set; }
        public int HorNum { get; set; }
        public int MineNumber { get; set; }

        public GameLevel(GameLevelEnum level, int vertNum, int horNum, int mineNumber)
        {
            this.level = level;
            VertNum = vertNum;
            HorNum = horNum;
            MineNumber = mineNumber;
        }
    }
}
