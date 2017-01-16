﻿#region File Description
//-----------------------------------------------------------------------------
// ExplosionSmokeParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;
#endregion


/// <summary>
/// ExplosionSmokeParticleSystem is a specialization of ParticleSystem which
/// creates a circular pattern of smoke. It should be combined with
/// ExplosionParticleSystem for best effect.
/// </summary>
public class ExplosionSmokeParticleSystem : ParticleSystem
{
    public ExplosionSmokeParticleSystem(Game game, int howManyEffects)
        : base(game, howManyEffects)
    {
    }

    /// <summary>
    /// Set up the constants that will give this particle system its behavior and
    /// properties.
    /// </summary>
    protected override void InitializeConstants()
    {
        texture = TEX_SMOKE;

        // less initial speed than the explosion itself
        minInitialSpeed = 40f;
        maxInitialSpeed = 60f;

        // acceleration is negative, so particles will accelerate away from the
        // initial velocity.  this will make them slow down, as if from wind
        // resistance. we want the smoke to linger a bit and feel wispy, though,
        // so we don't stop them completely like we do ExplosionParticleSystem
        // particles.
        minAcceleration = -1;
        maxAcceleration = -5;

        // explosion smoke lasts for longer than the explosion itself, but not
        // as long as the plumes do.
        minLifetime = 1.5f;
        maxLifetime = 3.5f;

        minNumParticles = 30;
        maxNumParticles = 45;

        minScale = 0.3f;
        maxScale = 1f;
        maxAlpha = 0.3f;
        alphaRamp = 0.2f;

        minRotationSpeed = -MathHelper.Pi/14;
        maxRotationSpeed = MathHelper.Pi/14;

       // spriteBlendMode = BlendState.Additive;
    }
}