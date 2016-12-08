using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HashContainer
{
    public string id, type, value;

    public HashContainer(string str)
    {
        string[] sub = str.Split('.');
        id = sub[0];
        type = sub[1];
        value = sub[2];
    }
}