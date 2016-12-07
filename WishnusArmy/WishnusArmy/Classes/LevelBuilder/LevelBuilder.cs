using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
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
        int i = 0;
        Hashtable file = new Hashtable();
        foreach (GameObject obj in GameWorld.FindByType<GameObject>())
        {
            file.Add(i.ToString() + ".X", obj.Position.X.ToString());
            if (obj is GridNode)
            {
                GridNode subObj = obj as GridNode;
                string[] str = subObj.texture.ToString().Split('/');
                file.Add(i.ToString() + ".Texture", str[str.Length - 1]);
            }
            ++i;
        }
        FileStream fs = new FileStream("level.dat", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, file);
        fs.Close();
        

    }
}
