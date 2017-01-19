﻿using System;
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
            if (_tower !=  null) //If the new tower isn't null...
                _tower.myNode.beacon = true; //Select the new tower.

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
                            tower.stats[z]++;
                            GameStats.EcResources -= (int)(UpgradeCost(tower.type)*Math.Pow(1.5,(tower.stats[z]-1)));
                        }
                    },
                Position = new Vector2(350, 84 + (blockSize.Y + blockSeperation + 15) * i),
                padding = new Point(1)
            });
            Add(buttons[i]);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawRectangle(new Rectangle(position.toPoint(), new Point(400, OVERLAY_SIZE.Y)), spriteBatch, Color.White, 2);
        if (tower != null)
        {
            spriteBatch.Draw(tower.baseTexture, position + new Vector2(30, 80), Color.White);
            DrawText(spriteBatch, FNT_MENU, TOWER_INFO[tower.ToString()].name, position + new Vector2(100, 25), Color.White, true);

            string[] str = new string[3] { "Damage:", "Range:", "Fire Rate:" };
            for (int z = 0; z < tower.stats.Length; ++z)
            {
                DrawText(spriteBatch, FNT_LEVEL_BUILDER, str[z], position + new Vector2(110, 82 + (blockSize.Y + 15) * z), Color.White);
                for (int i = 0; i < 5; ++i)
                {
                    DrawRectangleFilled(new Rectangle(position.toPoint() + new Point(220 + (blockSize.X + blockSeperation) * i, 80 + (blockSize.Y + 15) * z), blockSize), spriteBatch, Color.Black, 0.4f);
                    DrawRectangleFilled(new Rectangle(position.toPoint() + new Point(220 + (blockSize.X + blockSeperation) * i, 80 + (blockSize.Y + 15) * z), blockSize), spriteBatch, Color.White * (tower.stats[z] >= i).ToInt());
                }
            }
        }
    }
}