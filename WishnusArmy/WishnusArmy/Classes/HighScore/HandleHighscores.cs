using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Fonts;
using static Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class HandleHighscores : GameObject
{
    
    OnlineHighScore highscoreHandler;
    private highScoreTable scoreBoard;

    private Vector2 posName;
    private Vector2 posScore;

    private int updateTimer;

    public HandleHighscores()
    {
        highscoreHandler = new OnlineHighScore();
        scoreBoard = highscoreHandler.getScores();
    }

    public override void Update(object gameTime)
    {
        if (updateTimer > 500)
        {
            scoreBoard = highscoreHandler.getScores();
            Console.WriteLine("update...");
            updateTimer = 0;
        }

        updateTimer++;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(SCREEN_SIZE.X - 500, 340), new Point(480, 570)), spriteBatch, Color.Black, 0.7f);
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(20, 340), new Point(500, 570)), spriteBatch, Color.Black, 0.7f);

        ScoreDrawer(spriteBatch);
    }

    public void SubmitScore(string name, int waves, int kills, int resources, int score)
    {
        highscoreHandler.sendScore(name, waves, kills, resources, score);
        //scoreBoard = highscoreHandler.getScores(); Slows down the system too much, better to wait on the updateTimer interval
    }

    public void FetchScore()
    {
        scoreBoard = highscoreHandler.getScores();
    }

    public void ScoreDrawer(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 5) //Left highscore screen
            {
                spriteBatch.DrawString(FNT_GAMESTATS, "HIGHSCORES 1 - 5", new Vector2(105, 365), Color.White);

                posName = new Vector2(30, 425 + i * ((SCREEN_SIZE.Y - 600) / MAXSIZE_HIGHSCORELIST * 2));
                posScore = new Vector2(30, 463 + i * ((SCREEN_SIZE.Y - 600) / MAXSIZE_HIGHSCORELIST * 2));
            }else
            {
                spriteBatch.DrawString(FNT_GAMESTATS, "HIGHSCORES 6 - 10", new Vector2(SCREEN_SIZE.X - 425, 365), Color.White);

                posName = new Vector2(SCREEN_SIZE.X - 470, 425 + (i - 5) * ((SCREEN_SIZE.Y - 600) / MAXSIZE_HIGHSCORELIST * 2));
                posScore = new Vector2(SCREEN_SIZE.X - 470, 463 + (i - 5) * ((SCREEN_SIZE.Y - 600) / MAXSIZE_HIGHSCORELIST * 2));
            }

            try
            {
                if (scoreBoard.Names != null)
                {
                    spriteBatch.DrawString(FNT_GAMESTATS, scoreBoard.Names[i], posName, Color.White);
                }

                if (scoreBoard.Scores != null)
                {
                    spriteBatch.DrawString(FNT_GAMESTATS, scoreBoard.Scores[i].ToString(), posScore, Color.White);
                }
            }catch(Exception e)
            {
                spriteBatch.DrawString(FNT_GAMESTATS, "Unable to connect...", new Vector2(95, 600), Color.White);
                spriteBatch.DrawString(FNT_GAMESTATS, "Unable to connect...", new Vector2(SCREEN_SIZE.X - 420, 600 ), Color.White);
            }


        }
    }
}
