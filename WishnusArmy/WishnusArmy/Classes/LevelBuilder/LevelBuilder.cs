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
    ToolBarTextures tbTextures;
    ToolBarObjects tbObjects;
    public LevelBuilder() : base()
    {
        tbTextures = new ToolBarTextures();
        Add(tbTextures);
        tbObjects = new ToolBarObjects();
        tbObjects.active = false;
        Add(tbObjects);
        Add(new ToolBarSelector(tbTextures, tbObjects));
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
            file.Add(i.ToString() + ".[0]Type", obj.GetType());
            file.Add(i.ToString() + ".[1]X", obj.Position.X.ToString());
            file.Add(i.ToString() + ".[2]Y", obj.Position.Y.ToString());
            if (obj is GridNode)
            {
                GridNode subObj = obj as GridNode;
                string[] str = subObj.texture.ToString().Split('/');
                file.Add(i.ToString() + ".[3]Texture", str[str.Length - 1]);
                file.Add(i.ToString() + ".[4]Plane", ((int)subObj.plane).ToString());
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

        List<string> list = new List<string>();
        foreach(DictionaryEntry obj in file)
        {
            list.Add(obj.Key.ToString() + "." + obj.Value.ToString());
        }

        list.Sort();
        int z = 0;
        List<GridNode> nodes = GameWorld.FindByType<GridNode>(); //Get a list of all the gridnodes
        while(z < list.Count-1)
        {
            /*0*/string typeString = getHash(list[z++], 2);
            /*1*/float x = float.Parse(getHash(list[z++], 2));
            /*2*/float y = float.Parse(getHash(list[z++], 2));

            switch (typeString)
            {
                case "GridNode":
                    /*3*/int tex = int.Parse(getHash(list[z++], 2));
                    /*4*/Camera.Plane plane = (Camera.Plane)int.Parse(getHash(list[z++], 2));
                    for(int i=0; i<nodes.Count; ++i)
                    {
                        if (nodes[i].Position == new Vector2(x, y) && nodes[i].plane == plane)
                        {
                            nodes[i].texture = tex;
                        }
                    }
                    break;
            }
        }
        fs.Close();
    }

    string getHash(string str, int a)
    {
        //0 = ID
        //1 = Variable Name
        //2 = Value
        try
        {
            if (a == 1)
                return str.Split('.')[a].Split(']')[1];

            return str.Split('.')[a];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }
}
