using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics {
    public class GameGrid {

        private int[,] grid;

        public int Rows { get; }

        public int Columns { get; }

        public int this[int x, int y] {

            get => grid[x, y];
            set => grid[x, y] = value;
        }

        public GameGrid(int x, int y) {

            Rows = x;
            Columns = y;
            grid = new int[x, y];
        }

        public bool IsInside(int x, int y) {
            return x >= 0 && x < Rows && y >= 0 && y < Columns;
        }

        public bool IsEmpty(int x, int y) {
            return IsInside(x, y) && grid[x, y] == 0;
        }

        public bool IsRowFull(int x) {

            for (int y = 0; y < Columns; y++) {
                if (grid[x, y] == 0) return false;
            }
            return true;
        }

        public bool IsRowEmpty(int x) {

            for (int y = 0; y < Columns; y++) {
                if (grid[x, y] != 0) return false;
            }

            return true;
        }

        private void ClearRow(int x) {

            for (int y = 0; y < Columns; y++) {
                grid[x, y] = 0;
            }
        }

        private void MoveRowDown(int x, int num) {

            for (int y = 0; y < Columns; y++) {
                grid[x + num, y] = grid[x, y];
                grid[x, y] = 0;
            }
        }

        public int ClearFullRows() {

            int cleared = 0;

            for (int x = Rows-1 ; x >= 0; x--) { 
          
                if (IsRowFull(x)) {
                    ClearRow(x);
                    cleared++;
                }    
            
                else if (cleared > 0) {
                    MoveRowDown(x, cleared);
                }

            }
            return cleared;
        }
    }
}
