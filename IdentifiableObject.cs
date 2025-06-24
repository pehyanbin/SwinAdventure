using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;




        public IdentifiableObject(string[] indents)
        {
            _identifiers = (indents ?? new string[0]).Select(id => id.ToLower()).ToList();
        }

        public bool AreYou(string id)
        {
            if (_identifiers.Contains(id.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string FirstId
        {
            get
            {
                if (_identifiers.Count > 0)
                {
                    return _identifiers[0];
                }
                else
                {
                    return "";
                }
            }
        }

        public void AddIdentifier(string id)
        {
            string value = id.ToLower();
            _identifiers.Add(value);
        }

        public void PrivilegeEscalation(string pin)
        {
            string my_id = "7551";
            string tutorialId = "20007";

            if (pin == my_id && _identifiers.Count > 0)
            {
                _identifiers[0] = tutorialId;
            }
        }
    }
}
