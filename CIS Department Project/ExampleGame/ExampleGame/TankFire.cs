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


namespace EmawEngineLibrary.Graphics.Particles
{
    class TankFire : DrawableGameComponent
    {
        Vector3 position;

        ParticleEmitter barrelFlashParticleEmitter;
        ParticleSystem barrelFlashParticleSystem;

        ParticleEmitter barrelSmokeParticleEmitter;
        ParticleSystem barrelSmokeParticleSystem;

        private float gametime = 0.0f;

        public TankFire(Game game, Vector3 position)
            : base(game)
        {
            //barrelFlashParticleSystem = new barrelFlashparticleSystem(game, game.Content);
            //barrelFlashParticleEmitter = new ParticleEmitter(barrelFlashParticleSystem, 100, position);

            barrelSmokeParticleSystem = new BarrelSmokeParticleSystem(game, game.Content);
            barrelSmokeParticleEmitter = new ParticleEmitter(barrelSmokeParticleSystem, 100, position);

            this.position = position;

            game.Components.Add(this);
        }

        protected override void LoadContent()
        {
            //barrelFlashParticleSystem.Initialize();
            barrelSmokeParticleSystem.Initialize();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(this);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            gametime += gameTime.ElapsedGameTime.Milliseconds;
            if (gametime > 1000)
            {
                Dispose();
            }
            if (gametime < 100)
                barrelSmokeParticleEmitter.Update(gameTime, position);
            barrelSmokeParticleSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            barrelSmokeParticleSystem.Draw(gameTime);
        }
    }
}
