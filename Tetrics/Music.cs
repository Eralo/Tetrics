﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetrics {
    public class Music {

        private readonly Uri[] SoundsTable = new Uri[] {

            new Uri(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assets", "music_theme.wav")),
            new Uri(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assets", "music_theme.wav")),
        };


        private MediaPlayer MusicPlayer = new MediaPlayer();
        private MediaPlayer EffectPlayer = new MediaPlayer();


        public void Game_Theme() {

            MusicPlayer.Open(SoundsTable[0]);
            MusicPlayer.Play();
        }

        public void Clear() {

            EffectPlayer.Open(SoundsTable[1]);
            EffectPlayer.Play();
        }


    }
}
