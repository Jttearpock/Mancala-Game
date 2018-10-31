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
        /// Method that sets the game board objects to match the array values
        /// </summary>
        public void UpdateValues()
        {
            for (int x = 0; x < 14; x++)
            {
                string objectName = "txtboxSlot" + x;
                TextBox currentBox = FindName(objectName) as TextBox;

                currentBox.ToolTip = this.currentGame.ArrGameBoard[x].ToString();
            }
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

            this.currentGame.ArrGameBoard[int.Parse(startPos)] = 0;
            this.UpdateValues();
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
                }
            }
            else
            {
                this.currentGame.SetStartValues();
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
            else if (this.currentGame.OnGoingGame == false)
            {
                this.currentGame.SetStartValues();
            }
        }

        private void PlayerTurn_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.TakeMove(sender as TextBox);
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
    

