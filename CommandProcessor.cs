using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class CommandProcessor
    {
        public string ProcessCommand(Player myplayer, string raw_command)
        {
            LookCommand look = new LookCommand();
            MoveCommand move = new MoveCommand();
            Command take = new TakeCommand(); //polymorphism :>
            Command put = new PutCommand(); // polymorphism :>
            

            List<Command> commands = new List<Command> { look, move, take, put };

            string[] filtered_command = raw_command.Split(" ");

            string commandWord = filtered_command[0].ToLower();
            Command currentCommand = null;

            for(int i=0; i<commands.Count; i++)
            {
                if (commands[i].AreYou(commandWord))
                {
                    currentCommand = commands[i];
                    break;
                }
                else if (commandWord == "quit")
                {
                    Environment.Exit(0);
                }
            }

            if (currentCommand != null)
            {
                string output = currentCommand.Execute(myplayer, filtered_command);
                return output;
            }
            else
            {
                return "Unknown command";
            }

            
        }
    }
}
