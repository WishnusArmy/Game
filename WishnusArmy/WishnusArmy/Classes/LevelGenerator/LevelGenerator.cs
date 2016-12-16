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
    int[,] groundGrid = new int[LEVEL_SIZE, LEVEL_SIZE], airGrid = new int[LEVEL_SIZE, LEVEL_SIZE], undergroundGrid = new int[LEVEL_SIZE, LEVEL_SIZE];
    bool[,] tempGrid = new bool[LEVEL_SIZE, LEVEL_SIZE];

    public LevelGenerator() : base()
    {
        GenerateNewLevel();
    }

    public void GenerateNewLevel()
    {
        ClearGrid();
        GenerateSpecialTiles(1, 50, 10);     //Populate the ground level with forests
        GenerateSpecialTiles(2, 45, 5);     //Populate the ground level with mountains
        GenerateSpecialTiles(3, 5, 1);      //Populate the ground level with rivers
    }

    //Change all tiles in the level to fields
    public void ClearGrid()
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
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
        ClearNewGrid();
    }

    //Generate initial spread of special tiles in tempGrid. tempGrid is used to hold and edit the distribution of special tiles before they are added to the level
    public void PopulateNewGrid(int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
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
        //Choose direction to smoothen in
        switch (RANDOM.Next(3))
        {
            case 0:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y, tileType, initialRatio);
                    }
                }
                break;

            case 1:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y, tileType, initialRatio);
                    }
                }
                break;

            case 2:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y, tileType, initialRatio);
                    }
                }
                break;

            case 3:
            default:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y, tileType, initialRatio);
                    }
                }
                break;
        }
    }

    //If a tile neighbours many tiles of the same type, change it to that type
    public void ChangeTiles(int x, int y, int tileType, int initialRatio)
    {
        int neighbouringTiles = GetSurroundings(x, y, tileType, initialRatio);

        //Determine smoothing behavior
        switch (tileType)
        {
            case 0:
                break;

            case 1:
                if (neighbouringTiles > 5)
                {
                    tempGrid[x, y] = true;
                }
                else if (neighbouringTiles < 3)
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

            case 3:
                if (neighbouringTiles < 2 || neighbouringTiles > 2)
                {
                    tempGrid[x, y] = false;
                }
                else
                {
                    tempGrid[x, y] = true;
                }
                    break;

            default:
                break;
        }
    }

    //Count how many tiles neighbouring this tile are set to true
    public int GetSurroundings(int gridX, int gridY, int tileType, int initialRatio)
    {
        int count = 0;

        //count same tiles in a 3x3 grid centered on the current tile
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < LEVEL_SIZE && neighbourY >= 0 && neighbourY < LEVEL_SIZE)
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
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                if (tempGrid[x, y] == true)
                {
                    groundGrid[x, y] = tileType;
                }
            }
        }
    }

    //Reset the tempGrid for further use
    public void ClearNewGrid()
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
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

        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                switch (groundGrid[x, y])
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
                }
            }
        }
    }
}