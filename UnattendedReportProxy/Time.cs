using System;

namespace UnattendedReportProxy
{
    struct Time
    {
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int Millisecond { get; }

        public static Time Now
        {
            get
            {
                return new Time(DateTime.Now);
            }
        }


        public Time(int hour)
        {
            Hour = hour;
            Minute = 0;
            Second = 0;
            Millisecond = 0;
        }
        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
            Second = 0;
            Millisecond = 0;
        }
        public Time(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = 0;
        }
        public Time(int hour, int minute, int second, int millisecond)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }

        public Time(DateTime dateTime)
        {
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;
            Millisecond = dateTime.Millisecond;
        }

        public static long operator -(Time a, Time b)
        {
            if (a == b) return 0;
            long offset = 0;
            offset += a.Millisecond - b.Millisecond;
            offset += (a.Second - b.Second) * 1000;
            offset += (a.Minute - b.Minute) * 1000 * 60;
            offset += (a.Hour - b.Hour) * 1000 * 60 * 60;
            if (offset < 0)
                offset += 1000 * 60 * 60 * 24;
            return offset;
        }
        public static bool operator ==(Time a, Time b)
        {
            return a.Hour == b.Hour && a.Minute == b.Minute;
        }
        public static bool operator !=(Time a, Time b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Hour}:{Minute}:{Second}.{Millisecond}";
        }

        public static bool isAvailable(string strTime, out Time time)
        {
            int t;
            if (!int.TryParse(strTime, out t) || t < 0 || t > 2359 || t % 10 > 59)
            {
                time = Now;
                return false;
            }
            else
            {
                time = new Time(t / 100, t % 100);
                return true;
            }
        }
    }
}
