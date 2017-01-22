using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static ContentImporter.Fonts;
using static Constant;

public class GameOverState : GameObjectList
{
    protected Button backButton;
    private ParticleController pc;

    public GameOverState()
    {
        pc = new ParticleController();
        Add(pc);

        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2((SCREEN_SIZE.X - backButton.Dimensions.X) / 2, SCREEN_SIZE.Y - 150);
        Add(backButton);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
        }

        if (RANDOM.NextDouble() > 0.97)
        {
            pc.AddExplosion(new Vector2(RANDOM.Next(0, 1920), RANDOM.Next(0, 1080)));
            PlaySound(ContentImporter.Sounds.SND_EXPLOSION);
        }

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_GAMEOVERBACKGROUND, new Vector2(0, 0), Color.White);
        MainMenu.AddNoise(spriteBatch, 5);

        base.Draw(gameTime, spriteBatch);

        //All gamestats can be written here
        spriteBatch.DrawString(FNT_GAMEOVER, "KILLED ENEMIES: " + GameStats.TotalEnemiesKilled.ToString(), new Vector2((SCREEN_SIZE.X - FNT_GAMEOVER.MeasureString("KILLED ENEMIES: " + GameStats.TotalEnemiesKilled.ToString()).X) / 2, SCREEN_SIZE.Y - 700), Color.Black);
        spriteBatch.DrawString(FNT_GAMEOVER, "WAVES CONQUERED: " + (GameStats.Wave-1).ToString(), new Vector2((SCREEN_SIZE.X - FNT_GAMEOVER.MeasureString("WAVES CONQUERED: " + GameStats.Wave.ToString()).X) / 2, SCREEN_SIZE.Y - 600), Color.Black);
        spriteBatch.DrawString(FNT_GAMEOVER, "RESOURCES GATHERED: " + GameStats.totalResourcesGathered.ToString(), new Vector2((SCREEN_SIZE.X - FNT_GAMEOVER.MeasureString("RESOURCES GATHERED: " + GameStats.EcResources.ToString()).X) / 2, SCREEN_SIZE.Y - 500), Color.Black);


    }
}

