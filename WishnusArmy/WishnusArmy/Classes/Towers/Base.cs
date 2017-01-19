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
    static int maxRockets = 3;
    int damage = 50;
    float speed = 10;
    Color healthColor;
    float rotation;
    Texture2D cannonTexture, baseTexture;
    public Vector2 mousePosition;
    List<GridNode> myNodes;
    bool hover;

    public Base() : base()
    {
        type = Tower.Type.Base;
        healthColor = new Color(0, 255, 0);
        this.cannonTexture = SPR_BASEGUN;
        this.baseTexture = SPR_BASE;

    }

    public bool canShoot
    {
        get { return timer <= 0; }
    }

    public override void Update(object gameTime)
    {
        if (canShoot)
            cannonColor = Color.White;
        else
            cannonColor = Color.Red;
        base.Update(gameTime);
            timer--;

        if (myNodes == null)
        {
            myNodes = new List<GridNode>();
            myNodes.Add(MyPlane.NodeAt(position, true));
            myNodes.AddRange(myNodes[0].ExtendedNeighbours);
        }
        hover = false;
        foreach(GridNode  node in myNodes)
        {
            if (node.selected)
                hover = true;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        double p = ((double)BaseHealth / (double)MaxBaseHealth);
        //healthColor = new Color((int)((1-p)*74),74+(int)(28*p),74+(int)(130*p));
        healthColor = new Color((int)(255 * (1 - p)), (int)(255 * p), 0);
        healthColor = Color.White;
        //spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), 0, null, healthColor);
        spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), 0f, new Vector2(1f), healthColor * (1f - 0.4f * hover.ToInt()), SpriteEffects.None, 0);
        spriteBatch.Draw(cannonTexture, GlobalPosition, null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation, new Vector2(1f), cannonColor);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePosition = inputHelper.MousePosition / Camera.scale;
        Vector2 targetPos = mousePosition;
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
        if (inputHelper.MouseLeftButtonPressed() && inputHelper.MouseInGameWindow && canShoot)
        {
            Console.WriteLine(FindByType<Overlay>().Count);
            if (FindByType<BaseProjectile>().Count < maxRockets)
            {
                Add(new BaseProjectile(damage, speed));
                timer = TowerRate(type, stats);
            }
        }
    }
}
