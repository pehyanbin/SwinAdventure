using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;
using Path = SwinAdventure.Path;

namespace SwinAdventureTests
{
    [TestFixture]
    public class TestPlayer
    {
        private Player _player;
        private Item _shovel;
        private Location _start;
        private Location _destination;
        private Path _portal;

        [SetUp]
        public void Initialize()
        {
            _player = new Player("John", "the mighty programmer");
            _shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");

            _start = new Location(new string[] { "overworld" }, "Overworld", "The main dimension of the game.");
            _destination = new Location(new string[] { "nether" }, "Nether World", "The hot and dangerous place filled with lava and dangerous mobs.");
            _portal = new Path(new string[] { "north" }, _destination);
            _start.AddPath(_portal);

            _player.Location = _start;
        }

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            ClassicAssert.IsTrue(_player.AreYou("me"), "Player should identify as 'me'");
            ClassicAssert.IsTrue(_player.AreYou("inventory"), "Player should identify as 'inventory'");
            ClassicAssert.IsFalse(_player.AreYou("john"), "Player should not identify as name");
        }

        [Test]
        public void TestPlayerLocatesItems()
        {
            _player.Inventory.Put(_shovel);
            GameObject located = _player.Locate("shovel");
            ClassicAssert.IsNotNull(located, "Located item should not be null");
            ClassicAssert.IsTrue(located.AreYou("shovel"), "Located item should be shovel");
            ClassicAssert.IsTrue(_player.Inventory.HasItem("shovel"), "Shovel should remain in inventory");
        }

        [Test]
        public void TestPlayerLocatesItself()
        {
            GameObject located = _player.Locate("me");
            ClassicAssert.AreEqual(_player, located, "Player should locate itself for 'me'");
            located = _player.Locate("inventory");
            ClassicAssert.AreEqual(_player, located, "Player should locate itself for 'inventory'");
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            GameObject located = _player.Locate("sword");
            ClassicAssert.IsNull(located, "Player should return null for nonexistent item");
        }

        [Test]
        public void TestPlayerFullDescription()
        {
            _player.Inventory.Put(_shovel);
            string expected = "You are John, the mighty programmer.\nYou are carrying: \n\ta a shovel (shovel)";
            ClassicAssert.AreEqual(expected, _player.FullDescription, "Full description should include inventory");
        }

        [Test]
        public void TestPlayerLocatePath()
        {
            GameObject locatedPath = _player.Locate("north");
            ClassicAssert.IsNotNull(locatedPath);
            ClassicAssert.IsInstanceOf<Path>(locatedPath);
            ClassicAssert.AreEqual(_portal, locatedPath);
        }

        [Test] 
        public void TestPlayerLocateUnexistingPath()
        {
            GameObject locatedPath = _player.Locate("south");
            ClassicAssert.IsNull(locatedPath);
        }
    }
}