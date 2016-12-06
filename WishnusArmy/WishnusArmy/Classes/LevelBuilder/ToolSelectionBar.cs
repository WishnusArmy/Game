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
            spriteBatch.Draw(LIST_FLOOR_TEXTURES[i], new Vector2(140 * i + 75, SCREEN_SIZE.Y - 120), Color.White);
            string[] str = LIST_FLOOR_TEXTURES[i].ToString().Split('/');
            //Split the filepath at "/" and get the last element to get only the filename
            DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, str[str.Length-1], new Vector2(140 * i + 75 + NODE_SIZE/2, SCREEN_SIZE.Y - 20), Color.Black, true);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MouseLeftButtonDown())
        {
            Camera.Plane currentPlane;
            List<GridPlane> planes;
            try
            {
                currentPlane = GameWorld.FindByType<Camera>()[0].currentPlane; //Get the index for the current plane
                planes = GameWorld.FindByType<GridPlane>();  //Get a list of all the planes
                for (int x = 0; x < LEVEL_SIZE; ++x)
                {
                    for (int y = 0; y < LEVEL_SIZE; ++y)
                    {
                        GridNode node = planes[(int)currentPlane].grid[x, y]; //store in node for easy acces
                        if (node.selected)
                        {
                            node.texture = LIST_FLOOR_TEXTURES[0];
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

