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

public class GridNode : GameObject
{
    int obj; //Indicator for what is placed on the square (0 for emtpy)
    public int texture;
    public bool solid;
    public bool selected;

    public GridNode(Vector2 position, int texture, int obj = 0) : base()
    {
        this.texture = texture;
        this.obj = obj;
        this.position = position;
        this.texture = RANDOM.Next(2);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        selected = false;
        Vector2 mousePos = inputHelper.MousePosition;
        if (inputHelper.MouseInGameWindow &&
            mousePos.X >= GlobalPosition.X && mousePos.X < GlobalPosition.X + NODE_SIZE.X &&
            mousePos.Y >= GlobalPosition.Y + Math.Abs(GlobalPosition.X + NODE_SIZE.X/2 - mousePos.X) / (NODE_SIZE.X/NODE_SIZE.Y) &&
            mousePos.Y < GlobalPosition.Y + NODE_SIZE.Y - Math.Abs(GlobalPosition.X + NODE_SIZE.X/2 - mousePos.X) / (NODE_SIZE.X/NODE_SIZE.Y))
        {
            selected = true;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, null, null, Vector2.Zero, (float)(0.25 * Math.PI));
        spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, Color.White);
        if (selected)
        {
            //DrawingHelper.DrawRectangleFilled(new Rectangle(new Point((int)GlobalPosition.X, (int)GlobalPosition.Y), new Point(NODE_SIZE.X)), spriteBatch, Color.Black, 0.2f);
            //spriteBatch.Draw(TEX_EMPTY, GlobalPosition, null, Color.Black * 0.2f, (float)(0.25 * Math.PI), Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(LIST_LAND_TEXTURES[texture], GlobalPosition, Color.Black * 0.4f);
        }
    }
}

