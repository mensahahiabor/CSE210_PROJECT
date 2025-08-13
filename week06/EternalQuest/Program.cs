using System;
using System.IO;
using System.Collections.Generic;

namespace EternalQuest
{
    // Base Goal class
    public abstract class Goal
    {
        public string Name { get; protected set; }
        public int Points { get; protected set; }

        public Goal(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public abstract bool IsComplete();
        public abstract string GetDetailsString();
        public abstract int RecordEvent();
        public abstract string SaveFormat();
    }

    // Simple Goal - completed once
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, int points) : base(name, points)
        {
            _isComplete = false;
        }

        public SimpleGoal(string name, int points, bool isComplete) : base(name, points)
        {
            _isComplete = isComplete;
        }

        public override bool IsComplete() => _isComplete;

        public override string GetDetailsString()
        {
            return $"[{(_isComplete ? "X" : " ")}] {Name}";
        }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return Points;
            }
            return 0;
        }

        public override string SaveFormat()
        {
            return $"SimpleGoal|{Name}|{Points}|{_isComplete}";
        }
    }

    // Eternal Goal - never completed, always gives points
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points)
        {
        }

        public override bool IsComplete() => false;

        public override string GetDetailsString()
        {
            return $"[ ] {Name}";
        }

        public override int RecordEvent()
        {
            return Points;
        }

        public override string SaveFormat()
        {
            return $"EternalGoal|{Name}|{Points}";
        }
    }

    // Checklist Goal - completed multiple times with bonus
    public class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _currentCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) 
            : base(name, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = 0;
        }

        public ChecklistGoal(string name, int points, int targetCount, int bonusPoints, int currentCount) 
            : base(name, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _currentCount = currentCount;
        }

        public override bool IsComplete() => _currentCount >= _targetCount;

        public override string GetDetailsString()
        {
            return $"[{(IsComplete() ? "X" : " ")}] {Name} -- Currently completed: {_currentCount}/{_targetCount}";
        }

        public override int RecordEvent()
        {
            if (_currentCount < _targetCount)
            {
                _currentCount++;
                
                if (_currentCount == _targetCount)
                {
                    return Points + _bonusPoints; // Regular points + bonus for completion
                }
                else
                {
                    return Points; // Just regular points
                }
            }
            return 0;
        }

        public override string SaveFormat()
        {
            return $"ChecklistGoal|{Name}|{Points}|{_targetCount}|{_bonusPoints}|{_currentCount}";
        }
    }

    // Data Manager for saving/loading
    public static class DataManager
    {
        private const char DELIMITER = '|';
        private const string SAVE_FILE = "goals.txt";

        public static bool Save(int score, List<Goal> goals)
        {
            try
            {
                using var sw = new StreamWriter(SAVE_FILE);
                sw.WriteLine(score);

                foreach (var goal in goals)
                {
                    sw.WriteLine(goal.SaveFormat());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                return false;
            }
        }

        public static (int score, List<Goal> goals) Load()
        {
            var goals = new List<Goal>();

            try
            {
                if (!File.Exists(SAVE_FILE))
                {
                    return (0, goals);
                }

                var lines = File.ReadAllLines(SAVE_FILE);

                if (lines.Length == 0)
                {
                    return (0, goals);
                }

                if (!int.TryParse(lines[0], out int score))
                {
                    score = 0;
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    try
                    {
                        var parts = lines[i].Split(DELIMITER);

                        if (parts.Length < 2)
                        {
                            continue;
                        }

                        Goal goal = parts[0] switch
                        {
                            "SimpleGoal" when parts.Length >= 4 => new SimpleGoal(
                                parts[1],
                                int.TryParse(parts[2], out int points) ? points : 0,
                                bool.TryParse(parts[3], out bool isComplete) && isComplete
                            ),
                            "EternalGoal" when parts.Length >= 3 => new EternalGoal(
                                parts[1],
                                int.TryParse(parts[2], out int points) ? points : 0
                            ),
                            "ChecklistGoal" when parts.Length >= 6 => new ChecklistGoal(
                                parts[1],
                                int.TryParse(parts[2], out int points) ? points : 0,
                                int.TryParse(parts[3], out int target) ? target : 1,
                                int.TryParse(parts[4], out int bonus) ? bonus : 0,
                                int.TryParse(parts[5], out int current) ? current : 0
                            ),
                            _ => null
                        };

                        if (goal != null)
                        {
                            goals.Add(goal);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing line {i}: {ex.Message}");
                    }
                }

                return (score, goals);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return (0, goals);
            }
        }
    }

    // Main Program
    class Program
    {
        private static int _score;
        private static List<Goal> _goals;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to EternalQuest!");
            Console.WriteLine("This program helps you track your goals and earn points for completing them.");
            Console.WriteLine();

            // Load existing data
            (_score, _goals) = DataManager.Load();

            bool running = true;
            while (running)
            {
                Console.WriteLine($"\nYou have {_score} points.\n");
                
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Save and Quit");
                Console.Write("\nSelect a choice from the menu: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal();
                        break;
                    case "2":
                        ListGoals();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        SaveAndQuit();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CreateNewGoal()
        {
            Console.WriteLine("\nThe types of Goals are:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");

            string choice = Console.ReadLine();

            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();

            Console.Write("What is the amount of points associated with this goal? ");
            if (!int.TryParse(Console.ReadLine(), out int points))
            {
                Console.WriteLine("Invalid points. Setting to 0.");
                points = 0;
            }

            switch (choice)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, points));
                    break;
                case "3":
                    Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                    if (!int.TryParse(Console.ReadLine(), out int targetCount))
                    {
                        targetCount = 1;
                    }

                    Console.Write("What is the bonus for accomplishing it that many times? ");
                    if (!int.TryParse(Console.ReadLine(), out int bonusPoints))
                    {
                        bonusPoints = 0;
                    }

                    _goals.Add(new ChecklistGoal(name, points, targetCount, bonusPoints));
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void ListGoals()
        {
            Console.WriteLine("\nThe goals are:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        static void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals available to record events for.");
                return;
            }

            ListGoals();
            Console.Write("Which goal did you accomplish? ");

            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice >= 1 && choice <= _goals.Count)
            {
                int pointsEarned = _goals[choice - 1].RecordEvent();
                
                if (pointsEarned > 0)
                {
                    _score += pointsEarned;
                    Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
                    Console.WriteLine($"You now have {_score} points.");
                }
                else
                {
                    Console.WriteLine("This goal is already completed or cannot be recorded.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void SaveAndQuit()
        {
            if (DataManager.Save(_score, _goals))
            {
                Console.WriteLine("Data saved successfully!");
            }
            else
            {
                Console.WriteLine("Error saving data.");
            }
            Console.WriteLine("Thank you for using EternalQuest!");
        }
    }
}