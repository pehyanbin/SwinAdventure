using NUnit.Framework;
using SwinAdventure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using Path = SwinAdventure.Path;

namespace TestIdentifiableObject
{
    [TestFixture]
    public class TestMoveCommand
    {
        private Player _player;
        private Location _overworld;
        private Location _nether;
        private Location _theEnd;
        private Location _ancientCity;

        private Path _netherPortal;
        private Path _EndPortal;
        private Path _ancientCityEntrance;

        private MoveCommand _moveCommand;

        [SetUp]
        public void Setup()
        {
            _overworld = new Location(new string[] { "overworld", "main dimension" }, "Overworld", "The main dimension where the game begins.");
            _nether = new Location(new string[] { "nether world" }, "Nether", "The hot, dangerous, hell-like dimension.");
            _theEnd = new Location(new string[] { "the end" }, "The End", "The dark, space-like dimension.");
            _ancientCity = new Location(new string[] { "ancient city" }, "Ancient City", "The ancient underground city.");


            _netherPortal = new Path(new string[] { "north", "nether_portal" }, _nether);
            _EndPortal = new Path(new string[] { "west", "end_portal" }, _theEnd);
            _ancientCityEntrance = new Path(new string[] { "down", "ancient_city_entrance" }, _ancientCity);


            _overworld.AddPath(_netherPortal);
            _overworld.AddPath(_EndPortal);
            _overworld.AddPath(_ancientCityEntrance);


            _player = new Player("Player 1", "A player in this game.");
            _player.Location = _overworld;

            _moveCommand = new MoveCommand();
        }



        [Test]
        public void TestMoveCommandIdentify()
        {
            ClassicAssert.IsTrue(_moveCommand.AreYou("move"));
            ClassicAssert.IsTrue(_moveCommand.AreYou("go"));
            ClassicAssert.IsTrue(_moveCommand.AreYou("head"));
            ClassicAssert.IsTrue(_moveCommand.AreYou("leave"));
            ClassicAssert.IsFalse(_moveCommand.AreYou("teleport"));

        }


        [Test]
        public void TestMovePlayerToLocations()
        {
            string[] command = { "move", "north" };
            string result = _moveCommand.Execute(_player, command);

            ClassicAssert.AreEqual(_nether, _player.Location);
            StringAssert.Contains("Moved player to Nether.", result);
            StringAssert.Contains("You are at Nether, The hot, dangerous, hell-like dimension.", result);
        }

        [Test]
        public void TestMovePlayerToInvalidLocation()
        {
            string[] command = { "move" };
            string result = _moveCommand.Execute( _player, command);

            ClassicAssert.AreEqual(_overworld, _player.Location);
            StringAssert.Contains("I don't know how to move like that. move command format : move <direction>", result);
        }


    }
}
