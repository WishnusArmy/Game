﻿using System;
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
            SPR_BASE, 
            SPR_MAINMENUBACKGROUND,
            SPR_PLAYBUTTON,
            SPR_PULSE;

        public static void Initialize(ContentManager Content)
        {
            SPR_BASE = Content.Load<Texture2D>("Content/Sprites/BaseSprite");
            SPR_MAINMENUBACKGROUND = Content.Load<Texture2D>("Content/Sprites/MainMenuBackground");
            SPR_PLAYBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/PlayButton");
            SPR_PULSE = Content.Load<Texture2D>("Content/Sprites/Pulse");
        }
    }


    internal static class Textures
    {
        internal static List<Texture2D> LIST_FLOOR_TEXTURES;
        internal static Texture2D
            TEX_GRASS,
            TEX_GRASS_DIRT;

        public static void Initialize(ContentManager Content)
        {
            TEX_GRASS = Content.Load<Texture2D>("Content/Textures/tex_grass");
            TEX_GRASS_DIRT = Content.Load<Texture2D>("Content/Textures/tex_grass_dirt");
            LIST_FLOOR_TEXTURES = new List<Texture2D>
            {
                TEX_GRASS,
                TEX_GRASS_DIRT
            };
        }
    }

    internal static class Fonts
    {
        internal static SpriteFont
            FNT_LEVEL_BUILDER;

        public static void Iniatilize(ContentManager Content)
        {
            FNT_LEVEL_BUILDER = Content.Load<SpriteFont>("Content/Fonts/fnt_level_builder");
        }
    }
}
