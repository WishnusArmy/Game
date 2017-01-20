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
    int[,] landGrid = new int[LEVEL_SIZE.X, LEVEL_SIZE.Y], airGrid = new int[LEVEL_SIZE.X, LEVEL_SIZE.Y];
    bool[,] distributionGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y], tempGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y];

    public LevelGenerator() : base()
    {
        GenerateNewLevel();
    }

    //Generate the grids that make up the playing fields
    public List<int[,]> GenerateNewLevel()
    {
        ClearGrid();
        GenerateSpecialTiles(5, 45, 5);     //Populate the land level with forests
        GenerateSpecialTiles(2, 45, 5);     //Populate the land level with mountains
        GenerateSpecialTiles(4, 40, 5);     //Populate the land level with rivers

        List<int[,]> planes = new List<int[,]>();
        planes.Add(landGrid);
        return planes;
    }

    //Change all tiles in the level to the base tile
    public void ClearGrid()
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                landGrid[x, y] = 0;
                airGrid[x, y] = 0;
            }
        }
    }

    //Populate the level with special tiles
    public void GenerateSpecialTiles(int tileType, int initialRatio, int smoothingPasses)
    {
        PopulateDistributionGrid(tileType, initialRatio);

        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid(initialRatio);
        }
        AddDistributionGrid(tileType);
        ClearDistributionGrid();
    }

    //Generate initial spread of special tiles in distributionGrid. distributionGrid is used to hold and edit the distribution of special tiles before they are added to the level
    public void PopulateDistributionGrid(int tileType, int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (RANDOM.Next(100) < initialRatio)
                {
                    //Keep a square in the center of the grid clear of special tiles in the initial distribution
                    if ((x < LEVEL_CENTER.X - 5) || (x > LEVEL_CENTER.X + 5) || (y < LEVEL_CENTER.Y / 2 - 10) || (y > LEVEL_CENTER.Y / 2 + 10))
                    {
                        //Keep a horizontal and vertical line through the center clear of special tiles in the initial distribution
                        if ((x != LEVEL_CENTER.X) && (y != LEVEL_CENTER.Y))
                        {
                            distributionGrid[x, y] = true;
                        }
                    }
                }
            }
        }
    }

    //Go over the distributionGrid tile by tile, and adjust each tile's value to match the surroundings
    public void SmoothenGrid(int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                int neighbouringTiles = GetSurroundings(x, y, initialRatio);
                if (neighbouringTiles >= 5)
                {
                    tempGrid[x, y] = true;
                }
                else if (neighbouringTiles < 5)
                {
                    tempGrid[x, y] = false;
                }
            }
        }
        distributionGrid = tempGrid;
    }

    //Count how many tiles neighbouring this tile are set to 'true'
    public int GetSurroundings(int gridX, int gridY, int initialRatio)
    {
        int count = 0;

        //Count tiles in a 3x3 grid centered on the current tile
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                //Check if the tile does not border the edge of the level
                if (neighbourX >= 0 && neighbourX < LEVEL_SIZE.X && neighbourY >= 0 && neighbourY < LEVEL_SIZE.Y)
                {
                    //Only count tiles in the middle column...
                    if ((neighbourX == gridX) && (distributionGrid[neighbourX, neighbourY] == true))
                    {
                        count++;
                    }

                    //...and the top and bottom row (thus counting some again) to promote more vertical formations
                    if (((neighbourY == gridY - 1) || (neighbourY == gridY + 1)) && (distributionGrid[neighbourX, neighbourY] == true))
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //Use the distribution in distributionGrid to add special tiles to the level grid
    public void AddDistributionGrid(int tileType)
    {
        //Keep the borders and a line through the center clear of special tiles
        for (int x = 2; x < LEVEL_SIZE.X - 2; x++)
        {
            for (int y = 4; y < LEVEL_SIZE.Y - 4; y++)
            {
                if ((distributionGrid[x, y] == true) && (x != LEVEL_CENTER.X) && (y != LEVEL_CENTER.Y))
                {
                    landGrid[x, y] = tileType;
                }
            }
        }
    }

    //Reset the distributionGrid for further use
    public void ClearDistributionGrid()
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                distributionGrid[x, y] = false;
            }
        }
    }

    /*
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.IsKeyDown(Keys.Space))
        {
            ClearGrid();
            GenerateNewLevel();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 camPos = GameWorld.FindByType<Camera>()[0].Position;
        GridPlane plane = GameWorld.FindByType<Camera>()[0].Land;

        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                //plane.grid[x, y].texture = landGrid[x, y];  //Draw as isometric grids

                switch (landGrid[x, y])   //Draw as seperate topdown grids (for testing)
                {
                    case 0:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 725, 12 * y + 250) + camPos, Color.LawnGreen);
                        break;

                    case 5:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 725, 12 * y + 250) + camPos, Color.ForestGreen);
                        break;

                    case 2:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 725, 12 * y + 250) + camPos, Color.Brown);
                        break;

                    case 4:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 725, 12 * y + 250) + camPos, Color.CornflowerBlue);
                        break;

                    default:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 725, 12 * y + 250) + camPos, Color.White);
                        break;
                }

                switch (airGrid[x, y])   //Draw as seperate topdown grids (for testing)
                {
                    case 0:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 100, 12 * y + 250) + camPos, Color.CornflowerBlue);
                        break;

                    case 1:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 100, 12 * y + 250) + camPos, Color.Brown);
                        break;

                    default:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 100, 12 * y + 250) + camPos, Color.White);
                        break;
                }
            }
        }
    }*/
}