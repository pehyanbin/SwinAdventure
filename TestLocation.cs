using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;
using Path = SwinAdventure.Path;

namespace TestIdentifiableObject
{
    [TestFixture]
    public class TestLocation
    {
        private Location _location;
        private Item _item;
        private Location _destination;
        private Path _path;

        [SetUp]
        public void Setup()
        {
            _location = new Location(new string[] { "overworld", "main dimension" }, "Overworld", "The main dimension of the game");
            _item = new Item(new string[] { "gem", "materials", "diamond_ingot", "items" }, "diamond gem", "A rare material for crafting weapons, tools, armour, blocks and etc. Usually found below level 15.");
            _location.Inventory.Put(_item);

            _destination = new Location(new string[] { "nether", "hell" }, "Nether World", "Hot and dangerous dimension");
            _path = new Path(new string[] { "north", "portal to nether", "nether portal", "obsidian portal" }, _destination);
            _location.AddPath(_path);
        }

        [Test]
        public void TestLocationIdentifyItself()
        {
            bool result = _location.AreYou("overworld");
            ClassicAssert.IsTrue(result);
            result = _location.AreYou("main dimension");
            ClassicAssert.IsTrue(result);
            result = _location.AreYou("lobby");
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestLocationCanLocateItemsOrNot()
        {
            GameObject testobject = _location.Locate("gem");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual(_item, testobject);
        }

        [Test]
        public void TestLocationCanLocateItselfOrNot()
        {
            GameObject testobject = _location.Locate("main dimension");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual(testobject, _location);

            testobject = _location.Locate("overworld");
            ClassicAssert.IsNotNull(testobject);
            ClassicAssert.AreEqual(testobject, _location);

        }

        [Test]
        public void TestLocationFullDescription()
        {
            string expect = "You are at Overworld, The main dimension of the game.\nExits : \n\t a path to Nether World (north)\nIn this location, there are : \n\ta diamond gem (gem)";
            ClassicAssert.AreEqual(expect, _location.FullDescription);
        }


        [Test]
        public void TestLocatePath()
        {
            GameObject locate = _location.Locate("north");
            ClassicAssert.IsNotNull(locate);
            ClassicAssert.IsInstanceOf<Path>(locate);
            ClassicAssert.AreEqual(_path, locate);
        }


        [Test]
        public void TestNoPath()
        {
            Location noPath = new Location(new string[] { "nothing" }, "Empty Room", "an empty room with no items and paths");
            string expected = "You are at Empty Room, an empty room with no items and paths.\nIn this location, there are : \n";
            ClassicAssert.AreEqual(expected, noPath.FullDescription);
        }
    }
}
