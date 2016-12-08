using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter;

namespace WishnusArmy.Classes.Towers
{
    class Tower : GameObject
    {
        protected MouseState state = new MouseState();
        Vector2 gridPosition, pos, mousePosition;
        public Texture2D baseTexture;
        public Texture2D cannonTexture;
        float rotation;
        int range;
        int level = 1;
        int cost;

        public Tower(Vector2 position)
        {
            baseTexture = Sprites.SPR_ABSTRACT_TOWER;
            cannonTexture = Sprites.SPR_ABSTRACT_CANNON;
            gridPosition = position;
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            pos = gridPosition * NODE_SIZE + GlobalPosition;
            spriteBatch.Draw(baseTexture, pos);
            spriteBatch.Draw(cannonTexture, pos + new Vector2(NODE_SIZE/2, NODE_SIZE/2), null, null, new Vector2(33,33), rotation);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            double opposite = inputHelper.MousePosition.Y -32 - pos.Y;
            double adjacent = inputHelper.MousePosition.X -32 - pos.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float) Math.PI;
            mousePosition = inputHelper.MousePosition;
        }
        public Vector2 findNearestEnemy()
        {
            return mousePosition;
            //Change this into closest enemy position
        }

        public virtual void Attack()
        {

        }
        public virtual void Upgrade()
        {

        }
    }
}
