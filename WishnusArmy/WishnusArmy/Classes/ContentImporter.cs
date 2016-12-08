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

            SPR_BASE,
            SPR_ABSTRACT_TOWER,
            SPR_ABSTRACT_CANNON,
            SPR_MAINMENUBACKGROUND,
            SPR_CREDITSBACKGROUND,
            SPR_HELPBACKGROUND,
            SPR_CAMPAIGNBUTTON,
            SPR_SURVIVALBUTTON,
            SPR_HELPBUTTON,
            SPR_CREDITSBUTTON,
            SPR_BACKBUTTON,
            SPR_PULSE,
			SPR_BASEGUN;

        public static void Initialize(ContentManager Content)
        {
            SPR_BASE = Content.Load<Texture2D>("Content/Sprites/BaseSprite");
            SPR_MAINMENUBACKGROUND = Content.Load<Texture2D>("Content/Sprites/MainMenuBackground");
            SPR_CREDITSBACKGROUND = Content.Load<Texture2D>("Content/Sprites/CreditsBackground");
            SPR_HELPBACKGROUND = Content.Load<Texture2D>("Content/Sprites/HelpBackground");
            SPR_CAMPAIGNBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/CampaignButton");
            SPR_SURVIVALBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/SurvivalButton");
            SPR_HELPBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/HelpButton");
            SPR_CREDITSBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/CreditsButton");
            SPR_BACKBUTTON = Content.Load<Texture2D>("Content/Sprites/Buttons/BackButton");
            SPR_PULSE = Content.Load<Texture2D>("Content/Sprites/Pulse");
            SPR_BASEGUN = Content.Load<Texture2D>("Content/Sprites/BaseGun");
			SPR_ABSTRACT_TOWER = Content.Load<Texture2D>("Content/Sprites/abstractTower");
            SPR_ABSTRACT_CANNON = Content.Load<Texture2D>("Content/Sprites/cannon");
        }
    }


    internal static class Textures
    {
        internal static List<Texture2D> LIST_FLOOR_TEXTURES; //This is used for the levelbuilder toolbar.
        internal static Texture2D
            TEX_GRASS,
            TEX_GRASS_DIRT,
            SPR_PARTICLE;

        public static void Initialize(ContentManager Content)
        {
            TEX_GRASS = Content.Load<Texture2D>("Content/Textures/tex_grass");
            TEX_GRASS_DIRT = Content.Load<Texture2D>("Content/Textures/tex_grass_dirt");
            SPR_PARTICLE = Content.Load<Texture2D>("Content/Textures/particle_test");
            LIST_FLOOR_TEXTURES = new List<Texture2D>
            {
                //Textures that should show up in the LevelBuilder Toolbar
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
