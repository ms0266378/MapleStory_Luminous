using Luminous;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GS
{
    public partial class Form2 : Form
    {
        public Rectangle Screen = new Rectangle(0, 0, 300, 300);
        public Point CoordResult = new Point(-1, -1);
        public int ColorYellow = 0xFFDD44;
        public int X, Y;
        Galaxy.Properties.Settings MySetting = new Galaxy.Properties.Settings();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "X:   " + "   " + "Y:   ";
            label2.Text = "";
            label3.Text = "Times";
            label4.Text = "Delay";
            label5.Text = "Press Time";
            label6.Text = "SetDelay";
            timer1.Interval = 300;
            timer2.Interval = 100;
            timer1.Enabled = true;
            button1.Text = "Test"; 
            button2.Text = "Test Press";
            this.Text = "Tool";
           
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            MySetting.Save();

            Form form1 = new Form1();
            form1.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CoordResult = UFO202.ColorSearch.PixelSearch(Screen, ColorYellow, 1);
            X = Convert.ToInt16(CoordResult.X.ToString());
            Y = Convert.ToInt16(CoordResult.Y.ToString());
            label1.Text = "X:" + X + "   " + "Y: " + Y + " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) > 0 && Convert.ToInt32(textBox3.Text) > 0 && Convert.ToInt32(textBox4.Text) >= 0 && Convert.ToInt32(textBox5.Text)>0)
            {
                timer2.Enabled = true;
                this.WindowState = FormWindowState.Minimized;
                MapleStory.MapleStoryActions.Delay(10);

                for (byte j=0;j<Convert.ToInt32(textBox5.Text);j++)
                { 
                    for (byte i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                    {
                        MapleStory.MapleStoryActions.C_Down();
                        MapleStory.MapleStoryActions.Delay(Convert.ToInt32(textBox3.Text));
                        MapleStory.MapleStoryActions.C_Up();
                    }
                    if (Convert.ToInt32(textBox4.Text) != 0)
                        MapleStory.MapleStoryActions.Delay(Convert.ToInt32(textBox4.Text));

                }
                this.WindowState = FormWindowState.Normal;
                timer2.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            FocusMainWindow.SetMainWindows.MapleStoryKeepTop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(Convert.ToInt32(textBox2.Text)>0)
            {
                timer2.Enabled = true;
                this.WindowState = FormWindowState.Minimized;
                MapleStory.MapleStoryActions.Delay(10);
                MapleStory.MapleStoryActions.C_Down();
                MapleStory.MapleStoryActions.Delay(Convert.ToInt32(textBox1.Text));
                MapleStory.MapleStoryActions.C_Up();
                this.WindowState = FormWindowState.Normal;
                timer2.Enabled = false;
            }
        }

       

    }
}
