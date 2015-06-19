#region File Description
//-----------------------------------------------------------------------------
// CreditScreen.cs
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
    class CreditScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteBatch spriteBatch;

        string creditText;
        float scrollSpeed;

        Vector2 textPos;
        SpriteFont font;

        int numLines;
        int textHeight;

        bool paused;

        #endregion

        #region Initialization

        public CreditScreen()
        {
            // Sets how fast the credits will scroll
            scrollSpeed = 0.1f;

            // Build the credit text here
            creditText = "CREDITS\n\n" +
                         "Matthew Strayhall -- Credits Screen, Menu font and appearance, Splash Screen\n\n"
                         + "Greg Waters -- Game Types, Type Selection Menu, Dust Particle Artist, Pipeline Optimization, Net Code\n\n"
                         + "Matt Coplin -- Tank movement, Tank physics, Bullet physics, Net Code\n\n"
                         + "Joseph Heier -- Tank Audio, Bullet 3D Audio\n\n"
                         + "Jeff Richmond -- Game Music\n\n"
                         + "\n\n"
                         + "Anvil sound effect from http://www.freesound.org/ by artist Benboncan\n\n"
                         + "Game music from 'Celestial Aeon Project' http://www.mikseri.net/artists/?id=48147 \n\n";

        }

        /// <summary>
        /// Set the initial text position and calculate how many lines we have to scroll through.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            spriteBatch = ScreenManager.SpriteBatch;

            // Set the initial text position
            textPos = new Vector2(100, ScreenManager.GraphicsDevice.Viewport.Height);
            font = content.Load<SpriteFont>("creditFont");


            // See how many lines we have so we know how long to scroll before returning to menu
            string[] tokens = creditText.Split('\n');
            numLines = tokens.Length;
            textHeight = numLines * font.LineSpacing;

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Allow the user to pause the credits or exit them early.
        /// </summary>
        /// <param name="input"></param>
        public override void HandleInput(InputState input)
        {
            // Allow player to return to main menu at any time.
            PlayerIndex playerIndex;

            // If the user presses the ESCAPE key or the back button, back out of this screen
            if (input.IsNewKeyPress(Keys.Escape, null, out playerIndex) || input.IsNewButtonPress(Buttons.Back, null, out playerIndex))
            {
                this.ExitScreen();
            }

            // If the user presses the SPACE key or the A button, pause the credits
            if (input.IsNewKeyPress(Keys.Space, null, out playerIndex) || input.IsNewButtonPress(Buttons.A, null, out playerIndex))
            {
                if (paused == false) paused = true;
                else paused = false;
            }

            base.HandleInput(input);
        }

        /// <summary>
        /// Scroll the text until we are off the top of the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!paused)
            {
                // Update the text position if we aren't paused.
                textPos.Y -= scrollSpeed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (textPos.Y + textHeight < 0)
            {
                // We have scrolled through all credits off the top of the screen
                // so we can return to the main menu.
                this.ExitScreen();
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// Draw the credit text
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, creditText, textPos, Color.Wheat);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}
