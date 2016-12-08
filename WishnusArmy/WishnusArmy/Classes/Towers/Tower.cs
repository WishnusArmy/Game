using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

namespace WishnusArmy.Classes.Towers
{
    class Tower : AnimatedGameObject
    {
        Vector2 gridPosition;
        public Texture2D texture;
        int range;
        int level = 1;
        int cost;

        public Tower(Vector2 position)

        {
            gridPosition = position;
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 pos = position *= NODE_SIZE;
            spriteBatch.Draw(texture, pos);
        }

        public virtual void findNearestEnemy()
        {

        }

        public virtual void Attack()
        {

        }
    }
}
