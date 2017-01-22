using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static ContentImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishnusArmy.Classes.Enemies.Air
{
    class Clouds : DrawOnTop
    {
        private float rotation;
        private Texture2D currentTexture;
        private float opacity;

        public Clouds(Vector2 position)
        {
            velocity = new Vector2(1, -0.3f);
            rotation = RANDOM.Next(1, 20);
            this.position = position;

            opacity = (float)RANDOM.NextDouble() * 0.4f;

            if (RANDOM.NextDouble() > 0.5)
                currentTexture = Sprites.SPR_CLOUD1;
            else
                currentTexture = Sprites.SPR_CLOUD2;

        }

        public override void Update(object gameTime)
        {
            if (GlobalPosition.Y + Origin.Y * 10 < 0 && GlobalPosition.X + Origin.X * 10 > SCREEN_SIZE.X)
            {
                opacity = (float)RANDOM.NextDouble() * 0.5f;
                position = new Vector2(-Origin.X * 10, SCREEN_SIZE.Y + Origin.Y * 10);
            }
            else
                position += velocity;


            Console.WriteLine(GlobalPosition);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(currentTexture, GlobalPosition, null, Color.Black * opacity, (float)Math.PI / rotation, Origin, 10f, SpriteEffects.None, 0f); 
        }

        public Vector2 Origin
        {
            get { return new Vector2(currentTexture.Width, currentTexture.Height) / 2; }
        }

    }
}
