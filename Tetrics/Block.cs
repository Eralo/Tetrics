﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics {
    public abstract class Block {

        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        public Block() {

            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        public IEnumerable<Position> TilePositions() {
            foreach (Position p in Tiles[rotationState]) {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void Rotate() {
            rotationState = (rotationState+ 1) % Tiles.Length;
        }

        public void RotateInverse() {
            if (rotationState == 0) {

                rotationState = Tiles.Length - 1;
            }

            else {
                rotationState--;
            }
        }

        public void Move(int x, int y) {

            offset.Row += x;
            offset.Column += y;
        } 

        public void Reset() {

            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
