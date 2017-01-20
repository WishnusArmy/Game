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
    public static Vector2 origin = IMAGE_NODE_SIZE.toVector() / 2;
    public Vector2 offset;
    int _texture;
    public List<GridNode> neighbours, extendedNeighbours;
    public bool solid;
    public bool available;
    public int texture
    {
        get { return _texture;  }
        set
        {
            _texture = value;
            switch(value)
            {
                case 7:
                case 8:
                case 2: solid = true; offset.Y = -LIST_TEXTURES[texture].Height + IMAGE_NODE_SIZE.Y + 15; break; //mountains;
                case 4: solid = true; break; //water
                case 5: solid = true; offset.Y = -LIST_TEXTURES[texture].Height + IMAGE_NODE_SIZE.Y + 8;  break; //forest
                default: solid = false; break;
            }
            //origin = new Vector2(IMAGE_NODE_SIZE.X / 2, -LIST_TEXTURES[texture].Height - IMAGE_NODE_SIZE.Y- 20);
        }
    }

    public bool selected;
    public Camera.Plane plane;

    //AI
    public long Hval, Gval, Fval, Dval; //Heuristic, Movement, Sum, Danger
    public GridNode pathParent;
    public int congestion;

    //temp
    public bool beacon;

    public GridNode(Camera.Plane plane, Vector2 position, int texture) : base()
    {
        offset = Vector2.Zero;
        neighbours = new List<GridNode>();
        extendedNeighbours = new List<GridNode>();
        this.texture = texture;
        this.position = position;
        this.plane = plane;
        Hval = 0;
        Gval = 0;
        Fval = 0;
        Dval = 0;
        pathParent = this;
        beacon = false;
        congestion = 0;
        available = true;
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
            List<GridNode> list = new List<GridNode>();
            list.AddRange(neighbours);
            list.AddRange(extendedNeighbours);
            return list;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public void setDval(GridNode origin, int range, List<GridNode> done, int D)
    {
        //Console.WriteLine("I got the D! ("+D+" dicks)");
        int dis = (int)CalculateDistance(position, origin.position)+1;
        if (dis < range)
        {
            foreach(GridNode node in ExtendedNeighbours)
            { 
                if (!done.onList(node))
                {
                    done.Add(node);
                    node.Dval += (int)(D * (1-((float)dis/range)*0.8));
                    node.setDval(origin, range, done, D);
                }
            }
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

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(LIST_TEXTURES[texture], GlobalPosition + origin + offset, null, Color.White, 0f, origin, NODE_SIZE.toVector() / IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        //DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, "D: " + Dval.ToString(), GlobalPosition + new Vector2(30, 10), Color.Red);
        if (selected)
        {
            spriteBatch.Draw(LIST_TEXTURES[texture], GlobalPosition + origin + offset, null, Color.Black * 0.4f, 0f,  origin, NODE_SIZE.toVector()/IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        }

        if (beacon)
        {
            spriteBatch.Draw(LIST_TEXTURES[texture], GlobalPosition + origin + offset, null, Color.Blue * 0.4f, 0f, origin, NODE_SIZE.toVector() / IMAGE_NODE_SIZE.toVector(), SpriteEffects.None, 0);
        }
        base.Draw(gameTime, spriteBatch); 
    }
}

