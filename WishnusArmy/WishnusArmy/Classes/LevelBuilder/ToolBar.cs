using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Textures;
using static ContentImporter.Fonts;



public class ToolBar : GameObject
{
    public List<Texture2D> toolList;
    int selected;

    public ToolBar()
    {
        selected = 0;
        toolList = LIST_LAND_TEXTURES;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y, SCREEN_SIZE.X, SCREEN_SIZE.Y), spriteBatch, Color.White, 0.6f);
        for(int i=0; i<toolList.Count; ++i)
        {
            DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, "[" + i.ToString() + "]", new Vector2(140 * i + 110, SCREEN_SIZE.Y-138), Color.Black, true);
            spriteBatch.Draw(toolList[i], new Vector2(140 * i + 75, SCREEN_SIZE.Y - 120), Color.White);
            string[] str = toolList[i].ToString().Split('/');
            //Split the filepath at "/" and get the last element to get only the filename
            DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, str[str.Length-1], new Vector2(140 * i + 75 + NODE_SIZE.X/2, SCREEN_SIZE.Y - 20), Color.Black, true);
            if (i == selected)
            {
                DrawingHelper.DrawRectangle(new Rectangle(new Point(140 * i + 70, SCREEN_SIZE.Y - 125), new Point(NODE_SIZE.X + 10, 74)), spriteBatch, Color.Black, 3);
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        //Change the texture of the selected node
        if (inputHelper.MouseLeftButtonDown())
        {
            GridPlane currentPlane;
            try
            {
                currentPlane = GameWorld.FindByType<Camera>()[0].currentPlane; //Get the index for the current plane
                for (int x = 0; x < LEVEL_SIZE; ++x)
                {
                    for (int y = 0; y < LEVEL_SIZE; ++y)
                    {
                        GridNode node = currentPlane.grid[x, y]; //store in node for easy acces
                        if (node.selected)
                        {
                            node.texture = selected;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Change the selected texture
        if (inputHelper.AnyKeyPressed)
        {
            //Get the currently pressed keys
            Keys[] keys = inputHelper.CurrentPressedKeys();
            try
            {
                char[] str = keys[0].ToString().ToArray<char>(); //Get the char array of the name of the pressed key
                if (str.Length < 2) { return; }  //Eliminate the "D" Key itself
                if (str[0] == 'D') //Is it a number key?
                {
                    int s = int.Parse(str[1].ToString()); //Get the number pressed
                    if (s < toolList.Count) //is it in range of the available toollist?
                    {
                        selected = s; //Select a new texture
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

