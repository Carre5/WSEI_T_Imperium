using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_and_TimePeriod
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        readonly long period;

        public TimePeriod(long seconds)
        {
            period = seconds;
        }

        public TimePeriod(Time t1, Time t2)
        {
            period = 0;
            int p1 = (t1.Hours * 3600) + (t1.Minutes * 60) + t1.Seconds;
            int p2 = (t2.Hours * 3600) + (t2.Minutes * 60) + t2.Seconds;

            period = Math.Abs( p1 - p2 );
        }

        public TimePeriod(int h, int m)
        {
            period = (h * 3600) + (m * 60);
        }

        public TimePeriod(int h, int m, int s)
        {
            period = (h * 3600) + (m * 60) + s;
        }

        public TimePeriod(string tp)
        {
            period = 0;
            string[] pom = tp.Split(':');

            try
            {
                period += Int32.Parse(pom[0])*3600;
                period += Int32.Parse(pom[1])*60;
                period += Int32.Parse(pom[2]);
            }
            catch (Exception exp)
            {
                period = 0;
                Console.WriteLine("Operacja niedozwolona: {0}", exp.ToString());
            }
        }

        public TimePeriod(Time t1, TimePeriod tp1)
        {
            int p1 = (t1.Hours * 3600) + (t1.Minutes * 60) + t1.Seconds;

            period = p1 + tp1.period;
        }
        public override string ToString()
        {
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            long pom = period;

            hours = (int) pom / 3600;

            pom -= hours * 3600;

            minutes = (int)pom / 60;

            pom -= minutes * 60;

            seconds = (int) pom;

            return ""+hours+":"+minutes+":"+seconds;
        }

        public int[] Normalize()
        {
            string pom = ToString();

            string[] strTab = pom.Split(':');

            return new int[] { Int32.Parse(strTab[0]), Int32.Parse(strTab[1]), Int32.Parse(strTab[2]) };
        }

        public bool Equals(TimePeriod other)
        {
            return (this.period == other.period);
        }

        public int CompareTo(TimePeriod other)
        {
            if (this.period > other.period)
            {
                return 1;
            }
            else if (this.period == other.period)
            {
                return 0;
            }
            else{
                return -1;
            }
        }

        public static bool operator <(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1.period < tp2.period)
                return true;
            return false;
        }

        public static bool operator <=(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1.period > tp2.period)
                return false;
            return true;
        }
        public static bool operator ==(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1.period == tp2.period)
                return true;
            return false;
        }

        public static bool operator !=(TimePeriod tp1, TimePeriod tp2)
        {
            return !(tp1 == tp2);
        }

        public static bool operator >=(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1.period < tp2.period)
                return false;
            return true;
        }

        public static bool operator >(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1.period > tp2.period)
                return true;
            return false;
        }

        public TimePeriod Plus(TimePeriod tp)
        {
            return new TimePeriod(period + tp.period);
        }

        public TimePeriod Plus(TimePeriod tp1, TimePeriod tp2)
        {
            return new TimePeriod(tp1.period + tp2.period);
        }

        public TimePeriod Minus(TimePeriod tp)
        {
            return new TimePeriod(period - tp.period);
        }

        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Plus(tp2);
        }

        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Minus(tp2);
        }
    }
}
