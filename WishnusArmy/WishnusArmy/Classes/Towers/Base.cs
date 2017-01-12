using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

public class Base : GameObject
{
    float rotation;
    Texture2D cannonTexture, baseTexture;
    List<GridNode> myNodes;
    bool hover;


    public Base() : base()
    {
        this.cannonTexture = SPR_BASEGUN;
        this.baseTexture = SPR_BASE;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (myNodes == null)
        {
            myNodes = new List<GridNode>();
            myNodes.Add(MyPlane.NodeAt(position, true));
            myNodes.AddRange(myNodes[0].ExtendedNeighbours);
        }
        hover = false;
        foreach(GridNode  node in myNodes)
        {
            if (node.selected)
                hover = true;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), 0f, new Vector2(1f), Color.White * (1f - 0.4f * hover.ToInt()), SpriteEffects.None, 0);
        spriteBatch.Draw(cannonTexture, GlobalPosition, null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        Vector2 targetPos = inputHelper.MousePosition;
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
    }
}
