using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;
using static DrawingHelper;
using static GameStats;

public class Base : GameObjectList
{
    Color cannonColor;
    int timer;
    Tower.Type type;
    int[] stats = new int[] { 0, 0, 0 };
    double damage = 50;
    float speed = 15;
    Color healthColor;
    float rotation;
    Texture2D cannonTexture, baseTexture;
    public Vector2 mousePosition;
    List<GridNode> myNodes;
    bool hover;
    bool inUltimate;

    int animationTimer;
    int animationLength;
    int previoushealth;
    bool inAnimation;

    public Base() : base()
    {
        type = Tower.Type.Base;
        healthColor = Color.White;
        this.cannonTexture = SPR_BASEGUN;
        this.baseTexture = SPR_BASE;
        inUltimate = false;

        animationTimer = 0;
        animationLength = 3;
        previoushealth = BaseHealth;
        inAnimation = false;
    }

    public bool canShoot
    {
        get { return timer <= 0; }
    }

    public override void Update(object gameTime)
    {
        if (canShoot)
            baseTexture = SPR_BASE;
        else
            baseTexture = SPR_BASENOSHOOT;
            
        base.Update(gameTime);
            timer--;

        if (myNodes == null)
        {
            myNodes = new List<GridNode>();
            myNodes.Add(MyPlane.NodeAt(position, true));
            myNodes.AddRange(myNodes[0].ExtendedNeighbours);
        }
        hover = false;
        foreach(GridNode node in myNodes)
        {
            if (node.selected)
                hover = true;
        }

        if (BaseHealth != previoushealth && !inAnimation)
        {
            inAnimation = true;
            animationTimer = 0;
        }
        previoushealth = BaseHealth;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        if (inAnimation)
        {
            animationTimer++;
            inAnimation = animationTimer < animationLength;
            healthColor = new Color(255, 0, 0, 150);
        }
        else
        {
            healthColor = Color.White;
            animationTimer = 0;
        }
        
        spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), 0, null, healthColor);
        
        //spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), 0f, new Vector2(1f), healthColor * (1f - 0.4f * hover.ToInt()), SpriteEffects.None, 0);

        //Disabled the 2D cannon overlay
        //spriteBatch.Draw(cannonTexture, GlobalPosition, null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation, new Vector2(1f), cannonColor);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePosition = inputHelper.MousePosition / Camera.scale;
        Vector2 targetPos = mousePosition;
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
        if (inputHelper.MouseRightButtonDown() && inputHelper.MouseInGameWindow && canShoot)
        {
            Overlay overlay = GameWorld.FindByType<Overlay>()[0] as Overlay;
            if (!overlay.Busy)
            {
                MyPlane.Add(new BaseProjectile(TowerDamage(Tower.Type.Base, stats), speed) { Position = position });
                timer = TowerRate(type, stats);
            }
        }
    }

    public bool InUltimate
    {
        get
        {
            return inUltimate;
        }
        set
        {
            inUltimate = value;
        }
    }
}
