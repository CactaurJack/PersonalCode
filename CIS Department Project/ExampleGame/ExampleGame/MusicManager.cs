using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace ExampleGame
{
    class MusicManager : GameComponent
    {
        GameState gameState;

        private Song menuSong;
        private bool menusongPlay;
        private Song gameplaySong;
        private bool gameplaysongPlay;

        public MusicManager(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            menuSong = Game.Content.Load<Song>("Sounds/MenuSong");
            gameplaySong = Game.Content.Load<Song>("Sounds/GamePlaySong");
           

            MediaPlayer.IsRepeating = true;

            base.Initialize();
        }

        public void UpdateState(GameState _gameState)
        {
            gameState = _gameState;
        }

        public override void Update(GameTime gameTime)
        {
            if (!gameplaysongPlay && !menusongPlay)
            {
                if (gameState == GameState.NetworkGame)
                {
                    gameplaysongPlay = true;
                    menusongPlay = false;
                    MediaPlayer.Volume = .4f;
                    MediaPlayer.Play(gameplaySong);
                }
                else
                {
                    gameplaysongPlay = false;
                    menusongPlay = true;
                    MediaPlayer.Volume = .5f;
                    MediaPlayer.Play(menuSong);
                }
            }

            else
            {
                // If gameplay is active and menu song is playing, switch to gameplay song
                if (gameState == GameState.NetworkGame && menusongPlay == true)
                {
                    menusongPlay = false;
                    gameplaysongPlay = true;
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = .4f;
                    MediaPlayer.Play(gameplaySong);
                }
                else if (gameState != GameState.NetworkGame && gameplaysongPlay == true)
                {
                    gameplaysongPlay = false;
                    menusongPlay = true;
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = .6f;
                    MediaPlayer.Play(menuSong);
                }
            }
        }

    }
}
