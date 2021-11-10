using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalnaForenzikaAdb.CustomControls
{
    public class FlatButton : Button
    {
        public FlatButton()
        {
        }

        public FlatButton(string text, int width, int height, Point location)
        {
            Text = text;
            Width = width;
            ForeColor = Color.Yellow;
            Height = height;
            Location = location;
        }
    }
}
