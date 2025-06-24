using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Path : GameObject
    {
        private Location _destination;

        public Path(string[] ids, Location destination) : base(ids, "", $"Path to {destination.Name}")
        {
            _destination = destination;
        }

        public Location Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        public void MovePlayer(Player player)
        {
            player.Location = _destination;
        }
    }
}
