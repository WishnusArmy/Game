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
        ObjectLists.Initialize();
        GameStats.Initialize();
        Overlay overlay;
        Add(overlay = new Overlay());
        Camera camera = new Camera() { overlay = overlay };
        Add(camera);
        ObjectLists.Camera = camera;
        Add(new PathfindingControl());
        Add(new SortingThread());
    }
}