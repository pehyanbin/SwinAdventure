using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class TestPath
    {
        private Location _startLocation;
        private Location _endLocation;
        private Path _path;

        [SetUp]
        public void Setup()
        {
            _startLocation = new Location(new string[] { "start", "beginning" }, "Start Zone", "A mystical beginning.");
            _endLocation = new Location(new string[] { "end", "finish" }, "End Zone", "The final destination.");
            _path = new Path(new string[] { "east", "road" }, _endLocation);
        }

        [Test]
        public void TestPathIsGameObject()
        { 
            ClassicAssert.IsTrue(_path.AreYou("east"));
            ClassicAssert.IsTrue(_path.AreYou("road"));
            ClassicAssert.IsFalse(_path.AreYou("west"));
        }

        [Test]
        public void TestPathHasCorrectDestination()
        {
            ClassicAssert.AreEqual(_endLocation, _path.Destination);
        }

        [Test]
        public void TestPathMovesPlayer()
        {
            Player player = new Player("TestPlayer", "A player for testing.");
            player.Location = _startLocation; 

            _path.MovePlayer(player);

            ClassicAssert.AreEqual(_endLocation, player.Location);
        }

        
    }
}