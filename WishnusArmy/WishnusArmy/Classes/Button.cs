using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Button : GameObject
{
    protected bool pressed;
    protected Texture2D imageAsset;

    public Button(Texture2D imageAsset, int layer = 0, string id = "")
    {
        pressed = false;
        this.imageAsset = imageAsset;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(imageAsset, this.position, Color.White);
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }
}
