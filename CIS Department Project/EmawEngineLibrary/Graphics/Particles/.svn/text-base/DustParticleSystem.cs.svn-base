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
    /// Custom particle system for creating a dust trail
    /// </summary>
    public class DustParticleSystem : ParticleSystem
    {
        public DustParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "Dusty";

            settings.EmitterVelocitySensitivity = 0;//thanks to nate cramer for this line
            settings.MaxParticles = 2000;

            settings.Duration = TimeSpan.FromSeconds(2f);

            settings.MinHorizontalVelocity = 0f;
            settings.MaxHorizontalVelocity = 2;

            settings.MinVerticalVelocity = .5f;
            settings.MaxVerticalVelocity = 2;

            settings.Gravity = new Vector3(0, .2f, 0);

            settings.MinRotateSpeed = -1.5f;
            settings.MaxRotateSpeed = 1.5f;

            settings.MinStartSize = .2f;
            settings.MaxStartSize = 2f;

            settings.MinEndSize = 1f;
            settings.MaxEndSize = 3f;

        }
    }
}

