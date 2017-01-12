using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;
using Microsoft.Xna.Framework.Input;
using static DrawingHelper;

public partial class IsometricMovingGameObject : GameObject
{
    protected float rotation;
    protected Texture2D sprite;
    protected SpriteSheet sheet;
    protected Rectangle sheetRec;
    protected int sheetIndex;
    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;

    public IsometricMovingGameObject(Texture2D sprite, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        this.sprite = sprite;

        if (sprite.Name != null)
        {
            sheet = new SpriteSheet(sprite, sheetIndex);
        }
        else
        {
            sheet = null;
        }
    }

    public override void Update(GameTime gameTime)
    {
        sheet.SheetIndex = GetIsometricDirection();
        sheetRec = sheet.Update(gameTime);

        if (sprite == null)
        {
            return;
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //List<Enemy> airnemies = new List<Enemy>();
        //GameWorld.FindByType<Camera>()[0].Air.FindByType<Enemy>();
        //spriteBatch.Draw(sprite, GlobalPosition + new Vector2(NODE_SIZE.X, NODE_SIZE.Y) / 2, sheet.Update(gameTime), Color.Black, rotation, new Vector2(sprite.Width / 2, sprite.Height / 2), 1.1f, SpriteEffects.None, 0);
        spriteBatch.Draw(sprite, GlobalPosition, null, sheet.Update(gameTime), Vector2.Zero, 0f);
    }

    public int GetIsometricDirection()
    {
        if (velocity.X < 0 && velocity.Y < 0) return 0; //Object moves to top left
        else
        if (velocity.X > 0 && velocity.Y < 0) return 1; //Object moves to top right
        else
        if (velocity.X > 0 && velocity.Y > 0) return 2; //Object moves to bottom right
        else
        if (velocity.X < 0 && velocity.Y > 0) return 3; //Object moves to bottom left
        else return 5; //In this case velocity is 0
    }

    public SpriteSheet Sprite
    {
        get { return sheet; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sheet.Width;
        }
    }

    public int Height
    {
        get
        {
            return sheet.Height;
        }
    }

    public bool Mirror
    {
        get { return sheet.Mirror; }
        set { sheet.Mirror = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(IsometricMovingGameObject obj)
    {
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sheet.IsTranslucent(thisx, thisy) && obj.sheet.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

