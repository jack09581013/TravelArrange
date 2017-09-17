using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Attraction: IComparable<Attraction>
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

    public int CompareTo(Attraction other)
    {
        if (other.score < score) return -1;
        else if (other.score == score) return 0;
        else return 1;
    }
}

