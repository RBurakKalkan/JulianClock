using System;
using System.Drawing;
using System.Windows.Forms;

namespace JulianClock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DateTime jd, begin, today;
        TimeSpan difference;
        double a, b, c, ss;
        int WIDTH = 300, HEIGHT = 300, secHAND = 140;
        int cx, cy;
        Bitmap bmp;
        Graphics g;
        Int32 Days;
        DateTime simdi = DateTime.Now;
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime simdi = DateTime.Now;
            simdi = simdi.ToUniversalTime();
            today = DateTime.UtcNow;
            today = today.AddHours(-12);
            begin = new DateTime(2016, 5, 10, today.Hour, today.Minute, today.Second);
            begin = begin.AddHours(-12);
            difference = today - begin;
            Days = 2457519 + difference.Days;
            jd = simdi.AddHours(-12);
            a = jd.Hour;
            b = jd.Minute;
            c = jd.Second;
            ss = (b / 60 + c / 3600 + a) / 24 ;
            label1.Text = (Days + (b / 60 + c / 3600 + a) / 24).ToString("0.0000000");
            g = Graphics.FromImage(bmp);
            int[] handCoord = new int[2];
            g.Clear(Color.White);
            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, WIDTH, HEIGHT);
            g.DrawString("1", new Font("Arial", 12), Brushes.Black, new PointF(140, 0));
            g.DrawString("0.25", new Font("Arial", 12), Brushes.Black, new PointF(266, 140));
            g.DrawString("0.50", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("0.75", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, .5F), new Point(150, cy), new Point(handCoord[0], handCoord[1]));
            pictureBox1.Image = bmp;
            g.Dispose();
            lbl_tarih.Text = DateTime.Now.ToString();
            lbl_Utc.Text = DateTime.Now.ToUniversalTime().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime born;
            TimeSpan fark;
            born = dateTimePicker1.Value;
            fark = DateTime.Now - born;
            MessageBox.Show(born.ToShortDateString()+" ve "+ DateTime.Now.ToShortDateString() + " arasında "+ fark.Days.ToString()+" gün var.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            cx = WIDTH / 2;
            cy = HEIGHT / 2;
            pictureBox1.BackColor = Color.White;
        }
        private int[] msCoord(double val, int hlem)
        {
            double x;
            int[] coord = new int[2];
            x = 360 * val;
            label2.Text = x.ToString("0.000000");
            if (val >= 0 && val <= 360)
            {
                coord[0] = cx + (int)(hlem * Math.Sin(Math.PI * x / 180));
                coord[1] = cx - (int)(hlem * Math.Cos(Math.PI * x / 180));
            }
            else 
            {
                coord[0] = cx + (int)(hlem * -Math.Sin(Math.PI * x / 180));
                coord[1] = cx - (int)(hlem * Math.Cos(Math.PI * x / 180));
            }
            return coord;
        }
    }
}
