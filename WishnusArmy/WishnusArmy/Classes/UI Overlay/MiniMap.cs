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

    public MiniMap() : base()
    {
        timer = 0;
        // can be edited
        overlayPosition = new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X * 2, SCREEN_SIZE.Y - OVERLAY_SIZE.Y);
        minimapSize = new Point(OVERLAY_SIZE.X * 2, OVERLAY_SIZE.X);
        
        // constant
        enemySize = new Point(minimapSize.X/50);
        towerSize = new Point(minimapSize.X/30);
        baseSize = new Point(minimapSize.X/15);
        enemies = new List<Enemy>();
        towers = new List<Tower>();
    }

    public override void Update(object gameTime)
    {
        timer++;
        if (timer < 20)
            return;
        timer = 0;
        enemies = GameStats.enemies;
        towers = GameStats.towers;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int realMapSize = NODE_SIZE.X * LEVEL_SIZE.X;
        Point offset;

        // draw basic map
        DrawRectangleFilled(new Rectangle(overlayPosition, minimapSize), spriteBatch, Color.Black, 0.5f);

        // draw camera frame
        Point framesize = new Point((int)((minimapSize.X * 0.35)/CameraScale.X), (int)((minimapSize.Y * 0.35)/CameraScale.X));
        DrawRectangle(new Rectangle(toMiniMapPosition(GameStats.CameraPosition *-1) + overlayPosition, framesize), spriteBatch, Color.Yellow, 2, 0.5f);
        

        // draw enemies
        Vector2 pos;
        offset = new Point(enemySize.X/2, enemySize.Y/2);
        foreach (Enemy e in enemies)
        {
            pos = e.Position;
            if (pos.X < - NODE_SIZE.X/4 || pos.Y < -NODE_SIZE.X/4 || pos.X > realMapSize+NODE_SIZE.X/4 || pos.Y > (realMapSize/2)+ NODE_SIZE.Y/4)
                continue;
            spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.Red);
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
