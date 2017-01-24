using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sapper
{
    class ServiceField
    {
        public static void ShowField(Field GameField)
        {
            int horNum = GameField.GemeLevelOptions.HorNum;
            int vertNum = GameField.GemeLevelOptions.VertNum;

            for (int row = 1; row <= vertNum; row++)
            {
                for (int col = 1; col <= horNum; col++)
                {
                    ServiceCell.DrawCell(row, col, GameField);
                }
            }
        }        
    }
}