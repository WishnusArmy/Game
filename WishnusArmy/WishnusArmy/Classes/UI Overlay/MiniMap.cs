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
using static ObjectLists;

class MiniMap : DrawOnTop
{
    int timer;

    Point overlayPosition;
    Point minimapSize;

    Point enemySize;
    Point towerSize;
    Point baseSize;

    Point tileSize;

    GridNode[,] grid;

    public MiniMap() : base()
    {
        // can be edited
        minimapSize = new Point((OVERLAY_SIZE.Y-20) * 2, OVERLAY_SIZE.Y - 28);
        overlayPosition = new Point(SCREEN_SIZE.X - minimapSize.X-26, SCREEN_SIZE.Y - minimapSize.Y- 24);
        
        // constant
        enemySize = new Point(minimapSize.X/50);
        towerSize = new Point(minimapSize.X/40);
        baseSize = new Point(minimapSize.X/15);
        
        tileSize = new Point(6);
    }

    public override void Update(object gameTime)
    {
        if (grid == null)
            grid = GameWorld.FindByType<GridPlane>()[0].grid;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int realMapSize = NODE_SIZE.X * LEVEL_SIZE.X;
        Point offset;

        // draw basic map
        //DrawRectangleFilled(new Rectangle(overlayPosition, minimapSize), spriteBatch, Color.Black, 0.3f);

        // draw solid tiles
        Point position = new Point(0);
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            if (grid == null || grid.Length == 0)
                break;
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (grid[x, y].solid)
                {
                    position = new Point((int)((double)minimapSize.X * (double)((double)x / (double)LEVEL_SIZE.X)), (int)((double)minimapSize.Y * (double)((double)y / (double)LEVEL_SIZE.Y)));
                    DrawRectangleFilled(new Rectangle(overlayPosition + position, tileSize), spriteBatch, Color.Black, 0.2f);
                }
            }
        }

        // draw camera frame
        Point framesize = new Point((int)((minimapSize.X * 0.35)/CameraScale.X), (int)((minimapSize.Y * 0.35)/CameraScale.X));
        DrawRectangle(new Rectangle(toMiniMapPosition(CameraPosition*-1) + overlayPosition - new Point(5, 3), framesize), spriteBatch, Color.Yellow, 2, 0.5f);

        foreach (Tower t in ObjectLists.Towers)
        {
            if (t.hover)
            {
                int range = TowerRange(t.type, t.stats);
                int miniMapRangeX = (int)((double)range * ((double)minimapSize.X / (double)realMapSize*2));
                int miniMapRangeY = (int)((double)range * ((double)minimapSize.Y / (double)(realMapSize/1.8)));
                Color c = new Color(30, 30, 60, 20);
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(t.Position) - new Point(miniMapRangeX / 2, miniMapRangeY / 2), new Point(miniMapRangeX, miniMapRangeY)), c);
            }
        }

        // draw enemies
        Vector2 pos;
        offset = new Point(enemySize.X/2, enemySize.Y/2);
        foreach (Enemy e in ObjectLists.Enemies)
        {
            pos = e.Position;
            if (pos.X < - NODE_SIZE.X/4 || pos.Y < -NODE_SIZE.X/4 || pos.X > realMapSize+NODE_SIZE.X/4 || pos.Y > (realMapSize/2)+ NODE_SIZE.Y/4)
                continue;
            if (e is Tank)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.Red);
            if (e is Infantry)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, new Point((int)(enemySize.X/1.7))), Color.Salmon);
            if (e is Airplane)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.Yellow);
            if (e is Helicopter)
                spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(e.Position) - offset, enemySize), Color.MonoGameOrange);
        }

        // draw towers
        offset = new Point(towerSize.X / 2, towerSize.Y / 2);
        foreach (Tower t in ObjectLists.Towers)
        {
            spriteBatch.Draw(TEX_DOT, new Rectangle(overlayPosition + toMiniMapPosition(t.Position) - offset, towerSize), Color.LightBlue);
        }

        // draw base
        offset = new Point(baseSize.X / 2, baseSize.Y / 2);
        spriteBatch.Draw(TEX_DOT, new Rectangle(new Point(minimapSize.X / 2, minimapSize.Y / 2) - offset + overlayPosition, baseSize), Color.Green);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonDown())
        {
            Vector2 mp = inputHelper.MousePosition;
            {
                if (mp.X > overlayPosition.X && mp.X < overlayPosition.X + minimapSize.X &&
                    mp.Y > overlayPosition.Y && mp.Y < overlayPosition.Y + minimapSize.Y)
                {
                    //Console.WriteLine(new Vector2((mp.X - overlayPosition.X) / minimapSize.X * NODE_SIZE.X * LEVEL_SIZE.X, (mp.Y - overlayPosition.Y) / minimapSize.Y * NODE_SIZE.Y * LEVEL_SIZE.Y));
                    GameWorld.FindByType<Camera>()[0].Position = -new Vector2((mp.X - overlayPosition.X) / minimapSize.X * NODE_SIZE.X * LEVEL_SIZE.X, (mp.Y - overlayPosition.Y) / minimapSize.Y * NODE_SIZE.Y * LEVEL_SIZE.Y/2) + GAME_WINDOW_SIZE.toVector()/2 / CameraScale;
                    //Make sure the camera doesn't move out of bounds
                    if (position.X > -NODE_SIZE.X / 2 - GridNode.origin.X / 2) { position.X = -NODE_SIZE.X / 2 - GridNode.origin.X / 2; }
                    if (position.Y > -NODE_SIZE.Y / 2 - GridNode.origin.Y) { position.Y = -NODE_SIZE.Y / 2 - GridNode.origin.Y; }

                    if (position.X < -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X / CameraScale.X) { position.X = -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X / CameraScale.X; }
                    if (position.Y < -NODE_SIZE.Y / 2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y / CameraScale.Y) { position.Y = -NODE_SIZE.Y / 2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y / CameraScale.Y; }
                }
            }
        }
        base.HandleInput(inputHelper);
    }

    private Point toMiniMapPosition(Vector2 p)
    {
        double pX = p.X / (NODE_SIZE.X * LEVEL_SIZE.X);
        double pY = p.Y / (NODE_SIZE.Y * LEVEL_SIZE.Y / 2);
        return new Point((int)(minimapSize.X * pX), (int) (minimapSize.Y * pY));
    }
}
