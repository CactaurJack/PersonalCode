#region File Description
//-----------------------------------------------------------------------------
// SplashScreen.cs
//
// Designed to work with GameState example from create.msdn.com, copyright as follows:
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace ExampleGame
{
    class SplashScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D splashTexture;
        TimeSpan displayTime;
        private string m_textureName;

        #endregion

        #region Initialization

        public SplashScreen(string logoTexturePath)
        {
            TransitionOnTime = TimeSpan.FromSeconds(2.0);
            TransitionOffTime = TimeSpan.FromSeconds(1);

            displayTime = new TimeSpan();

            m_textureName = logoTexturePath;
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            splashTexture = content.Load<Texture2D>(m_textureName);

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // check how long the splash screen has been visible
            displayTime += gameTime.ElapsedGameTime;
            if (displayTime > TimeSpan.FromSeconds(2.0))
            {
                // go to the main menu
                ScreenManager.AddScreen(new MainMenuScreen(), null);
                ScreenManager.RemoveScreen(this);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(InputState input)
        {
            // allow the splash screen to be skipped
            PlayerIndex playerIndex;

            // If the user preses the start button or space key, move to the next screen
            if (input.IsNewKeyPress(Keys.Space, null, out playerIndex) || input.IsNewButtonPress(Buttons.Start, null, out playerIndex))
            {
                ScreenManager.AddScreen(new MainMenuScreen(), null);
                ScreenManager.RemoveScreen(this);
            }

            base.HandleInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(splashTexture, ScreenManager.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

    }
}
