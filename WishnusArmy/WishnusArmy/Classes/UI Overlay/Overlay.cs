using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public class Overlay : GameObjectList
{
    public OverlayItem selected;
    Vector2 mousePos;

    public Overlay() : base()
    {
        selected = null;
        mousePos = Vector2.Zero;
        OverlayItem item = new OverlayItem("LaserTower");
        item.Position = new Vector2(SCREEN_SIZE.X - 100, 100);
        Add(item);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePos = inputHelper.MousePosition;

        if (inputHelper.MouseLeftButtonPressed() &&
            inputHelper.MouseInGameWindow &&
            selected != null)
        {
            GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
            Vector2 pos;
            try
            {
                pos = plane.NodeAt(inputHelper.MousePosition).Position;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return;
            }
            Type t = Type.GetType(selected.itemType); //Get the type of the object
            object temp = Activator.CreateInstance(t); //Create an instance of that object
            GameObject obj = temp as GameObject; //Cast it as a GameObject
            obj.Position = pos;
            plane.Add(obj);
            selected = null;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, 0), new Point(OVERLAY_SIZE.X, SCREEN_SIZE.Y)), spriteBatch, Color.Black);
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(0, SCREEN_SIZE.Y - OVERLAY_SIZE.Y), new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, OVERLAY_SIZE.Y)), spriteBatch, Color.Black);
        if (selected != null)
        {
            spriteBatch.Draw(selected.icon, mousePos, Color.White);
        }
        base.Draw(gameTime, spriteBatch);
    }
}
