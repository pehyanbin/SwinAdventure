using System;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using SwinAdventure;
using System.Collections.Generic;
using Path = SwinAdventure.Path;

namespace SwinAdventureTests
{
    [TestFixture]
    public class TestCommandProcessor
    {
        private Player _player;
        private CommandProcessor _cp;
        private Location _overworld;
        private Location _nether;
        private Path _netherPortal;
        private Item _sword;
        private Item _gem;
        private Bag _bag;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Test Player", "A test player");
            

            _cp = new CommandProcessor();
            _overworld = new Location(new string[] { "spawn", "overworld" }, "Overworld", "The main dimension of the game");
            _nether = new Location(new string[] { "destination", "nether", "hell" }, "Nether World", "The hot and dangerous dimension filled with lava and dangerous mobs.");
            _netherPortal = new Path(new string[] {"north"}, _nether);
            _overworld.AddPath(_netherPortal);

            


            _cp = new CommandProcessor();
            _player.Location = _overworld;

            _sword = new Item(new string[] { "sword", "weapon" }, "sword", "An enchanted sword with Fire Aspect 3, Sharpness 5, Unbreaking 3");
            _player.Inventory.Put(_sword);

            _gem = new Item(new string[] { "gem" }, "gem", "A diamond gem");
            _overworld.Inventory.Put(_gem);

            
            _bag = new Bag(new string[] { "bag" }, "leather bag", "A leather bag");
            _player.Inventory.Put(_bag);
        }

        [Test] 
        public void TestCPLook()
        {
            string command = "look at me";
            string result = _cp.ProcessCommand(_player, command);
            StringAssert.Contains("You are Test Player, A test player.", result);
            StringAssert.Contains("You are carrying:", result);
        }


        [Test]
        public void TestCPLookAtItem()
        {
            string command = "look at sword";
            string result = _cp.ProcessCommand(_player, command);
            StringAssert.Contains("An enchanted sword with Fire Aspect 3, Sharpness 5, Unbreaking 3", result);
        }

        [Test]
        public void TestCPMoveCommand()
        {
            string command = "move north";
            string result = _cp.ProcessCommand(_player, command);
            StringAssert.Contains("Moved player to Nether World.\nYou are at Nether World, The hot and dangerous dimension filled with lava and dangerous mobs..\nIn this location, there are : \n", result);
        }

        [Test]
        public void TestProcessUnknownCommand()
        {
            string command = "fly to sky";
            string result = _cp.ProcessCommand(_player, command);
            StringAssert.Contains("Unknown command", result);
        }

        [Test]
        public void TestCPTakeCommand_ExecutesSuccessfully()
        {
            string result = _cp.ProcessCommand(_player, "take gem");
            Assert.That(result, Is.EqualTo("You took a gem (gem) from Overworld"));
            Assert.That(_player.Inventory.HasItem("gem"), Is.True);
            Assert.That(_overworld.Inventory.HasItem("gem"), Is.False);
        }

        [Test]
        public void TestCPPutCommand_ExecutesSuccessfully()
        {
            string result = _cp.ProcessCommand(_player, "put sword");
            Assert.That(result, Is.EqualTo("You dropped a sword (sword)"));
            Assert.That(_player.Inventory.HasItem("sword"), Is.False);
            Assert.That(_overworld.Inventory.HasItem("sword"), Is.True);
        }

        [Test]
        public void TestCPEmptyCommand_ReturnsError()
        {
            string result = _cp.ProcessCommand(_player, "");
            Assert.That(result, Is.EqualTo("Empty command"));
        }

        [Test]
        public void TestCPQuitCommand_ReturnsGoodbye()
        {
            string result = _cp.ProcessCommand(_player, "quit");
            Assert.That(result, Is.EqualTo("Goodbye!"));
        }

        [Test]
        public void TestTakeCommand_TakeItemFromLocation_Success()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "take", "gem" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("You took a gem (gem) from Overworld"));
            Assert.That(_player.Inventory.HasItem("gem"), Is.True);
            Assert.That(_overworld.Inventory.HasItem("gem"), Is.False);
        }

        [Test]
        public void TestTakeCommand_TakeItemFromExplicitContainer_Success()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "take", "gem", "from", "overworld" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("You took a gem (gem) from Overworld"));
            Assert.That(_player.Inventory.HasItem("gem"), Is.True);
            Assert.That(_overworld.Inventory.HasItem("gem"), Is.False);
        }

        [Test]
        public void TestTakeCommand_InvalidItem_ReturnsError()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "take", "nonexistent" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("I cannot find nonexistent in Overworld"));
            Assert.That(_player.Inventory.HasItem("nonexistent"), Is.False);
        }

        [Test]
        public void TestTakeCommand_InvalidContainer_ReturnsError()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "take", "gem", "from", "invalid" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("I cannot find the invalid"));
        }

        [Test]
        public void TestTakeCommand_WrongSyntax_ReturnsError()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "take", "gem", "with", "bag" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("Where do you want to take from ?"));
        }

        [Test]
        public void TestTakeCommand_WrongCommand_ReturnsError()
        {
            TakeCommand take = new TakeCommand();
            string[] command = new string[] { "grab", "gem" };
            string result = take.Execute(_player, command);

            Assert.That(result, Is.EqualTo("Error in take input."));
        }

        [Test]
        public void TestPutCommand_DropItemToLocation_Success()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "put", "sword" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("You dropped a sword (sword)"));
            Assert.That(_player.Inventory.HasItem("sword"), Is.False);
            Assert.That(_overworld.Inventory.HasItem("sword"), Is.True);
        }

        [Test]
        public void TestPutCommand_PutItemInBag_Success()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "put", "sword", "in", "bag" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("You put a sword (sword) in leather bag"));
            Assert.That(_player.Inventory.HasItem("sword"), Is.False);
            Assert.That(_bag.Inventory.HasItem("sword"), Is.True);
        }

        [Test]
        public void TestPutCommand_InvalidItem_ReturnsError()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "put", "nonexistent" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("You don't have nonexistent"));
            Assert.That(_overworld.Inventory.HasItem("nonexistent"), Is.False);
        }

        [Test]
        public void TestPutCommand_InvalidContainer_ReturnsError()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "put", "sword", "in", "invalid" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("I can't find invalid"));
            Assert.That(_player.Inventory.HasItem("sword"), Is.True);
        }

        [Test]
        public void TestPutCommand_WrongSyntax_ReturnsError()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "put", "sword", "on", "bag" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("Where do you want to put in ?"));
        }

        [Test]
        public void TestPutCommand_WrongCommand_ReturnsError()
        {
            PutCommand put = new PutCommand();
            string[] command = new string[] { "dropitem", "sword" };
            string result = put.Execute(_player, command);

            Assert.That(result, Is.EqualTo("Error in put input."));
        }
    }
}