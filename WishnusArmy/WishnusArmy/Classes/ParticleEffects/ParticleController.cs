using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public class ParticleController : DrawOnTopList
{
    List<GameObject> effects;

    public ParticleController()
    {
        effects = new List<GameObject>
        {
            new ExplosionParticleSystem(WishnusArmy.WishnusArmy.self, 30),
            new ExplosionSmokeParticleSystem(WishnusArmy.WishnusArmy.self, 50)
        };
        foreach(GameObject obj in effects)
        {
            Add(obj);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public void AddExplosion(Vector2 pos)
    {
        foreach (ParticleSystem e in effects)
        {
            if (e is ExplosionSmokeParticleSystem || e is ExplosionParticleSystem)
                e.AddParticles(pos);
        }
    }
}