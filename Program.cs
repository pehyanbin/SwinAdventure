using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Name : ");
            string name = Console.ReadLine();

            Console.WriteLine("Description : ");
            string description = Console.ReadLine();


            Player myplayer = new Player(name, description);


            
            

            Location overworld = new Location(new string[] { "overworld", "main dimension" }, "overworld", "The main dimension of where the game begins. ");
            myplayer.Location = overworld;

            Location nether = new Location(new string[] { "nether world", "hell", "lava world" }, "Nether", "The hot, dangerous, hell-like dimension surrounded with lava, fire, hostile mobs and unqiue structures an biomes.");

            Location the_end = new Location(new string[] { "the end", "The End", "ender world", "space" }, "The End", "The dark, space-like dimension with many floating islands that are lived by endermen and the ultimate boss --- Ender Dragon.");

            Location ancient_city = new Location(new string[] { "underground ancient city", "abandoned city", "warden city" }, "Ancient City", "The ancient underground city with dangerous Wardens.");





            Path netherPortal_in = new Path(new string[] { "north", "path to nether" }, nether);
            Path netherPortal_out = new Path(new string[] { "south", "path to overworld from nether" }, overworld);
            Path endPortal_in = new Path(new string[] { "west", "path to ender"}, the_end);
            Path endPortal_out = new Path(new string[] { "east", "path to overworld from ender" }, overworld);
            Path ancientCity_entrance = new Path(new string[] { "down", "hole to ancient city" }, ancient_city);
            Path ancientCity_exit = new Path(new string[] { "up", "hole back to overworld" }, overworld);


            overworld.AddPath(netherPortal_in);
            overworld.AddPath(endPortal_in);
            overworld.AddPath(ancientCity_entrance);


            nether.AddPath(netherPortal_out);
            
            the_end.AddPath(endPortal_out);

            ancient_city.AddPath(ancientCity_exit);



            Item item1 = new Item(new string[] { "sword", "weapon" }, "sword", "An enchanted sword with Fire Aspect 3, Sharpness 5, Unbreaking 3");
            myplayer.Inventory.Put(item1);

            Item item2 = new Item(new string[] { "tralalelotralala", "brainrot" }, "tralalelotralala", "A three legged shark wearing shoes.");
            myplayer.Inventory.Put(item2);



            Bag bag1 = new Bag(new string[] { "bag", "backpack" }, "leather bag", "This is a leather bag made from cow's leather.");
            myplayer.Inventory.Put(bag1);




            Item item3 = new Item(new string[] { "gun", "weapon", "assault rifle" }, "AK-47 assault rifle", "A AK-47 assault rifle with red dot sight and tactical foregrip.");
            bag1.Inventory.Put(item3);




            Item locationItem1 = new Item(new string[] {"food", "steak", "cooked_beef"}, "cooked_beef", "Cooked meat of cows.");
            overworld.Inventory.Put(locationItem1);


            Item locationItem2 = new Item(new string[] { "combat", "tools", "totem" }, "Totem of Undying", "A totem that revives player once and apply beneficial effects to the player for a short period of time.");
            overworld.Inventory.Put(locationItem2);

            
            

            Console.WriteLine(myplayer.Location.FullDescription);

            CommandProcessor cp = new CommandProcessor();

            while (true)
            {
                Console.Write("Command : ");
                string raw_command = Console.ReadLine();


                string output = cp.ProcessCommand(myplayer, raw_command);
                Console.WriteLine(output);

            }
        }
    }
}