using System;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create locations
        Location home = new Location("Home", "You are at your cozy home.");
        Location cityCenter = new Location("City Center", "You've arrived at the bustling city center.");
        Location airport = new Location("Airport", "You are at the busy airport terminal.");
        Location mall = new Location("Mall", "You've reached the crowded shopping mall.");
        Location downtown = new Location("Downtown", "You are in the heart of downtown.");

        // Connect the locations
        home.AddExit("drive to city center", cityCenter);
        home.AddExit("drive to airport", airport);
        cityCenter.AddExit("drive to airport", airport);
        cityCenter.AddExit("drive to mall", mall);
        airport.AddExit("drive to downtown", downtown);
        mall.AddExit("drive to downtown", downtown);
        downtown.AddExit("drive home", home);

        // Initialize the game
        Location currentLocation = home;
        int passengersPickedUp = 0;

        // Game loop
        while (true)
        {
            Console.WriteLine(currentLocation.Description);

            if (currentLocation == airport && passengersPickedUp == 3)
            {
                Console.WriteLine("Congratulations! You've picked up all your passengers and earned some good money. Time to head home.");
                break;
            }

            Console.Write("Enter a command (e.g., 'drive to city center', 'quit'): ");
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                Console.WriteLine("Thanks for a day's work! Goodbye.");
                break;
            }
            else if (currentLocation.HasExit(input))
            {
                currentLocation = currentLocation.GetExit(input);

                // Simulate picking up a passenger at the airport
                if (currentLocation == airport)
                {
                    passengersPickedUp++;
                    Console.WriteLine("You've picked up a passenger at the airport. Passengers picked up: " + passengersPickedUp);
                }
            }
            else
            {
                Console.WriteLine("You can't go that way or the command is invalid.");
            }
        }
    }
}

class Location
{
    public string Name { get; }
    public string Description { get; }
    private Dictionary<string, Location> exits = new Dictionary<string, Location>();

    public Location(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddExit(string command, Location location)
    {
        exits[command.ToLower()] = location;
    }

    public bool HasExit(string command)
    {
        return exits.ContainsKey(command.ToLower());
    }

    public Location GetExit(string command)
    {
        if (exits.ContainsKey(command.ToLower()))
        {
            return exits[command.ToLower()];
        }
        return null;
    }
}
