using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Attraction
{
    public Attraction()
    {
        cal = new Calendar();
    }
    public double score;
    public string id;
    public string name;
    public double lon, lat;
    public Calendar cal;
}

