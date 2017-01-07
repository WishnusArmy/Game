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

public class Overlay : GameObjectList
{
    public OverlayItem selected;
    bool selectedPossible;
    Vector2 mousePos;
 

    public Overlay() : base()
    {
        selected = null;
        selectedPossible = false;
        mousePos = Vector2.Zero;
        OverlayItem item = new OverlayItem("LaserTower");
        item.Position = new Vector2(SCREEN_SIZE.X - 100, 100);
        Add(item);
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

        selectedPossible = !node.solid;

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
        DrawText(spriteBatch, FNT_OVERLAY, "Resources: " + EcResources.ToString(), new Vector2(50, SCREEN_SIZE.Y - OVERLAY_SIZE.Y + 20), Color.White);
        base.Draw(gameTime, spriteBatch);
    }
}