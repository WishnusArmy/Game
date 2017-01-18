using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static Constant;
using static GameStats;
using static ContentImporter.Textures;

class MiniMap : DrawOnTop
{
    int timer;

    Point overlayPosition;
    Point minimapSize;

    Point enemySize;
    Point towerSize;
    Point baseSize;
    
    List<Enemy> enemies;
    List<Tower> towers;

    Point cameraPosition;
    Vector2 scale;

    public MiniMap() : base()
    {
        timer = 50;
        // can be edited
        overlayPosition = new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X * 2, SCREEN_SIZE.Y - OVERLAY_SIZE.Y);
        minimapSize = new Point(OVERLAY_SIZE.X * 2, OVERLAY_SIZE.X);
        
        // constant
        enemySize = new Point(minimapSize.X/50);
        towerSize = new Point(minimapSize.X/40);
        baseSize = new Point(minimapSize.X/15);
        enemies = new List<Enemy>();
        towers = new List<Tower>();

        scale = new Vector2(1000,1000);
        cameraPosition = new Point(0, 0);
    }

    public override void Update(object gameTime)
    {
        Camera c = GameWorld.FindByType<Camera>()[0];
        cameraPosition = (c.Position * -1).toPoint();
        scale = c.Scale;

        timer++;
        if (timer < 30)
            return;
        timer = 0;

        enemies = GameWorld.FindByType<Enemy>();
        towers = GameWorld.FindByType<Tower>();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int realMapSize = NODE_SIZE.X * LEVEL_SIZE.X;
        Point offset;

        // draw basic map
        DrawRectangleFilled(new Rectangle(overlayPosition, minimapSize), spriteBatch, Color.Black, 0.5f);

        // draw camera frame
        Point framesize = new Point((int)((minimapSize.X * 0.35)/scale.X), (int)((minimapSize.Y * 0.35)/scale.X));
        DrawRectangle(new Rectangle(toMiniMapPosition(cameraPosition.toVector()) + overlayPosition, framesize), spriteBatch, Color.Yellow, 2, 0.5f);

        foreach (Tower t in towers)
        {
            if (t.hover)
            {
                int rangesize = (int)(TowerRange(t.type, t.stats));
                Point rangeSize = new Point((int)((double)rangesize * ((double)rangesize / (double)realMapSize)));
                Color c = new Color(30, 30, 60, 20);
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(t.Position) - new Point(rangeSize.X / 2, rangeSize.X / 2), rangeSize), c);
            }
        }

        // draw enemies
        Vector2 pos;
        offset = new Point(enemySize.X/2, enemySize.Y/2);
        foreach (Enemy e in enemies)
        {
            pos = e.Position;
            if (pos.X < - NODE_SIZE.X/4 || pos.Y < -NODE_SIZE.X/4 || pos.X > realMapSize+NODE_SIZE.X/4 || pos.Y > (realMapSize/2)+ NODE_SIZE.Y/4)
                continue;
            if(e is Tank)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.Red);
            if (e is Airplane)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.Yellow);
        }

        // draw towers
        offset = new Point(towerSize.X / 2, towerSize.Y / 2);
        foreach (Tower t in towers)
        {
            spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(t.Position) - offset, towerSize), Color.LightBlue);
        }

        // draw base
        offset = new Point(baseSize.X / 2, baseSize.Y / 2);
        spriteBatch.Draw(TEX_DOT, new Rectangle(new Point(minimapSize.X / 2, minimapSize.Y / 2) - offset + overlayPosition, baseSize), Color.Green);

    }

    private Point toMiniMapPosition(Vector2 p)
    {
        double pX = p.X / (NODE_SIZE.X * LEVEL_SIZE.X);
        double pY = p.Y / (NODE_SIZE.Y * LEVEL_SIZE.Y / 2);
        return new Point((int)(minimapSize.X * pX), (int) (minimapSize.Y * pY));
    }
}
