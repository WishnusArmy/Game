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
using WishnusArmy.Classes.Enemies.Air;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public enum Plane { Land };
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
        for (int i=0; i<1; ++i)
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
                int tex = list[0][x, y];
                if (tex == 2) //Mountain
                {
                    tex = Functions.choose(new List<int> { 2, 7, 8 });
                }
                if (tex == 5)
                {
                    tex = Functions.choose(new List<int> { 5, 9 });
                }
                Land.grid[x, y].texture = tex;
            }
        }
        currentPlane.Add(new EnemySpawner(currentPlane)); // The grid must be finished

        for (int i = 1; i <= 5; i+=2)
            currentPlane.Add(new Clouds(new Vector2(-1500/i, SCREEN_SIZE.Y + 1800/i)));
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        //Manually handle the updates because the planes are inactive.
        for(int i=0; i<1; ++i)
        {
            planes[i].Update(gameTime);
        }

        if (GameStats.BaseHealth <= 0)
        {
            GameEnvironment.GameStateManager.AddGameState("GameOverState", new GameOverState());
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //Only draw the active plane;
        SpriteBatch batchLevel = new SpriteBatch(DrawingHelper.Graphics);
        batchLevel.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(scale.X, scale.Y, 1f) * WishnusArmy.WishnusArmy.self.spriteScale) ;
        Land.Draw(gameTime, batchLevel);
        batchLevel.End();
    }


    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.CurrentPressedKeys().ToList().Contains(Keys.D) && inputHelper.CurrentPressedKeys().ToList().Contains(Keys.LeftShift))
        {
            GameEnvironment.GameStateManager.AddGameState("GameOverState", new GameOverState());
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        }
        if (inputHelper.CurrentPressedKeys().ToList().Contains(Keys.W) && inputHelper.CurrentPressedKeys().ToList().Contains(Keys.LeftShift))
        {
            GameStats.Wave++;
        }
        //zoom
        Vector2 oldScale = scale;
        if (inputHelper.ScrollUp())
        {
            if (scale.X < 1f)
                scale *= new Vector2(1.07f);
        }
        if (inputHelper.ScrollDown())
        {
            if ((LEVEL_SIZE.X * NODE_SIZE.X * scale.X > GAME_WINDOW_SIZE.X + 96))
                scale *= new Vector2(1 / 1.07f);
        }
        if (inputHelper.IsKeyDown(Keys.Q))
        {
            if (scale.X < 1f)
                scale *= new Vector2(1.02f);
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            if ((LEVEL_SIZE.X * NODE_SIZE.X * scale.X > GAME_WINDOW_SIZE.X + 96))
                scale *= new Vector2(1 / 1.02f);
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

    public Vector2 Scale
    {
        get
        {
            return scale;
        }
    }
}