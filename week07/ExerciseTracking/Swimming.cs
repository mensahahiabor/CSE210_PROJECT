using System;

namespace ExerciseTracking
{
    // Swimming activity - tracks number of laps
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            // Distance (miles) = swimming laps * 50 / 1000 * 0.62
            return (_laps * 50.0 / 1000.0) * 0.62;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / Minutes) * 60; // mph
        }

        public override double GetPace()
        {
            return Minutes / GetDistance(); // minutes per mile
        }

        public override string GetActivityName()
        {
            return "Swimming";
        }
    }
}