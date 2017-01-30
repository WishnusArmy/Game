using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static ContentImporter.Fonts;
using static Constant;

public class GameOver : GameObjectList
{
    protected Button backButton;
    protected Button clearHighScoreButton;
    protected TextForm scoreForm;
    private ParticleController pc;
    private HandleHighscores handler;

    protected bool scoreAdded;
    protected bool worthy;

    public GameOver()
    {
        pc = new ParticleController();
        Add(pc);

        handler = new HandleHighscores();

        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2((SCREEN_SIZE.X - backButton.Dimensions.X) / 2, SCREEN_SIZE.Y - 70) + new Vector2(30, 0);
        Add(backButton);

        scoreForm = new TextForm(new Rectangle(new Point(SCREEN_SIZE.X/2 - 240, 780), new Point(480, 145)), FNT_GAMESTATS, "Enter your name here!", "", Color.Black, Color.White);

        scoreAdded = false;
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        handler.Update(gameTime);

        if (!scoreAdded)
        {
            Add(scoreForm);
            scoreAdded = true;
        }

        if (backButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");

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
        spriteBatch.DrawString(FNT_GAMEOVER, "SCORE: " + GameStats.FinalScore, new Vector2((SCREEN_SIZE.X - FNT_GAMEOVER.MeasureString("SCORE: " + GameStats.FinalScore).X) / 2, SCREEN_SIZE.Y - 400), Color.Black);

        handler.Draw(gameTime, spriteBatch);

    }
}