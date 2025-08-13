using System;

namespace ExerciseTracking
{
    // Cycling activity - tracks speed
    public class Cycling : Activity
    {
        private double _speed; // in mph

        public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
        {
            _speed = speed;
        }

        public override double GetDistance()
        {
            return (_speed * Minutes) / 60; // miles
        }

        public override double GetSpeed()
        {
            return _speed;
        }

        public override double GetPace()
        {
            return 60 / _speed; // minutes per mile
        }

        public override string GetActivityName()
        {
            return "Cycling";
        }
    }
}