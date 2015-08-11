using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuctionClient
{
    public partial class AnalogClock : UserControl
    {
        //variable for crrent time
        private DateTime nowOClock;
        //centre of control
        private Point centre;
        //length of second, minute and hour hand
        private float scdLength;
        private float minLength;
        private float hrsLength;
        
        const float pi = 3.141592F;

        public AnalogClock()
        {
            InitializeComponent();
            //add drawClock to picturebox paint event handler
            this.pictureBox1.Paint += new PaintEventHandler(this.DrawClock);

            centre = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            nowOClock = DateTime.Now;
            //divide third of the height of the control by differentiating value
            scdLength = this.Height / 3 / 1.15F;
            minLength = this.Height / 3 / 1.20F;
            hrsLength = this.Height / 3 / 1.65F;
            this.Refresh();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            //every time timer clicks update time and refresh control
            nowOClock = DateTime.Now;
            this.Refresh();
        }

        private void DrawClock(object sender, PaintEventArgs pevt)
        {
            //get number of radians for hour, minute and second
            //hourRadians = ((value representing current time) * number of degrees per hour "tick" on clock face * pi)/180
            float hourRadians = (nowOClock.Hour % 12 + nowOClock.Minute / 60F) * 30 * pi / 180;
            //minuteRadians = ((number of minutes) * number of degrees per second "tick" on clock face * pi)/180
            float minuteRadians = (nowOClock.Minute) * 6 * pi / 180;
            //minuteRadians = ((number of minutes) * number of degrees per second "tick" on clock face * pi)/180
            float secondRadians = (nowOClock.Second) * 6 * pi / 180;

            //for each draw clock hand
            //for each point find x and y with the pythagorean theorem
            pevt.Graphics.DrawLine(new Pen(Color.DimGray, (float)this.Height / 100),
                centre.X - (float)(scdLength / 9 * System.Math.Sin(secondRadians)),
                centre.Y + (float)(scdLength / 9 * System.Math.Cos(secondRadians)),
                centre.X + (float)(scdLength * System.Math.Sin(secondRadians)),
                centre.Y - (float)(scdLength * System.Math.Cos(secondRadians)));

            pevt.Graphics.DrawLine(new Pen(Color.DimGray, (float)this.Height / 95),
                centre.X - (float)(minLength / 9 * System.Math.Sin(minuteRadians)),
                centre.Y + (float)(minLength / 9 * System.Math.Cos(minuteRadians)),
                centre.X + (float)(minLength * System.Math.Sin(minuteRadians)),
                centre.Y - (float)(minLength * System.Math.Cos(minuteRadians)));

            pevt.Graphics.DrawLine(new Pen(Color.DimGray, (float)this.Height / 92),
                centre.X - (float)(hrsLength / 9 * System.Math.Sin(hourRadians)),
                centre.Y + (float)(hrsLength / 9 * System.Math.Cos(hourRadians)),
                centre.X + (float)(hrsLength * System.Math.Sin(hourRadians)),
                centre.Y - (float)(hrsLength * System.Math.Cos(hourRadians)));
        }
    }
}
