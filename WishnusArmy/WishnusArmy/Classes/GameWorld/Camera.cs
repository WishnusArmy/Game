using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Textures;
using Microsoft.Xna.Framework.Graphics;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public enum Plane { Land, Air };
    public GridPlane currentPlane;
    public GridPlane Land, Air;
    List<GridPlane> planes;
    public Overlay overlay;
    public static Vector2 scale;

    public Camera() : base()
    {
        position = -LEVEL_CENTER + GAME_WINDOW_SIZE.toVector() / 2;
        scale = new Vector2(1f);
        planes = new List<GridPlane>();
        for (int i=0; i<2; ++i)
        { 
            GridPlane p = new GridPlane((Plane)i);
            p.active = false;
            Add(p);
            switch((Plane)i)
            {
                case Plane.Land:
                    Land = p;
                    planes.Add(Land);
                    //Add items to the land plane (p.Add)
                    p.Add(new Base { Position = LEVEL_CENTER });
                    //p.Add(new ParticleController());
                    break;

                case Plane.Air:
                    Air = p;
                    planes.Add(Air);
                    //Add items to the air plane (p.Add)
                    break;
            }
        }
        currentPlane = planes[(int)Plane.Land]; //Reference the current plane to one of the three
        Console.WriteLine("Current Plane: " + currentPlane.planeType.ToString());
        LevelGenerator levelGenerator = new LevelGenerator();
        List<int[,]> list = new List<int[,]>();
        list = levelGenerator.GenerateNewLevel();
        for(int x=0; x<LEVEL_SIZE.X; ++x)
        {
            for(int y=0; y<LEVEL_SIZE.Y; ++y)
            {
                Land.grid[x, y].texture = list[0][x,y];
                Air.grid[x, y].texture = 6; //Air
            }
        }
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        //Manually handle the updates because the planes are inactive.
        for(int i=0; i<2; ++i)
        {
            planes[i].Update(gameTime);
        }

        int r = RANDOM.Next(80);
        if (r == 0)
        {
            GridNode node = Land.grid[0, RANDOM.Next(LEVEL_SIZE.Y)];
            Land.Add(new Tank { startNode = node, Position = node.Position - new Vector2(100,0) });
        }
        if (r==1)
        {
            GridNode node = Air.grid[0, RANDOM.Next(LEVEL_SIZE.Y)];
            Air.Add(new Airplane { startNode = node, Position = node.Position - new Vector2(100,0) });
        }

        // tijdelijke oplossing om Camera positie op te vragen in andere classes (MiniMap)
        GameStats.CameraPosition = position;
        GameStats.CameraScale = scale;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //Only draw the active plane;
        SpriteBatch batchLevel = new SpriteBatch(DrawingHelper.Graphics);
        batchLevel.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(scale.X, scale.Y, 1f) * WishnusArmy.WishnusArmy.self.spriteScale) ;
        Land.Draw(gameTime, batchLevel);
        if (currentPlane == Air)
            Air.Draw(gameTime, batchLevel);
        batchLevel.End();
    }


    public override void HandleInput(InputHelper inputHelper)
    {
        //Change the plane
        if (inputHelper.KeyPressed(Keys.Up) || inputHelper.KeyPressed(Keys.Down))
        {
            if (currentPlane == Land)
                currentPlane = Air;
            else
                currentPlane = Land;
        }

        //zoom
        Vector2 oldScale = scale;
        if (inputHelper.IsKeyDown(Keys.Q))
        {
            if (scale.X < 1f)
                scale *= new Vector2(1.015f);
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            if ((LEVEL_SIZE.X * NODE_SIZE.X * scale.X > GAME_WINDOW_SIZE.X + 96))
                scale *= new Vector2(1 / 1.01f);
        }
        Vector2 dScale = scale - oldScale;
        position -= (position + (SCREEN_SIZE.toVector())) * dScale/oldScale;
        //Focus on base
        if (inputHelper.IsKeyDown(Keys.Space))
        {
            position += (-position - LEVEL_CENTER*scale + scale*GAME_WINDOW_SIZE.toVector()/2) / 22;
        }
        //Manually handle the input of the active plane
        currentPlane.HandleInput(inputHelper);

        //Camera Movement
        Vector2 mp = inputHelper.MousePosition;
        float spd = SLIDE_SPEED / scale.X;
        if (mp.X < SLIDE_BORDER)
            position.X += spd;
        if (mp.X > SCREEN_SIZE.X - SLIDE_BORDER)
            position.X -= spd;
        if (mp.Y < SLIDE_BORDER)
            position.Y += spd;
        if (mp.Y > SCREEN_SIZE.Y - SLIDE_BORDER)
            position.Y -= spd;

        //Make sure the camera doesn't move out of bounds
        if (position.X > -NODE_SIZE.X/2 - GridNode.origin.X/2) { position.X = -NODE_SIZE.X/2 - GridNode.origin.X/2; }
        if (position.Y > -NODE_SIZE.Y/2 - GridNode.origin.Y) { position.Y = -NODE_SIZE.Y/2 - GridNode.origin.Y; }

        if (position.X < -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X/scale.X) { position.X = -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X/scale.X;  }
        if (position.Y < -NODE_SIZE.Y/2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y/scale.Y) { position.Y = -NODE_SIZE.Y/2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y/scale.Y; }

        base.HandleInput(inputHelper);
    }
}