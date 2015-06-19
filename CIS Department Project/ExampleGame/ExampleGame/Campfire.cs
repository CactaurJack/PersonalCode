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
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Cameras;

namespace ExampleGame
{
    /// <summary>
    /// A class representing a campfire in our game world.
    /// TODO: Replace the sphere primitive representation
    /// of the campfire with a particle system modeling 
    /// flames
    /// </summary>
    public class Campfire : DrawableGameComponent
    {
        #region Fields

        // The position of the campfire in world coordinates
        Vector3 position;

        // A particle system and emitter to control the campfire's 
        // smoke
        ParticleSystem smokeParticleSystem;
        ParticleEmitter smokeParticleEmitter;

        // A particle system and emitter to control the campfire's 
        // flame
        ParticleSystem flameParticleSystem;
        ParticleEmitter flameParticleEmitter;

        // The translation matrix that will orient the sphere primitive in the game world
        Matrix campfireTranslation;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new campfire instance and places it in the world.  
        /// </summary>
        /// <param name="game">The game this campfire belongs in</param>
        /// <param name="position">The position of the campfire in the game world</param>
        public Campfire(Game game, Vector3 position) : base (game)
        {
            // Create the smoke particle system and emitter

            //Edited the particles per second on smoke. If a fire this small makes that much smoke,
            //you're doing something wrong.
            smokeParticleSystem = new SmokePlumeParticleSystem(game, game.Content);
            smokeParticleEmitter = new ParticleEmitter(smokeParticleSystem, 20, position);

            flameParticleSystem = new SmallFireParticleSystem(game, game.Content);
            flameParticleEmitter = new ParticleEmitter(flameParticleSystem, 60, position);

            // Save the campfire's position for reference
            this.position = position;

            // Generate the campfire's translation matrix only once, as
            // it will be stationary in our world
            campfireTranslation = Matrix.CreateTranslation(position);

            // Add the campfire to the game's component list.  As our
            // campfire inherits from DrawableGameComponent, the game
            // will now automatically call the Initialize(), Update(), 
            // and Draw() methods for us at the appropriate times.
            //
            // Normally we would register the particle system as well,
            // but since they only exist for this instance of the campfire
            // we'll call the methods ourselves - doing so simplifies
            // cleanup
            game.Components.Add(this);
        }

        /// <summary>
        /// Loads graphics for the campfire.
        /// </summary>
        protected override void LoadContent()
        {
            // Call the initialize method of our SmokeParticleSystem.
            // Doing so will also call its LoadContent() method
            smokeParticleSystem.Initialize();

            // Initialize the campfire
            flameParticleSystem.Initialize();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            // Call the update method on our particle emitter.
            // Since our campfire never moves, we just give it
            // the same position
            smokeParticleEmitter.Update(gameTime, position);
            flameParticleEmitter.Update(gameTime, position);

            // Manually call the update method of our particle
            // system
            smokeParticleSystem.Update(gameTime);
            flameParticleSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Grab the current camera from our game's services
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            // Draw the campfire
            flameParticleSystem.Draw(gameTime);

            // Draw our particle system's particles manully
            smokeParticleSystem.Draw(gameTime);
        }

        #endregion
    }
}
