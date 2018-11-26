//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Mancala
{
    using System.Threading;
using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

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
        /// Instantiate the playerOne Player class
        /// </summary>
        private Player playerOne;

        /// <summary>
        /// Instantiate the playerTwo Player  class
        /// </summary>
        private Player playerTwo;

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
        public async void TakeMove(Border currentBox)
        {
            string startPos;
            if (currentBox.Name.Length == 6)
            {
                startPos = currentBox.Name.Substring(4, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(4, 1);
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
            if (extraTurn == false)
            {
                this.currentGame.ChangePlayerTurn();
            }

            this.EndGame();
            this.UpdateValues();
            await Task.Delay(2000);
            if (this.currentGame.OnGoingGame == true)
            {
                this.IsAiTurn();
            }
        }

        /// <summary>
        /// Method that checks if game is over
        /// </summary>
        public void EndGame()
        {
            // Sum Player 1's side of board 0-5
            int sum1 = 0;
            for (int x = 0; x < 6; x++)
            {
                sum1 += this.currentGame.ArrGameBoard[x];
            }

            // Sum Player 2's side of board 7-12
            int sum2 = 0;
            for (int x = 7; x < 13; x++)
            {
                sum2 += this.currentGame.ArrGameBoard[x];
            }

            // If game is over
            if (sum1 == 0 || sum2 == 0)
            {
                this.currentGame.ArrGameBoard[6] += sum1;
                this.currentGame.ArrGameBoard[13] += sum2;

                for (int i = 0; i < 14; i++)
                {
                    if (i != 6 && i != 13)
                    {
                        this.currentGame.ArrGameBoard[i] = 0;
                    }
                }

                this.UpdateValues();
                if (this.currentGame.ArrGameBoard[6] > this.currentGame.ArrGameBoard[13])
                {
                    MessageBox.Show(this.playerOne.Name + " wins!");
                    PlayerOneTurnLabel.Content = this.playerOne.Name + " wins!";
                    PlayerOneTurnLabel.Foreground = Brushes.Firebrick;
                    PlayerTwoTurnLabel.Content = this.playerTwo.Name;
                    PlayerTwoTurnLabel.Foreground = Brushes.Black;
                    this.currentGame.OnGoingGame = false;
                }
                else if (this.currentGame.ArrGameBoard[13] > this.currentGame.ArrGameBoard[6])
                {
                    MessageBox.Show(this.playerTwo.Name + " wins!");
                    PlayerTwoTurnLabel.Content = this.playerTwo.Name + " wins!";
                    PlayerTwoTurnLabel.Foreground = Brushes.Firebrick;
                    PlayerOneTurnLabel.Content = this.playerOne.Name;
                    PlayerOneTurnLabel.Foreground = Brushes.Black;
                    this.currentGame.OnGoingGame = false;
                }
            }
        }

        /// <summary>
        /// Makes sure the current player chose a valid starting position
        /// </summary>
        /// /// <param name="currentBox">The object that initiated the event.</param>
        public void ConfirmMove(Border currentBox)
        {
            string startPos;
            if (currentBox.Name.Length == 6)
            {
                startPos = currentBox.Name.Substring(4, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(4, 1);
            }

            int position = int.Parse(startPos);
            if (this.currentGame.PlayerOneTurn == true && this.playerOne.IsAi != true)
            {
                if (position >= 0 && position <= 5 && this.currentGame.ArrGameBoard[position] != 0)
                {
                    this.TakeMove(currentBox);
                }
            }
            else if (this.currentGame.PlayerOneTurn == false && this.playerTwo.IsAi != true)
            {
                if (position >= 7 && position <= 12 && this.currentGame.ArrGameBoard[position] != 0)
                {
                    this.TakeMove(currentBox);
                }
            }
        }

        /// <summary>
        /// Checks the ending position and takes appropriate action
        /// </summary>
        /// <param name="position"> The object that initiated the event. </param>
        /// <returns> True or False. </returns>
        public bool CheckEndingPosition(int position)
        {
            //// If Player One Turn
            if (this.currentGame.PlayerOneTurn == true)
            {
                //// If Ending Position is on Player One's Side
                if (position >= 0 && position <= 5)
                {
                    //// If Ending Position was empty
                    if (this.currentGame.ArrGameBoard[position] == 1)
                    {
                        //// Find Opposite Pit and if not empty, add beads from both pits to Player's Score                       
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

                //// If ending in Player's Own Store
                else if (position == 6)
                {
                    return true;
                }

                return false;
            }

            //// If Player Two Turn            
            else
            {
                //// If Ending Position is on Player Two's Side
                if (position >= 7 && position <= 12)
                {
                    //// If Ending Position was empty
                    if (this.currentGame.ArrGameBoard[position] == 1)
                    {
                        //// Find Opposite Pit and if not empty, add beads from both pits to Player's Score                       
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

                //// If ending in Player's Own Store
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
        public void EnableBoard()
        {
            for (int i = 0; i < 14; i++)
            {
                string boxName = "slot" + i;
                Border currentBox = FindName(boxName) as Border;
                currentBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// Method that updates labels and values on the board
        /// </summary>
        public void UpdateValues()
        {
            for (int x = 0; x < 14; x++)
            {
                string objectName = "slot" + x;
                Border currentBox = FindName(objectName) as Border;

                currentBox.Tag = this.currentGame.ArrGameBoard[x].ToString();

                if (x == 6 || x == 13)
                {
                    for (int y = 1; y <= 15; y++)
                    {
                        string manyImageHidden = "Image" + x + "_" + y;
                        Image currentManyHidden = FindName(manyImageHidden) as Image;
                        if (currentManyHidden.Visibility == Visibility.Visible)
                        {
                            currentManyHidden.Visibility = Visibility.Hidden;
                        }
                    }
                }
                else
                {
                    for (int y = 1; y <= 9; y++)
                    {
                        string imageHidden = "Image" + x + "_" + y;
                        Image currentHidden = FindName(imageHidden) as Image;
                        currentHidden.Visibility = Visibility.Hidden;
                    }
                }

                int pieceCount;
                pieceCount = this.currentGame.ArrGameBoard[x];

                if (x == 6 || x == 13)
                {
                    if (pieceCount > 15)
                    {
                        pieceCount = 15;
                    }

                    for (int count = 1; count <= pieceCount; count++)
                    {
                        string imageName = "Image" + x + "_" + count;
                        Image currentImage = FindName(imageName) as Image;
                        currentImage.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (pieceCount > 9)
                    {
                        pieceCount = 9;
                    }

                    for (int count = 1; count <= pieceCount; count++)
                    {
                        string imageName = "Image" + x + "_" + count;
                        Image currentImage = FindName(imageName) as Image;
                        currentImage.Visibility = Visibility.Visible;
                    }
                }
            }

            if (this.currentGame.PlayerOneTurn == true)
            {
                PlayerOneTurnLabel.Content = this.playerOne.Name + ", your turn!";
                PlayerOneTurnLabel.Foreground = Brushes.Firebrick;
                PlayerTwoTurnLabel.Content = this.playerTwo.Name;
                PlayerTwoTurnLabel.Foreground = Brushes.Black;
            }
            else
            {
                PlayerTwoTurnLabel.Content = this.playerTwo.Name + ", your turn!";
                PlayerTwoTurnLabel.Foreground = Brushes.Firebrick;
                PlayerOneTurnLabel.Content = this.playerOne.Name;
                PlayerOneTurnLabel.Foreground = Brushes.Black;
            }
        }

        /// <summary>
        /// Sets the values of the two Player classes
        /// </summary>
        public void CreatePlayers()
        {
            string nameOne = PlayerOneNameTextbox.Text.Trim();
            string nameTwo = PlayerTwoNameTextbox.Text.Trim();
            bool? playerOneAi = playerOneAiCheckBox.IsChecked;
            bool? playerTwoAi = playerTwoAiCheckBox.IsChecked;
            string playerOneDifficulty = null;
            string playerTwoDifficulty = null;

            if (playerOneAi == true)
            {
                if (p1Easy_RB.IsChecked == true)
                {
                    playerOneDifficulty = "Easy";
                }
                else
                {
                    playerOneDifficulty = "Hard";
                }
            }

            if (playerTwoAi == true)
            {
                if (p2Easy_RB.IsChecked == true)
                {
                    playerTwoDifficulty = "Easy";
                }
                else
                {
                    playerTwoDifficulty = "Hard";
                }
            }

            if (string.IsNullOrWhiteSpace(nameOne))
            {
                nameOne = "Player One";
            }

            if (string.IsNullOrWhiteSpace(nameTwo))
            {
                nameTwo = "Player Two";
            }

            this.playerOne = new Player(nameOne, playerOneAi, playerOneDifficulty);
            this.playerTwo = new Player(nameTwo, playerTwoAi, playerTwoDifficulty);
        }

        /// <summary>
        /// Checks if it's an AI's turn
        /// </summary>
        public void IsAiTurn()
        {
            if (this.currentGame.PlayerOneTurn == true && this.playerOne.IsAi == true)
            {
                this.TakeAiTurn(this.playerOne);
            }
            else if (this.currentGame.PlayerOneTurn == false && this.playerTwo.IsAi == true)
            {
                this.TakeAiTurn(this.playerTwo);
            }
        }

        /// <summary>   
        /// Determines and triggers the AI move
        /// </summary>
        /// <param name="currentPlayer">The current Player</param>
        public async void TakeAiTurn(Player currentPlayer)
        {
            int position;
            if (currentPlayer.Difficulty == "Easy")
            {
                position = currentPlayer.EasyTurn(this.currentGame.ArrGameBoard, this.currentGame.PlayerOneTurn);
            }
            else
            {
                position = currentPlayer.FindBestMove(this.currentGame.ArrGameBoard, this.currentGame.PlayerOneTurn);
            }            

            string boxName = "slot" + position;
            Border currentBox = FindName(boxName) as Border;
            currentBox.BorderBrush = Brushes.LightSkyBlue;

            await Task.Delay(1000);
            this.TakeMove(currentBox);
            currentBox.BorderBrush = Brushes.Black;
        }

        /// <summary>
        /// Click event that exits program with confirmation check
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param> 
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
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
        private async void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentGame.OnGoingGame == true)
            {
                if (MessageBox.Show("Start new game?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.currentGame.OnGoingGame = false;
                    await Task.Delay(1500);
                    this.CreatePlayers();
                    this.currentGame.SetStartValues();
                    this.EnableBoard();
                    this.UpdateValues();
                    await Task.Delay(1000);
                    this.IsAiTurn();  
                }
            }
            else
            {
                this.CreatePlayers();
                this.currentGame.SetStartValues();
                this.EnableBoard();
                this.UpdateValues();
                await Task.Delay(1000);
                this.IsAiTurn();
            }
        }

        /// <summary>
        /// Click event that resets puzzle with confirmation check
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private async void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentGame.OnGoingGame == true)
            {
                if (MessageBox.Show("Start new game with same players?", "Confirm", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    this.currentGame.OnGoingGame = false;
                    await Task.Delay(1500);
                    this.currentGame.SetStartValues();
                    this.UpdateValues();
                    await Task.Delay(1000);
                    this.IsAiTurn();
                }
            }
            else if (this.currentGame.OnGoingGame == false)
            {
                this.currentGame.SetStartValues();
                this.UpdateValues();
                await Task.Delay(1000);
                this.IsAiTurn();
            }
        }

        /// <summary>
        /// Click event that resets puzzle with confirmation check
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void GameBoardPit_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ConfirmMove(sender as Border);
        }

        /// <summary>
        /// Displays bead count on mouse enter
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void PitValueVisible_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border currentBox = sender as Border;
            string startPos;
            if (currentBox.Name.Length == 6)
            {
                startPos = currentBox.Name.Substring(4, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(4, 1);
            }

            string labelName = "labSlot" + startPos;
            Label currentLabel = FindName(labelName) as Label;
            currentLabel.Content = currentBox.Tag.ToString();
            currentBox.BorderBrush = Brushes.LightSkyBlue;
            currentLabel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Heads bead count display on mouse leave
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void PitValueHidden_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border currentBox = sender as Border;
            string startPos;
            if (currentBox.Name.Length == 6)
            {
                startPos = currentBox.Name.Substring(4, 2);
            }
            else
            {
                startPos = currentBox.Name.Substring(4, 1);
            }

            string labelName = "labSlot" + startPos;
            Label currentLabel = FindName(labelName) as Label;
            currentBox.BorderBrush = Brushes.Black;
            currentLabel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// On checkbox check enable appropriate menu
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void PlayerAiCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // if player one AI's checkbox is unchecked, enable the difficulty levels.
            if (playerOneAiCheckBox.IsChecked == true)
            {
                playerOneAiMenu.IsEnabled = true;
            }

            // if player two AI's checkbox is unchecked, enable the difficulty levels.
            if (playerTwoAiCheckBox.IsChecked == true)
            {
                playerTwoAiMenu.IsEnabled = true;
            }
        }

        /// <summary>
        /// On checkbox uncheck disable appropriate menu
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void PlayerAiCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // if player one AI's checkbox is checked, disable the difficulty levels.
            if (playerOneAiCheckBox.IsChecked == false)
            {
                playerOneAiMenu.IsEnabled = false;
            }

            // if player two AI's checkbox is checked, disable the difficulty levels.
            if (playerTwoAiCheckBox.IsChecked == false)
            {
                playerTwoAiMenu.IsEnabled = false;
            }
        }
    }
}