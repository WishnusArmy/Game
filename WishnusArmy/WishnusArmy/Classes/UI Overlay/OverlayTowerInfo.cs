using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static Constant;
using static ContentImporter.Fonts;

public class OverlayTowerInfo : GameObjectList
{
    Tower _tower;
    List<ButtonWithDelegate> buttons;
    static Point blockSize = new Point(15, 20);
    static int blockSeperation = 4;

    public Tower tower
    {
        get
        {
            return _tower;
        }
        set
        {
            if (_tower != null)
                _tower.myNode.beacon = false; //Unselected the current tower
            _tower = value; //set the new tower

            foreach (Button but in buttons)
                but.active = (tower != null);
        }
    }

    public OverlayTowerInfo() : base()
    {
        buttons = new List<ButtonWithDelegate>();
        tower = null;
        for (int i = 0; i < 3; ++i)
        {
            int z = i; //strangely i is seen as a pointer
            buttons.Add(new ButtonWithDelegate("+", Color.White, Color.Blue, FNT_LEVEL_BUILDER)
            {
                active = false,
                obj = delegate 
                    {
                        if (tower.stats[z] < 4)
                        {
                            int cost = (int)(UpgradeCost(tower.type) * Math.Pow(1.5, (tower.stats[z] - 1)));
                            if (cost <= GameStats.EcResources)
                            {
                                tower.stats[z]++;
                                GameStats.EcResources -= cost;
                            }
                        }
                    },
                Position = new Vector2(350, 60 + (blockSize.Y + blockSeperation + 15) * i),
                padding = new Point(1)
            });
            Add(buttons[i]);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (tower == null)
            return;

        _tower.myNode.beacon = true; //Select the new tower.

        if (inputHelper.MouseLeftButtonPressed() && inputHelper.MouseInGameWindow && !tower.myNode.selected)
        {
            tower = null;
        }
    }
    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        if (tower != null)
        tower.hover = true;
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (tower != null)
        {
            spriteBatch.Draw(tower.baseTexture, position + new Vector2(45, 52), null, Color.White, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f );
            DrawText(spriteBatch, FNT_MENU, TOWER_INFO[tower.ToString()].name, position + new Vector2(130, 25), Color.White, true);

            string[] str = new string[3] { "Damage:", "Range:", "Fire Rate:" };
            if (tower is ResourceTower)
                str = new string[3] { "Investment:", "Area:", "Rate:" };
            for (int z = 0; z < tower.stats.Length; ++z)
            {
                DrawText(spriteBatch, FNT_LEVEL_BUILDER, str[z], position + new Vector2(125, 57 + (blockSize.Y + 15) * z), Color.White);
                for (int i = 0; i < 5; ++i)
                {
                    DrawRectangleFilled(new Rectangle(position.toPoint() + new Point(235 + (blockSize.X + blockSeperation) * i, 55 + (blockSize.Y + 15) * z), blockSize), spriteBatch, Color.Black, 0.4f);
                    DrawRectangleFilled(new Rectangle(position.toPoint() + new Point(235 + (blockSize.X + blockSeperation) * i, 55 + (blockSize.Y + 15) * z), blockSize), spriteBatch, Color.White * (tower.stats[z] >= i).ToInt());
                }
            }

            //Hover menu with costs
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].hover)
                {
                    DrawRectangleFilled(new Rectangle(buttons[i].GlobalPosition.toPoint() - new Point(0, 75), new Point(150, 75)), spriteBatch, Color.White);
                    spriteBatch.DrawString(FNT_LEVEL_BUILDER, "Costs: " + ((int)(UpgradeCost(tower.type) * Math.Pow(1.5, (tower.stats[i] - 1)))).ToString(), buttons[i].GlobalPosition - new Vector2(-23, 48), Color.Black);
                    DrawRectangle(new Rectangle(buttons[i].GlobalPosition.toPoint() - new Point(0, 75), new Point(150, 75)), spriteBatch, Color.Black, 3);
                }
            }
        }
    }
}