using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ToolBarSelectorItem
{
    public enum ItemType { Textures, Object }

    public ItemType type;
    public string name;
    public Texture2D image;
    public List<Texture2D> tex_list;
    public List<ToolBarObjectsItem> obj_list;

    public ToolBarSelectorItem(string name, List<Texture2D> tex_list)
    {
        this.name = name;
        this.tex_list = tex_list;
        type = ItemType.Textures;
    }

    public ToolBarSelectorItem(string name, List<ToolBarObjectsItem> obj_list)
    {
        this.name = name;
        this.obj_list = obj_list;
        type = ItemType.Object;
    }
}
