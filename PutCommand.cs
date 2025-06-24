using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class PutCommand : Command
    {
        public PutCommand() : base(new string[] { "put", "drop" })
        {

        }


        public override string Execute(Player player, string[] text)
        {
            string thingId;
            string containerId;

            int textlength = text.Length;

            

            if (text[0].ToLower() != "put" && text[0].ToLower() != "drop")
            {
                return "Error in put input";
            }


            if (textlength != 2 && textlength != 4)
            {
                return "I don't know what to put";
            }

            if (textlength == 4)
            {
                if (text[2] != "in")
                {
                    return "Where do you want to put in ?";
                }

                containerId = text[3];
            }
            else
            {
                containerId = "here";
            }

            thingId = text[1];

            IHaveInventory container = FetchContainer(player, containerId);
            if (container == null)
            {
                return $"I can't find {containerId}";
            }


            if (player.Inventory.HasItem(thingId) == false)
            {
                return $"You don't have {thingId}";
            }

            

            Item item = player.Inventory.Fetch(thingId);

            player.Inventory.Take(thingId);
            container.Inventory.Put(item);

            if (containerId.Equals("here", StringComparison.OrdinalIgnoreCase))
            {
                return $"You dropped {item.ShortDescription}";
            }
            else
            {
                return $"You put {item.ShortDescription} in {container.Name}";
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
            {
                return container;
            }
            else
            {
                return null;
            }
        }
    }
}