using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class TakeCommand : Command
    {
        public TakeCommand() : base(new string[] { "take", "pickup" })
        {

        }


        public override string Execute(Player player, string[] text)
        {
            string thingId;
            string containerId;

            int textlength = text.Length;

            if (text[0].ToLower() != "take" && text[0].ToLower() != "pickup")
            {
                return "Error in take input.";
            }

            if (textlength != 2 && textlength != 4)
            {
                return "I don't know what to take";
            }

            if (textlength == 4)
            {
                if (text[2] != "from")
                {
                    return "Where do you want to take from ?";
                }

                containerId = text[3];
            }
            else
            {
                containerId = null;
            }

            thingId = text[1];

            IHaveInventory container = FetchContainer(player, containerId);

            if (container == null)
            {
                return $"I cannot find the {containerId}";
            }


            Item item = (Item)container.Locate(thingId);
            if (item == null)
            {
                return $"I cannot find {thingId} in {container.Name}";
            }

            if (container.Inventory.HasItem(thingId))
            {
                container.Inventory.Take(thingId);
                player.Inventory.Put(item);
                return $"You took {item.ShortDescription} from {container.Name}";
            }
            else
            {
                return $"Couldn't take {thingId} from {container.Name}";
            }


        }


        public IHaveInventory FetchContainer(Player player, string containerid)
        {
            if (containerid == null)
            {
                return player.Location;
            }

            GameObject containerobj = player.Locate(containerid);
            if (containerobj is IHaveInventory container)
                return container;
            else
                return null;

            

        }
    }
}