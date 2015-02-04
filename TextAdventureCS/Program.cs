﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Originally made by Sietse Dijks
// Releasedate: 18-01-2014
// Current version: 1.5
// Last changes by: Michiel Pot and Alex van Pelt
// Change Date: 09-01-2015

namespace TextAdventureCS
{
    class Program
    {
        // Define the directions available to the player.
        // Refactored by Michiel and Alex
        const string MOVE_NORTH = "Go North";
        const string MOVE_WEST = "Go West";
        const string MOVE_SOUTH = "Go South";
        const string MOVE_EAST = "Go East";
        
        // Cluster the directions for validation purposes.
        // Refactored by Michiel and Alex
        static List<string> validDirections = new List<string> {
            MOVE_NORTH, 
            MOVE_WEST, 
            MOVE_SOUTH, 
            MOVE_EAST        
        };

        // Refactored by Michiel and Alex
        const string ACTION_SEARCH = "Search";
        const string ACTION_FIGHT = "Fight";
        const string ACTION_RUN = "Run";
        const string ACTION_QUIT = "Exit";
        const string ACTION_BOSS = "Boss fight";
        const string ACTION_INN = "Go to the inn";
        const string ACTION_INV = "Show inventory";
        const string ACTION_STATS = "Show stats";

        static void Main(string[] args)
        {
            // General initializations to prevent magic numbers
            int mapwidth = 12;
            int mapheight = 12;
            int ystartpos = 2;
            int xstartpos = 2;
            
            // Welcome the player
            Console.WriteLine("Welcome to a textbased adventure");
            Console.WriteLine("Before you can start your journey, you will have to enter your name.");

            string name = null;
            string input = null;

            // Check for the correct name
            // Refactored from do - while to improve readability by Michiel and Alex
            while(input != "Y") 
            {
                if( input == null || input == "N" )
                {
                    Console.WriteLine("Please enter your name and press enter:");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Your name is {0}",name);
                Console.WriteLine("Is this correct? (y/n)");
                input = Console.ReadLine();
                input = input.ToUpper();
            }           

            // Make the player
            Player player = new Player(name, 100);
            //Welcome the player
            Welcome(ref player);

            // Initialize the map
            Map map = new Map(mapwidth, mapheight, xstartpos, ystartpos);
            // Put the locations with their items on the map
            InitMap(ref map);
            // Start the game
            Start(ref map, ref player);
            // End the program
            Quit();
        }

        static void Welcome(ref Player player)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the world of Flightwood");
            Console.WriteLine("You just woke up from a very long sleep.");
            Console.WriteLine("You can't really remember anything but your name.");
            Console.WriteLine("Which by the way is {0}", player.GetName());
            
            // Added newline to improve readability.
            Console.WriteLine();

            player.ShowInventory();
            Console.WriteLine("You look around you and realise that you are in a forest.");
            Console.WriteLine("In the distance you hear the howl of an animal.");
            Console.WriteLine("You slowly come to your senses and choose to go.");
            Console.WriteLine("Press a key to continue..");
            Console.ReadKey();
        }

        static void InitMap(ref Map map)
        {
            // Add locations with their coordinates to this list.
            Forest forest = new Forest("Black Forest");
            map.AddLocation(forest, 3, 2);
            map.AddLocation(forest, 4, 2);
            map.AddLocation(forest, 1, 4);
            Cliff cliff = new Cliff("Rockface");
            map.AddLocation(cliff, 1, 5);
            Church church = new Church("Old Chapel");
            map.AddLocation(church, 3, 4);
            Swamp swamp = new Swamp("Bog");
            map.AddLocation(swamp, 4, 1);
            Town town = new Town("Skyrim");
            map.AddLocation(town, 2, 2);
            Ravine ravine = new Ravine("High pass");
            map.AddLocation(ravine, 1, 2);
            Lake lake = new Lake("");
            map.AddLocation(lake, 0, 2);
            Tomb tomb = new Tomb("");
            map.AddLocation(tomb, 4, 3);
            Road road = new Road("");
            map.AddLocation(road, 2, 1);
            map.AddLocation(road, 2, 0);
            map.AddLocation(road, 2, 3);
            map.AddLocation(road, 2, 4);
            Castle castle = new Castle("");
            map.AddLocation(castle, 1, 0);
            CastleArmory castleArmory = new CastleArmory("");
            map.AddLocation(castleArmory, 0, 0);
            TombHall tombHall = new TombHall("");
            map.AddLocation(tombHall, 5, 3);
            map.AddLocation(tombHall, 8, 1);
            TombHall1 tombHall1 = new TombHall1("");
            map.AddLocation(tombHall1, 7, 5);
            map.AddLocation(tombHall1, 9, 2);
            TombHall2 tombHall2 = new TombHall2("");
            map.AddLocation(tombHall2, 7, 2);
            map.AddLocation(tombHall2, 9, 3);
            TombRoom tombRoom = new TombRoom("");
            map.AddLocation(tombRoom, 6, 3);
            map.AddLocation(tombRoom, 6, 5);
            map.AddLocation(tombRoom, 6, 1);
            map.AddLocation(tombRoom, 10, 3);
            TombRoom1 tombRoom1 = new TombRoom1("");
            map.AddLocation(tombRoom1, 5, 4);
            map.AddLocation(tombRoom1, 7, 4);
            map.AddLocation(tombRoom1, 7, 1);
            TombRoom2 tombRoom2 = new TombRoom2("");
            map.AddLocation(tombRoom2, 5, 5);
            map.AddLocation(tombRoom2, 6, 2);
            map.AddLocation(tombRoom2, 9, 1);
            TombTreasure tombTreasure = new TombTreasure("");
            map.AddLocation(tombTreasure, 11, 3);
            
        }

