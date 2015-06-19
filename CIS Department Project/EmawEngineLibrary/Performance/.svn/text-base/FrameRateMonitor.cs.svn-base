#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace EmawEngineLibrary.Performance
{
    /// <summary>
    /// This class is a drawable game component implementing a 
    /// frame rate monitor.  The framerate is displayed in the 
    /// upper-right hand corner of the display.
    /// 
    /// It is useful for performance monitoring purposes - the 
    /// higher the framerate the swifter your program's execution.
    /// 
    /// However, it turns off synchonizaiton of the screen, and
    /// as such should never be used in produciton code (but it
    /// is relatively easy to add or remove from a game).
    /// </summary>
    public class FrameRateMonitor : DrawableGameComponent
    {
        #region Fields

        // Our frames per second
        private float fps;

        // A spritebatch and spritefont for rendering our FPS
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new FrameRateMonitor 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphics"></param>
        public FrameRateMonitor(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            // Set the game to be variable-step
            game.IsFixedTimeStep = false;

            // Turn off vertical retrace synchronizaiton
            graphics.SynchronizeWithVerticalRetrace = false;

            // Make sure we render last
            this.DrawOrder = 9000;

            // Add ourselves to the game's component list
            Game.Components.Add(this);
        }

        /// <summary>
        /// Loads the graphical parts of this component
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize our SpriteBatch
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Load the font with the game's content manager
            spriteFont = Game.Content.Load<SpriteFont>("DemoFont");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Update the FrameRateMonitor with the latest framerate
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Calculate our framerate (the inverse of the elapsed time)
            fps = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// Render the framerate in the upper-right-hand corner of the screen 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // Draw the framerate
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "FPS: " + fps, new Vector2(20, 20), Color.Wheat);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
