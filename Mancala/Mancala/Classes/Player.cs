//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mancala
{
    using System;
    using System.Linq;

    /// <summary>
    /// The Class that holds all Player data
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Player name
        /// </summary>
        private string name;

        /// <summary>
        /// Bool stating if player is AI
        /// </summary>
        private bool? isAI;

        /// <summary>
        /// Array that holds the score for each position
        /// </summary>
        private int[] bestMoveArray;

        /// <summary>
        /// Difficulty of the AI if enabled
        /// </summary>
        private string difficulty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="isAi">isAI bool</param>
        /// <param name="difficulty">AI difficulty</param>
        public Player(string name, bool? isAi, string difficulty)
        {
            this.isAI = isAi;
            this.difficulty = difficulty;
            if (isAi == true)
            {
                string compName = "[AI]";
                this.name = compName += name;
                this.bestMoveArray = new int[6];
            }
            else
            {
                this.name = name;
            }
        }

        /// <summary>
        /// Gets the player name
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the bool if AI
        /// </summary>
        public bool? IsAi
        {
            get { return this.isAI; }
        }

        /// <summary>
        /// Gets the difficulty
        /// </summary>
        public string Difficulty
        {
            get { return this.difficulty; }
        }

        /// <summary>
        /// Method that determines the move of Easy AI
        /// </summary>
        /// <param name="currentBoard"> Current game board as it is.</param>
        /// <param name="playerOneTurn"> Which player turn it is .</param>
        /// <returns>Int of position to use.</returns>
        public int EasyTurn(int[] currentBoard, bool? playerOneTurn)
        {
            if (playerOneTurn == true)
            {
                Random randomNum = new Random();
                int randNum = randomNum.Next(0, 5);
                while (currentBoard[randNum] == 0)
                {
                    randNum++;
                    if (randNum == 6)
                    {
                        randNum = 0;
                    }
                }

                return randNum;
            }
            else
            {
                Random randomNum = new Random();
                int randNum = randomNum.Next(7, 12);
                while (currentBoard[randNum] == 0)
                {
                    randNum++;
                    if (randNum == 13)
                    {
                        randNum = 7;
                    }
                }

                return randNum;
            }
        }

        /// <summary>
        /// Method that assigns values to each move for hard AI
        /// </summary>
        /// <param name="currentBoard">The first name to join.</param>
        /// <param name="playerOneTurn">The last name to join.</param>
        /// <returns>Int of position of move</returns>       
        public int FindBestMove(int[] currentBoard, bool? playerOneTurn)
        {
            if (playerOneTurn == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    int count = currentBoard[i];
                    int endPos = i + count;
                    if (endPos > 12)
                    {
                        endPos -= 13;
                    }

                    if (endPos == 6)
                    {
                        this.bestMoveArray[i] = 1;
                    }
                    else if (count == 0)
                    {
                        this.bestMoveArray[i] = 4;
                    }
                    else if (currentBoard[endPos] == 0 && currentBoard[12 - endPos] != 0)
                    {
                        this.bestMoveArray[i] = 2;
                    }
                    else
                    {
                        this.bestMoveArray[i] = 3;
                    }
                }
            }
            else
            {
                for (int i = 7; i < 13; i++)
                {
                    int count = currentBoard[i];
                    int endPos = i + count;
                    if (endPos > 13)
                    {
                        endPos -= 14;
                    }

                    if (endPos == 6)
                    {
                        endPos++;
                    }

                    if (endPos == 13)
                    {
                        this.bestMoveArray[i - 7] = 1;
                    }
                    else if (count == 0)
                    {
                        this.bestMoveArray[i - 7] = 4;
                    }
                    else if (currentBoard[endPos] == 0 && currentBoard[12 - endPos] != 0)
                    {
                        this.bestMoveArray[i - 7] = 2;
                    }
                    else
                    {
                        this.bestMoveArray[i - 7] = 3;
                    }
                }
            }

            return this.SelectMove(playerOneTurn);
        }

        /// <summary>
        /// Method that chooses best move for hard AI
        /// </summary>
        /// <param name="playerOneTurn">The last name to join.</param>
        /// <returns>Int of position to use</returns>
        public int SelectMove(bool? playerOneTurn)
        {
            // Check the for the best move 
            int value = 0;
            if (this.bestMoveArray.Contains(1))
            {
                for (int x = 5; x >= 0; x--)
                {
                    if (this.bestMoveArray[x] == 1)
                    {
                        value = x;
                        break;
                    }
                }
            }
            else if (this.bestMoveArray.Contains(2))
            {
                for (int x = 5; x >= 0; x--)
                {
                    if (this.bestMoveArray[x] == 2)
                    {
                        value = x;
                        break;
                    }
                }
            }
            else
            {
                Random random = new Random();
                value = random.Next(0, 5);
                while (this.bestMoveArray[value] == 4)
                {
                    value++;
                    if (value > 5)
                    {
                        value = random.Next(0, 5);
                    }
                }
            }

            if (playerOneTurn == true)
            {
                return value;
            }
            else
            {
                return value + 7;
            }
        }
    }
}