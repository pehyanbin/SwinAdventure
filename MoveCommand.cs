using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave"})
        { 
            
        }


        public override string Execute(Player player, string[] text)
        {
            if (player.Location == null)
            {
                return "You are at no location.";
            }
            else if (text.Length != 2)
            {
                return "I don't know how to move like that. move command format : move <direction>";
            }

            string direction = text[1].ToLower();
            GameObject pathObj = player.Location.Locate(direction);

            if (pathObj is Path path)
            {
                path.MovePlayer(player);
                return $"Moved player to {player.Location.Name}.\n{player.Location.FullDescription}";
            }
            else
            {
                return $"Cannot find a path at direction : {direction}";
            }
        }
    }
}