        static void Start(ref Map map, ref Player player)
        {
            List<string> menuItems = new List<string>();
            int choice;

            // Refactored by Michiel and Alex
            do
            {
                Console.Clear();
                map.GetLocation().Description();
                choice = ShowMenu(map, ref menuItems);

                if ( choice != menuItems.Count() )
                {
                    if ( validDirections.Contains( menuItems[choice] ) )
                    {
                        map.Move( menuItems[choice] );
                    }

                    switch ( menuItems[choice] )
                    {
                        case ACTION_STATS:
                            player.ShowStats(player);
                            Console.ReadKey();
                        break;

                        case ACTION_INV:
                        
                            player.ShowInventory();
                            Console.ReadKey();
                        break;

                        case ACTION_SEARCH:
                            // Add code to perform an item pickup
                        
                        Console.ReadKey();
                        break;

                        case ACTION_FIGHT:
                            // Add code for fighting here                            
                        break;

                        case ACTION_RUN:
                            // Add code for running here                            
                        break;

                        case ACTION_BOSS:
                            //add code for bossfight here
                        break;

                        case ACTION_INN:
                        player.SetHealth();
                        Console.WriteLine("You have been healed!");
                        Console.ReadKey();
                        break;
                    }
                    
                }
            } 
            // When the choice is equal to the total item it means exit has been chosen.
            while ( choice < menuItems.Count() - 1);
        }

        // This Method builds the menu
        static int ShowMenu(Map map, ref List<string> menu)
        {
            int choice;
            string input;

            menu.Clear();
            ShowDirections(map, ref menu);
            
            if (map.GetLocation().CheckForItems())
            {
                bool acquirableitems = false;
                Dictionary<string, Objects> list = map.GetLocation().GetItems();
                Objects[] obj = list.Values.ToArray();
                for (int i = 0; i < obj.Count(); i++)
                {
                    if (obj[i].GetAcquirable())
                        acquirableitems = true;
                }
                if(acquirableitems)
                    menu.Add( ACTION_SEARCH );
            }
            if (map.GetLocation().HasEnemy())
            {
                menu.Add( ACTION_FIGHT );
                menu.Add( ACTION_RUN );
            }
            if (map.GetLocation().HasBossEnemy())
            {
                menu.Add(ACTION_BOSS);
            }
            if (map.GetLocation().HasInn())
            {
                menu.Add(ACTION_INN);
            }
            menu.Add(ACTION_STATS);
            menu.Add(ACTION_INV);
            menu.Add( ACTION_QUIT );

            do
            {
                for (int i = 0; i < menu.Count(); i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, menu[i]);
                }
                Console.WriteLine("Please enter your choice: 1 - {0}", menu.Count());
                input = Console.ReadLine();
            } while (!int.TryParse(input, out choice) || (choice > menu.Count() || choice < 0));

            //return choice;
            return ( choice - 1 );
        }

        static void ShowDirections(Map map, ref List<string> items)
        {
            map.AllowedDirections();

            if (map.GetNorth() == 1)
                items.Add( MOVE_NORTH );
            if (map.GetEast() == 1)
                items.Add( MOVE_EAST );
            if (map.GetSouth() == 1)
                items.Add( MOVE_SOUTH );
            if (map.GetWest() == 1)
                items.Add( MOVE_WEST );
        }

        static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing and have a nice day!");
            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
        }
    }
}