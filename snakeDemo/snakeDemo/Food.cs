using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace snakeDemo
{
    class Food
    {
        public Label foodBox = new Label();
        int spawnX, spawnY; // x and y vars to randomly set from gameCanvas based on canvas size

        public Food()
        {
            
        }

        public void setFoodProperties()
        {
            foodBox.Width = 16;
            foodBox.Height = 16;
            foodBox.Location = new Point(spawnX, spawnY);
            foodBox.BackColor = Color.Red;

        }


        // mutators for spawnX and spawnY vars

        public void setX(int x)
        {
            spawnX = x;
        }

        public void setY(int y)
        {
            spawnY = y;
        }
    }
}
