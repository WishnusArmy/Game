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
    bool[,] distributionGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y], tempGrid = new bool[LEVEL_SIZE.X, LEVEL_SIZE.Y];

    public LevelGenerator() : base()
    {
        //GenerateNewLevel();
    }

    public List<int[,]> GenerateNewLevel()
    {
        ClearGrid();
        GenerateSpecialTiles(0, 5, 45, 5);     //Populate the ground level with forests
        GenerateSpecialTiles(0, 2, 45, 5);     //Populate the ground level with mountains
        GenerateSpecialTiles(0, 4, 40, 5);     //Populate the ground level with rivers
        //GenerateSpecialTiles(2, 1, 60, 5);     //Populate the underground level with earth
        //GenerateSpecialTiles(2, 2, 40, 5);     //Populate the underground level with granite
        //GenerateSpecialTiles(2, 3, 35, 5);     //Populate the underground level with gold
        List<int[,]> planes = new List<int[,]>();
        planes.Add(groundGrid);
        planes.Add(airGrid);
        return planes;
    }

    //Change all tiles in the level to the base tile
    public void ClearGrid()
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                groundGrid[x, y] = 0;
                airGrid[x, y] = 0;
                undergroundGrid[x, y] = 0;
            }
        }
    }

    //Populate the level with special tiles
    public void GenerateSpecialTiles(int level, int tileType, int initialRatio, int smoothingPasses)
    {
        PopulateDistributionGrid(initialRatio);

        for (int i = 0; i < smoothingPasses; i++)
        {
            SmoothenGrid(tileType, initialRatio);
        }
        AddDistributionGrid(level, tileType);
        ClearDistributionGrid();
    }

    //Generate initial spread of special tiles in distributionGrid. distributionGrid is used to hold and edit the distribution of special tiles before they are added to the level
    public void PopulateDistributionGrid(int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (RANDOM.Next(100) < initialRatio)
                {
                    //Keep a rectangle in the center of the grid clear of special tiles
                    if ((x < LEVEL_SIZE.X / 2 - 5) || (x > LEVEL_SIZE.X / 2 + 5) || (y < LEVEL_SIZE.Y / 2 - 2) || (y > LEVEL_SIZE.Y / 2 + 2))
                    {
                        distributionGrid[x, y] = true;
                    }
                }
            }
        }
    }

    //Go over the distributionGrid tile by tile, and adjust each tile's value to match the surroundings
    public void SmoothenGrid(int tileType, int initialRatio)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                int neighbouringTiles = GetSurroundings(x, y, tileType, initialRatio);

                //Determine smoothing behavior for the current tiletype
                switch (tileType)
                {
                    case 0:
                        break;

                    case 5:
                        if (neighbouringTiles >= 5)
                        {
                            tempGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 5)
                        {
                            tempGrid[x, y] = false;
                        }
                        break;

                    case 2:
                        if (neighbouringTiles >= 5)
                        {
                            distributionGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 5)
                        {
                            distributionGrid[x, y] = false;
                        }
                        break;

                    case 4:
                        if (neighbouringTiles >= 5)
                        {
                            distributionGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 5)
                        {
                            distributionGrid[x, y] = false;
                        }
                        break;

                    default:
                        if (neighbouringTiles >= 5)
                        {
                            distributionGrid[x, y] = true;
                        }
                        else if (neighbouringTiles < 5)
                        {
                            distributionGrid[x, y] = false;
                        }
                        break;
                }
            }
        }

        distributionGrid = tempGrid;
    }

    //Count how many tiles neighbouring this tile are set to 'true'
    public int GetSurroundings(int gridX, int gridY, int tileType, int initialRatio)
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
                    if (distributionGrid[neighbourX, neighbourY] == true)
                    {
                        count++;
                    }
                }
                else
                {
                    //Count tiles outside of the grid as either true or false, based on the tiletype's initial ratio
                    if (RANDOM.Next(100) < initialRatio)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //Use the distribution in distributionGrid to add special tiles to the level grid
    public void AddDistributionGrid(int level, int tileType)
    {
        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                if (distributionGrid[x, y] == true)
                {
                    if (level == 0)
                    {
                        groundGrid[x, y] = tileType;
                        if (tileType == 2)
                        {
                            airGrid[x, y] = 1;
                        }
                    }
                    else if (level == 1)
                    {
                        airGrid[x, y] = tileType;
                    }
                    else if (level == 2)
                    {
                        undergroundGrid[x, y] = tileType;
                    }
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

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.IsKeyDown(Keys.Space))
        {
            ClearGrid();
            ClearDistributionGrid();
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
        GridPlane plane = GameWorld.FindByType<Camera>()[0].Land; 

        for (int x = 0; x < LEVEL_SIZE.X; x++)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; y++)
            {
                plane.grid[x, y].texture = groundGrid[x, y];  //Draw as isometric grids

                /* switch (groundGrid[x, y])   //Draw as seperate topdown grids (for testing)
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

                switch (undergroundGrid[x, y])   //Draw as seperate topdown grids (for testing)
                {
                    case 0:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 1350, 12 * y + 250) + camPos, Color.RosyBrown);
                        break;

                    case 1:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 1350, 12 * y + 250) + camPos, Color.Brown);
                        break;

                    case 2:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 1350, 12 * y + 250) + camPos, Color.Gray);
                        break;

                    case 3:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 1350, 12 * y + 250) + camPos, Color.Goldenrod);
                        break;

                    default:
                        spriteBatch.Draw(TEX_EMPTY_SMALL, new Vector2(12 * x + 1350, 12 * y + 250) + camPos, Color.White);
                        break;
                } */
            }
        }
    }
}