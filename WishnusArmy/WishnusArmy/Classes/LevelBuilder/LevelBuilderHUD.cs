using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class LevelBuilderHud : GameObjectList
{
    public LevelBuilderHud() : base()
    {
        Add(new ToolSelectionBar());
    }
}
