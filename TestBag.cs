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
    public class TestBag
    {
        private Bag _box;
        private Bag _bundle;
        private Item _diamond;
        private Item _netherite_axe;

        [SetUp]
        public void Setup()
        {
            _box = new Bag(new string[] { "shulker_box", "yellow_shulker_box" }, "a shulker box", "Just a yellow shulker box. ");
            _bundle = new Bag(new string[] { "bundle", "red_bundle" }, "bundle_1", "a red bundle from Yan Bin's clan.");
            _diamond = new Item(new string[] { "raw_material", "gem"}, "a diamond gem", "A diamond gem obtained from Yan Bin's clan.");
            _netherite_axe = new Item(new string[] { "weapon", "netherite_weapon"}, "a netherite axe", "A legendary netherite axe found in the ender city.");
        }

        [Test]
        public void TestBagLocatesItem()
        {
            _box.Inventory.Put(_diamond);
            ClassicAssert.IsNotNull(_box.Locate("raw_material"), "_box / yellow shulker box locate item in inventory.");
            ClassicAssert.AreSame(_diamond, _box.Locate("raw_material"), "located item == same object test");
            ClassicAssert.IsTrue(_box.Inventory.HasItem("raw_material"), "item remains in _box / yellow shulker box after locating");
        }

        [Test]
        public void TestBagLocatesItself()
        {
            ClassicAssert.AreSame(_box, _box.Locate("shulker_box"), "shulker box able to locate itself under box");
            ClassicAssert.AreSame(_box, _box.Locate("yellow_shulker_box"), "shulker box able to locate itself under box 2");
        }

        [Test]
        public void TestBagLocatesNothing()
        {
            ClassicAssert.IsNull(_box.Locate("weapon"), "return null for unexisting item in box");
            _box.Inventory.Put(_diamond);
            ClassicAssert.IsNull(_box.Locate("weapon"), "retrun null for unexisting item in box ( when something else is inside )");
        }

        [Test]
        public void TestBagFullDescription()
        {
            _box.Inventory.Put(_diamond);
            _box.Inventory.Put(_netherite_axe);
            string expected = $"In the a shulker box, you can see : \n\ta a diamond gem (raw_material)\n\ta a netherite axe (weapon)";
            string actual = _box.FullDescription.Replace("\r\n", "\n");
            ClassicAssert.AreEqual(expected, actual, "Full description format incorrect");
        }

        
        [Test]
        public void TestBagInBag()
        {
            _box.Inventory.Put(_bundle);
            _box.Inventory.Put(_diamond);

            ClassicAssert.AreSame(_bundle, _box.Locate("bundle"), "_box able to locate _bundle");

            ClassicAssert.AreSame(_diamond, _box.Locate("raw_material"), "_box able to locate _diamond ('raw_material') in inventory. ");

            Item axeInBundle = new Item(new string[] { "AxeInBundle"}, "a hidden axe", "sword hidden in bundle");
            _bundle.Inventory.Put(axeInBundle);
            ClassicAssert.IsNull(_box.Locate("AxeInBundle"), "shulker box cannot locate items in bundle directly");

            GameObject locatedBundle = _box.Locate("bundle");
            ClassicAssert.IsNotNull(locatedBundle, "bundle located from shulker box");
            ClassicAssert.IsInstanceOf<Bag>(locatedBundle, "Located object == Bag");

            Bag actualBundle = locatedBundle as Bag;
            ClassicAssert.IsNotNull(actualBundle, "Cast to Bag");

            ClassicAssert.AreSame(axeInBundle, actualBundle.Locate("AxeInBundle"), "Axe locatable from bundle");
        }



        [Test]
        public void TestBagInBagWithPrivilegeItem()
        {
            _box.Inventory.Put(_bundle); 
            Item privilegedItem = new Item(new string[] { "secret", "ancient_relic" }, "a glowing orb", "An orb of immense power.");
            _bundle.Inventory.Put(privilegedItem);
            
            privilegedItem.PrivilegeEscalation("7551");
            ClassicAssert.IsTrue(privilegedItem.AreYou("20007"), "Privileged item should now be identifiable by tutorial ID.");
            ClassicAssert.IsFalse(privilegedItem.AreYou("secret"), "Privileged item should no longer be identifiable by old ID 'secret'.");

            ClassicAssert.IsNull(_box.Locate("secret"), "shulker box should not locate 'secret' (old ID of item in bundle).");
            ClassicAssert.IsNull(_box.Locate("20007"), "shulker box should not locate '20007' (privileged ID of item in bundle).");

            GameObject locatedBundle = _box.Locate("bundle");
            ClassicAssert.IsNotNull(locatedBundle, "Bundle findable in shulker box");
            Bag bundleAsBag = locatedBundle as Bag;
            ClassicAssert.IsNotNull(bundleAsBag, "Located bundle should be a shulker box");

            ClassicAssert.IsNotNull(bundleAsBag.Locate("20007"), "Privileged item should be locatable within bundle by its new ID.");
            ClassicAssert.IsNull(bundleAsBag.Locate("secret"), "Privileged item should NOT be locatable within bundle by its old ID.");

            
        }
    }
}
