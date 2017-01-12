using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static DrawingHelper;
using static ContentImporter.Fonts;
using static Economy;
using static Functions;
using static GameStats;

public class Overlay : GameObjectList
{
    public OverlayTowerItem selected;
    public OverlayTowerInfo TowerInfo;
    bool selectedPossible;
    Vector2 mousePos;

    int gridSize;
    int gridWidth;
    int gridHeight;
    Vector2 gridPos;


    public Overlay() : base()
    {
        selected = null;
        selectedPossible = false;
        mousePos = Vector2.Zero;
        gridSize = 64;
        gridWidth = OVERLAY_SIZE.X / gridSize;
        gridHeight = 4;
        gridPos = new Vector2(SCREEN_SIZE.X - OVERLAY_SIZE.X + 2, 40);
        List<string> TowerNames = new List<string>(TOWER_INFO.Keys);
        for(int i=0; i<TowerNames.Count; ++i)
        {
            Add(new OverlayTowerItem(TowerNames[i], gridPos + new Vector2(gridSize * (i%gridWidth), gridSize * (i/gridWidth))));
        }
        Add(TowerInfo = new OverlayTowerInfo { Position = new Vector2(5, SCREEN_SIZE.Y - OVERLAY_SIZE.Y) });
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePos = inputHelper.MousePosition;

        GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
        GridNode node;
        node = plane.NodeAt(inputHelper.MousePosition, false);
        if (node == null)
            return;

        selectedPossible = !node.solid && inputHelper.MouseInGameWindow;

        if (inputHelper.MouseLeftButtonPressed() &&
            inputHelper.MouseInGameWindow &&
            selected != null &&
            selectedPossible)
        {
            Type t = Type.GetType(selected.itemType); //Get the type of the object
            object temp = Activator.CreateInstance(t); //Create an instance of that object
            GameObject obj = temp as GameObject; //Cast it as a GameObject
            obj.Position = node.Position + new Vector2(NODE_SIZE.X / 2, 0); //Adjust the position to the middle of the GridNode
            plane.Add(obj); //Add it to the hierarchy
            EcResources -= selected.cost; //Subract its cost from the resources
            selected = null; //Reset the selected object reference
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draw the background
        DrawRectangleFilled(new Rectangle(new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, 0), new Point(OVERLAY_SIZE.X, SCREEN_SIZE.Y)), spriteBatch, Color.Black, 0.4f);
        DrawRectangleFilled(new Rectangle(new Point(0, SCREEN_SIZE.Y - OVERLAY_SIZE.Y), new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, OVERLAY_SIZE.Y)), spriteBatch, Color.Black, 0.4f);
        if (selected != null)
        {
            GridNode node = null;
            GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
            node = plane.NodeAt(mousePos, false);

            if (node != null)
            {
                spriteBatch.Draw(selected.icon, node.GlobalPosition + new Vector2(NODE_SIZE.X / 2, 0) - new Vector2(selected.icon.Width, selected.icon.Height) / 2, Color.White * (selectedPossible.ToInt()+0.5f)); //Draw the selected object at the mouse
            }
        }
        DrawText(spriteBatch, FNT_OVERLAY, "Resources: " + EcResources.ToString(), new Vector2(400, SCREEN_SIZE.Y - OVERLAY_SIZE.Y + 20), Color.White);
        DrawText(spriteBatch, FNT_OVERLAY, "Total Kills: " + TotalEnemiesKilled.ToString(), new Vector2(400, SCREEN_SIZE.Y - OVERLAY_SIZE.Y + 60), Color.White);
        DrawGrid(spriteBatch);

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
}