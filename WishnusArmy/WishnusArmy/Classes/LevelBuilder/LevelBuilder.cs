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

        if (inputHelper.KeyPressed(Keys.L))
        {
            Console.Write("Loading Level...");
            LoadLevel();
            Console.WriteLine("DONE!");
        }
    }

    public void SaveLevel()
    {
        int i = 0;
        Hashtable file = new Hashtable();
        foreach (GameObject obj in GameWorld.FindByType<GameObject>())
        {
            file.Add(i.ToString() + ".AAType", obj.GetType());
            file.Add(i.ToString() + ".X", obj.Position.X.ToString());
            file.Add(i.ToString() + ".Y", obj.Position.Y.ToString());
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

    public void LoadLevel()
    {
        Hashtable file = null;
        FileStream fs = new FileStream("level.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize the hashtable from the file and 
            // assign the reference to the local variable.
            file = (Hashtable)formatter.Deserialize(fs);
        }
        catch(Exception e)
        {
            Console.WriteLine("Deserialization failed: " + e.Message);
        }

        int i = 0;
        List<string> list = new List<string>();
        foreach(DictionaryEntry obj in file)
        {
            list.Add(obj.Key.ToString() + "." + obj.Value.ToString());
            ++i;
        }
        list.Sort();
        for(int z=0; z<list.Count; ++z)
        {
            HashContainer hash = new HashContainer(list[z++]);
            string type = hash.type;
            hash = new HashContainer(list[z++]);
            float x = float.Parse(hash.value);
            hash = new HashContainer(list[z++]);
            float y = float.Parse(hash.value);
            switch(type)
            {
                case "GridNode":
                    hash = new HashContainer(list[z++]);
                    string texture = hash.value;
                    Console.WriteLine(texture);
                    break;
            }
        }
    }

    
}
