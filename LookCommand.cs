using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "inventory", "look" })
        {

        }

        public override string Execute(Player p, string[] text)
        {
            string thingId;
            string containerId;

            int textlength = text.Length;

            string donnoHowToLook = "I don't know how to look like that";

            if (text[0].ToLower() == "inventory")
            {
                if (text.Length == 1)
                {
                    return p.FullDescription;
                }
                else
                {
                    return donnoHowToLook;
                }
            }


            if (textlength != 3 && textlength != 5)
            {
                return donnoHowToLook;
            }

            if (text[0].ToLower() != "look")
            {
                return "Error in look input";
            }

            if (text[1].ToLower() != "at")
            {
                return "What do you want to look at?";
            }

            if (textlength == 5)
            {
                if (text[3] != "in")
                {
                    return "What do you want to look in?";
                }
            }

            thingId = text[2];

            if (textlength == 5)
            {
                containerId = text[4];
            }
            else
            {
                containerId = null;
            }


            IHaveInventory container = FetchContainer(p, containerId);

            if (container == null)
            {
                return $"I cannot find the {containerId} container";
            }



            return LookAtIn(thingId, container);
        }

        public IHaveInventory FetchContainer(Player p, string containerid)
        {
            if (containerid != null)
            {
                GameObject container = p.Locate(containerid);
                return (IHaveInventory)container;
            }
            else
            {
                return (IHaveInventory)p;
            }
        }

        public string LookAtIn(string thingId, IHaveInventory container)
        {
            GameObject gameobject = container.Locate(thingId);

            if (gameobject == null)
            {
                return $"I cannot find the {thingId} in the {container.Name}";
            }


            return gameobject.FullDescription;
        }
    }


}
