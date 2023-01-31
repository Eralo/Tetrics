using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics {
    public class Scoring {

        public int Score { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int Lines { get; private set; } = 0; //number of line FOR CURRENT LEVEL
        public bool BacktoBack { get; private set; } = false;

        private readonly int[] level_table = new int[] { //increase level for X line number (26lvl + are same)
            10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100, 100, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200
        };

        private readonly int[] speed = new int[] { //speed per level: obtained by number of frames divided by 60 (frames sec). Stays at 30 for 19-28 and 16 above
                                                    //Because we use Delay(), it may change depending of game speed (though it is an alternate thread)
            800, 717, 633, 550, 466, 383, 300, 216, 133, 100, 83, 83, 83, 66, 66, 66, 50, 50, 50, 30, 16
        };


        public void AddScore(int lines, int combo) {
            switch (lines) {
                case 1: Score += 100 * Level; BacktoBack = false; break;
                case 2: Score += 300 * Level; BacktoBack = false; break;
                case 3: Score += 500 * Level; BacktoBack = false; break;
                case 4: 
                    if (BacktoBack) Score += 800 * Level + ((800 * Level) / 2);  //I don't want to use float to do *1.5 so use of approximation
                    else Score += 800 * Level; 
                    BacktoBack = true; 
                    break;   
                case 0:
                default:
                    return;
            }

            Score +=  (50 * combo * Level) ;
            Lines += lines ;

            LevelUp();
        }

        public void Tspin(int lines, int combo) { 
        
            switch (lines) {
                case 0: Score += 400 * Level; break;
                case 1: Score += 800 * Level; BacktoBack = true; break;
                case 2: Score += 1200 * Level; BacktoBack = true; break;
                case 3: Score += 1600 * Level; BacktoBack = true; break;
                default: return;
            }

            Score += (50 * combo * Level);
            Lines += lines;

            LevelUp();
        }

        private void LevelUp() {

            if (Level >= 26) {
                if (Lines >= 200) {
                    Level++;
                    Lines = 0;
                }
            }

            else if (Lines >= level_table[Level - 1]) {   //leveling up is always after count
                Level++;
                Lines = 0;  //reset number of lines
            }
        }

        public int CurrentSpeed() {

            if (Level <=19) return speed[Level-1];
            if (Level >= 29) return speed[20];
            else return speed[19];

        }
        public int LinesLeft() {
            return level_table[Level-1] - Lines;
        }
    }
}
