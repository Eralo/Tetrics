﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetrics.Blocks
{
    public class Iblock : Block
    {

        private readonly Position[][] tiles = new Position[][] {

            new Position[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new Position[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new Position[] { new(2,0), new(2,1), new(2,2), new(2,3) },
            new Position[] { new(0,1), new(1,1), new(2,1), new(3,1) },
        };

        private readonly int[][] kick = new int[][] {      //kick is trying to fit the block while rotating by kicking it to a near location. The map:

            new int[] { -2,0, 1,0, -2,-1, 1,2}, //0 to 1
            new int[] { -1,0, 2,0, -1,2, 2,-1}, //1 to 2
            new int[] { 2,0, -1,0, 2,1, -1,-2}, //2 to 3
            new int[] {1, 0, -2, 0, 1, -2, -2, 1 }, //3 to 0

            new int[] { 2,0, -1,0, 2,1, -1,-2}, //1 to 0
            new int[] { 1,0, -2,0, 1,-2, -2,1}, //2 to 1
            new int[] {-2,0, 1,0, -2,-1, 1,2 }, //3 to 2
            new int[] {-1, 0, 2, 0, -1, 2, 2, -1 }, //0 to 3
        };

        public override int Id => 1;

        protected override Position StartOffset => new Position(-1, 3);
        protected override Position[][] Tiles => tiles;

        public override int[][] Kick => kick;

    }
}
