#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace EmawEngineLibrary.Graphics.Particles
{
    public class WaterfallChurnParticleSystem : ParticleSystem
    {
        public WaterfallChurnParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "bubble";

            settings.MaxParticles = 6000;

            settings.Duration = TimeSpan.FromSeconds(2);

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 4;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 4;

            settings.Gravity = new Vector3(0, -3, 0);

            settings.EndVelocity = 0f;

            settings.MinStartSize = 1f;
            settings.MaxStartSize = 1f;

            settings.MinEndSize = 1f;
            settings.MaxEndSize = 1f;

            settings.BlendState = BlendState.Additive;
        }

    }
}
