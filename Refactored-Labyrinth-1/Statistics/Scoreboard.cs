namespace Labyrinth.Statistics
{
    using System;
    using System.IO;
    using Labyrinth.Interfaces;

    public class Scoreboard : IScoreboard
    {
        private FileInfo CreateScoreboardDatabaseFile()
        {
            FileInfo scoreboardDatabaseFile = new FileInfo("scoreboard");
            using (FileStream fileStream = scoreboardDatabaseFile.Open(FileMode.OpenOrCreate, FileAccess.Read))
            {
                fileStream.Close();
            }

            return scoreboardDatabaseFile;
        }
        public void ShowStatistics()
        {
            FileInfo scoreboardFile = CreateScoreboardDatabaseFile();
            using (StreamReader scoreboardReader = scoreboardFile.OpenText())
            {
                string lineToRead = null;
                bool isEmptyScoreboard = true;
                string[] scores;
                int playerScoreboardPosition = 1;
                while ((lineToRead = scoreboardReader.ReadLine()) != null)
                {
                    isEmptyScoreboard = false;
                    scores = lineToRead.Split();
                    Console.WriteLine("{0}: {1} -> {2}", playerScoreboardPosition, scores[0], scores[1]);
                    playerScoreboardPosition++;
                }

                if (isEmptyScoreboard)
                {
                    Console.WriteLine("Scoreboard is empty.");
                } 
            }
        }

        public void AddTopScoreToScoreboard(string playerName, int playerScore)
        {
            FileInfo scoreboardFile = CreateScoreboardDatabaseFile();
            int lineIndex = 0;
            int[] playerScores = new int[5];
            string[] playerNames = new string[5];
            using (StreamReader scoreboardReader = scoreboardFile.OpenText())
            {
                string lineToRead = null;                                
                string[] scores;
                while ((lineToRead = scoreboardReader.ReadLine()) != null)
                {
                    scores = lineToRead.Split();
                    playerNames[lineIndex] = scores[0];
                    playerScores[lineIndex] = int.Parse(scores[1]);
                    lineIndex++;
                }

                if (lineIndex < 5)
                {
                    playerScores[lineIndex] = playerScore;
                    playerNames[lineIndex] = playerName;
                }
                else
                {
                    lineIndex--;
                    if (playerScore < playerScores[lineIndex])
                    {
                        playerScores[lineIndex] = playerScore;
                        playerNames[lineIndex] = playerName;
                    }
                    else
                    {
                        scoreboardReader.Close();
                    }
                }

                int swapScore;
                string swapName = String.Empty;

                for (int i = 0; i < lineIndex; i++)
                {
                    for (int j = i + 1; j <= lineIndex; j++)
                    {
                        if (playerScores[i] > playerScores[j])
                        {
                            swapScore = playerScores[i];
                            playerScores[i] = playerScores[j];
                            playerScores[j] = swapScore;

                            swapName = playerNames[i];
                            playerNames[i] = playerNames[j];
                            playerNames[j] = swapName;
                        }
                    }
                }
            }

            using (StreamWriter updateScoreboardFile = scoreboardFile.CreateText())
            {
                for (int i = 0; i <= lineIndex; i++)
                {
                    updateScoreboardFile.WriteLine("{0} {1}", playerNames[i], playerScores[i]);
                }
            }
        }
    }
}
