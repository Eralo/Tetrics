using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tetrics.Blocks;

namespace Tetrics {
    public class BlockQueue {

        private readonly Block[] blocks = new Block[] {     //Not adaptable to different sizes. How ?

            new Iblock(),
            new Jblock(),
            new Lblock(),
            new Oblock(),
            new Sblock(),
            new Tblock(),
            new Zblock()
        };

        private readonly Random random = new Random();

        private List<int> Bag { get; set; } = new List<int>() { 0, 1, 2, 3, 4, 5, 6}; //same here

        private int BagPtr { get; set; } = 0;

        public Block NextBlock { get; private set; }

        public BlockQueue() {
            Shuffle();
            GetAndUpdate();
        }


        private void Shuffle() {  //Fisher-Yates
            int n = Bag.Count;
            while (n > 1) {
                n--;
                int rd = random.Next(n+1);
                int value = Bag[rd];
                Bag[rd] = Bag[n];
                Bag[n] = value;
            }
        }

        public Block GetAndUpdate() {

            Block block = NextBlock;

            if (BagPtr == blocks.Length-1) {
                Shuffle();
                BagPtr= 0;
            }

            NextBlock =  blocks[Bag[BagPtr]];
            BagPtr++;

             return block;
        }
    }
}
