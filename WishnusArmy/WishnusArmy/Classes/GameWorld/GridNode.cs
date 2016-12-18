using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Constant;
using static ContentImporter.Textures;
using static ContentImporter.Fonts;

public class GridNode : GameObject
{
    int obj; //Indicator for what is placed on the square (0 for emtpy)
    public int texture;
    public bool solid;
    public bool selected;
    public Camera.Plane plane;

    //AI
    public int Hval, Gval, Fval;
    public GridNode pathParent;

    //temp
    public bool beacon;

    public GridNode(Camera.Plane plane, Vector2 position, int texture, int obj = 0) : base()
    {
        this.texture = texture;
        this.obj = obj;
        this.position = position;
        this.texture = RANDOM.Next(2);
        this.plane = plane;
        Hval = 0;
        Gval = 0;
        Fval = 0;
        beacon = false;
    }

    public List<GridNode> Neighbours
    {
        get
        {
            List<GridNode> n = new List<GridNode>();
            GridPlane plane = Parent as GridPlane;
            List<GridNode> all = plane.FindByType<GridNode>();
            for(int i=0; i<all.Count; ++i)
            {
                if (all[i].position == position + new Vector2(NODE_SIZE.X/2, NODE_SIZE.Y/2) ||
                    all[i].position == position + new Vector2(-NODE_SIZE.X / 2, NODE_SIZE.Y / 2) ||
                    all[i].position == position + new Vector2(-NODE_SIZE.X / 2, -NODE_SIZE.Y / 2) ||
                    all[i].position == position + new Vector2(NODE_SIZE.X / 2, -NODE_SIZE.Y / 2))
                {
                    n.Add(all[i]);
                }
            }
            return n;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        selected = false;
        Vector2 mousePos = inputHelper.MousePosition;
        if (inputHelper.MouseInGameWindow && HoversMe(mousePos))
        {
           selected = true;
        }
    }

    public bool HoversMe(Vector2 pos)
    {
        if (pos.X >= GlobalPosition.X && pos.X < GlobalPosition.X + NODE_SIZE.X &&
        pos.Y >= GlobalPosition.Y + Math.Abs(GlobalPosition.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y) &&
        pos.Y < GlobalPosition.Y + NODE_SIZE.Y - Math.Abs(GlobalPosition.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y))
        {
            return true;
        }
        return false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, Color.White);
        if (selected)
        {
            spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, Color.Black * 0.4f);
        }
        DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, "H: " + Hval.ToString(), GlobalPosition + new Vector2(30, 30), Color.Red);
        if (beacon)
        {
            spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, Color.Blue * 0.4f);
        }
    }
}

