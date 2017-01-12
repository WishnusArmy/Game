using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sprites;
using WishnusArmy.GameManagement;

public class Tank : Enemy
{
    public Tank() : base()
    {
        this.sprite = SPR_ENEMY;
        this.speed = 5;
        PlaySound(ContentImporter.Sounds.SND_HELICOPTER_LOOPING, true);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}
