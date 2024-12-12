using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    //sets passcodes for the ST and PT so only them can access there part 
    const string PS_PASSCODE = "12345";
    const string ST_PASSCODE = "67890";
    const string SUPERVISOR_NAME = "Nia Lee";
    const string TUTOR_NAME = "Andy Grey";

    static void Main(string[] args)
    {
        //asks user to select the role they what to access
        Console.WriteLine("Welcome to our program, please select what the following applies to you:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Personal Supervisor (PS)");
        Console.WriteLine("3. Senior Tutor (ST)");
        string role = Console.ReadLine();
