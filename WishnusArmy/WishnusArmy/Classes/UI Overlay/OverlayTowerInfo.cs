using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static Constant;
using static ContentImporter.Fonts;

public class OverlayTowerInfo : GameObject
{
    Tower _tower;
    public Tower tower
    {
        get
        {
            return _tower;
        }
        set
        {
            if (_tower != null)
                _tower.myNode.beacon = false; //Unselected the current tower
            _tower = value; //set the new tower
            if (_tower !=  null) //If the new tower isn't null...
                _tower.myNode.beacon = true; //Select the new tower.
        }
    }

    public OverlayTowerInfo() : base()
    {
        tower = null;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawRectangle(new Rectangle(position.toPoint(), new Point(400, OVERLAY_SIZE.Y)), spriteBatch, Color.White, 2);
        if (tower != null)
        {
            spriteBatch.Draw(tower.baseTexture, position + new Vector2(30, 80), Color.White);
            DrawText(spriteBatch, FNT_MENU, TOWER_INFO[tower.ToString()].name, position + new Vector2(100, 25), Color.White, true);

            Point blockSize = new Point(15, 20);
            int blockSeperation = 4;
            for (int z = 0; z < tower.stats.Length; ++z)
            {
                for (int i = 0; i < tower.stats[z] + 1; ++i)
                {
                    DrawRectangleFilled(new Rectangle(position.toPoint() + new Point(180 + (blockSize.X + blockSeperation) * i, 80 + (blockSize.Y + 15) * z), blockSize), spriteBatch, Color.White);
                }
            }
        }
    }
}