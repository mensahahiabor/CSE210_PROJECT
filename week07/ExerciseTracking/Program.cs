using System;
using System.Collections.Generic;

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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exercise Tracking Program");
            Console.WriteLine("========================");
            Console.WriteLine();

            // Create a list to hold different types of activities
            List<Activity> activities = new List<Activity>();

            // Create one activity of each type
            // Running: 3 miles in 30 minutes on November 3rd, 2022
            activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));

            // Cycling: 15 mph average speed for 45 minutes on November 4th, 2022
            activities.Add(new Cycling(new DateTime(2022, 11, 4), 45, 15.0));

            // Swimming: 20 laps in 25 minutes on November 5th, 2022
            activities.Add(new Swimming(new DateTime(2022, 11, 5), 25, 20));

            // Additional activities to show more variety
            activities.Add(new Running(new DateTime(2022, 11, 6), 45, 5.5));
            activities.Add(new Cycling(new DateTime(2022, 11, 7), 60, 18.5));
            activities.Add(new Swimming(new DateTime(2022, 11, 8), 35, 30));

            // Demonstrate polymorphism - iterate through the list and call GetSummary
            // The correct derived class method will be called for each activity type
            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }

            Console.WriteLine();
            Console.WriteLine("Program demonstrates:");
            Console.WriteLine("• Inheritance: All activities inherit from base Activity class");
            Console.WriteLine("• Polymorphism: GetSummary() calls different overridden methods");
            Console.WriteLine("• Encapsulation: All member variables are private with protected access");
            Console.WriteLine("• Method Overriding: Each activity calculates distance, speed, pace differently");
        }
    }
}