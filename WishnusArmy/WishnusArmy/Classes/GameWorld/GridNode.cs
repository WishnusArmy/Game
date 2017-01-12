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

public class GridNode : GameObjectList
{
    static Vector2 origin = IMAGE_NODE_SIZE.toVector() / 2;
    int _texture;
    public List<GridNode> neighbours, extendedNeighbours;
    public int texture
    {
        get { return _texture;  }
        set
        {
            _texture = value;
            switch(value)
            {
                case 4: solid = true; break; //water
                case 5: solid = true; break; //forest
                default: solid = false; break;
            }
        }
    }
    public bool solid;
    public bool selected;
    public Camera.Plane plane;

    //AI
    public int Hval, Gval, Fval, Dval; //Heuristic, Movement, Sum, Danger
    public GridNode pathParent;
    public int congestion;

    //temp
    public bool beacon;

    public GridNode(Camera.Plane plane, Vector2 position, int texture) : base()
    {
        neighbours = new List<GridNode>();
        this.texture = texture;
        this.position = position;
        this.texture = RANDOM.Next(2);
        this.plane = plane;
        Hval = 0;
        Gval = 0;
        Fval = 0;
        Dval = 0;
        pathParent = this;
        beacon = false;
        congestion = 0;
    }

    public List<GridNode> Neighbours
    {
        get
        {
            return neighbours;
        }
    }

    public List<GridNode> ExtendedNeighbours
    {
        get
        {
            return extendedNeighbours;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        selected = false;
        Vector2 mousePos = inputHelper.MousePosition;
        if (inputHelper.MouseInGameWindow && HoversMeRelative(mousePos))
        {
           selected = true;
        }
    }

    public bool HoversMeRelative(Vector2 pos)
    {
        if (pos.X >= GlobalPosition.X && pos.X < GlobalPosition.X + NODE_SIZE.X &&
        pos.Y >= GlobalPosition.Y + Math.Abs(GlobalPosition.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y) &&
        pos.Y < GlobalPosition.Y + NODE_SIZE.Y - Math.Abs(GlobalPosition.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y))
        {
            return true;
        }
        return false;
    }

    public bool HoversMe(Vector2 pos)
    {
        if (pos.X >= Position.X && pos.X < Position.X + NODE_SIZE.X &&
        pos.Y >= Position.Y + Math.Abs(Position.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y) &&
        pos.Y < Position.Y + NODE_SIZE.Y - Math.Abs(Position.X + NODE_SIZE.X / 2 - pos.X) / (NODE_SIZE.X / NODE_SIZE.Y))
        {
            return true;
        }
        return false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        beacon = congestion != 0;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition + origin, null, Color.White, 0f, origin, NODE_SIZE.toVector() / IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        //DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, "H: " + Hval.ToString(), GlobalPosition + new Vector2(30, 10), Color.Red);
        if (selected)
        {
            spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition + origin, null, Color.Black * 0.4f, 0f,  origin, NODE_SIZE.toVector()/IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        }

        if (beacon)
        {
            spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition + origin, null, Color.Blue * 0.4f, 0f, origin, NODE_SIZE.toVector() / IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        }
        base.Draw(gameTime, spriteBatch); 
    }
}

