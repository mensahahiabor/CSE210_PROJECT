using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
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