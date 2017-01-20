using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using WishnusArmy.GameManagement;

internal static class ContentImporter
{
    public static void Initialize(ContentManager Content)
    {
        Textures.Initialize(Content);
        Fonts.Initialize(Content);
        Sprites.Initialize(Content);
        Music.Initialize(Content);
        Sounds.Initialize(Content);
        Sheets.Initialize(Content);
        Icons.Initialize(Content);
    }

    internal static class Sprites
    {
        internal static Texture2D
            SPR_MAINBACKGROUND,
            SPR_GAMEOVERBACKGROUND,
            SPR_CREDITSPLANE,
            SPR_OVERLAY,
            SPR_ENEMY,
            SPR_SHADOW,
            SPR_WHITEPIXEL,

            // Projectiles
            SPR_PULSE,
            SPR_ROCKET,
            SPR_CANNONBALL,

            // Towers
            SPR_ABSTRACT_TOWER,
            SPR_ABSTRACT_CANNON,
            SPR_BASE,
            SPR_BASEGUN,
            SPR_CIRCLE,
            SPR_LASER_TOWER,
            SPR_PULSE_TOWER,
            SPR_ROCKET_TOWER,

            // Tower Icons
            SPR_CANNON_ICON,
            SPR_FLAMETHROWER_ICON,
            SPR_GATTLING_ICON,
            SPR_GRENADE_ICON,
            SPR_LASER_ICON,
            SPR_MACHINEGUN_ICON,
            SPR_PULSE_ICON,
            SPR_ROCKETLAUNCHER_ICON,
            SPR_SNIPER_ICON,
            SPR_TESLACOIL_ICON;

        public static void Initialize(ContentManager Content)
        {
            
            SPR_MAINBACKGROUND = Content.Load<Texture2D>("Content/Sprites/MainBackground");
            SPR_CREDITSPLANE = Content.Load<Texture2D>("Content/Sprites/Credits");
            SPR_ENEMY = Content.Load<Texture2D>("Content/Sprites/enemySprite");
            SPR_OVERLAY = Content.Load<Texture2D>("Content/Sprites/overlaybackground");
            SPR_GAMEOVERBACKGROUND = Content.Load<Texture2D>("Content/Sprites/GameOver");
            //SPR_AIRPLANE = Content.Load<Texture2D>("Content/Sprites/plane");
            SPR_WHITEPIXEL = Content.Load<Texture2D>("Content/Sprites/WhitePixel");
            SPR_SHADOW = Content.Load<Texture2D>("Content/Sprites/shadow");

            //Projectiles
            SPR_PULSE = Content.Load<Texture2D>("Content/Sprites/Projectiles/SPR_PULSE");
            SPR_ROCKET = Content.Load<Texture2D>("Content/Sprites/Projectiles/SPR_BULLET");
            SPR_CANNONBALL = Content.Load<Texture2D>("Content/Sprites/Projectiles/cannonBall");

            //Towers
            SPR_ABSTRACT_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/abstractTower");
            SPR_ABSTRACT_CANNON = Content.Load<Texture2D>("Content/Sprites/Towers/cannon");
            SPR_BASE = Content.Load<Texture2D>("Content/Sprites/Towers/BaseSprite");
            SPR_BASEGUN = Content.Load<Texture2D>("Content/Sprites/Towers/BaseGun");
            SPR_CIRCLE = Content.Load<Texture2D>("Content/Sprites/Towers/radius");
            SPR_LASER_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/spr_laser_tower");
            SPR_PULSE_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/SPR_PULSE_TOWER");
            SPR_ROCKET_TOWER = Content.Load<Texture2D>("Content/Sprites/Towers/SPR_ROCKET_TOWER");

            //Tower Icons
            SPR_CANNON_ICON = Content.Load<Texture2D>("Content/Sprites/spr_cannon");
            SPR_FLAMETHROWER_ICON = Content.Load<Texture2D>("Content/Sprites/spr_flamethrower");
            SPR_GATTLING_ICON = Content.Load<Texture2D>("Content/Sprites/spr_gattling");
            SPR_GRENADE_ICON = Content.Load<Texture2D>("Content/Sprites/spr_grenade");
            SPR_LASER_ICON = Content.Load<Texture2D>("Content/Sprites/spr_laser");
            SPR_MACHINEGUN_ICON = Content.Load<Texture2D>("Content/Sprites/spr_machinegun");
            SPR_PULSE_ICON = Content.Load<Texture2D>("Content/Sprites/spr_pulse");
            SPR_ROCKETLAUNCHER_ICON = Content.Load<Texture2D>("Content/Sprites/spr_rocketlauncher");
            SPR_SNIPER_ICON = Content.Load<Texture2D>("Content/Sprites/spr_snipertower");
            SPR_TESLACOIL_ICON = Content.Load<Texture2D>("Content/Sprites/spr_tesla");
        }
    }


