﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].Layer > obj.Layer)
            {
                children.Insert(i, obj);
                return;
            }
        }
        children.Add(obj);
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
                if (subList != null)
                {
                    list.AddRange(subList);
                }
            }
        }
        if (list.Count == 0)
            return null;
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
        foreach (GameObject obj in children)
        {
            if (obj.active == true)
            {
                obj.Update(gameTime);
            }
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
