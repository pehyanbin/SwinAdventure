using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class LookCommandTests
    {
        private Player _player;
        private LookCommand _lookCommand;
        private Item _gem;
        private Bag _bag;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Fred", "the mighty programmer");
            _lookCommand = new LookCommand();
            _gem = new Item(new string[] { "gem", "stone" }, "gem", "A shiny gem");
            _bag = new Bag(new string[] { "bag", "sack" }, "small bag", "A small cloth bag");
            _player.Inventory.Put(_gem);
            _player.Inventory.Put(_bag);
        }

        [Test]
        public void TestLookAtMe()
        {
            string[] command = new string[] { "look", "at", "inventory" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Does.Contain("You are Fred, the mighty programmer."));
            Assert.That(result, Does.Contain("You are carrying:"));
            Assert.That(result, Does.Contain("a gem (gem)"));
            Assert.That(result, Does.Contain("a small bag (bag)"));
        }

        [Test]
        public void TestLookAtGem()
        {
            string[] command = new string[] { "look", "at", "gem" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("A shiny gem"));
        }

        [Test]
        public void TestLookAtUnk()
        {
            string[] command = new string[] { "look", "at", "sword" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("I cannot find the sword in the Fred"));
        }

        [Test]
        public void TestLookAtGemInMe()
        {
            string[] command = new string[] { "look", "at", "gem", "in", "inventory" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("A shiny gem"));
        }

        [Test]
        public void TestLookAtGemInBag()
        {
            _bag.Inventory.Put(_gem);
            _player.Inventory.Take("gem");
            string[] command = new string[] { "look", "at", "gem", "in", "bag" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("A shiny gem"));
        }

        [Test]
        public void TestLookAtGemInNoBag()
        {
            string[] command = new string[] { "look", "at", "gem", "in", "chest" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("I cannot find the chest container"));
        }

        [Test]
        public void TestLookAtNoGemInBag()
        {
            string[] command = new string[] { "look", "at", "sword", "in", "bag" };
            string result = _lookCommand.Execute(_player, command);
            Assert.That(result, Is.EqualTo("I cannot find the sword in the small bag"));
        }

        [Test]
        public void TestInvalidLook()
        {
            string[] command1 = new string[] { "look", "around" };
            Assert.That(_lookCommand.Execute(_player, command1), Is.EqualTo("I don't know how to look like that"));

            string[] command2 = new string[] { "hello", "at", "Fred" };
            Assert.That(_lookCommand.Execute(_player, command2), Is.EqualTo("Error in look input"));

            string[] command3 = new string[] { "look", "for", "gem" };
            Assert.That(_lookCommand.Execute(_player, command3), Is.EqualTo("What do you want to look at?"));

            string[] command4 = new string[] { "look", "at", "gem", "from", "bag" };
            Assert.That(_lookCommand.Execute(_player, command4), Is.EqualTo("What do you want to look in?"));
        }
    }
}