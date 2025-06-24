using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class GameObject : IdentifiableObject
    {
        private string _description;
        private string _name;

        public GameObject(string[] indents, string name, string desc) : base(indents)
        {
            _name = name;
            _description = desc;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string ShortDescription
        {
            get
            {
                return $"a {Name} ({FirstId})";
            }
        }

        public virtual string FullDescription
        {
            get
            {
                return _description;
            }
        }
    }
}
