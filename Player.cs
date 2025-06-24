using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Location _location;


        public Player(string name, string desc) : base(new string[] { "me", "inventory"}, name, desc)
        {
            _inventory = new Inventory();
            _location = null;
        }


        public GameObject Locate(string id)
        {
            if(AreYou(id))
            {
                return this;
            }
            else
            {
                GameObject item = _inventory.Fetch(id);

                if (item != null)
                {
                    return item;
                }

                if (_location != null)
                {
                    GameObject currentLocation = _location.Locate(id);

                    if (currentLocation != null)
                        return currentLocation;
                }

                return null;
            }
        }

        public override string FullDescription
        {
            get
            {
                string inventoryList = _inventory.ItemList;

                return $"You are {Name}, {base.FullDescription}.\nYou are carrying: \n{inventoryList}";
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }


        public Location Location
        {
            get { return _location; }   set { _location = value; }
        }
    }
}
