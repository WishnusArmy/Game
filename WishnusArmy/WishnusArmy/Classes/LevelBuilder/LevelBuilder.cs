using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public class LevelBuilder : GameObjectList
{
    public LevelBuilder() : base()
    {
        Add(new ToolSelectionBar());
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.S))
        {
            Console.Write("Saving Level... ");
            SaveLevel();
            Console.WriteLine("DONE!");
        }
    }

    public void SaveLevel()
    {
        IniFile file = new IniFile("level.ini");
        int i = 0; //general index counter
        foreach (GameObject obj in GameWorld.FindByType<GameObject>())
        {
            file.Write("X", obj.Position.X.ToString(), i.ToString());
            file.Write("Y", obj.Position.Y.ToString(), i.ToString());
            if (obj is GridPlane)
            {
                GridPlane subObj = obj as GridPlane;
                for(int x=0; x<LEVEL_SIZE; ++x)
                {
                    for(int y=0; y<LEVEL_SIZE; ++y)
                    {
                        GridNode node = subObj.grid[x, y]; //container for easy acces
                        //Split the filepath at "/" and get the last element to get only the filename
                        string[] str = node.texture.ToString().Split('/');
                        file.Write("Texture"+x.ToString()+"x"+y.ToString(), str[str.Length - 1], i.ToString());
                    }
                }
            }
            ++i;
        }
    }
}
