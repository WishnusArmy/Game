﻿using System;
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
    int _texture;
    public int texture
    {
        get { return _texture;  }
        set
        {
            solid = false;
            _texture = value;
            switch(value)
            {
                case 4: solid = true; break; //water
            }
        }
    }
    public bool solid;
    public bool selected;
    public Camera.Plane plane;

    //AI
    public int Hval, Gval, Fval, Dval; //Heuristic, Movement, Sum, Danger
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
        Dval = 0;
        pathParent = this;
        beacon = false;
    }

    public List<GridNode> Neighbours
    {
        get
        {
            List<GridNode> n = new List<GridNode>(); //Make a container
            GridPlane plane = Parent as GridPlane; //Get the parent plane
            List<GridNode> all = plane.FindByType<GridNode>(); //Get all the nodes in the plane
            for(int i=0; i<all.Count; ++i)
            {
                if (all[i].position == position + new Vector2(NODE_SIZE.X / 2, NODE_SIZE.Y / 2) ||
                    all[i].position == position + new Vector2(-NODE_SIZE.X / 2, NODE_SIZE.Y / 2) ||
                    all[i].position == position + new Vector2(-NODE_SIZE.X / 2, -NODE_SIZE.Y / 2) ||
                    all[i].position == position + new Vector2(NODE_SIZE.X / 2, -NODE_SIZE.Y / 2))
                {
                    n.Add(all[i]); //Add if it's a neighbour
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
