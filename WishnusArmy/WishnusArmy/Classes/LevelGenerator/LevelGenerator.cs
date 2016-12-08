using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Textures;

public class LevelGenerator : GameObject
{
    int[,] grid;
    public LevelGenerator() : base()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        //Determine occurance of each tile type and how many times to smoothen.
        int mountainRatio = 45;
        int smoothingPasses = 5;

        //Initialize grid with random tiles
        grid = new int[LEVEL_SIZE, LEVEL_SIZE];

        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                if (RANDOM.Next(100) >= mountainRatio)
                {
                    grid[x, y] = 0;
                }
                else
                {
                    grid[x, y] = 1;
                }
            }
        }

        //"Smoothen" the grid to create believable clusters of ti
        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid();
        }
    }

    //Go over the grid tile by tile, and adjust them to match their neighbours
    public void SmoothenGrid()
    {
        int[,] smoothGrid = new int[LEVEL_SIZE, LEVEL_SIZE];

        //Choose direction to smoothen in
        switch (RANDOM.Next(3))
        {
            case 0:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y);
                    }
                }
                break;

            case 1:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y);
                    }
                }
                break;

            case 2:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y);
                    }
                }
                break;

            case 3:
            default:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y);
                    }
                }
                break;
        }
    }

    //If a tile neighbours many tiles of the same type, change it to that type
    public void ChangeTiles(int x, int y)
    {
        int neighbouringMountains = GetSurroundings(x, y, 1);
        if (neighbouringMountains > 4)
        {
            grid[x, y] = 1;
        }
        else if (neighbouringMountains < 4)
        {
            grid[x, y] = 0;
        }
    }

    //Count how many tiles of a certain type are neighbouring this tile
    public int GetSurroundings(int gridX, int gridY, int tileType)
    {
        int count = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < LEVEL_SIZE && neighbourY >= 0 && neighbourY < LEVEL_SIZE)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (grid[neighbourX, neighbourY] == tileType)
                        {
                            count++;
                        }
                    }
                }
            }
        }
        return count;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Space))
        {
            GenerateLevel();
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Update(gameTime);
        Vector2 camPos = GameWorld.FindByType<Camera>()[0].Position;

        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                switch (grid[x, y])
                {
                    case 0:
                        spriteBatch.Draw(TEX_GRASS, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.White);
                        break;

                    case 1:
                        spriteBatch.Draw(TEX_GRASS, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.SaddleBrown);
                        break;

                    default:
                        spriteBatch.Draw(TEX_GRASS, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.White);
                        break;
                }
            }
        }
    }
}