using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MDTRA
{
    public MDTRA(Attraction[] attr, TravelTime[][] tt)
    {
        this.attr = attr;
        this.tt = tt;        
    }

    private Attraction[] attr;
    private TravelTime[][] tt;
    private int[][] priority;
    private Arrangement[][] arr;

    public Arrangement[][] arrangeAttractions()
    {
        priority = new int[tt.Length][];
        for (int i = 0; i < priority.Length; ++i)
            priority[i] = new int[tt[i].Length];

        arr = new Arrangement[tt.Length][];
        for (int i = 0; i < arr.Length; ++i)
            arr[i] = new Arrangement[tt[i].Length];

        for (int top = 0; top < attr.Length; ++top)
        {
            if (isAllArrange()) break;

            List<ArrangementTemp> buffer = new List<ArrangementTemp>();
            for(int day = 0; day<tt.Length; ++day)
            {
                ArrangementTemp data = arrangeAttraction(top, day);
                if (data.addDistance != -1)
                    buffer.Add(data);
            }
            if(buffer.Count > 0)
            {
                buffer.Sort(ArrangementTemp.compare);
                double addDistance = buffer[0].addDistance;
                priority[buffer[0].day] = buffer[0]._priority;
                arr[buffer[0].day] = buffer[0]._arr;
            }
        }
        return arr;
    }

    public static string arrangementToString(Arrangement[][] arrs)
    {
        string schedule = "";
        for (int day=0; day<arrs.Length; ++day)
        {
            for(int i=0; i<arrs[day].Length; ++i)
            {
                if (arrs[day][i] == null)
                    schedule += "#";
                else
                    schedule += arrs[day][i].attr.name;
            }
            schedule += "-";
        }
        return schedule.Substring(0, schedule.Length - 1);
    }


    private bool isAllArrange()
    {
        for (int i = 0; i < priority.Length; ++i)
            for (int j = 0; j < priority[i].Length; ++j)
                if (priority[i][j] != -1)
                    return false;
        return true;
    }

    private ArrangementTemp arrangeAttraction(int top, int day)
    {
        //Copy priority
        int[] _priority = new int[priority[day].Length];
        for(int i=0; i<_priority.Length; ++i)        
            _priority[i] = priority[day][i];

        //Copy arrangement
        Arrangement[] _arr = new Arrangement[arr[day].Length];
        for (int i = 0; i < _arr.Length; ++i)        
            if(arr[day][i] != null)
            {
                _arr[i] = new Arrangement();
                _arr[i].attr = arr[day][i].attr;
                _arr[i].startTime = arr[day][i].startTime;
                _arr[i].endTime = arr[day][i].endTime;
            }                      
        
        
        Calendar cal = attr[top].cal;
        List<int> positionList = new List<int>();
        bool hasArrange = false;
        double originDistance = arrangementDistance(_arr);

        for (int p=2; p>=0; p--)        
            for (int i = 0; i < priority[day].Length; ++i)
                if (priority[day][i] == p)
                    positionList.Add(i);

        foreach(int i in positionList)
        {
            if (hasArrange) break;
            TravelTime chooseTime = tt[day][i];
            if(cal.attribute == "all")
            {
                _priority[i] = -1;
                if (i != 0 && _priority[i - 1] != -1) _priority[i - 1] += 1;
                if (i != _priority.Length - 1 && _priority[i + 1] != -1) _priority[i + 1] += 1;
                
                _arr[i] = new Arrangement();
                _arr[i].startTime = chooseTime.startTime;
                _arr[i].endTime = chooseTime.endTime;
                _arr[i].attr = attr[top];
                hasArrange = true;

            }
            else if(cal.attribute == "none")
            {
                //Do nothing
            }
            else if (cal.attribute == "part")
            {
                foreach (string[] opentimes in cal.calendar[chooseTime.week])
                {
                    if (string.Compare(opentimes[0], chooseTime.endTime) < 0 &&
                        string.Compare(chooseTime.startTime, opentimes[1]) < 0)
                    {
                        string maxStart = string.Compare(opentimes[0], chooseTime.startTime) < 0 ? chooseTime.startTime : opentimes[0];
                        string minEnd = string.Compare(opentimes[1], chooseTime.endTime) < 0 ? opentimes[1] : chooseTime.endTime;

                        DateTime maxStartTime = DateTime.Parse("2000-1-1 " + maxStart + ":00");
                        DateTime minEndTime = DateTime.Parse("2000-1-1 " + minEnd + ":00");
                        TimeSpan span = minEndTime - maxStartTime;
                        TimeSpan threshold = new TimeSpan(1, 0, 0);
                        TimeSpan standard = new TimeSpan(0, 0, 0);
                        if (span - threshold >= standard)
                        {
                            _priority[i] = -1;
                            if (i != 0 && _priority[i - 1] != -1) _priority[i - 1] += 1;
                            if (i != _priority.Length - 1 && _priority[i + 1] != -1) _priority[i + 1] += 1;

                            _arr[i] = new Arrangement();
                            _arr[i].startTime = chooseTime.startTime;
                            _arr[i].endTime = chooseTime.endTime;
                            _arr[i].attr = attr[top];
                            hasArrange = true;
                        }
                    }
                }
            }
        }

        double addDistance;
        if (hasArrange)
            addDistance = arrangementDistance(_arr) - originDistance;
        else addDistance = -1;

        ArrangementTemp data = new ArrangementTemp();
        data.addDistance = addDistance;
        data._priority = _priority;
        data._arr = _arr;
        data.day = day;
        return data;
    }

    private double arrangementDistance(Arrangement[] _arr)
    {
        double distance = 0;
        bool first = true;
        double preLon = 0, preLat = 0;

        foreach(Arrangement a in _arr)
        {
            if(a != null)
            {
                double lon = a.attr.lon;
                double lat = a.attr.lat;

                if (first)
                {
                    preLon = lon;
                    preLat = lat;
                    first = false;
                }
                else
                {
                    double x = (preLon - lon) * 101779;
                    double y = (preLat - lat) * 110840;
                    distance += Math.Sqrt(x*x + y*y);
                }
            }
        }
        return distance;
    }
}

class ArrangementTemp
{
    public static int compare(ArrangementTemp a, ArrangementTemp b)
    {
        if (a.addDistance > b.addDistance) return 1;
        else if (a.addDistance == b.addDistance) return 0;
        else return -1;
    }

    public double addDistance;
    public int[] _priority;
    public Arrangement[] _arr;
    public int day;
}

