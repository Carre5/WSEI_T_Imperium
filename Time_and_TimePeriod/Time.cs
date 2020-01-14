using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_and_TimePeriod
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte hours, minutes, seconds;

        public byte Hours{ get { return hours; } }
        public byte Minutes { get { return minutes; } }
        public byte Seconds { get { return seconds; } }
        public Time(int _hours, int _minutes, int _seconds)
        {

            Time pomT = Normalize(_hours, _minutes, _seconds);

            hours = pomT.hours;
            minutes = pomT.minutes;
            seconds = pomT.seconds;
        }

        public Time(string time)
        {
            string[] pom = time.Split(':');

            try
            {
                Time pomT = Normalize(Int32.Parse(pom[0]), Int32.Parse(pom[1]), Int32.Parse(pom[2]));

                hours = pomT.hours;
                minutes = pomT.minutes;
                seconds = pomT.seconds;
            }
            catch(Exception exp )
            {
                hours = 0;
                minutes = 0;
                seconds = 0;
                Console.WriteLine("Operacja niedozwolona: {0}", exp.ToString());
            }
        }

        public Time(DateTime date)
        {
            hours = (byte) date.Hour;
            minutes = (byte) date.Minute;
            seconds = (byte) date.Second;

            //Nie wymaga normalizacji, bo zapewnia to sama klasa
        }

        /// <summary>  
        ///  Converts the Time value to its string equivalent  
        /// </summary>
        public override string ToString()
        {
            return "" + (hours < 10 ? "0" + hours : "" + hours) +
                ":" + (minutes < 10 ? "0" + minutes : "" + minutes) +
                ":" + (seconds < 10 ? "0" + seconds : "" + seconds);
        }

        /// <summary>  
        ///  Checks if Time is equal to other Time
        /// </summary> 
        public bool Equals(Time other)
        {
            return this == other;
        }

        /// <summary>  
        ///  Compares Time to other Time 
        /// </summary> 
        public int CompareTo(Time other)
        {
            if (this > other)
            {
                return 1;
            }
            else if (this == other)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public static bool operator ==(Time t1, Time t2)
        {
            if (t1.hours == t2.hours && t1.minutes == t2.minutes && t1.seconds == t2.seconds)
                return true;
            return false;
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1 == t2);
        }

        public static bool operator >(Time t1, Time t2)
        {
            if (t1.hours > t2.hours)
            {
                return true;
            }
            else if (t1.hours == t2.hours)
            {
                if (t1.minutes > t2.minutes)
                {
                    return true;
                }
                else if (t1.minutes == t2.minutes)
                {
                    if (t1.seconds > t2.seconds)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator <(Time t1, Time t2)
        {
            if (t1.hours < t2.hours)
            {
                return true;
            }
            else if (t1.hours == t2.hours)
            {
                if (t1.minutes < t2.minutes)
                {
                    return true;
                }
                else if (t1.minutes == t2.minutes)
                {
                    if (t1.seconds < t2.seconds)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator >=(Time t1, Time t2)
        {
            if (t1.hours > t2.hours)
            {
                return true;
            }
            else if (t1.hours == t2.hours)
            {
                if (t1.minutes > t2.minutes)
                {
                    return true;
                }
                else if (t1.minutes == t2.minutes)
                {
                    if (t1.seconds >= t2.seconds)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator <=(Time t1, Time t2)
        {
            if (t1.hours < t2.hours)
            {
                return true;
            }
            else if (t1.hours == t2.hours)
            {
                if (t1.minutes < t2.minutes)
                {
                    return true;
                }
                else if (t1.minutes == t2.minutes)
                {
                    if (t1.seconds <= t2.seconds)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static Time operator +(Time t1, Time t2)
        {
            return Normalize(t1.hours + t2.hours, t1.minutes + t2.minutes, t1.seconds + t2.seconds);
        }
        public static Time operator -(Time t1, Time t2)
        {
            int hour = t1.hours - t2.hours;
            int minute = t1.minutes - t2.minutes;
            int second = t1.seconds - t2.seconds;

            if (second < 0)
            {
                second += 60;
                minute--;
            }

            if (minute < 0)
            {
                minute += 60;
                hour--;
            }

            if (hour < 0)
            {
                hour += 24;
            }

            return Normalize(hour, minute, second);
        }

        public Time Plus(TimePeriod tp)
        {
            int[] normalizedTp = tp.Normalize();

            return Normalize(hours + normalizedTp[0], minutes + normalizedTp[1], seconds + normalizedTp[2]);
        }

        public Time Minus(TimePeriod tp)
        {
            int[] normalizedTp = tp.Normalize();

            int hour = hours - normalizedTp[0];
            int minute = minutes - normalizedTp[1];
            int second = seconds - normalizedTp[2];

            if (second < 0)
            {
                second += 60;
                minute--;
            }

            if (minute < 0)
            {
                minute += 60;
                hour--;
            }

            if (hour < 0)
            {
                hour += 24;
            }

            return Normalize(hour, minute, second);
        }

        //Konwersja przekazanych wartości w akceptowalne dla struktury dane
        private static Time Normalize(int hour, int minute, int second)
        {
            int normalizedHour = 0;
            int normalizedMinute = 0;
            int normalizedSecond = 0;

            ///////////////////////

            if (hour >= 24)
                normalizedHour = hour % 24;
            else if (hour < 0)
                normalizedHour = (hour * -1) % 24;
            else
                normalizedHour = hour;

            ///////////////////////

            if (minute >= 60)
                normalizedMinute = minute % 60;
            else if (minute < 0)
                normalizedMinute = (minute * -1) % 60;
            else
                normalizedMinute = minute;

            ///////////////////////

            if (second >= 60)
                normalizedSecond = second % 60;
            else if (second < 0)
                normalizedSecond = (second * -1) % 60;
            else
                normalizedSecond = second;

            //wykorzystuję konstruktor z DateTime, ponieważ jako jedyny z konstruktorów nie wymaga normalizacji, co powodowałoby pętle
            return new Time(new DateTime(1, 1, 1, (byte) normalizedHour, (byte) normalizedMinute, (byte) normalizedSecond));
        }
    }
}
