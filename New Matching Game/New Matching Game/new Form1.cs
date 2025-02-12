using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Matching_Game
{
    public partial class Form1 : Form
    {
        // firstClicked points to the first Label control that the player clicks, but it will be null 
        // if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control that the player clicks
        Label secondClicked = null;

        //private int trycount = 0;
        private int elapsedTime = 0;

        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon in the Webdings font, and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "A", "A", "B", "B", "C", "C", "D", "D",
            "E", "E", "F", "F", "G", "G", "H", "H",
            "I", "I", "J", "J"
        };


        // Assign each icon from the list of icons to a random square
        private void AssignIconsToSquares()
        {
            // The TableLayoutPanel has 36 labels, and the icon list has 36 icons,
            // so an icon is pulled at random from the list and added to each label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label; //converts the control variable to a label named iconLabel
                if (iconLabel != null) //check the conversion
                {
                    int randomNumber = random.Next(icons.Count); // get next random number within range defined by icons.Count
                    iconLabel.Text = icons[randomNumber]; // assign icon to Text property of location
                    iconLabel.ForeColor = iconLabel.BackColor; // hides the icons
                    icons.RemoveAt(randomNumber); // removes icon used from the list
                }
            }
        }

        private void InitializeGame()
        { 
            // Reset elapsed time and start the timer
            elapsedTime = 0;
            lblTimer.Text = "Timer: 0";
            timerGame.Start();

            // Hide the restart button
           /* if (button1 != null)
            {
                button1.Visible = false;
            }*/
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();

            InitializeGame();
        }


        /// Every label's Click event is handled by this event handler
        /// <param name="sender">The label that was clicked</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching icons have been shown to the player, 
            // so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is white, the player clicked an icon that's already been revealed -- ignore the click
                if (clickedLabel.ForeColor == Color.White)
                    return;

                // If firstClicked is null, this is the first icon in the pair that the player clicked,
                // so set firstClicked to the label that the player clicked, change its color to white, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.White;

                    return;
                }

                // If the player gets this far, the timer isn't running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // Set its color to white
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.White;

                // Check to see if the player won
                CheckForWinner();

                // If the player clicked two matching icons, keep them white and reset firstClicked and secondClicked 
                // so the player can click another icon
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player gets this far, the player clicked two different icons, so start the timer 
                timer1.Start();
            }
        }

        /// <summary>
        /// This timer is started when the player clicks two icons that don't match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked so the next time a label is clicked,
            // the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }

        /// Check every icon to see if it is matched, by comparing its foreground color to its background color. 
        /// If all of the icons are matched, the player wins
        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            timerGame.Stop();

            // Show the restart button
            /* button1.Click += button1_Click; ;
             this.Controls.Add(button1);
             button1.Visible = true;

            WinDialog winDialog = new WinDialog(elapsedTime);
            winDialog.ShowDialog();

            if (winDialog.RestartRequested)
            {
                InitializeGame();

                AssignIconsToSquares();
            }*/

            // If the loop didn’t return, it didn't find any unmatched icons
            // That means the user won. Show a message and close the form
             MessageBox.Show("You matched all the icons in " + elapsedTime + " seconds!", "Congratulations");
             Close();
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            lblTimer.Text = "Time: " + elapsedTime; // Update the timer label
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            // Remove the restart button and reinitialize the game
            this.Controls.Remove(button1);
            InitializeGame();
        }*/
    }
}


