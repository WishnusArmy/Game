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
    int[,] groundGrid = new int[LEVEL_SIZE.X, LEVEL_SIZE.Y], airGrid = new int[LEVEL_SIZE.X, LEVEL_SIZE.Y], undergroundGrid = new int[LEVEL_SIZE.X, LEVEL_SIZE.Y];
    bool[,] tempGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y], tempTempGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y];

    public LevelGenerator() : base()
    {
        GenerateNewLevel();
    }

    public void GenerateNewLevel()
    {
        ClearGrid();
        GenerateSpecialTiles(5, 45, 5);     //Populate the ground level with forests
        GenerateSpecialTiles(2, 45, 5);     //Populate the ground level with mountains
        GenerateSpecialTiles(4, 40, 5);    //Populate the ground level with rivers
    }

    //Change all tiles in the level to fields
    public void ClearGrid()
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                groundGrid[x, y] = 0;
            }
        }
    }

    //Populate the level with special tiles
    public void GenerateSpecialTiles(int tileType, int initialRatio, int smoothingPasses)
    {
        PopulateNewGrid(initialRatio);

        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid(tileType, initialRatio);
        }

        AddTempGrid(tileType);
        ClearTempGrid();
    }

    //Generate initial spread of special tiles in tempGrid. tempGrid is used to hold and edit the distribution of special tiles before they are added to the level
    public void PopulateNewGrid(int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (RANDOM.Next(100) < initialRatio)
                {
                    tempGrid[x, y] = true;
                }
            }
        }
    }

    //Go over the newGrid tile by tile, and adjust each tile's value to match the surroundings
    public void SmoothenGrid(int tileType, int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                int neighbouringTiles = GetSurroundings(x, y, tileType, initialRatio);

                //Determine smoothing behavior
                switch (tileType)
                {
                    case 0:
                        break;

                    case 5:
                        if (neighbouringTiles > 4)
                        {
                            tempGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 4)
                        {
                            tempGrid[x, y] = false;
                        }
                        break;

                    case 2:
                        if (neighbouringTiles > 4)
                        {
                            tempGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 4)
                        {
                            tempGrid[x, y] = false;
                        }
                        break;

                    case 4:
                        if (neighbouringTiles > 4)
                        {
                            tempGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 4)
                        {
                            tempGrid[x, y] = false;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }

    //Count how many tiles neighbouring this tile are set to true
    public int GetSurroundings(int gridX, int gridY, int tileType, int initialRatio)
    {
        int count = 0;

        //Count same tiles in a 3x3 grid centered on the current tile
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < LEVEL_SIZE.X && neighbourY >= 0 && neighbourY < LEVEL_SIZE.Y)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (tempGrid[neighbourX, neighbourY] == true)
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    //Count tiles outside of the grid as either true or false, based on chance and the tiletype's initial ratio
                    if (RANDOM.Next(100) < initialRatio)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //Use the distribution in tempGrid to add special tiles to the level grids
    public void AddTempGrid(int tileType)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (tempGrid[x, y] == true)
                {
                    groundGrid[x, y] = tileType;
                }
            }
        }
    }

    //Reset the tempGrid for further use
    public void ClearTempGrid()
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                tempGrid[x, y] = false;
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Space))
        {
            GenerateNewLevel();
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
        GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;

        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                plane.grid[x, y].texture = groundGrid[x, y];


                /* switch (groundGrid[x, y])
                {
                    case 0:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE.X * x, NODE_SIZE.X * y) + camPos, Color.LawnGreen);
                        break;

                    case 1:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE.X * x, NODE_SIZE.X * y) + camPos, Color.ForestGreen);
                        break;

                    case 2:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE.X * x, NODE_SIZE.X * y) + camPos, Color.Brown);
                        break;

                    case 3:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE.X * x, NODE_SIZE.X * y) + camPos, Color.CornflowerBlue);
                        break;

                    default:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE.X * x, NODE_SIZE.X * y) + camPos, Color.White);
                        break;
                } */
            }
        }
    }
}