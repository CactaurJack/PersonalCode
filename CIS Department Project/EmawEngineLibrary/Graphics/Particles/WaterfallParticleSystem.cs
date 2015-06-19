#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace EmawEngineLibrary.Graphics.Particles
{
    public class WaterfallParticleSystem : ParticleSystem
    {
        public WaterfallParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "waterdrop";

            settings.MaxParticles = 6000;

            settings.Duration = TimeSpan.FromSeconds(1);

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 7;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 0;

            settings.Gravity = new Vector3(0, -20, 0);

            settings.EndVelocity = 0f;

            settings.MinStartSize = 1f;
            settings.MaxStartSize = 1f;

            settings.MinEndSize = 1f;
            settings.MaxEndSize = 1f;

            settings.BlendState = BlendState.Additive;
        }

    }
}
