using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using static Constant;

public class GameObjectList : GameObject
{
    protected List<GameObject> children;

    public GameObjectList(int layer = 0, string id = "") : base(layer, id)
    {
        children = new List<GameObject>();
    }

    public List<GameObject> Children
    {
        get { return children; }
    }

    public void Add(GameObject obj)
    {
        obj.Parent = this;
        /*
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].Layer > obj.Layer)
            {
                children.Insert(i, obj);
                return;
            }
        }
        */
        children.Add(obj);

        if (!WishnusArmy.WishnusArmy.startSorting)
            return;
        children = children.OrderBy(o => o.Position.Y).ToList(); //Sort all the children
        int lastNode = 0;
        for (int i = 0; i < children.Count; ++i) //Iterate through all the children
        {
            if (children[i] is GridNode) //If the current child is a GridNode...
            {
                GameObject temp = children[i]; //Buffer the child
                children.RemoveAt(i);  //Remove the child from the sorted list
                children.Insert(lastNode++, temp); //Squeeze it in at the beginning to make sure the grid is always drawn on the bottom.
            }
        }

        for (int i = children.Count - 1; i >= 0; --i)
        {
            if (children[i] is DrawOnTop) //If the current child should be drawn on top
            {
                GameObject temp = children[i];
                children.RemoveAt(i);
                children.Insert(children.Count - 1, temp);
            }
        }
    }

    public void Remove(GameObject obj)
    {
        children.Remove(obj);
        obj.Parent = null;
    }

    public GameObject FindById(string id)
    {
        foreach (GameObject obj in children)
        {
            if (obj.Id == id)
            {
                return obj;
            }
            if (obj is GameObjectList)
            {
                GameObjectList objList = obj as GameObjectList;
                GameObject subObj = objList.FindById(id);
                if (subObj != null)
                {
                    return subObj;
                }
            }
        }
        return null;
    }

    public List<T> FindByType<T>() where T : GameObject
    {
        List<T> list = new List<T>();
        foreach (GameObject obj in children)
        {
            if (obj is T)
            {
                list.Add(obj as T);
            }
            if (obj is GameObjectList)
            {
                GameObjectList objList = obj as GameObjectList;
                List<T> subList = objList.FindByType<T>();
                list.AddRange(subList);
            }
        }
        return list;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            if (children[i].active)
            {
                children[i].HandleInput(inputHelper);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        for (int i = children.Count-1; i >= 0; --i)
        {
            if (children[i].active == true)
                children[i].Update(gameTime);
            if (children[i].Kill)
                children.RemoveAt(i);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
        {
            return;
        }

        List<GameObject>.Enumerator e = children.GetEnumerator();
        while (e.MoveNext())
        {
            if (e.Current.active)
            {
               e.Current.Draw(gameTime, spriteBatch);
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in children)
        {
            obj.Reset();
        }
    }
}
