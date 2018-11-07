﻿//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------



namespace Mancala
{
    using System.Windows;
    using System.Windows.Controls;
    using MyNamespace;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instantiate the currentGame GameState class
        /// </summary>
        private GameState currentGame = new GameState();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Method that actually takes the turn and changes the values
        /// </summary>
        /// <param name="currentBox">The object that initiated the event.</param>
        public void TakeMove(TextBox currentBox)
        {
            string startPos;
            if (currentBox.Name.Length == 12)
            {
                startPos = currentBox.Name.Substring(10, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(10, 1);
            }

            int position = int.Parse(startPos);
            int count = this.currentGame.ArrGameBoard[position];
            this.currentGame.ArrGameBoard[int.Parse(startPos)] = 0;
            for (int x = count; x > 0; x--)
            {
                position++;
                if (position == 6 && this.currentGame.PlayerOneTurn == false)
                {
                    position++;
                }

                if (position == 13 && this.currentGame.PlayerOneTurn == true)
                {
                    position++;
                }

                if (position > 13)
                {
                    position = 0;
                }

                this.currentGame.ArrGameBoard[position]++;
            }

            bool extraTurn = this.CheckEndingPosition(position);
            this.UpdateValues();
            if (extraTurn == false)
            {
                this.currentGame.ChangePlayerTurn();
            }


            // TODO Method Call here, needs to run after every move    
            // EndGame();
        }

        /// <summary>
        /// Method that checks if game is over
        /// </summary>
        public void EndGame()
        {
            //TODO Could do it like this with the loop
            //int sum1 = 0         
            //for (int x = 0; x < 6; x++)
            //{
            //    sum1 += this.currentGame.ArrGameBoard[x];
            //}
            //int sum2 = 0 
            //for (int x = 7; x < 13; x++)
            //{
            //    sum2 += this.currentGame.ArrGameBoard[x];
            //}

            // Sum Player 1's side of board 0-5
            int sum1 = 0;
            for (int x = 0; x <= 0; x++)
            {
                // TODO Remove loop or change it to be x
                // These 5 lines negate the purpose loop.
                // Either remove the For and keep the 5 lines or change the For to loop through the positions
                sum1 += this.currentGame.ArrGameBoard[0];
                sum1 += this.currentGame.ArrGameBoard[1];
                sum1 += this.currentGame.ArrGameBoard[2];
                sum1 += this.currentGame.ArrGameBoard[3];
                sum1 += this.currentGame.ArrGameBoard[4];
                sum1 += this.currentGame.ArrGameBoard[5];
            }

            // Sum Player 2's side of board 7-12
            int sum2 = 0;
            for (int x = 0; x <= 0; x++)
            {
                sum2 += this.currentGame.ArrGameBoard[7];
                sum2 += this.currentGame.ArrGameBoard[8];
                sum2 += this.currentGame.ArrGameBoard[9];
                sum2 += this.currentGame.ArrGameBoard[10];
                sum2 += this.currentGame.ArrGameBoard[11];
                sum2 += this.currentGame.ArrGameBoard[12];
            }

            // TODO Need If Statement Here to check if game is actually over
            // If either sum = 0 then game is over
            // Code Below this should ONLY run if the game is actually over
            // Don't need an else statement, If over do this, otherwise do nothing. 


            this.currentGame.ArrGameBoard[6] += sum1;
            this.currentGame.ArrGameBoard[13] += sum2;
            // After totaling the scores, all remaining positions should be set to zero: 0-5 & 7-12
            this.UpdateValues();
            if (currentGame.ArrGameBoard[6] > currentGame.ArrGameBoard[13])
            {
                MessageBox.Show("Player 1 wins!");
                this.currentGame.OnGoingGame = false;
            }
            else if (currentGame.ArrGameBoard[13] > currentGame.ArrGameBoard[6])
            {
                MessageBox.Show("Player 2 wins!");
                this.currentGame.OnGoingGame = false;
            }
        }

        /// <summary>
        /// Makes sure the current player chose a valid starting position
        /// </summary>
        public void ConfirmMove(TextBox currentBox)
        {
            string startPos;
            if (currentBox.Name.Length == 12)
            {
                startPos = currentBox.Name.Substring(10, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(10, 1);
            }

            int position = int.Parse(startPos);

            if (this.currentGame.PlayerOneTurn == true)
            {
                if (position >= 0 && position <= 5)
                {
                    TakeMove(currentBox);

                }
            }
            else if (this.currentGame.PlayerOneTurn == false)
            {
                if (position >= 7 && position <= 12)
                {
                    TakeMove(currentBox);
                }
            }

        }

        /// <summary>
        /// Checks the final position & takes appropriate action
        /// </summary>
        public bool CheckEndingPosition(int position)
        {
            //If Player One Turn
            if (this.currentGame.PlayerOneTurn == true)
            {
                // If Ending Position is on Player One's Side
                if (position >= 0 && position <= 5)
                {
                    // If Ending Position was empty
                    if (this.currentGame.ArrGameBoard[position] == 1)
                    {
                        // Find Opposite Pit and if not empty, add beads from both pits to Player's Score                       
                        switch (position)
                        {
                            case 0:
                                if (this.currentGame.ArrGameBoard[12] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[12] + 1;
                                    this.currentGame.ArrGameBoard[12] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 1:
                                if (this.currentGame.ArrGameBoard[11] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[11] + 1;
                                    this.currentGame.ArrGameBoard[11] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 2:
                                if (this.currentGame.ArrGameBoard[10] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[10] + 1;
                                    this.currentGame.ArrGameBoard[10] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 3:
                                if (this.currentGame.ArrGameBoard[9] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[9] + 1;
                                    this.currentGame.ArrGameBoard[9] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 4:
                                if (this.currentGame.ArrGameBoard[8] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[8] + 1;
                                    this.currentGame.ArrGameBoard[8] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 5:
                                if (this.currentGame.ArrGameBoard[7] != 0)
                                {
                                    this.currentGame.ArrGameBoard[6] += this.currentGame.ArrGameBoard[7] + 1;
                                    this.currentGame.ArrGameBoard[7] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                        }

                        return false;
                    }
                }
                // If ending in Player's Own Store
                else if (position == 6)
                {
                    return true;
                }

                return false;
            }
            // If Player Two Turn
            else
            {
                // If Ending Position is on Player Two's Side
                if (position >= 7 && position <= 12)
                {
                    // If Ending Position was empty
                    if (this.currentGame.ArrGameBoard[position] == 1)
                    {
                        // Find Opposite Pit and if not empty, add beads from both pits to Player's Score                       
                        switch (position)
                        {
                            case 7:
                                if (this.currentGame.ArrGameBoard[5] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[5] + 1;
                                    this.currentGame.ArrGameBoard[5] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 8:
                                if (this.currentGame.ArrGameBoard[4] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[4] + 1;
                                    this.currentGame.ArrGameBoard[4] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 9:
                                if (this.currentGame.ArrGameBoard[3] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[3] + 1;
                                    this.currentGame.ArrGameBoard[3] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 10:
                                if (this.currentGame.ArrGameBoard[2] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[2] + 1;
                                    this.currentGame.ArrGameBoard[2] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 11:
                                if (this.currentGame.ArrGameBoard[1] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[1] + 1;
                                    this.currentGame.ArrGameBoard[1] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                            case 12:
                                if (this.currentGame.ArrGameBoard[0] != 0)
                                {
                                    this.currentGame.ArrGameBoard[13] += this.currentGame.ArrGameBoard[0] + 1;
                                    this.currentGame.ArrGameBoard[0] = 0;
                                    this.currentGame.ArrGameBoard[position] = 0;
                                }

                                return false;
                        }

                        return false;
                    }
                }
                // If ending in Player's Own Store
                else if (position == 13)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Enables game board at start of new game
        /// </summary>
        public void SetBoard()
        {
            for (int i = 0; i < 14; i++)
            {
                string boxName = "txtboxSlot" + i;
                TextBox currentBox = FindName(boxName) as TextBox;
                currentBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// Method that sets the game board objects to match the array values
        /// </summary>
        public void UpdateValues()
        {
            for (int x = 0; x < 14; x++)
            {
                string objectName = "txtboxSlot" + x;
                TextBox currentBox = FindName(objectName) as TextBox;

                currentBox.ToolTip = this.currentGame.ArrGameBoard[x].ToString();

                int pieceCount;
                pieceCount = this.currentGame.ArrGameBoard[x];

                for (int count = 1; count <= pieceCount; count++)
                {
                    string imageName = "Image" + x + "_" + count;
                    Image currentImage = FindName(imageName) as Image;
                    currentImage.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Click event that exits program with confirmation check
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param> 
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.OKCancel) ==
                MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Click event that starts a new puzzle with confirmation check
        /// Doesn't seek confirmation if no game has been started
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentGame.OnGoingGame == true)
            {
                if (MessageBox.Show("Start new game?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    this.currentGame.SetStartValues();
                    this.SetBoard();
                    UpdateValues();
                }
            }
            else
            {
                this.currentGame.SetStartValues();
                this.SetBoard();
                UpdateValues();
            }
        }

        /// <summary>
        /// Click event that resets puzzle with confirmation check
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentGame.OnGoingGame == true)
            {
                if (MessageBox.Show("Restart current puzzle?", "Confirm", MessageBoxButton.OKCancel) ==
                    MessageBoxResult.OK)
                {
                    this.currentGame.SetStartValues();
                }
            }
        }

        private void PlayerTurn_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ConfirmMove(sender as TextBox);
        }

        private void aiCheckBox_Checked(object sender, RoutedEventArgs e)
        // checkbox for choosing to play against AI
        {
            MessageBox.Show("AI is enabled");
            name2TextBox.Clear();
            name2TextBox.IsEnabled = false;
            player2Lbl.Content = "";


        }

        private void nameButton_Click(object sender, RoutedEventArgs e)
        // Allow player(s) to enter their name(s) 
        {
            player1Lbl.Content = nameTextBox.Text;

            if (aiCheckBox.IsChecked.Equals(true))
            {
                name2TextBox.IsEnabled = false;
            }
            else
            {
                player2Lbl.Content = name2TextBox.Text;
            }
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}



