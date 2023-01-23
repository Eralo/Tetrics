using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics {
    public class Position {

        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int x, int y) {
            Row = x; 
            Column = y;
        }
    }
}
