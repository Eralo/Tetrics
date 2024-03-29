﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetrics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[] {

            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[] {

            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)),
        };

        private Music Music = new Music();

        private GameState gameState = new GameState();

        private readonly Image[,] imageControls;

        private bool pause = false;

        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int DelayDecrease = 25;

        public MainWindow() {

            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
            gameState.Clear += GameState_Clear;
        }

        private Image[,] SetupGameCanvas(GameGrid grid) {

            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int CellSize = 25;

            for (int x=0; x < grid.Rows; x++) {
                for (int y=0; y < grid.Columns; y++) {
                    Image imageControl = new Image {
                        Width = CellSize,
                        Height = CellSize
                    };

                    Canvas.SetTop(imageControl, (x - 2) * CellSize + 10);
                    Canvas.SetLeft(imageControl, y * CellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[x, y] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawGrid(GameGrid grid) {
            
            for (int x =0; x < grid.Rows; x++) {

                for (int y = 0; y < grid.Columns;y++) {
                    int id = grid[x, y];
                    imageControls[x, y].Opacity = 1;
                    imageControls[x, y].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block) {

            foreach (Position p in block.TilePositions()) {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue) {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private void DrawHeldBlock(Block heldBlock) {

            if (heldBlock == null) HoldImage.Source = blockImages[0];
            else HoldImage.Source = blockImages[heldBlock.Id];
        }

        private void DrawGhostBlock(Block block) {
            int dropDistance = gameState.BlockDropDistance();

            foreach (Position p in block.TilePositions()) {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState) {

            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            ScoreText.Text = $"Score: {gameState.Score.Score}";
            LevelText.Text = $"Level: {gameState.Score.Level}";
            LinesText.Text = $"Lines left: {gameState.Score.LinesLeft()}";
        }

    private async Task GameLoop() {

            Draw(gameState);

            while (!gameState.GameOver && !pause) {

                int delay = gameState.Score.CurrentSpeed();
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score.Score}";
            
        }



        private async void Window_KeyDown(object sender, KeyEventArgs e) {

            if (gameState.GameOver) _ = e.Handled;

            if (Keyboard.IsKeyDown(Key.Up)) gameState.RotateBlock();
            if (Keyboard.IsKeyDown(Key.Down)) {
                gameState.MoveBlockDown();
                gameState.Score.Score += 1;
            };
            if (Keyboard.IsKeyDown(Key.Left)) gameState.MoveBlockleft();
            if (Keyboard.IsKeyDown(Key.Right)) gameState.MoveBlockRight();
            if (Keyboard.IsKeyDown(Key.X)) gameState.RotateInverseBlock();
            if (Keyboard.IsKeyDown(Key.C)) gameState.HoldBlock();
            if (Keyboard.IsKeyDown(Key.Space)) gameState.DropBlock();
            if (Keyboard.IsKeyDown(Key.Escape)) pause = true;
            Draw(gameState);

            await Task.Delay(100);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e) {
            Music.Game_Theme();
            await GameLoop();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            gameState.Clear += GameState_Clear;
            await GameLoop();
        }
        private void GameState_Clear(object? sender, EventArgs e) {
            Music.Clear();
        }
    }
}
