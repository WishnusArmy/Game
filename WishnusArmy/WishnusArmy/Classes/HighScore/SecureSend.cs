using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class OnlineHighScore
{

    private string hashString(string _value)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.ASCII.GetBytes(_value);
        data = x.ComputeHash(data);
        string ret = "";
        for (int i = 0; i < data.Length; i++) ret += data[i].ToString("x2").ToLower();
        return ret;
    }

    //The following code can be called to send out a new score to the server. It returns the new unique server ID of the high score entry as string.
    public string sendScore(string name, int waves, int kills, int resources, int score)
    {
        string highscoreString = name + waves + kills + resources + score + "SuperSecretPasswordString";
        string postString = "&Name=" + name + "&Waves=" + waves + "&Kills=" + kills + "&Resources=" + resources + "&Score=" + score + "&Hash=" + hashString(highscoreString);
        string response = null;
        response = webPost("https://wishnusarmy.000webhostapp.com/newscore.php", postString);
        Console.WriteLine(postString);
        Console.WriteLine(response);
        return response.Trim();
    }
}
