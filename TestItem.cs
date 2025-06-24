using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;

namespace SwinAdventureTests
{
    [TestFixture]
    public class TestItem
    {
        [Test]
        public void TestItemIsIdentifiable()
        {
            Item item = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");
            ClassicAssert.IsTrue(item.AreYou("shovel"), "Item should identify as 'shovel'");
            ClassicAssert.IsTrue(item.AreYou("spade"), "Item should identify as 'spade'");
            ClassicAssert.IsFalse(item.AreYou("axe"), "Item should not identify as 'axe'");
        }

        [Test]
        public void TestShortDescription()
        {
            Item item = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");
            ClassicAssert.AreEqual("a a shovel (shovel)", item.ShortDescription, "Short description should match format");
        }

        [Test]
        public void TestFullDescription()
        {
            Item item = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");
            ClassicAssert.AreEqual("A sturdy digging tool", item.FullDescription, "Full description should return description");
        }

        [Test]
        public void TestPrivilegeEscalation()
        {
            Item item = new Item(new string[] { "shovel", "spade" }, "a shovel", "A sturdy digging tool");
            ClassicAssert.AreEqual("shovel", item.FirstId, "Initial FirstId should be 'shovel'");

            item.PrivilegeEscalation("0000"); 
            ClassicAssert.AreEqual("shovel", item.FirstId, "FirstId should not change with wrong pin");

            item.PrivilegeEscalation("7551"); 
            ClassicAssert.AreEqual("20007", item.FirstId, "FirstId should be tutorial ID after escalation");
            ClassicAssert.IsTrue(item.AreYou("20007"), "Item should identify as tutorial ID");
            ClassicAssert.IsFalse(item.AreYou("shovel"), "Item should not identify as old ID");
            ClassicAssert.IsTrue(item.AreYou("spade"), "Item should still identify as 'spade'");
        }
    }
}