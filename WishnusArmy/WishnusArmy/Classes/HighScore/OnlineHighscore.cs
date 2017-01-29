using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

partial class OnlineHighScore 
{
    private highScoreTable parseToHighscoreTable(string tableString)
    {
        const string SERVER_VALID_DATA_HEADER = "SERVER_";
        if (tableString.Trim().Length < SERVER_VALID_DATA_HEADER.Length ||
        !tableString.Trim().Substring(0, SERVER_VALID_DATA_HEADER.Length).Equals(SERVER_VALID_DATA_HEADER)) return null;
        string toParse = tableString.Trim().Substring(SERVER_VALID_DATA_HEADER.Length);

        string[] names = new string[10];
        int[] kills = new int[10];
        int[] waves = new int[10];
        int[] resources = new int[10];
        int[] scores = new int[10];
        int[] ranks = new int[10];

        string[] rows = Regex.Split(toParse, "_ROW_");
        for (int i = 0; i < 10; i++)
        {
            if (rows.Length > i && rows[i].Trim() != "")
            {
                string[] cols = Regex.Split(rows[i], "_COL_");
                if (cols.Length == 6)
                {
                    names[i] = cols[0].Trim();
                    kills[i] = int.Parse(cols[1]);
                    waves[i] = int.Parse(cols[2]);
                    resources[i] = int.Parse(cols[3]);
                    scores[i] = int.Parse(cols[4]);
                    ranks[i] = int.Parse(cols[5].Trim());
                }
            }
            else
            {
                names[i] = "";
                kills[i] = 0;
                waves[i] = 0;
                resources[i] = 0;
                scores[i] = 0;
                ranks[i] = 0;
            }
        }
        return new highScoreTable(ranks, names, kills, waves, resources, scores);
    }

    private string webPost(string _URL, string _postString)
    {
        const string REQUEST_METHOD_POST = "POST";
        const string CONTENT_TYPE = "application/x-www-form-urlencoded";
        Stream dataStream = null;
        StreamReader reader = null;
        WebResponse response = null;
        string responseString = null;

        // Create a request using a URL that can receive a post.
        WebRequest request = WebRequest.Create(_URL);
        // Set the Method property of the request to POST.
        request.Method = REQUEST_METHOD_POST;
        // Create POST data and convert it to a byte array.
        string postData = _postString;
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        // Set the ContentType property of the WebRequest.
        request.ContentType = CONTENT_TYPE;
        // Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length;
        // Get the request stream.
        dataStream = request.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.
        dataStream.Close();
        // Get the response.
        response = request.GetResponse();
        // Display the status.
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        reader = new StreamReader(dataStream);
        // Read the content.
        responseString = reader.ReadToEnd();
        // Clean up the streams.
        if (reader != null) reader.Close();
        if (dataStream != null) dataStream.Close();
        if (response != null) response.Close();

        return responseString;
    }

    
    public highScoreTable getScores()
    {
        //string postString = "&Format=" + format;
        return parseToHighscoreTable(webPost("https://wishnusarmy.000webhostapp.com/connectandget.php", ""));
    }
}

