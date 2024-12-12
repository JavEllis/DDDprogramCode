using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    // Sets passcodes for the ST and PS so only they can access their part
    const string PS_PASSCODE = "12345";
    const string ST_PASSCODE = "67890";
    const string SUPERVISOR_NAME = "Nia Lee";
    const string TUTOR_NAME = "Andy Grey";

    public static void Main(string[] args)
    {
        // Asks user to select the role they want to access
        Console.WriteLine("Welcome to our program, please select what the following applies to you:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Personal Supervisor (PS)");
        Console.WriteLine("3. Senior Tutor (ST)");
        string role = Console.ReadLine();

        switch (role)
        {
            // Calls functions based on the role they have selected
            case "1":
                StudentInteraction();
                break;
            case "2":
                PSInteraction();
                break;
            case "3":
                STInteraction();
                break;
            default:
                Console.WriteLine("Invalid role, Please select the correct role.");
                break;
        }
    }

    public static void StudentInteraction()
    {
        // Handles student interactions
        Console.Write("Please enter your name: ");
        string studentName = Console.ReadLine();

        // Asks user to select how they are feeling through selections
        Console.WriteLine("Hello, how are you feeling right now?");
        string[] feelings = { "not good", "unsure", "ok", "good", "great" };
        for (int i = 0; i < feelings.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {feelings[i]}");
        }
        int feelingChoice = int.Parse(Console.ReadLine());
        string feeling = feelings[feelingChoice - 1];

        // Asks user to select the reason why they are reporting
        Console.WriteLine("Are you self-reporting about:");
        Console.WriteLine("1. Current feelings");
        Console.WriteLine("2. University/School related");
        string reportType = Console.ReadLine() == "1" ? "current" : "university/school";

        Console.Write("Please describe in detail what happened and what you are currently feeling: ");
        string statusDescription = Console.ReadLine();

        // Asks user if they would like to book a meeting with Nia Lee
        Console.WriteLine("Would you like to book a meeting with personal supervisor Nia Lee?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        string bookMeetingChoice = Console.ReadLine();
        string meetingInfo = "No meeting booked";
        if (bookMeetingChoice == "1")
        {
            (string date, string time) = BookMeeting();
            meetingInfo = $"{date} at {time} with {SUPERVISOR_NAME}";
        }

        string data = $"{studentName}\n{feeling}\n{statusDescription}\n{bookMeetingChoice}\n{meetingInfo}";
        SaveToFile(data);
        Console.WriteLine("Your status has been saved. Hope to see you soon. Goodbye and have a nice day!");
    }

    public static (string, string) BookMeeting()
    {
        // Asks user the date and time they want to have their meeting with the personal supervisor
        Console.Write("Enter the date for the meeting (YYYY-MM-DD): ");
        string date = Console.ReadLine();
        Console.WriteLine("Available time slots:");
        Console.WriteLine("1. 12pm to 1pm");
        Console.WriteLine("2. 2pm to 3pm");
        Console.WriteLine("3. 4pm to 5pm");
        Console.WriteLine("4. 6pm to 7pm");
        string timeSlot = Console.ReadLine();
        string[] timeSlots = { "12pm to 1pm", "2pm to 3pm", "4pm to 5pm", "6pm to 7pm" };
        string time = timeSlots[int.Parse(timeSlot) - 1];
        return (date, time);
    }

    // Saves user data to the text file
    public static void SaveToFile(string data)
    {
        using (FileStream fs = new FileStream("info.txt", FileMode.Append, FileAccess.Write, FileShare.Read))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(data);
        }
    }

    public static void PSInteraction()
    {
        // Asks personal supervisor to enter their passcode and if they would like to review student status or to book a meeting with students
        Console.Write("Enter the PS passcode: ");
        string passcode = Console.ReadLine();
        if (passcode == PS_PASSCODE)
        {
            Console.WriteLine("Access granted. Welcome to the system, Nia Lee.");
            Console.WriteLine("1. Review student statuses");
            Console.WriteLine("2. Book a meeting with a student");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ReviewStudentStatuses();
            }
            else if (choice == "2")
            {
                BookMeetingWithStudent();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        else
        {
            Console.WriteLine("Access denied, please try again.");
        }
    }

    public static void ReviewStudentStatuses()
    {
        if (File.Exists("info.txt"))
        {
            using (FileStream fs = new FileStream("info.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("No student statuses found.");
        }
    }

    public static void BookMeetingWithStudent()
    {
        Console.Write("Enter the student's name: ");
        string studentName = Console.ReadLine();
        (string date, string time) = BookMeeting();
        string meetingInfo = $"{date} at {time} with {SUPERVISOR_NAME}";
        string data = $"{studentName}\nMeeting booked\n{meetingInfo}";
        SaveToFile(data);
        Console.WriteLine("Meeting has been booked. Thank you for booking with us.");
    }

    public static void STInteraction()
    {
        // Asks Senior Tutor to enter their passcode and if they would like to review student status
        Console.Write("Enter the ST passcode: ");
        string passcode = Console.ReadLine();
        if (passcode == ST_PASSCODE)
        {
            Console.WriteLine("Access granted. Welcome to our system, Andy Grey.");
            ReviewStudentStatuses();
        }
        else
        {
            Console.WriteLine("Access denied.");
        }
    }
}