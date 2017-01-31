using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static Constant;
using static GameStats;
using static ContentImporter.Textures;
using static ContentImporter.Fonts;
using static ContentImporter.Sprites;

class UltimateTimer : DrawOnTop
{
    int timer;
    int coolDownTime;
    Button button;
    bool ready;

    public UltimateTimer() : base()
    {
        timer = 0;
        this.coolDownTime = 32000;
        position = new Vector2(SCREEN_SIZE.X - 440, SCREEN_SIZE.Y - 100);

        button = new Button("0%", Color.Transparent, Color.Transparent, FNT_ULTIMATE);
        button.textColor = Color.White;
        button.Position = position;
    }
    public override void Update(object gameTime)
    {
        List<Base> baseList = GameWorld.FindByType<Base>();
        if (baseList.Count > 0 && !baseList[0].InUltimate)
            timer++;
        
        button.buttonText = (int)(((double)timer / (double)coolDownTime) * 100) + "%";

        if (timer > coolDownTime)
        {
            timer = coolDownTime;
            ready = true;
        }
        else
        {
            return;
        }

        button.Update(gameTime);

        if (button.Pressed && ready)
        {
            timer = 0;
            ready = false;
            Pulse p = new Pulse(Wave * 1000, SCREEN_SIZE.X * 1.8, ObjectLists.Enemies);
            p.ultimate = true;
            p.color = Color.Red;
            p.speed = 25;
            if (baseList.Count > 0)
            {
                baseList[0].Add(p);
                baseList[0].InUltimate = true;
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        button.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SPR_ULTIMATE, new Rectangle(position.ToPoint() - new Point(75, 70), new Point(140, 140)), Color.White);//new Color(255, 30, 30, 255 -(int)(((double)timer / (double)coolDownTime) * 255)));
        button.Draw(gameTime, spriteBatch);  
    }

    public int Timer
    {
        set { timer = value; }
        get { return timer; }
    }
}
