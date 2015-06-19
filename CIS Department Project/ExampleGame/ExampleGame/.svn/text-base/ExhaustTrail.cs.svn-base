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
    public class ExhaustTrail : DrawableGameComponent
    {
        #region Fields

        Vector3 position;

        ParticleSystem m_exhaustParticleSystem;
        ParticleEmitter m_exhaustParticleEmitter;

        int emitterFrequency;

        #endregion

        #region Initialization

        public ExhaustTrail(Game game, Vector3 position)
            : base(game)
        {
            m_exhaustParticleSystem = new ExhaustParticleSystem(game, game.Content);
            m_exhaustParticleEmitter = new ParticleEmitter(m_exhaustParticleSystem, 10, position);
            emitterFrequency = 10;

            this.position = position;
        }

        protected override void LoadContent()
        {
            m_exhaustParticleSystem.Initialize();
        }

        #endregion

        #region Update and Draw
        public void SetParticleFrequency(int frequency)
        {
            if (emitterFrequency != frequency)
            {
                m_exhaustParticleEmitter = new ParticleEmitter(m_exhaustParticleSystem, frequency, position);
                emitterFrequency = frequency;
            }
        }

        //public void Update(GameTime gameTime, Matrix world, float facing)
        //{
        //    Vector3 step1 = Vector3.Transform(position, Matrix.CreateRotationY(facing));
        //    Vector3 step2 = Vector3.Transform(step1, world);
        //    m_exhaustParticleEmitter.Update(gameTime, step2);
        //    m_exhaustParticleSystem.Update(gameTime);
        //}


        public void Update(GameTime gameTime, Vector3 position)
        {
            m_exhaustParticleEmitter.Update(gameTime, position);
            m_exhaustParticleSystem.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            m_exhaustParticleSystem.Draw(gameTime);
        }

        #endregion
    }
}
