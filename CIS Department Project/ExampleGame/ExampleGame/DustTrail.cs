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
    public class DustTrail : DrawableGameComponent
    {
        #region Fields

        Vector3 position;

        ParticleSystem m_dustParticleSystem;
        ParticleEmitter m_dustParticleEmitter;

        int emitterFrequency;

        #endregion

        #region Initialization

        public DustTrail(Game game, Vector3 position)
            : base(game)
        {
            m_dustParticleSystem = new DustParticleSystem(game, game.Content);
            m_dustParticleEmitter = new ParticleEmitter(m_dustParticleSystem, 200, position);

            emitterFrequency = 200;

            this.position = position;
        }

        protected override void LoadContent()
        {
            m_dustParticleSystem.Initialize();
        }

        #endregion

        #region Update and Draw

        public void SetParticleFrequency(int frequency)
        {
            if (emitterFrequency != frequency)
            {
                m_dustParticleEmitter = new ParticleEmitter(m_dustParticleSystem, frequency, position);
                emitterFrequency = frequency;
            }
        }

        public void Update(GameTime gameTime, Vector3 position)
        {
            m_dustParticleEmitter.Update(gameTime, position);
            m_dustParticleSystem.Update(gameTime);
        }

        //public void Update(GameTime gameTime, Matrix world, float facing)
        //{
        //    Vector3 step1 = Vector3.Transform(position, Matrix.CreateRotationY(facing));
        //    Vector3 step2 = Vector3.Transform(step1, world);
        //    m_dustParticleEmitter.Update(gameTime, step2);
        //    m_dustParticleSystem.Update(gameTime);
        //}

        public override void Draw(GameTime gameTime)
        {
            m_dustParticleSystem.Draw(gameTime);
        }

        #endregion
    }
}