    internal static class Textures
    {
        internal static Texture2D
            TEX_GRASS,
            TEX_GRASS_DIRT,
            TEX_EMPTY,
            TEX_EMPTY_SMALL,
            TEX_DOT,
            TEX_STONE_ROAD,
            TEX_DIRT,
            TEX_WATER,
            TEX_FOREST,
            TEX_AIR,
            TEX_MOUNTAIN_1,
            TEX_MOUNTAIN_2,
            TEX_MOUNTAIN_3,
            TEX_EXPLOSION,
            TEX_SMOKE;

        public static void Initialize(ContentManager Content)
        {
            TEX_GRASS = Content.Load<Texture2D>("Content/Textures/tex_grass");
            TEX_GRASS_DIRT = Content.Load<Texture2D>("Content/Textures/tex_grass_dirt");
            TEX_EMPTY = Content.Load<Texture2D>("Content/Textures/emptytexture");
            TEX_DOT = Content.Load<Texture2D>("Content/Textures/dot");
            TEX_EMPTY_SMALL = Content.Load<Texture2D>("Content/Textures/emptytexturesmall");
            TEX_STONE_ROAD = Content.Load<Texture2D>("Content/Textures/tex_stone_road");
            TEX_DIRT = Content.Load<Texture2D>("Content/Textures/tex_dirt");
            TEX_WATER = Content.Load<Texture2D>("Content/Textures/tex_water");
            TEX_FOREST = Content.Load<Texture2D>("Content/Textures/tex_woods_tile");
            TEX_AIR = Content.Load<Texture2D>("Content/Textures/tex_air");
            TEX_MOUNTAIN_1 = Content.Load<Texture2D>("Content/Textures/tex_mountain_1");
            TEX_MOUNTAIN_2 = Content.Load<Texture2D>("Content/Textures/tex_mountain_2");
            TEX_MOUNTAIN_3 = Content.Load<Texture2D>("Content/Textures/tex_mountain_3");
            TEX_EXPLOSION = Content.Load<Texture2D>("Content/Textures/Explosion");
            TEX_SMOKE = Content.Load<Texture2D>("Content/Textures/smoke");
        }
    }

    internal static class Icons
    {
        internal static Texture2D
            ICON_KILLS,
            ICON_COINS,
            ICON_WAVE,
            ICON_LIFE;

        public static void Initialize(ContentManager Content)
        {
            ICON_COINS = Content.Load<Texture2D>("Content/Sprites/Icons/coin");
            ICON_KILLS = Content.Load<Texture2D>("Content/Sprites/Icons/kill");
            ICON_WAVE = Content.Load<Texture2D>("Content/Sprites/Icons/wave");
            ICON_LIFE = Content.Load<Texture2D>("Content/Sprites/Icons/life");
        }
    }

    internal static class Sheets
    {
        internal static Texture2D
            SHEET_TANK,
            SHEET_AIRPLANE,
            SHEET_HELICOPTER,
            SHEET_INFANTRY;

