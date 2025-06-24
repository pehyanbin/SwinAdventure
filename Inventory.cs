using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        private List<Item> _item;

        public Inventory()
        {
            _item = new List<Item>();
        }

        public bool HasItem(string id)
        {
            foreach (Item item in _item)
                if (item.AreYou(id))
                    return true;
            return false;
        }

        public void Put(Item item)
        {
            _item.Add(item);
        }

        public Item Take(string id)
        {
            Item item = Fetch(id);
            if (item != null)
            {
                _item.Remove(item);
            }
            return item;
        }

        public Item Fetch(string id)
        {
            foreach (Item item in _item)
                if (item.AreYou(id))
                    return item;
            return null;
        }




        public string ItemList
        {
            get
            {
                if (_item.Count == 0)
                    return "";

                StringBuilder list = new StringBuilder();
                foreach (Item item in _item)
                {
                    list.AppendLine($"\t{item.ShortDescription}");
                }
                return list.ToString().TrimEnd();
            }
        }
    }
}
