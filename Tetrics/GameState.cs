﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool GameOver { get; private set; }

        public GameState() {

            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock= BlockQueue.GetAndUpdate();
        }

        private bool BlockFits() {

            foreach (Position p in currentBlock.TilePositions()) {

                if (!GameGrid.IsEmpty(p.Row, p.Column)) {
                    return false;
                }
            }
            return true;
        }

        public void RotateBlock() {
            CurrentBlock.Rotate();

            if (!BlockFits()) CurrentBlock.RotateInverse();
        }
        public void RotateInverseBlock() {
            CurrentBlock.RotateInverse();

            if (!BlockFits()) CurrentBlock.Rotate();
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

            GameGrid.ClearFullRows();

            if (IsGameOver()) GameOver = true;
            else CurrentBlock = BlockQueue.GetAndUpdate();
        }

        public void MoveBlockDown() {

            CurrentBlock.Move(1, 0);

            if (!BlockFits()) {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }

        }
    }
}
