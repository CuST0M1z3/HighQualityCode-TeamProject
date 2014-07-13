namespace GameLabyrinthTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Labyrinth.Engine;
    using Labyrinth.Interfaces;
    using Labyrinth.Enumerations;
    using System.Text;

    public class TestObjectsFactory
    {
        private ILabyrinthFactory factory;
        private IPlayfield playfield;
        private IPlayer player;
        private IGameDialog dialogs;
        private IScoreboard scoreboard;

        public TestObjectsFactory()
        {
            this.factory = new LabyrinthFactory();
            this.playfield = this.factory.CreatePlayfield();
            this.player = this.factory.CreatePlayer();
            this.dialogs = this.factory.CreateDialogs();
            this.scoreboard = this.factory.CreateScoreboard();
        }

        public IPlayfield Playfield
        {
            get
            {
                return this.playfield;
            }
        }

        public IPlayer Player
        {
            get
            {
                return this.player;
            }
        }

        public IGameDialog Dialog
        {
            get
            {
                return this.dialogs;
            }
        }

        public IScoreboard Scoreboard
        {
            get
            {
                return this.scoreboard;
            }
        }
    }

    [TestClass]
    public class GameLabyrinthTests
    {
        private TestObjectsFactory testObjectsFactory = new TestObjectsFactory();

        [TestMethod]
        public void TestPlayfieldCreation()
        {
            int[,] playfieldArray = new int[7, 7];
            int[,] expectedPlayfield = testObjectsFactory.Playfield.GetLabyrinthArray();

            for (int row = 0; row < expectedPlayfield.GetLength(0); row++)
            {
                for (int col = 0; col < expectedPlayfield.GetLength(1); col++)
                {
                    Assert.AreEqual(expectedPlayfield[row, col], playfieldArray[row, col]);
                }
            }          
        }

        [TestMethod]
        public void TestPlayerStartPosition()
        {
            IPlayer player = testObjectsFactory.Player;
            Assert.AreEqual(3, player.XPosition);
            Assert.AreEqual(3, player.YPosition);
        }

        [TestMethod]
        public void TestRandomGeneratedPlayfield()
        {
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

            string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
                                                                   { "X", "-", "-", "-", "X", "X", "X" }, 
                                                                   { "X", "-", "-", "-", "-", "X", "-" }, 
                                                                   { "-", "X", "-", "*", "-", "-", "X" },
                                                                   { "X", "X", "-", "X", "-", "X", "X" },
                                                                   { "X", "X", "-", "X", "X", "X", "X" },
                                                                   { "X", "X", "X", "X", "-", "-", "-" } };
            StringBuilder expectedResult = new StringBuilder();
            for (int col = 0; col < expectedOutputPlayfield.GetLength(0); col++)
            {
                for (int row = 0; row < expectedOutputPlayfield.GetLength(0); row++)
                {
                    expectedResult.Append(expectedOutputPlayfield[row, col]);
                }
                expectedResult.Append("\n");
            }

            Assert.AreEqual(expectedResult.ToString(), result);
            
        }

        //[TestMethod] TODO: When state pattern for move is completed!
        //public void TestPlayerUpMove()
        //{
        //    int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
        //                                            { 1, 0, 0, 0, 1, 1, 1 }, 
        //                                            { 1, 0, 0, 0, 0, 1, 0 }, 
        //                                            { 0, 1, 0, 0, 0, 0, 1 },
        //                                            { 1, 1, 0, 1, 0, 1, 1 },
        //                                            { 1, 1, 0, 1, 1, 1, 1 },
        //                                            { 1, 1, 1, 1, 0, 0, 0 } };

        //    testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);            
        //    if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.Up))
        //    {
        //        testObjectsFactory.Player.Move(Directions.Up);
        //    }
                        
        //    string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

        //    string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
        //                                                           { "X", "-", "-", "-", "X", "X", "X" }, 
        //                                                           { "X", "-", "-", "-", "-", "X", "-" }, 
        //                                                           { "-", "X", "*", "-", "-", "-", "X" },
        //                                                           { "X", "X", "-", "X", "-", "X", "X" },
        //                                                           { "X", "X", "-", "X", "X", "X", "X" },
        //                                                           { "X", "X", "X", "X", "-", "-", "-" } };
        //    StringBuilder expectedResult = new StringBuilder();
        //    for (int col = 0; col < expectedOutputPlayfield.GetLength(0); col++)
        //    {
        //        for (int row = 0; row < expectedOutputPlayfield.GetLength(0); row++)
        //        {
        //            expectedResult.Append(expectedOutputPlayfield[row, col]);
        //        }
        //        expectedResult.Append("\n");
        //    }

        //    Assert.AreEqual(expectedResult.ToString(), result);

        //}

        //[TestMethod]
        //public void TestPlayerRightMove()
        //{
        //    int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
        //                                            { 1, 0, 0, 0, 1, 1, 1 }, 
        //                                            { 1, 0, 0, 0, 0, 1, 0 }, 
        //                                            { 0, 1, 0, 0, 0, 0, 1 },
        //                                            { 1, 1, 0, 1, 0, 1, 1 },
        //                                            { 1, 1, 0, 1, 1, 1, 1 },
        //                                            { 1, 1, 1, 1, 0, 0, 0 } };

        //    testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);

        //    string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

        //    string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
        //                                                           { "X", "-", "-", "-", "X", "X", "X" }, 
        //                                                           { "X", "-", "-", "-", "-", "X", "-" }, 
        //                                                           { "-", "X", "-", "-", "-", "-", "X" },
        //                                                           { "X", "X", "-", "X", "-", "X", "X" },
        //                                                           { "X", "X", "-", "X", "X", "X", "X" },
        //                                                           { "X", "X", "X", "X", "-", "-", "-" } };
        //    StringBuilder expectedResult = new StringBuilder();
        //    for (int col = 0; col < expectedOutputPlayfield.GetLength(0); col++)
        //    {
        //        for (int row = 0; row < expectedOutputPlayfield.GetLength(0); row++)
        //        {
        //            expectedResult.Append(expectedOutputPlayfield[row, col]);
        //        }
        //        expectedResult.Append("\n");
        //    }

        //    Assert.AreEqual(expectedResult.ToString(), result);

        //}
    }
}
