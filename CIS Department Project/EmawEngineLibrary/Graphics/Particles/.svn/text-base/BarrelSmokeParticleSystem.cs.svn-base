using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmawEngineLibrary.Graphics.Particles
{
    public class BarrelSmokeParticleSystem : ParticleSystem
    {
            public BarrelSmokeParticleSystem(Game game, ContentManager content)
                : base(game, content)
            { }


            protected override void InitializeSettings(ParticleSettings settings)
            {
                settings.TextureName = "barrelSmoke";

                settings.MaxParticles = 800;

                settings.Duration = TimeSpan.FromSeconds(1f);

                settings.MinHorizontalVelocity = 5;
                settings.MaxHorizontalVelocity = 10;

                settings.MinVerticalVelocity = 0;
                settings.MaxVerticalVelocity = 5;

                settings.Gravity = new Vector3(0, 3, 0);

                settings.EndVelocity = 1f;

                settings.MinRotateSpeed = -1;
                settings.MaxRotateSpeed = 1;

                settings.MinStartSize = 2;
                settings.MaxStartSize = 5;

                settings.MinEndSize = 5;
                settings.MaxEndSize = 8;

                settings.BlendState = BlendState.Additive;
            }
    }
}
