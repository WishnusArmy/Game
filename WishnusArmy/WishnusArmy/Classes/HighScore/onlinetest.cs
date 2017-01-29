using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class onlinetest
{
    OnlineHighScore hs;
    highScoreTable table;

    public onlinetest()
    {
        hs = new OnlineHighScore();
        table = hs.getScores();

    }

    public void zendtest()
    {
        //hs.sendScore("ONLINEHIGHSCORESFUCKYEA", 3424, 324, 4324, 23423);
        /*
        for (int i = 0; i < 10; i++)
            if (table.names[i] != null)
                Console.WriteLine(table.names[i]);
        */
    }
}
