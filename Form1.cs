using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace game2
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging,lose=false;
        private int countCoins = 0;
        public Form1()
        {
            InitializeComponent();
            pic1.MouseDown += MouseClickDown;
            pic1.MouseUp += MouseClickUp;
            pic1.MouseMove += MouseClickMove;

            pic2.MouseDown += MouseClickDown;
            pic2.MouseUp += MouseClickUp;
            pic2.MouseMove += MouseClickMove;
            label2.Visible = false;
            label1.Visible=false;
            label4.Visible = false;
            label3.Visible = true;
            KeyPreview = true;

        }
        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if(dragging)
            {
                Point point=PointToScreen(new Point(e.X,e.Y));
                this.Location=new Point(point.X-pos.X,point.Y-pos.Y+pic1.Top);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) return;
           
            int speed = 7;
            if((e.KeyCode==Keys.Left || e.KeyCode==Keys.A)&&player.Left>95)
            {
                player.Left -= speed;
            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Left <355)
            {
                player.Left += speed;
            }
        }
       

        private void timer_Tick_1(object sender, EventArgs e)
        {
            int speed = 5;
            pic1.Top += speed;
            pic2.Top += speed;
            if (pic1.Top >= 360)
            {
                pic1.Top = 0;
                pic2.Top = -360;

            }
            coin.Top += speed;
            if (coin.Top >= 360)
            {
                coin.Top = -60;
                Random rand = new Random();
                coin.Left = rand.Next(100, 345);
            }
            int speedCar = 5;

            bot1.Top +=speedCar;
            bot2.Top += speedCar;
            if(bot1.Top>=310)
            {
                bot1.Top = -100;
                Random rand=new Random();
                bot1.Left = rand.Next(100, 345);
            }

            if (bot2.Top >= 310)
            {
                bot2.Top = -100;
                Random rand = new Random();
                bot2.Left = rand.Next(100, 345);
            }
            if (player.Bounds.IntersectsWith(bot1.Bounds) || player.Bounds.IntersectsWith(bot2.Bounds) )
            {
                label1.Visible = true;
                label2.Visible = true;
                label4.Text = "Собранные монеты " + countCoins.ToString();
                label3.Visible = false;
                label4.Visible = true;

                timer.Enabled = false;
                lose = true;

            }
            if(player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins++;
                label3.Text="Монеты "+countCoins.ToString();
                coin.Top = -60;
                Random rand = new Random();
                coin.Left = rand.Next(100, 345);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            bot1.Top = -100;
            bot2.Top = -130;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = true;
            label4.Visible = false;

            timer.Enabled = true;
            lose = false;
            countCoins = 0;
            label3.Text = "Монеты " + countCoins.ToString();
            coin.Top = -400;

        }
    }
}
