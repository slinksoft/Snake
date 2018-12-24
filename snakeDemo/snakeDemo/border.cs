using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace snakeDemo
{
    class border
    {
        int hWidth, hHeight;  // horizontal border data vars
        int vWidth, vHeight; // vertical border data vars
        
        // border objects in form of Labels
        public Label top = new Label();
        public Label bottom = new Label();
        public Label left = new Label();
        public Label right = new Label();

        public border()
        {
            setVerticalBarsProperites();
            setHorizontalBarProperties();

        }

        public void setVerticalBarsProperites()
        {
            top.Size = new Size(vWidth, vHeight);
            top.Name = "top"; //setting names for use of foreach collision check in collisionCheck timer in gameCanvas
            bottom.Size = new Size(vWidth, vHeight);
            bottom.Name = "bottom";
            top.BackColor = Color.Blue;
            bottom.BackColor = Color.Blue;
        }

        public void setHorizontalBarProperties()
        {
            left.Size = new Size(hWidth, hHeight);
            left.Name = "left"; //setting names for use of foreach collision check in collisionCheck timer in gameCanvas
            right.Size = new Size(hWidth, hHeight);
            right.Name = "right"; //setting names for use of foreach collision check in collisionCheck timer in gameCanvas
            left.BackColor = Color.Blue;
            right.BackColor = Color.Blue;
        }

        //mutators for width and height of border objects. Using mutators to set the properites of all horizontal and vertical borders at once in gameCanvas

        public void setHWidth(int width)
        {
            hWidth = width;
        }

        public void setHHeight(int height)
        {
            hHeight = height;
        }


        public void setVWidth(int width)
        {
            vWidth = width;
        }

        public void setVHeight(int height)
        {
            vHeight = height;
        }
    }
}
