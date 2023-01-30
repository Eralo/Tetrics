using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics {
    public class GameState {

        private Block currentBlock;

        public Block CurrentBlock {
            get => currentBlock;
            private set {
                currentBlock = value;
                currentBlock.Reset(); 

                for (int i=0; i < 2; i++) {

                    currentBlock.Move(1, 0);
                    if (!BlockFits()) currentBlock.Move(-1,0);
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public Scoring Score { get; }
        public bool GameOver { get; private set; }
        public Block HeldBlock { get; private set; }
        public bool CanHold { get; private set; }

        public int Linger { get; private set; } = 5;

        int CurrentLinger = 0;
        public int Combo { get; private set; } = 0;

        public GameState() {

            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            Score = new Scoring();
            CurrentBlock= BlockQueue.GetAndUpdate();
            CanHold = true;
        }

        private bool BlockFits() {

            foreach (Position p in currentBlock.TilePositions()) {

                if (!GameGrid.IsEmpty(p.Row, p.Column)) {
                    return false;
                }
            }
            return true;
        }

        public void HoldBlock() {

            if (!CanHold) return;

            if (HeldBlock == null) {
            
                HeldBlock = CurrentBlock;
            CurrentBlock = BlockQueue.GetAndUpdate();
            }

            else {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold= false;
            CurrentLinger = 0;
        }

        public void RotateBlock() {

            CurrentBlock.Rotate();

            if (BlockFits()) return;
                                                        //SRS
            for (int i = 0; i < 8; i += 2) {    //2 positions in Kick array corresponding to x and y
                if (i != 0) CurrentBlock.Move(CurrentBlock.Kick[CurrentBlock.rotationState][i], CurrentBlock.Kick[CurrentBlock.rotationState][i + 1]);
                if (BlockFits()) return;
                if (i != 0) CurrentBlock.Move(-CurrentBlock.Kick[CurrentBlock.rotationState][i], -CurrentBlock.Kick[CurrentBlock.rotationState][i + 1]);
            }

            CurrentBlock.RotateInverse();
    
        }
        public void RotateInverseBlock() {

            CurrentBlock.RotateInverse();

            if (BlockFits()) return;
                                                         //SRS
            for (int i = 0; i < 8; i += 2) {    //2 positions in Kick array corresponding to x and y
                if (i != 0) CurrentBlock.Move(CurrentBlock.Kick[CurrentBlock.rotationState+4][i], CurrentBlock.Kick[CurrentBlock.rotationState+4][i + 1]);
                if (BlockFits()) return;
                if (i != 0) CurrentBlock.Move(-CurrentBlock.Kick[CurrentBlock.rotationState+4][i], -CurrentBlock.Kick[CurrentBlock.rotationState+4][i + 1]);
            }
            CurrentBlock.Rotate();
        }




        public void MoveBlockleft() {

            CurrentBlock.Move(0, -1);

            if (!BlockFits()) CurrentBlock.Move(0,1);
        }
        public void MoveBlockRight() {

            CurrentBlock.Move(0, 1);

            if (!BlockFits()) CurrentBlock.Move(0, -1);
        }


        private bool IsGameOver() {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock() {

            foreach (Position p in CurrentBlock.TilePositions()) {
                GameGrid[p.Row, p.Column] = currentBlock.Id;
            }

            int i = GameGrid.ClearFullRows();
            if (i == 0) Combo = 0;

            Score.AddScore(i, Combo);

            if (IsGameOver()) GameOver = true;
            else {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
                CurrentLinger = 0;
            }
        }

        public void MoveBlockDown() {

            CurrentBlock.Move(1, 0);

            if (!BlockFits()) {
                CurrentBlock.Move(-1, 0);
                CurrentLinger++;
                if (CurrentLinger>Linger) PlaceBlock();
            }
        }
        
        private int TileDropDistance(Position p) {
            int drop = 0;
            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column)) drop++;
            return drop;
        }

        public int BlockDropDistance() {

            int drop = GameGrid.Rows;

            foreach (Position p in CurrentBlock.TilePositions()) drop = System.Math.Min(drop, TileDropDistance(p));
            return drop;
        }

        public void DropBlock() {
            int score = BlockDropDistance();
            Score.Score += 2 * BlockDropDistance(); 

            CurrentBlock.Move(score, 0);
            PlaceBlock();
        }
    }
}
