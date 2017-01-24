using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

class ResourceTower : Tower
{
    int timer;
    public HealthText healthText;

    public ResourceTower() : base(Type.ResourceTower)
    {
        timer = 0;
        baseTexture = ContentImporter.Sprites.SPR_MERCHANT_TOWER;
    }

    public override void Update(object gameTime)
    {
        timer++;
        base.Update(gameTime);

        if (healthText != null)
        {
            healthText.Position = Position - new Vector2(0, 40);
        }

        if (timer < TowerRate(type, stats))
            return;
        timer = 0;
        GatherResources();

    }

    private void GatherResources()
    {
        int resource = (int)TowerDamage(type, stats);
        GameStats.EcResources += resource;
        GameStats.totalResourcesGathered += resource;
        if (healthText == null || healthText.p > 0.7f)
        {
            healthText = new HealthText("$" + resource, 1) { Position = GlobalPosition + new Vector2(0, -15) };
            healthText.startColor = Color.Yellow;
            healthText.endColor = Color.Yellow;
            MyPlane.Add(healthText);
        }
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

}
