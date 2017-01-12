﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public abstract class Projectile : GameObject
{
    public Enemy target;
    public double damage;
    protected Texture2D sprite;

    public Projectile(double damage) : base()
    {
        target = null;
        this.damage = damage;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public bool HasTarget
    {
        get
        {
            return (target != null && !target.Kill);
        }
    }

    public Vector2 GlobalPositionCenter
    {
        get
        {
            return GlobalPosition + new Vector2(sprite.Width, sprite.Height) / 2;
        }
    }
}