using System;

namespace ExerciseTracking
{
    // Running activity - tracks distance
    public class Running : Activity
    {
        private double _distance; // in miles

        public Running(DateTime date, int minutes, double distance) : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance()
        {
            return _distance;
        }

        public override double GetSpeed()
        {
            return (_distance / Minutes) * 60; // mph
        }

        public override double GetPace()
        {
            return Minutes / _distance; // minutes per mile
        }

        public override string GetActivityName()
        {
            return "Running";
        }
    }
}