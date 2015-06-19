#region File Description
//-----------------------------------------------------------------------------
// SmokePlumeParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace EmawEngineLibrary.Graphics.Particles
{
    /// <summary>
    /// Custom particle system for creating a small exhaust trail
    /// </summary>
    public class ExhaustParticleSystem : ParticleSystem
    {
        public ExhaustParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "smoke";
            settings.EmitterVelocitySensitivity = 0;//thanks to nate cramer for this line

            settings.MaxParticles = 50;

            settings.Duration = TimeSpan.FromSeconds(1);

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = .25f;

            settings.MinVerticalVelocity = 1f;
            settings.MaxVerticalVelocity = 3f;

            settings.Gravity = new Vector3(-1, -1, 0);

            settings.MinRotateSpeed = -1.5f;
            settings.MaxRotateSpeed = 1.5f;

            settings.MinStartSize = 1;
            settings.MaxStartSize = 2;

            settings.MinEndSize = 2;
            settings.MaxEndSize = 4;
        }
    }
}