        public static void Initialize(ContentManager Content)
        {
            SHEET_TANK = Content.Load<Texture2D>("Content/Sheets/sheet_tank_upgraded@4x1");
            SHEET_AIRPLANE = Content.Load<Texture2D>("Content/Sheets/sheet_plane@4x1");
            SHEET_HELICOPTER = Content.Load<Texture2D>("Content/Sheets/sheet_helicopter@4x1");
            SHEET_INFANTRY = Content.Load<Texture2D>("Content/Sheets/sheet_infantry@4x1");
        }
    }

    internal static class Fonts
    {
        internal static SpriteFont
            FNT_LEVEL_BUILDER,
            FNT_MENU,
            FNT_GAMEOVER,
            FNT_OVERLAY,
            FNT_OVERLAY_INFO,
            FNT_HEALTH_INFO,
            FNT_GAMESTATS;

        public static void Initialize(ContentManager Content)
        {
            FNT_LEVEL_BUILDER = Content.Load<SpriteFont>("Content/Fonts/fnt_level_builder");
            FNT_MENU = Content.Load<SpriteFont>("Content/Fonts/fnt_menu");
            FNT_GAMEOVER = Content.Load<SpriteFont>("Content/Fonts/fnt_gameover");
            FNT_OVERLAY = Content.Load<SpriteFont>("Content/Fonts/fnt_overlay");
            FNT_OVERLAY_INFO = Content.Load<SpriteFont>("Content/Fonts/fnt_overlay_info");
            FNT_HEALTH_INFO = Content.Load<SpriteFont>("Content/Fonts/fnt_health_info");
            FNT_GAMESTATS = Content.Load<SpriteFont>("Content/Fonts/fnt_gamestats");
        }
    }

    internal static class Music
    {
        private static SoundManager soundManagerMSC;

        internal static Song
            SNG_MAINMENU,
            SNG_LAST_DAWN,
            SNG_RUN,
            SNG_THE_GAME_IS_ON,
            SNG_FALL;

        public static void Initialize(ContentManager Content)
        {
            soundManagerMSC = new SoundManager();
            SNG_MAINMENU = Content.Load<Song>("Content/Music/mainMenu");
            SNG_LAST_DAWN = Content.Load<Song>("Content/Music/Last_Dawn");
            SNG_RUN = Content.Load<Song>("Content/Music/Run");
            SNG_THE_GAME_IS_ON = Content.Load<Song>("Content/Music/the_game_is_on");
            SNG_FALL = Content.Load<Song>("Content/Music/Fall");
        }

        public static void PlayMusic(Song music, bool isRepeating = true)
        {
            MediaPlayer.IsRepeating = isRepeating;
            MediaPlayer.Play(music);
        }
    }

    internal static class Sounds
    {
        private static SoundManager soundManagerSFX;
        internal static SoundEffect
            //Buttons
            SND_BUTTON_BASIC,

            //Projectiles
            SND_ROCKET_IMPACT,
            SND_LASER,
            SND_EXPLOSION,

            //Enemies
            SND_HELICOPTER_LOOPING,
            SND_WILHELM_SCREAM;

        public static void Initialize(ContentManager Content)
        {
            soundManagerSFX = new SoundManager();
            //Buttons
            SND_BUTTON_BASIC = Content.Load<SoundEffect>("Content/SoundEffects/Buttons/click_basic");
            //Projectiles
            SND_LASER = Content.Load<SoundEffect>("Content/SoundEffects/Projectiles/laser02");
            SND_ROCKET_IMPACT = Content.Load<SoundEffect>("Content/SoundEffects/Projectiles/rocketImpact");
            SND_EXPLOSION = Content.Load<SoundEffect>("Content/SoundEffects/Projectiles/explosion");

            //Enemies
            SND_HELICOPTER_LOOPING = Content.Load<SoundEffect>("Content/SoundEffects/Enemies/helicopterLoop");
            SND_WILHELM_SCREAM = Content.Load<SoundEffect>("Content/SoundEffects/Enemies/wilhemScream");
        }

       // public static void PlaySound(SoundEffect snd)
        //{
            //soundManagerSFX.PlaySound(snd);
       // }
    }
}
