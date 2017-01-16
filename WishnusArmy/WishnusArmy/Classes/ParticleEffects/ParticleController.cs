using System;
using System.Threading;
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
            new ExplosionSmokeParticleSystem(WishnusArmy.WishnusArmy.self, 50),
            new BuildTowerParticleSystem(WishnusArmy.WishnusArmy.self, 5)
        };
        foreach(GameObject obj in effects)
        {
            Add(obj);
        }
    }

    public void AddExplosion(Vector2 pos)
    {
        foreach (ParticleSystem e in effects)
            if (e is ExplosionSmokeParticleSystem || e is ExplosionParticleSystem)
                e.AddParticles(pos);
    }

    public void AddTowerBuildGlow(Vector2 pos)
    {
        foreach (ParticleSystem e in effects)
            if (e is BuildTowerParticleSystem)
                e.AddParticles(pos);
    }
}