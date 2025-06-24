using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;

namespace TestIdentifiableObject
{
    [TestFixture]
    public class TestPlayerWithLocation
    {
        private Player _player;
        private Location _location;
        private Item _item;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Peh Yan Bin", "A computer science student who fails all the subjects. ");
            _location = new Location(new string[] { "overworld", "main dimension" }, "Overworld", "The main dimension of the game.");
            _player.Location = _location;

            _item = new Item(new string[] { "weapon", "sword" }, "diamond sword", "A sword made out of diamond");
            _location.Inventory.Put(_item);

            Item stick = new Item(new string[] { "materials", "wooden stick", "stick" }, "Wooden Stick", "Just a normal wooden stick.");
            _player.Inventory.Put(stick);
        }

        [Test]
        public void TestPlayerLocatesItemsInInventory()
        {
            GameObject testobject = _player.Locate("weapon");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual("weapon", testobject.FirstId);
        }

        [Test]
        public void TestPlayerLocatesItemsInLocation()
        {
            GameObject testobject = _player.Locate("materials");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual("materials", testobject.FirstId);
        }

        [Test]
        public void TestPlayerLocatesItself()
        {
            // locate 'me' ( look at me )
            GameObject testobject = _player.Locate("me");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual(_player, testobject);

            //locate 'inventory' ( look at inventory ) 
            testobject = _player.Locate("inventory");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual(_player, testobject);
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            GameObject testobject = _player.Locate("nonexistent");
            ClassicAssert.IsNull(testobject, "Player should return null for nonexistent items.");
        }
    }
}
