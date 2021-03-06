﻿using System.Drawing;
using System.Windows.Forms;

namespace Common.Models
{
    public class GripSplitContainer : System.Windows.Forms.SplitContainer
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle splitter = this.SplitterRectangle;
            int count = 0;
            Rectangle grip = new Rectangle(splitter.X, splitter.Y, splitter.Width-1 , 2); 
            SolidBrush gripBrush = new SolidBrush(Color.Gray);
            SolidBrush gripBrushHighlight = new SolidBrush(Color.White);
            Brush systemBrush = SystemBrushes.ScrollBar;

            e.Graphics.FillRectangle(systemBrush, splitter);

            while (grip.Y < (splitter.Height - 6))
            {
                if (count != 0 && count % 2 == 0)
                {
                    grip.Y += 1;
                    grip.Width += 1;
                    e.Graphics.FillRectangle(gripBrushHighlight, grip);
                    grip.Y -= 1;
                    grip.Width -= 1;
                    e.Graphics.FillRectangle(gripBrush, grip);
                }

                grip.Y += 4;
                count++;
            }

            gripBrush.Dispose();
            gripBrushHighlight.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

    }
}
