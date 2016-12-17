using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Use this class to add your object to one of the toolbar lists
public class ToolBarObjectsItem
{
    public string name;
    public Texture2D sprite;

    public ToolBarObjectsItem(string name, Texture2D sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}