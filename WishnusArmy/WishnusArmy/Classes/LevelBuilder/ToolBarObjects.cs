using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ObjectLists;
using static ContentImporter.Fonts;

public class ToolBarObjects : GameObject
{
    public List<ToolBarObjectsItem> toolList;
    int selected;

    public ToolBarObjects()
    {
        selected = 0;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);


        if (inputHelper.MouseLeftButtonPressed() && inputHelper.MouseInGameWindow)
        {
            try
            {
                GridNode nodeAt = CameraGridPlane.NodeAt(inputHelper.MousePosition - CameraPosition);  //Get the node at the mouse
                Type t = Type.GetType(toolList[selected].name); //Get the type of the object
                object temp = Activator.CreateInstance(t); //Create an instance of that object
                GameObject obj = temp as GameObject; //Cast it as a GameObject
                obj.Position = nodeAt.Position; //Set it's position at the mouse node
                CameraGridPlane.Add(obj); //Add it to the plane
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //Change the selected object
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
                        selected = s; //Select a new object
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y, SCREEN_SIZE.X, SCREEN_SIZE.Y), spriteBatch, Color.White, 0.6f);
        for(int i=0; i<toolList.Count; i++)
        {
            DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, "["+i.ToString()+"]  "+toolList[i].name, new Vector2(150 + 150 * i, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y + 20), Color.Black, true);
            if (selected == i)
            {
                DrawingHelper.DrawRectangle(new Rectangle(75 + 150 * i, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y + 5, 150, 30), spriteBatch, Color.Black, 2);
            }
            spriteBatch.Draw(toolList[i].sprite, new Vector2(150 + 150 * i - toolList[i].sprite.Width/2, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y + 40), Color.White);
        }
    }
}
