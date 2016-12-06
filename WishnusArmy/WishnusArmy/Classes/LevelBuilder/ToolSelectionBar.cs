using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static ContentImporter.Textures;
using static ContentImporter.Fonts;

public class ToolSelectionBar : GameObjectList
{
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, SCREEN_SIZE.Y - 128, SCREEN_SIZE.X, SCREEN_SIZE.Y), spriteBatch, Color.White, 0.6f);
        for(int i=0; i<LIST_FLOOR_TEXTURES.Count; ++i)
        {
            spriteBatch.Draw(LIST_FLOOR_TEXTURES[i], new Vector2(140 * i + 25, SCREEN_SIZE.Y - 120), Color.White);
            string[] str = LIST_FLOOR_TEXTURES[i].ToString().Split('/');
            DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, str[str.Length-1], new Vector2(140 * i + 25 + NODE_SIZE/2, SCREEN_SIZE.Y - 20), Color.Black, true);
        }
    }
}
