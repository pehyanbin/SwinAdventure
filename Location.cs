using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _itemsAtLocation;
        private Dictionary<string, Path> _paths;

        public Location(string[] ids,string location, string desc) : base(ids, location, desc)
        {
            _itemsAtLocation = new Inventory();
            _paths = new Dictionary<string, Path>();
            
        }

        public GameObject Locate(string id)
        {
            if(AreYou(id))
            {
                return this;
            }
            else
            {
                GameObject item = _itemsAtLocation.Fetch(id);
                if (item != null)
                {
                    return item;
                }

                if (_paths.ContainsKey(id.ToLower()))
                {
                    return _paths[id.ToLower()];
                }
                else
                {
                    return null;
                }
                
            }
        }


        public Inventory Inventory
        {
            get
            {
                return _itemsAtLocation;
            }
        }

        

        public override string FullDescription
        {
            get
            {
                string fulldesc = $"You are at {Name}, {base.FullDescription}.\n";
                string itemsAtLocation = _itemsAtLocation.ItemList;

                string pathdesc = "";
                if (_paths.Count > 0)
                {
                    pathdesc += "Exits : \n";

                    foreach(var path in _paths)
                    {
                        pathdesc += $"\t a path to {path.Value.Destination.Name} ({path.Key})\n";
                    }
                }

                

                fulldesc += pathdesc;
                fulldesc += $"In this location, there are : \n{itemsAtLocation}";

                


                return fulldesc;
            }
        }

        
        public void AddPath(Path path)
        {
            foreach(string ids in path.FirstId.Split(","))
            {
                string Id = ids.ToLower().Trim();
                _paths.Add(Id,path);
            }
        }

        
        
    }
}
