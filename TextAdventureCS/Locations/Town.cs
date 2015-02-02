using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventureCS.Locations
{
    class Town : Location
    {
        public Town(string name)
            : base(name)
        {
 
        }

        public override void Description()
        {
            Console.WriteLine("You are standing in a village");
        }
    }
}
