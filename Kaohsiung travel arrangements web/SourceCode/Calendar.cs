using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Calendar
{
    public string attribute;
    public Dictionary<int, List<string[]>> calendar = new Dictionary<int, List<string[]>>();

    public Calendar()
    {
        for (int i = 1; i <= 7; ++i)
            calendar[i] = new List<string[]>();
    }
}

/*public class Opentime
{
    void Main(string[] args)
    {
        Calendar[] c = new Calendar[2];
        for (int i = 0; i < c.Length; ++i)
            c[i] = new Calendar();

        c[0].attribute = "all";
        c[1].attribute = "part";
        c[1].calendar[2].Add(new string[]{"08:00", "12:00"});
        Console.WriteLine(JsonConvert.SerializeObject(c));

        Calendar[] c;
        string text = File.ReadAllText("attractions-opentime.json");

        c = JsonConvert.DeserializeObject<Calendar[]>(text);
        foreach(var data in c)
        {
            Console.WriteLine(data.attribute);
        }
        Console.Read();
    }   
}*/





