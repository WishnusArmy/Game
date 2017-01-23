using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;
using static ContentImporter.Sprites;
using static Constant;
using System.Xml;

/*
    scores afdrukken als:

    foreach(Score s in GameWorld.FindByType<HighScore>()[0].List){
        drawText(s.ToString(), font, .. , ..);
    }

    score toevoegen als:
    
    GameWorld.FindByType<HighScore>()[0].AddScore("Your Name", 420);
 */

public class HighScore : GameObject
{

    private List<Score> highScores;
    private int maxSizeOfList;

    public HighScore()
    {
        maxSizeOfList = MAXSIZE_HIGHSCORELIST;
        highScores = new List<Score>();
        ReadHighScores();
    }

    public List<Score> List
    {
        get
        {
            return highScores;
        }
    }

    public void AddScore(string name, int score)
    {
        highScores.Add(new Score(name, score));
        Sort();
        MaxSize();
        WriteHighScores();
    }

    private void MaxSize()
    {
        if (highScores.Count > maxSizeOfList)
        highScores = highScores.GetRange(0, maxSizeOfList - 1);
    }
    
    private void Sort()
    {
        if (highScores.Count < 2)
            return;
        int n = highScores.Count;
        for (int i = 0; i < n; i++)
        {
            Score best = highScores[i];
            for( int r = i+1; r<n; r++)
            {
                Score s = highScores[r];
                if (s.greaterThan(best))
                {
                    best = s;
                }
            }
            highScores.Remove(best);
            highScores.Insert(i, best);
        }
    }

    public bool Worthy(int score)
    {
        if (highScores.Count < MAXSIZE_HIGHSCORELIST)
            return true;
        foreach (Score s in highScores)
        {
            if ((new Score("", score)).greaterThan(s))
            {
                return true;
            }
        }
        return false;
    }

    private void ReadHighScores()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Content/Content/Stats/HighScores.xml");

            foreach (XmlNode score in doc.DocumentElement.ChildNodes)
            {
                string nodeName = score.SelectSingleNode("Name").InnerText;
                int nodeScore = Int32.Parse(score.SelectSingleNode("Amount").InnerText);
                highScores.Add(new Score(nodeName, nodeScore));
            }
        }
        catch(Exception e)
        {
            AddScore("werkt niet", 666);
            Console.WriteLine("Kan geen Highscore inlezen");
        }
    }

    private void WriteHighScores()
    {
        try
        {
            XmlDocument doc = new XmlDocument();

            XmlElement highscore = doc.CreateElement("HighScore");
            doc.AppendChild(highscore);

            foreach (Score s in highScores)
            {
                XmlElement score = doc.CreateElement("Score");

                XmlElement name = doc.CreateElement("Name");
                name.AppendChild(doc.CreateTextNode(s.Name));

                XmlElement amount = doc.CreateElement("Amount");
                amount.AppendChild(doc.CreateTextNode("" + s.Amount));

                score.AppendChild(name);
                score.AppendChild(amount);
                highscore.AppendChild(score);
            }

            doc.Save("Content/Content/Stats/HighScores.xml");
        }
        catch(Exception e)
        {
            Console.WriteLine("Kan HighScore niet wegschrijven");
        }
    }
}

public class Score
{
    private string name;
    private int score;

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public override string ToString()
    {
        return score + " " + name;
    }

    public bool greaterThan(Score that)
    {
        if (this.score > that.score)
            return true;
        if (this.score < that.score)
            return false;
        if (this.name.CompareTo(that.name) == -1)
            return true;
        return false;
    }

    public string Name
    {
        get
        {
            return name;
        }
    }
    public int Amount
    {
        get
        {
            return score;
        }
    }
}
