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
    protected bool hover;
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
        hover = BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);


        //Hey Abel, als je hier nou gewoon een rectangle tekent met de width van de tekst? (SpriteFont.MeasureString(string).X)
        if (hover)
            spriteBatch.Draw(imageAsset, this.position, Color.Orange);
        else
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

    public override Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, imageAsset.Width, imageAsset.Height);
        }
    }
}
