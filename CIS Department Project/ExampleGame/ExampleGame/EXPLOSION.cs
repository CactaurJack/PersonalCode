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
    class EXPLOSION : DrawableGameComponent
    {
        Vector3 position;

        ParticleEmitter explosionParticleEmitter;
        ParticleSystem explosionParticleSystem;
        private float gametime = 0.0f;
        private bool active;

        public EXPLOSION(Game game, Vector3 position)
            : base(game)
        {
            explosionParticleSystem = new EXPLOSIONparticleSystem(game, game.Content);
            explosionParticleEmitter = new ParticleEmitter(explosionParticleSystem, 100, position);

            active = true;

            this.position = position;

            game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            explosionParticleSystem.Initialize();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(this);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            gametime += gameTime.ElapsedGameTime.Milliseconds;
            if (active && gametime > 500)
            {
                explosionParticleEmitter = new ParticleEmitter(explosionParticleSystem, 0, position);
                active = false;
            }
            if (!active && gametime > 2500)
            {
                Dispose();
            }
                explosionParticleEmitter.Update(gameTime, position);
                explosionParticleSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            explosionParticleSystem.Draw(gameTime);
        }
    }
}
