using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using WishnusArmy.GameManagement;

public abstract class GameObject : DrawableGameComponent, IGameLoopObject
{
    static SoundManager soundManager = new SoundManager();
    protected GameObject parent;
    protected Vector2 position, velocity;
    protected int layer;
    protected string id;
    public bool visible, active, kill;

    public GameObject(int layer = 0, string id = "") : base(WishnusArmy.WishnusArmy.self)
    {
        this.layer = layer;
        this.id = id;
        position = Vector2.Zero;
        velocity = Vector2.Zero; 
        visible = true;
        kill = false;
        active = true;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(object gameTime)
    {
        GameTime gt = gameTime as GameTime;
        position += (velocity*60) * (float)gt.ElapsedGameTime.TotalSeconds;
        active = !kill;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }

    public virtual void Reset()
    {
        visible = true;
    }

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public virtual Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
            {
                return parent.GlobalPosition + Position;
            }
            else
            {
                return Position;
            }
        }
    }

    public GameObject Root
    {
        get
        {
            if (parent != null)
            {
                return parent.Root;
            }
            else
            {
                return this;
            }
        }
    }

    public GameObjectList GameWorld
    {
        get
        {
            return Root as GameObjectList;
        }
    }

    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public string Id
    {
        get { return id; }
    }

    public new bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public virtual GridPlane MyPlane
    {
        get
        {
            if (this is GridPlane)
                return this as GridPlane;

            if (parent != null)
                return parent.MyPlane;
            else
                return null;
        }
    }

    public ParticleController MyParticleControl
    {
        get
        {
            return MyPlane.particleControl;
        }
    }

    public bool Kill
    {
        get { return kill; }
        set { kill = value; }
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }
    //returns the length of the direct line between two points
    public float CalculateDistance(Vector2 A, Vector2 B)
    {
        float K = (A.Y - B.Y)*2;
        float L = A.X - B.X;
        float distance = (float)Math.Sqrt(K * K + L * L);
        return distance;
    }
    public void PlaySound(SoundEffect soundEffect, Boolean looping = false)
    {
        soundManager.PlaySound(soundEffect, looping);
    }
    public void StopSoundLoops()
    {
        soundManager.instance.Dispose();
    }
}