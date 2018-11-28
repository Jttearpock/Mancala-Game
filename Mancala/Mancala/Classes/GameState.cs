//-----------------------------------------------------------------------
// <copyright file="GameState.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mancala
{
    /// <summary>
    /// The Class that holds all current game data
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Array that holds the bead count for each position on the game board 
        /// </summary>
        private int[] arrGameBoard;

        /// <summary>
        /// Bool tracking which player's turn it is
        /// </summary>
        private bool? playerOneTurn;

        /// <summary>
        /// Bool stating if there is an active game or not
        /// </summary>
        private bool? onGoingGame;

        /// <summary>
        /// Gets or sets Array that holds the bead count for each position on the game board 
        /// </summary>
        public int[] ArrGameBoard
        {
            get { return this.arrGameBoard; }
            set { this.arrGameBoard = value; }
        }

        /// <summary>
        /// Gets or sets Bool tracking which player's turn it is
        /// </summary>
        public bool? PlayerOneTurn
        {
            get { return this.playerOneTurn; }
            set { this.playerOneTurn = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is an active game
        /// </summary>
        public bool? OnGoingGame
        {
            get { return this.onGoingGame; }
            set { this.onGoingGame = value; }
        }

        /// <summary>
        /// Method that sets a new GameBoard
        /// </summary>
        public void SetStartValues()
        {
            this.arrGameBoard = new int[14];
            for (int x = 0; x < 14; x++)
            {
                this.ArrGameBoard[x] = 4;
            }

            this.ArrGameBoard[6] = 0;
            this.ArrGameBoard[13] = 0;     

            this.PlayerOneTurn = true;
            this.OnGoingGame = true;           
        }

        /// <summary>
        /// Method that changes the current turn
        /// </summary>
        public void ChangePlayerTurn()
        {
            if (this.PlayerOneTurn == true)
            {
                this.PlayerOneTurn = false;
            }
            else if (this.playerOneTurn == false)
            {
                this.PlayerOneTurn = true;
            }
        }
    }
}