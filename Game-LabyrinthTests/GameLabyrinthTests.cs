namespace GameLabyrinthTests
{
    using Labyrinth.Engine;
    using Labyrinth.Enumerations;
    using Labyrinth.Interfaces;
    using Labyrinth.Memento;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;

    public class TestObjectsFactory
    {
        private ILabyrinthFactory factory;
        private IPlayfield playfield;
        private IPlayer player;
        private IGameDialog dialogs;
        private IScoreboard scoreboard;
        private SaveSystem save;

        public TestObjectsFactory()
        {
            this.factory = new LabyrinthFactory();
            this.playfield = this.factory.CreatePlayfield();
            this.player = this.factory.CreatePlayer();
            this.dialogs = this.factory.CreateDialogs();
            this.scoreboard = this.factory.CreateScoreboard();
            this.save = new SaveSystem();
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

        [TestMethod]
        public void TestPlayerUpMove()
        {
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.U))
            {
                testObjectsFactory.Player.Move(Directions.U);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }

            string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

            string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
                                                                   { "X", "-", "-", "-", "X", "X", "X" }, 
                                                                   { "X", "-", "-", "-", "-", "X", "-" }, 
                                                                   { "-", "X", "*", "-", "-", "-", "X" },
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
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid move!")]
        public void TestPlayerRightMove()
        {
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.R))
            {
                testObjectsFactory.Player.Move(Directions.R);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
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

        [TestMethod]
        public void TestPlayerDownMove()
        {
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.D))
            {
                testObjectsFactory.Player.Move(Directions.D);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }

            string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

            string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
                                                                   { "X", "-", "-", "-", "X", "X", "X" }, 
                                                                   { "X", "-", "-", "-", "-", "X", "-" }, 
                                                                   { "-", "X", "-", "-", "*", "-", "X" },
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

        [TestMethod]
        public void TestPlayerLeftMove()
        {
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }

            string result = testObjectsFactory.Playfield.PrintPlayfield(testObjectsFactory.Player);

            string[,] expectedOutputPlayfield = new string[7, 7] { { "X", "-", "-", "X", "X", "X", "-" }, 
                                                                   { "X", "-", "-", "-", "X", "X", "X" }, 
                                                                   { "X", "-", "-", "*", "-", "X", "-" }, 
                                                                   { "-", "X", "-", "-", "-", "-", "X" },
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid command!")]
        public void TestInvalidCommand()
        {
            string inputCommand = "move";
            string[] validCommands = new string[] { "TOP", "RESTART", "SAVE", "LOAD", "U", "L", "D", "R", "EXIT" };
            bool isValid = false;
            for (int i = 0; i < validCommands.Length; i++ )
            {
                if (inputCommand == validCommands[i])
                {
                    isValid = true;
                    break;
                }
            }

            if (!isValid)
            {
                throw new ArgumentException(testObjectsFactory.Dialog.InvalidCommandMessage());
            }
        }

        [TestMethod]

        public void PlayerIsWinningSituation()
        {
            bool isWinning;
            string result = "Player is NOT Winning";
            string expectedResult = "Player is Winning";
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.U))
            {
                testObjectsFactory.Player.Move(Directions.U);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }

            isWinning = testObjectsFactory.Playfield.IsPlayerWinning(testObjectsFactory.Player);
            if (isWinning)
            {
                result = "Player is Winning";
            }

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]

        public void PlayerIsNOTWinningSituation()
        {
            bool isWinning;
            string result = "Player is NOT Winning";
            string expectedResult = "Player is NOT Winning";
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.U))
            {
                testObjectsFactory.Player.Move(Directions.U);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.L))
            {
                testObjectsFactory.Player.Move(Directions.L);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }

            isWinning = testObjectsFactory.Playfield.IsPlayerWinning(testObjectsFactory.Player);

            if (isWinning)
            {
                result = "Player is Winning";
            }

            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void TestIntroMessage()
        {
            string testMessage = testObjectsFactory.Dialog.IntroMessage();
            string expectedResult = "Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game, 'save' to save current position, 'load' to load last saved position and 'exit' to quit the game.";

            Assert.AreEqual(expectedResult, testMessage);
        }

        [TestMethod]
        public void TestEnterYourMoveMessage()
        {
            string testMessage = testObjectsFactory.Dialog.EnterYourMoveMessage();
            string expectedResult = "Enter your move (L=left, R=right, U=up, D=down): ";

            Assert.AreEqual(expectedResult, testMessage);
        }

        [TestMethod]
        public void TestWinnerMessage()
        {
            string testMessage = testObjectsFactory.Dialog.WinnerMessage(3);
            string expectedResult = "Congratulations! You escaped in 3 moves.\nPlease enter your name for the top scoreboard: ";

            Assert.AreEqual(expectedResult, testMessage);
        }

        [TestMethod]

        public void TestIsValidMovePossitionMethod()
        {
            bool testResult = testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.U);

            Assert.IsTrue(testResult);
        }

        [TestMethod]
        public void PlayerIsBlankSituation()
        {
            bool isWinning;
            string result = "Player is NOT Blank";
            string expectedResult = "Player is NOT Blank";
            int[,] playfieldArray = new int[7, 7] { { 1, 0, 0, 1, 1, 1, 0 }, 
                                                    { 1, 0, 0, 0, 1, 1, 1 }, 
                                                    { 1, 0, 0, 0, 0, 1, 0 }, 
                                                    { 0, 1, 0, 0, 0, 0, 1 },
                                                    { 1, 1, 0, 1, 0, 1, 1 },
                                                    { 1, 1, 0, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 0, 0, 0 } };

            testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);
            if (testObjectsFactory.Playfield.IsValidMovePosition(testObjectsFactory.Player, Directions.U))
            {
                testObjectsFactory.Player.Move(Directions.U);
            }
            else
            {

                throw new ArgumentException(testObjectsFactory.Dialog.InvalidMoveMessage());
            }
           

            isWinning = testObjectsFactory.Playfield.IsBlankMovePosition(testObjectsFactory.Player);

            if (isWinning)
            {
                result = "Player is Blank";
            }

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ResetPlayfieldTest()
        {
            string oldLabyrinth, newLabyrinth;
            int[,] playfieldArray = new int[7, 7] { { 1, 1, 1, 1, 1, 1, 1 }, 
                                                    { 1, 1, 1, 1, 1, 1, 1 }, 
                                                    { 1, 1, 1, 1, 1, 1, 1 }, 
                                                    { 1, 1, 1, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 1, 1, 1 },
                                                    { 1, 1, 1, 1, 1, 1, 1 } };

           testObjectsFactory.Playfield.SetLabyrinthArray(playfieldArray);

           oldLabyrinth = testObjectsFactory.Playfield.GetLabyrinthArray().ToString();

           testObjectsFactory.Playfield.ResetPlayfield();

           newLabyrinth = testObjectsFactory.Playfield.GetLabyrinthArray().ToString();

           Assert.AreEqual(oldLabyrinth, newLabyrinth);
        }

        [TestMethod]
        public void CreatePlayerTest()
        {

            LabyrinthFactory factory = new LabyrinthFactory();
            IPlayer player = factory.CreatePlayer(5, 4);

            bool isRightPlayer = false;

            if (player.XPosition == 5 && player.YPosition == 4)
            {
                isRightPlayer = true;
            }

            Assert.IsTrue(isRightPlayer);
        }

        [TestMethod]
        public void PlayerUpdateMethodTest()
        {
            testObjectsFactory.Player.Update(testObjectsFactory.Player, testObjectsFactory.Playfield, 4, testObjectsFactory.Scoreboard, testObjectsFactory.Dialog);           
        }
    }
}
