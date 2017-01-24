using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static DrawingHelper;
using static ContentImporter.Fonts;
using static ContentImporter.Sprites;
using static ContentImporter.Icons;
using static ContentImporter.Sounds;
using static Functions;
using static GameStats;

public class Overlay : DrawOnTopList
{
    public OverlayTowerItem selected;
    public OverlayTowerInfo TowerInfo;
    bool selectedPossible;
    Vector2 mousePos;
    int gridSize;
    int gridWidth;
    int gridHeight;
    Vector2 gridPos;
    GridPlane plane;
    GridNode node, previousNode;


    public Overlay() : base()
    {
        selected = null;
        selectedPossible = false;
        mousePos = Vector2.Zero;
        gridSize = 128;
        gridWidth = 4;
        gridHeight = 4;
        gridPos = new Vector2(450 + 2, SCREEN_SIZE.Y - OVERLAY_SIZE.Y+2);
        List<string> TowerNames = new List<string>(TOWER_INFO.Keys);
        for(int i=0; i<TowerNames.Count; ++i)
        {
            Add(new OverlayTowerItem(TowerNames[i], gridPos + new Vector2(gridSize*i, 16)));
        }
        Add(TowerInfo = new OverlayTowerInfo { Position = new Vector2(5, SCREEN_SIZE.Y - OVERLAY_SIZE.Y) });
        Add(new MiniMap());
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePos = inputHelper.MousePosition;

        plane = GameWorld.FindByType<Camera>()[0].currentPlane;
        node = plane.NodeAt(mousePos / Camera.scale, false);
        if (previousNode != node)
        {
            if (previousNode != null)
                previousNode.selected = false;
            previousNode = node;
        }

        if (node == null)
            return;
        else
            node.selected = true;

        selectedPossible = !node.solid && node.available && inputHelper.MouseInGameWindow;

        if (inputHelper.MouseLeftButtonPressed() &&
            inputHelper.MouseInGameWindow &&
            selected != null &&
            selectedPossible)
        {
            Type t = Type.GetType(selected.itemType); //Get the type of the object
            object temp = Activator.CreateInstance(t); //Create an instance of that object
            GameObject obj = temp as GameObject; //Cast it as a GameObject
            obj.Position = node.Position + new Vector2(NODE_SIZE.X / 2, 0); //Adjust the position to the middle of the GridNode
            if (selected.itemType.Equals("ResourceTower") && GameWorld.FindByType<ResourceTower>().Count > 2)
            {
                selected = null;
                return;
            } 
            plane.Add(obj); //Add it to the hierarchy
            obj.MyParticleControl.AddTowerBuildGlow(obj.Position); //Add particle effect
            EcResources -= selected.cost; //Subtract its cost from the resources
            PlaySound(SND_TOWERPLACE);

            if (!inputHelper.IsKeyDown(Keys.LeftShift) || selected.cost > EcResources) //allow shift-clicking multiple towers
            {
                selected = null; //Reset the selected object reference
            }
}

        //Cancel the current selection with X or right click
        if (inputHelper.KeyPressed(Keys.X) && selected != null || inputHelper.MouseRightButtonPressed() && selected != null)
            selected = null;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draw the background
        //DrawRectangleFilled(new Rectangle(new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, 0), new Point(OVERLAY_SIZE.X, SCREEN_SIZE.Y)), spriteBatch, Color.Black, 0.4f);
        spriteBatch.Draw(SPR_OVERLAY, new Rectangle(new Point(0, SCREEN_SIZE.Y - OVERLAY_SIZE.Y - 24), new Point(SCREEN_SIZE.X, OVERLAY_SIZE.Y +24)), Color.White);
        //DrawRectangleFilled(new Rectangle(new Point(20, SCREEN_SIZE.Y - OVERLAY_SIZE.Y), new Point(SCREEN_SIZE.X-45, OVERLAY_SIZE.Y-20)), spriteBatch, Color.DarkGray, 1f);
        if (selected != null)
        {
            if (node != null)
            {
                spriteBatch.Draw(SPR_CIRCLE, (node.GlobalPosition + new Vector2(NODE_SIZE.X / 2, 0)) * Camera.scale, null, null, new Vector2(SPR_CIRCLE.Width / 2, SPR_CIRCLE.Height / 2), 0f, Camera.scale * new Vector2(1f, 0.5f) * ((float)selected.range) / ((float)SPR_CIRCLE.Width / 2), new Color(0.2f, 0.2f, 0.2f, 0.05f)); // draw the range indicator
                spriteBatch.Draw(selected.sprite, (node.GlobalPosition + new Vector2(NODE_SIZE.X/2, 0))*Camera.scale, null,  null, new Vector2(selected.sprite.Width, selected.sprite.Height)/2, 0f, Camera.scale, new Color(255,255*selectedPossible.ToInt(), 255*selectedPossible.ToInt(), selectedPossible.ToInt() + 0.5f), SpriteEffects.None, 0); //Draw the selected object at the mouse
            }
        }

        //draw gamestats
        spriteBatch.Draw(ICON_WAVE, new Rectangle(new Point(5,9), new Point(40, 28)), Color.White);
        DrawText(spriteBatch, FNT_GAMESTATS, "Wave " +Wave.ToString(), new Vector2(55, 7), Color.White);

        spriteBatch.Draw(ICON_KILLS,new Rectangle(new Point(3, 52),new Point(40, 50)), Color.White);
        DrawText(spriteBatch, FNT_GAMESTATS, TotalEnemiesKilled.ToString(), new Vector2(55, 62), Color.White);

        spriteBatch.Draw(ICON_COINS, new Rectangle(new Point(3,113), new Point(40, 50)), Color.White);
        DrawText(spriteBatch, FNT_GAMESTATS, EcResources.ToString(), new Vector2(55, 114), Color.White);

        spriteBatch.Draw(ICON_LIFE, new Rectangle(new Point(3, 174), new Point(40, 40)), Color.White);
        DrawText(spriteBatch, FNT_GAMESTATS, BaseHealth.ToString() +"/"+ MaxBaseHealth.ToString(), new Vector2(55, 170), Color.White);

        //
        //DrawGrid(spriteBatch);

        //Draw the WaveTime
        if (!GameStats.InWave)
        {
            DrawText(spriteBatch, FNT_MENU, "Next wave in: " + (int)(GameStats.WaveTimer / 60), GAME_WINDOW_SIZE.toVector() / 2, Color.White, true);
            DrawText(spriteBatch, FNT_MENU, "Press enter to call wave early", GAME_WINDOW_SIZE.toVector() / 2 + new Vector2(0, 50), Color.White, true);
        }

        base.Draw(gameTime, spriteBatch);
    }

    void DrawGrid(SpriteBatch spriteBatch)
    {
        //Draw the grid at the top right
        for (int x = 0; x < gridWidth; ++x)
        {
            DrawLine(spriteBatch, gridPos + new Vector2(gridSize * x, 0), gridPos + new Vector2(gridSize * x, gridSize * gridHeight), Color.Black, 2, 0.4f);
        }

        for (int y = 0; y < gridHeight; ++y)
        {
            DrawLine(spriteBatch, gridPos + new Vector2(0, gridSize * y), gridPos + new Vector2(gridSize * gridWidth, gridSize * y), Color.Black, 2, 0.4f);
        }
    }

    public bool Busy
    {
        get
        {
            if (node == null)
                return true;
            if (TowerInfo.tower != null)
                return (selected != null && !node.beacon);
            return selected != null;
        }
    }
}