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
    int forestRatio = 50, mountainRatio = 45, riverRatio = 0, cityRatio = 0;
    int[,] levelGrid = new int[LEVEL_SIZE, LEVEL_SIZE];
    bool[,] tempGrid = new bool[LEVEL_SIZE, LEVEL_SIZE];

    public LevelGenerator() : base()
    {
        GenerateNewLevel();
    }

    public void GenerateNewLevel()
    {
        ClearGrid();
        GenerateForests();
        GenerateMountains();
        //GenerateRivers();
        //GenerateVillages();
    }

    //Change all tiles in the level to fields (tile type 0)
    public void ClearGrid()
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                levelGrid[x, y] = 0;
            }
        }
    }

    //Populate the level with forests (tile type 1)
    public void GenerateForests()
    {
        PopulateNewGrid(forestRatio);

        int smoothingPasses = 5;
        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid(1);
        }

        AddTempGrid(1);
        ClearNewGrid();
    }

    //Populate the level with mountains (tile type 2)
    public void GenerateMountains()
    {
        PopulateNewGrid(mountainRatio);

        int smoothingPasses = 5;
        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid(2);
        }

        AddTempGrid(2);
        ClearNewGrid();
    }

    //Populate the level with rivers (tile type 3)
    public void GenerateRivers()
    {

    }

    //Populate the level with villages (tile type 4)
    public void GenerateVillages()
    {

    }

    //Generate initial spread of special tiles in tempGrid. newGrid is used to hold and edit the distribution of special tiles before they are added to the level
    public void PopulateNewGrid(int tileRatio)
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                if (RANDOM.Next(100) < tileRatio)
                {
                    tempGrid[x, y] = true;
                }
            }
        }
    }

    //Go over the newGrid tile by tile, and adjust each tile's value to match the surroundings
    public void SmoothenGrid(int tileType)
    {
        //Choose direction to smoothen in
        switch (RANDOM.Next(3))
        {
            case 0:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y, tileType);
                    }
                }
                break;

            case 1:
                for (int x = 0; x < LEVEL_SIZE; x++)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y, tileType);
                    }
                }
                break;

            case 2:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = 0; y < LEVEL_SIZE; y++)
                    {
                        ChangeTiles(x, y, tileType);
                    }
                }
                break;

            case 3:
            default:
                for (int x = LEVEL_SIZE - 1; x >= 0; x--)
                {
                    for (int y = LEVEL_SIZE - 1; y >= 0; y--)
                    {
                        ChangeTiles(x, y, tileType);
                    }
                }
                break;
        }
    }

    //If a tile neighbours many tiles of the same type, change it to that type
    public void ChangeTiles(int x, int y, int tileType)
    {
        int neighbouringTiles = GetSurroundings(x, y, tileType);

        //Determine smoothing behavior
        switch (tileType)
        {
            case 0:
                break;

            case 1:
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

            case 3:
                break;

            default:
                break;
        }
    }

    //Count how many tiles of the same type are neighbouring this tile
    public int GetSurroundings(int gridX, int gridY, int tileType)
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
            }
        }
        return count;
    }

    //Use the distribution in tempGrid to add special tiles to the levelGrid
    public void AddTempGrid(int tileType)
    {
        for (int x = 0; x < LEVEL_SIZE; x++)
        {
            for (int y = 0; y < LEVEL_SIZE; y++)
            {
                if (tempGrid[x, y] == true)
                {
                    levelGrid[x, y] = tileType;
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
                switch (levelGrid[x, y])
                {
                    case 0:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.LawnGreen);
                        break;

                    case 1:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.ForestGreen);
                        break;

                    case 2:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.SaddleBrown);
                        break;

                    case 3:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.Blue);
                        break;

                    default:
                        spriteBatch.Draw(TEX_EMPTY, new Vector2(NODE_SIZE * x, NODE_SIZE * y) + camPos, Color.White);
                        break;
                }
            }
        }
    }
}