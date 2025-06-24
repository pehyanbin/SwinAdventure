using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class TestInventory
    {
        private Inventory _inventory;
        private Item _shovel;
        private Item _sword;

        [SetUp]
        public void Initialize()
        {
            _inventory = new Inventory();
            _shovel = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");
            _sword = new Item(new string[] { "sword", "blade" }, "a bronze sword", "A sharp blade");
        }

        [Test]
        public void TestFindItem()
        {
            _inventory.Put(_shovel);
            ClassicAssert.IsTrue(_inventory.HasItem("shovel"), "Inventory should have shovel");
            ClassicAssert.IsTrue(_inventory.HasItem("spade"), "Inventory should have spade");
        }

        [Test]
        public void TestNoItemFind()
        {
            ClassicAssert.IsFalse(_inventory.HasItem("shovel"), "Inventory should not have shovel initially");
            _inventory.Put(_shovel);
            ClassicAssert.IsFalse(_inventory.HasItem("sword"), "Inventory should not have sword");
        }

        [Test]
        public void TestFetchItem()
        {
            _inventory.Put(_shovel);
            Item fetched = _inventory.Fetch("shovel");
            ClassicAssert.IsNotNull(fetched, "Fetched item should not be null");
            ClassicAssert.IsTrue(fetched.AreYou("shovel"), "Fetched item should be shovel");
            ClassicAssert.IsTrue(_inventory.HasItem("shovel"), "Shovel should remain in inventory");
        }

        [Test]
        public void TestTakeItem()
        {
            _inventory.Put(_shovel);
            Item taken = _inventory.Take("shovel");
            ClassicAssert.IsNotNull(taken, "Taken item should not be null");
            ClassicAssert.IsTrue(taken.AreYou("shovel"), "Taken item should be shovel");
            ClassicAssert.IsFalse(_inventory.HasItem("shovel"), "Shovel should be removed from inventory");
        }

        [Test]
        public void TestItemList()
        {
            _inventory.Put(_shovel);
            _inventory.Put(_sword);
            string expected = "\ta a shovel (shovel)\n\ta a bronze sword (sword)";
            string actual = _inventory.ItemList.Replace("\r\n", "\n");
            ClassicAssert.AreEqual(expected, actual, "Item list should match expected format");
        }
    }
}