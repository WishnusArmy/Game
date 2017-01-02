using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

internal static class ContentImporter
{
    public static void Initialize(ContentManager Content)
    {
        Textures.Initialize(Content);
        Fonts.Iniatilize(Content);
        Sprites.Initialize(Content);
    }

    internal static class Sprites
    {
        internal static Texture2D
            SPR_MAINBACKGROUND,
            SPR_CREDITSPLANE,
            SPR_ENEMY,

            SPR_WHITEPIXEL,

            // Projectiles

            SPR_PULSE,
            SPR_BULLET,

            // Towers

            SPR_ABSTRACT_TOWER,
            SPR_ABSTRACT_CANNON,
            SPR_BASE,
            SPR_BASEGUN,
            SPR_RADIUS,
            SPR_LASER_TOWER,
            SPR_PULSE_TOWER;

        public static void Initialize(ContentManager Content)
        {
            
            SPR_MAINBACKGROUND = Content.Load<Texture2D>("Content/Sprites/MainBackground");
            SPR_CREDITSPLANE = Content.Load<Texture2D>("Content/Sprites/Credits");
            SPR_ENEMY = Content.Load<Texture2D>("Content/Sprites/enemySprite"); 
            SPR_WHITEPIXEL = Content.Load<Texture2D>("Content/Sprites/WhitePixel");

            //Projectiles
            SPR_PULSE = Content.Load<Texture2D>("Content/Sprites/Projectiles/SPR_PULSE");
            SPR_BULLET = Content.Load<Texture2D>("Content/Sprites/Projectiles/SPR_BULLET");

            //Towers
            SPR_ABSTRACT_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/abstractTower");
            SPR_ABSTRACT_CANNON = Content.Load<Texture2D>("Content/Sprites/Towers/cannon");
            SPR_BASE = Content.Load<Texture2D>("Content/Sprites/Towers/BaseSprite");
            SPR_BASEGUN = Content.Load<Texture2D>("Content/Sprites/Towers/BaseGun");
            SPR_RADIUS = Content.Load<Texture2D>("Content/Sprites/Towers/radius");
            SPR_LASER_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/spr_laser_tower");
            SPR_PULSE_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/pulseTower");
        }
    }


    internal static class Textures
    {
        internal static Texture2D
            TEX_GRASS,
            TEX_GRASS_DIRT,
            TEX_EMPTY,
            TEX_EMPTY_SMALL,
            TEX_STONE_ROAD,
            TEX_DIRT,
            TEX_WATER,
            TEX_FOREST;

        public static void Initialize(ContentManager Content)
        {
            TEX_GRASS = Content.Load<Texture2D>("Content/Textures/tex_grass");
            TEX_GRASS_DIRT = Content.Load<Texture2D>("Content/Textures/tex_grass_dirt");
            TEX_EMPTY = Content.Load<Texture2D>("Content/Textures/emptytexture");
            TEX_EMPTY_SMALL = Content.Load<Texture2D>("Content/Textures/emptytexturesmall");
            TEX_STONE_ROAD = Content.Load<Texture2D>("Content/Textures/tex_stone_road");
            TEX_DIRT = Content.Load<Texture2D>("Content/Textures/tex_dirt");
            TEX_WATER = Content.Load<Texture2D>("Content/Textures/tex_water");
            TEX_FOREST = Content.Load<Texture2D>("Content/Textures/tex_forest");
        }
    }

    internal static class Fonts
    {
        internal static SpriteFont
            FNT_LEVEL_BUILDER,
            FNT_MENU;

        public static void Iniatilize(ContentManager Content)
        {
            FNT_LEVEL_BUILDER = Content.Load<SpriteFont>("Content/Fonts/fnt_level_builder");
            FNT_MENU = Content.Load<SpriteFont>("Content/Fonts/fnt_menu");
        }
    }
}
