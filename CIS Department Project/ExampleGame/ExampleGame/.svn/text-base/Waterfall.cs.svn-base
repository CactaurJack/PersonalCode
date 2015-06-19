using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using EmawEngineLibrary;
using EmawEngineLibrary.Graphics;
using EmawEngineLibrary.Graphics.Particles;
using EmawEngineLibrary.Cameras;
namespace ExampleGame
{
    /// <summary>
    /// A class representing a waterfall in our game world.
    /// TODO: Replace the cylinder primitive representation
    /// of the waterfall with a particle system in the
    /// same world space.
    /// 
    /// </summary>
    class Waterfall : DrawableGameComponent
    {
        #region Fields

        // A cylinder to represent our waterfall

        WaterfallParticleSystem waterfallParticleSystem;
        ParticleEmitter waterfallParticleEmitter;

        WaterfallChurnParticleSystem waterfallChurnParticleSystem;
        ParticleEmitter waterfallChurnParticleEmitter;

        // The origin of the waterfall in world coordinates
        Vector3 origin;

        //The base of the waterfall;
        Vector3 bottom;

        // A transformation matrix for placing the cylinder in the world
        Matrix transform;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new campfire instance and places it in the world.  
        /// </summary>
        /// <param name="game">The game this campfire belongs in</param>
        /// <param name="position">The position of the campfire in the game world</param>
        public Waterfall(Game game, Vector3 origin) : base (game)
        {
            // Save the waterfall's origin for reference
            this.bottom = origin;
            this.origin = origin + 10 * Vector3.Up;

            // Generate the campfire's translation matrix only once, as
            // it will be stationary in our world
            transform = Matrix.CreateScale(1, 10, 1) * Matrix.CreateTranslation(origin);

            waterfallParticleSystem = new WaterfallParticleSystem(game, game.Content);
            waterfallParticleEmitter = new ParticleEmitter(waterfallParticleSystem, 1000, origin);

            waterfallChurnParticleSystem = new WaterfallChurnParticleSystem(game, game.Content);
            waterfallChurnParticleEmitter = new ParticleEmitter(waterfallChurnParticleSystem, 60, bottom);

            // Add the campfire to the game's component list.  As our
            // waterfall inherits from DrawableGameComponent, the game
            // will now automatically call the Initialize(), Update(), 
            // and Draw() methods for us at the appropriate times.
            game.Components.Add(this);
        }

        /// <summary>
        /// Loads graphics for the campfire.
        /// </summary>
        protected override void LoadContent()
        {
            waterfallParticleSystem.Initialize();
            waterfallChurnParticleSystem.Initialize();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            waterfallParticleEmitter.Update(gameTime, origin);
            waterfallParticleSystem.Update(gameTime);
            waterfallChurnParticleEmitter.Update(gameTime, bottom);
            waterfallChurnParticleSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Grab the current camera from our game's services
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            waterfallParticleSystem.Draw(gameTime);
            waterfallChurnParticleSystem.Draw(gameTime);

        }

        #endregion
    }
}
