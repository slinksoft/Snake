using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace snakeDemo
{
    class Snake
    {
        public List<Label> snakeBody = new List<Label>();
        int size = 5; // default size var of body pieces;
        int widthHeight = 16;
        int speed = 18; // speed that snake is moving per pixel on canvas (SHOULD BE THE SAME AS THE SNAKE'S WIDTH AND HEIGHT  (controlled by widthHeight var) OR UNEXPECTED RESULTS MAY OCCUR)
        int defaultX = 150; // for use in initial  x coordinate spawning of snake body; snake will spawn horizontaly
        int direction = 0; // var to handle switch case for directions

        public Snake()
        {
            setSnakeProperties();
        }

        private void setSnakeProperties()
        {
            // loop to handle initial body spawning for the snake
            for (int i = 0; i < size; i++)
            {
                snakeBody.Add(new Label());
                snakeBody[i].Width = widthHeight; 
                snakeBody[i].Height = widthHeight;
                snakeBody[i].Location = new Point(defaultX, 150);
                snakeBody[i].BackColor = Color.Green;
                defaultX -= widthHeight; // using widthHeight's int value to space out each body piece evenly on initial spawn of the snake body
            }
        }

        private void controlSnakeBody()
        {
            // loop to control movement of the entire snake body. Simplest algorithm to use. Starts moving the snake from the tail to the head.
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i].Left = snakeBody[i - 1].Left;
                snakeBody[i].Top = snakeBody[i - 1].Top;
            }
        }

        public void moveSnake()
        {   
            // controls direction on where the snake is moving. the first moving part of the snake body is the head. body movement updates via controlSnakeBody()
            switch (direction)
            {
                case 0:
                    {
                        controlSnakeBody();
                        snakeBody[0].Left += speed;
                        break;
                    }

                case 1:
                    {
                        controlSnakeBody();
                        snakeBody[0].Left -= speed;
                        break;
                    }

                case 2:
                    {
                        controlSnakeBody();
                        snakeBody[0].Top -= speed;
                        break;
                    }

                case 3:
                    {
                        controlSnakeBody();
                        snakeBody[0].Top += speed;
                        break;
                    }
            }
        }

        // method/function to add piece to the snake body. adds an element to the snakeBody list, sets new pieces properties, and sets coords to place it near the tail upon spawn
        public void addPiece()
        {
            snakeBody.Add(new Label());
            snakeBody[snakeBody.Count - 1].Width = widthHeight;
            snakeBody[snakeBody.Count - 1].Height = widthHeight;
            snakeBody[snakeBody.Count - 1].Location = new Point(snakeBody[snakeBody.Count - 1].Location.X, snakeBody[snakeBody.Count - 1].Location.Y);
            snakeBody[snakeBody.Count - 1].BackColor = Color.Green;
        }

        // accessor for default size variable and speed
        public int getSize()
        {
            return size;
        }

        public int getSpeed()
        {
            return speed;
        }

        // mutator to set direction from gameCanvas
        public void setDirection(int dir)
        {
            direction = dir;
        }

    }
}
