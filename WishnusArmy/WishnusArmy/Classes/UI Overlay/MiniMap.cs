using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;

class MiniMap : DrawOnTop
{
    int timer;
    Point size;
    Point overlayPosition;
    List<Enemy> enemies;
    List<Tower> towers;

    public MiniMap() : base()
    {
        timer = 0;
        overlayPosition = new Point(500, 500);
        size = new Point(100,50);
        enemies = new List<Enemy>();
        towers = new List<Tower>();
    }

    public override void Update(GameTime gameTime)
    {
        timer++;
        if (timer < 20)
            return;
        timer = 0;
        enemies = MyPlane.FindByType<Enemy>();
        towers = MyPlane.FindByType<Tower>();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // draw basic map
        DrawRectangleFilled(new Rectangle(overlayPosition, size), spriteBatch, Color.Black, 0.8f);

        // draw enemies

        // draw towers

    }
}
