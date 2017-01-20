using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Level : GameObjectList
{ 
    public Level() : base()
    {
        GameStats.Initialize();
        Overlay overlay;
        Add(overlay = new Overlay());
        Add(new Camera() { overlay = overlay });
        Add(new PathfindingControl());
        Add(new SortingThread());
    }
}