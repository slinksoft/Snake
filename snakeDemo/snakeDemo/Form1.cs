using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace snakeDemo
{
    public partial class gameCanvas : Form
    {
        Snake snakee = new Snake(); // create new instance for Snake object
        Label scoreDisplay = new Label(); // create control/object for score display label
        Food foodd = new Food(); // create new instance for Food object
        border borderr = new border(); // create new instance for border object
        bool up, down, left, right; // vars to control snake movement via key press
        bool firstFoodIsSpawned = false; // bool only allow Controls.Add() to execute once for initial food spawn
        bool isGameOver = false; // bool to flag a gameover; so multiple game over collision checks do not occur in collisionCheck timer
        int score = 0;
        public gameCanvas()
        {
            InitializeComponent();

            // set window/canvas size

            this.Width = 525;
            this.Height = 525;
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(this.Width, this.Height);

            spawnInitialSnake(); // call to spawn initial snake
            right = true; // set default direction
            spawnFood(); // call to spawn food
            spawnBorder(); // spawns border
            createScoreLabel();
        }

        private void spawnInitialSnake()
        {
            for (int i = 0; i < snakee.getSize(); i++)
            {
                Controls.Add(snakee.snakeBody[i]);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // switch statement that controls movement boolean vars via key press
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        if (down == false)
                        {
                            up = true;
                            down = false;
                            left = false;
                            right = false;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (up == false)
                        {
                            up = false;
                            down = true;
                            left = false;
                            right = false;
                        }
                        break;
                    }

                case Keys.Left:
                    {
                        if (right == false)
                        {
                            up = false;
                            down = false;
                            left = true;
                            right = false;
                        }
                        break;
                    }

                case Keys.Right:
                    {
                        if (left == false)
                        {
                            up = false;
                            down = false;
                            left = false;
                            right = true;
                        }
                        break;
                    }

                case Keys.R:
                    {
                        Application.Restart();
                        break;
                    }
            }
        }

        private void movementTimer_Tick(object sender, EventArgs e)
        {
            // 0 = right ; 1 = left; 2 = up ; 3 = down
            if (left == true)
            {
                snakee.setDirection(1);
                snakee.moveSnake();
            }
            else if (right == true)
            {
                snakee.setDirection(0);
                snakee.moveSnake();
            }
            else if (up == true)
            {
                snakee.setDirection(2);
                snakee.moveSnake();
            }
            if (down == true)
            {
                snakee.setDirection(3);
                snakee.moveSnake();
            }

        }

        // function/method to spawn food within boundaries of the game canvas
        private void spawnFood()
        {
            // bool value check in place to execute Controls.Add() on its first spawn
            if (firstFoodIsSpawned == false)
            {
                firstFoodIsSpawned = true;
                Random randd = new Random();
                foodd.setX(randd.Next(snakee.getSpeed() * 2, this.Width - 200));
                foodd.setY(randd.Next(snakee.getSpeed() * 2, this.Height - 200));
                foodd.setFoodProperties();
                Controls.Add(foodd.foodBox);
            }

            // spawns food at specified coordinates correlating within the boundaries of thew game canvas
            Random rand = new Random();
            foodd.setFoodProperties();
            foodd.setX(rand.Next(snakee.getSpeed(), this.Width - 100));
            foodd.setY(rand.Next(snakee.getSpeed(), this.Height - 100));
            foodd.setFoodProperties();

            // checks to se if food spawned on top of a snake piece. If it does, run spawnFood() again to give it another random location to spawn at
            for (int i = 1; i < snakee.snakeBody.Count; i++)
            {
                if (foodd.foodBox.Bounds.IntersectsWith(snakee.snakeBody[i].Bounds))
                    spawnFood();
            }
        }

        private void collisionCheck_Tick(object sender, EventArgs e)
        {
            // checks to see if the snake's head collided with the food object. Will respawn the food in a different location, add a snake piece to its body, and increment the score value.
            if (snakee.snakeBody[0].Bounds.IntersectsWith(foodd.foodBox.Bounds))
            {
                score++;
                foodd.foodBox.Location = new Point(-100, -100);
                snakee.addPiece();
                Controls.Add(snakee.snakeBody[snakee.snakeBody.Count - 1]);
                spawnFood();
                if (movementTimer.Interval > 10)
                    movementTimer.Interval -= 2;
            }

            // checks to see if the snake's head collided with any of the 4 border objects. using foreach to avoid using multiple if statements
            foreach (Control c in this.Controls)
            {
                if (c.Name == "top" || c.Name == "bottom" || c.Name == "right" || c.Name == "left")
                {
                    if (snakee.snakeBody[0].Bounds.IntersectsWith(c.Bounds) && isGameOver == false)
                    {
                        // will take every body piece and send it to a bogus location to then dispose of each object.
                        for (int i = 0; i < snakee.snakeBody.Count; i++)
                        {
                            snakee.snakeBody[i].Location = new Point(-100, -100);
                            snakee.snakeBody[i].Dispose();
                        }

                        isGameOver = true;
                        movementTimer.Stop(); // stops movement timer;
                        collisionCheck.Stop();
                        MessageBox.Show("Game Over!" + System.Environment.NewLine + "Final Score: " + score + System.Environment.NewLine + "Press R To Restart", "Game Over!"); // displays game over in a winform message box
                    }
                }
            }

            for (int i = 1; i < snakee.snakeBody.Count; i++) // starts at 1 so the head doesnt check for first piece upon initial spawn, fixing instant game over bug
            {
                if (snakee.snakeBody[0].Bounds.IntersectsWith(snakee.snakeBody[i].Bounds) && isGameOver == false)
                {
                    // will take every body piece and send it to a bogus location to then dispose of each object.
                    for (int j = 0; j < snakee.snakeBody.Count; j++)
                    {
                        snakee.snakeBody[j].Location = new Point(-100, -100);
                        snakee.snakeBody[j].Dispose();
                    }

                    isGameOver = true;
                    movementTimer.Stop(); // stops movement timer;
                    collisionCheck.Stop();
                    MessageBox.Show("Game Over!" + System.Environment.NewLine + "Final Score: " + score + System.Environment.NewLine + "Press R To Restart", "Game Over!"); // displays game over in a winform message box
                }
            }

            scoreDisplay.Text = Convert.ToString(score); // updates score display label
        }

        // method/function to set border object properties in border and spawn borders into the canvas. Borders are relative to canvas size, so the game canvas window can be any desires size. see constructor to change size of the game canvas.
        private void spawnBorder()
        {
            // sets top and bottom borders x and y coordinates, as well as their width and height according to size of canvas
            borderr.top.Left = 0;
            borderr.top.Top = -40;
            borderr.bottom.Left = 0;
            borderr.bottom.Top = this.Height - 50;
            borderr.setVWidth(this.Width);
            borderr.setVHeight(50);

            // sets left and right borders x and y coordinates, as well as their width and height according to size of canvas
            borderr.left.Left = -40;
            borderr.left.Top = 0;
            borderr.right.Left = this.Width - 28;
            borderr.right.Top = 0;
            borderr.setHHeight(this.Height);
            borderr.setHWidth(50);

            //spawn border objects and update their properties
            Controls.Add(borderr.top);
            Controls.Add(borderr.bottom);
            Controls.Add(borderr.left);
            Controls.Add(borderr.right);
            borderr.setVerticalBarsProperites();
            borderr.setHorizontalBarProperties();
        }

        private void createScoreLabel()
        {
            scoreDisplay.ForeColor = Color.White;
            scoreDisplay.Location = new Point(10, 10);
            scoreDisplay.Text = "0";
            Controls.Add(scoreDisplay);
        }

    }
}
