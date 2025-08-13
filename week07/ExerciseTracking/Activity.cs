using System;

namespace ExerciseTracking
{
    // Base Activity class with shared attributes and abstract methods
    public abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        protected DateTime Date => _date;
        protected int Minutes => _minutes;

        // Abstract methods to be implemented by derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();
        public abstract string GetActivityName();

        // Virtual method that can be overridden if needed
        public virtual string GetSummary()
        {
            return $"{_date:dd MMM yyyy} {GetActivityName()} ({_minutes} min) - " +
                   $"Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, " +
                   $"Pace: {GetPace():F1} min per mile";
        }
    }
}