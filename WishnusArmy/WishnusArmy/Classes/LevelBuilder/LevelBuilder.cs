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
        int i = 0; //Iterator for the object id
        Hashtable file = new Hashtable(); //Make a hashtable
        List<GameObject> allGameObjects = GameWorld.FindByType<GameObject>();
        foreach (GameObject obj in allGameObjects)
        {
            file.Add(i.ToString() + ".[0]Type", obj.GetType());  //Add the type to the hashtable
            file.Add(i.ToString() + ".[1]X", obj.Position.X.ToString()); //Add the x
            file.Add(i.ToString() + ".[2]Y", obj.Position.Y.ToString()); //Add the y
            if (obj is GridNode) //If it is a GridNode
            {
                GridNode subObj = obj as GridNode; //Get the object as a GridNode
                string[] str = subObj.texture.ToString().Split('/'); //Get the name of the texture
                file.Add(i.ToString() + ".[3]Texture", str[str.Length - 1]); //Add the texture to the hashtable
                file.Add(i.ToString() + ".[4]Plane", ((int)subObj.plane).ToString()); //Add the id number of the plane
            }
            ++i; //Increment the object counter
        }
        FileStream fs = new FileStream("level.dat", FileMode.Create); //Create a filestream
        BinaryFormatter formatter = new BinaryFormatter(); //Create a binarry formatter
        formatter.Serialize(fs, file); //Use that to serialize that shit into a save file!
        fs.Close(); //Close the filestream
    }

    public void LoadLevel()
    {
        Hashtable file = null; //Get an empty hashtable
        FileStream fs = new FileStream("level.dat", FileMode.Open); //Open a filestream
        try
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Make a new binary formatter
            file = (Hashtable)formatter.Deserialize(fs); //Deserialize the hash table
        }
        catch(Exception e)
        {
            Console.WriteLine("Deserialization failed: " + e.Message);
        }

        List<string> list = new List<string>(); //Get a new list of strings
        foreach(DictionaryEntry obj in file)
        {
            list.Add(obj.Key.ToString() + "." + obj.Value.ToString()); //Put the deserialized file in the list
        }

        list.Sort(); //Sort the list (very important!)
        int z = 0; //Set a counter for the list line
        List<GridNode> nodes = GameWorld.FindByType<GridNode>(); //Get a list of all the gridnodes
        while(z < list.Count-1)
        {
            /*0*/string typeString = getHash(list[z++], 2); //Get the type string
            /*1*/float x = float.Parse(getHash(list[z++], 2)); //Get the x
            /*2*/float y = float.Parse(getHash(list[z++], 2)); //Get the y

            switch (typeString)
            {
                case "GridNode": //If the type was a GridNode
                    /*3*/int tex = int.Parse(getHash(list[z++], 2)); //Add the texture
                    /*4*/Camera.Plane plane = (Camera.Plane)int.Parse(getHash(list[z++], 2));
                    for(int i=0; i<nodes.Count; ++i) //Find the corresponding node in the grid
                    {
                        if (nodes[i].Position == new Vector2(x, y) && nodes[i].plane == plane) //If it matches the position AND plane
                        {
                            nodes[i].texture = tex; //Set the texture from loadfile
                        }
                    }
                    break;
            }
        }
        fs.Close(); //Close the filestream
    }

    string getHash(string str, int a)
    {
        //0 = ID
        //1 = Variable Name
        //2 = Value
        try
        {
            if (a == 1)
                return str.Split('.')[a].Split(']')[1]; //Remove the [n] indicator

            return str.Split('.')[a];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }
}
