using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static Constant;
using Microsoft.Xna.Framework.Media;

public class LeaderBoard : GameObjectList
{
    private OnlineHighScore highscoreManager;
    private highScoreTable board;

    public LeaderBoard()
    {
        highscoreManager = new OnlineHighScore();
        board = highscoreManager.getScores();
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_LEADERBOARD, new Vector2(0, 0), Color.White);
        MainMenu.AddNoise(spriteBatch, 5);

        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(50, 50), new Point(SCREEN_SIZE.X - 100, SCREEN_SIZE.Y - 100)), spriteBatch, Color.Black, 0.7f);

        //Title
        //spriteBatch.DrawString(Fonts.FNT_GAMEOVER, "LEADERBOARD", new Vector2(((SCREEN_SIZE.X / 2 + 300) - (Fonts.FNT_GAMEOVER.MeasureString("LEADERBOARD").X)) / 2, 60), Color.White);
   
        //Column names
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "RANK", new Vector2(60, 50), Color.White);
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "NAME", new Vector2((SCREEN_SIZE.X - 100) / 6, 50), Color.White);
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "WAVE", new Vector2((SCREEN_SIZE.X - 100) / 6 * 2, 50), Color.White);
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "KILLS", new Vector2((SCREEN_SIZE.X - 100) / 6 * 3, 50), Color.White);
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "RESOURCES", new Vector2((SCREEN_SIZE.X - 100) / 6 * 4, 50), Color.White);
        spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "SCORE", new Vector2((SCREEN_SIZE.X) / 6 * 5, 50), Color.White);

        ScoreDrawer(spriteBatch);

        base.Draw(gameTime, spriteBatch);
    }

    public void ScoreDrawer(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < 10; i++)
        {
            //Ranks
            spriteBatch.DrawString(Fonts.FNT_ULTIMATE, (i+1).ToString() + ".", new Vector2(105 - Fonts.FNT_ULTIMATE.MeasureString((i + 1).ToString()).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);

            try
            {
                if (board.Names[i] != "")
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, board.Names[i], new Vector2(355 - Fonts.FNT_ULTIMATE.MeasureString(board.Names[i]).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);
                else
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, "TBD", new Vector2(355 - Fonts.FNT_ULTIMATE.MeasureString("TBD").X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);

                if (board.Waves[i].ToString() != null)
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, board.Waves[i].ToString(), new Vector2(658 - Fonts.FNT_ULTIMATE.MeasureString(board.Waves[i].ToString()).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);

                if (board.Kills[i].ToString() != null)
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, board.Kills[i].ToString(), new Vector2(961 - Fonts.FNT_ULTIMATE.MeasureString(board.Kills[i].ToString()).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);

                if (board.Resources[i].ToString() != null)
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, board.Resources[i].ToString(), new Vector2(1315 - Fonts.FNT_ULTIMATE.MeasureString(board.Resources[i].ToString()).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);

                if (board.Scores[i].ToString() != null)
                    spriteBatch.DrawString(Fonts.FNT_ULTIMATE, board.Scores[i].ToString(), new Vector2(1663 - Fonts.FNT_ULTIMATE.MeasureString(board.Scores[i].ToString()).X / 2, 120 + i * (930 / MAXSIZE_HIGHSCORELIST)), Color.White);
            }catch(Exception e)
            {
                spriteBatch.DrawString(Fonts.FNT_GAMEOVER, "CANNOT CONNECT TO INTERNET...", new Vector2(SCREEN_SIZE.X - Fonts.FNT_GAMEOVER.MeasureString("CANNOT CONNECT TO INTERNET...").X, SCREEN_SIZE.Y - Fonts.FNT_GAMEOVER.MeasureString("CANNOT CONNECT TO INTERNET...").Y)/2, Color.White);
            }
        }
    }
}

