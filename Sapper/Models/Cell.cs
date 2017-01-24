using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    class Cell
    {
        public bool IsOpen { get; set; }
        public bool HasMine { get; set; }
        public bool HasFlag { get; set; }
        public const int cellWidh = 20;
        public const int cellHeight = 20;
        public int Value { get; set; }

        public Cell(bool isOpen, bool hasMine , bool hasFlag, int value)
        {
            IsOpen = isOpen;
            HasMine = hasMine;
            HasFlag = hasFlag;
            Value = value;
        }
    }
}