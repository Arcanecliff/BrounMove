using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrounMove
{
    public partial class Form1 : Form
    {
        public float dt = 0.01f;
        List<particle> listParticle = new List<particle>();
        public class particle
        {
            public float x;
            public float y;
            public float vx;
            public float vy;
            public float m;
            public float size;
            public particle(float x, float y, float vx, float vy, float m, float size)
            {
                this.x = x;
                this.y = y;
                this.vx = vx;
                this.vy = vy;
                this.m = m;
                this.size = size;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Timer timer = new Timer();
            timer.Interval = 20;
            for (int i = 0; i < 30; i++)
            {
                particle p = new particle(rnd.Next(1, pictureBox1.Width), rnd.Next(1, pictureBox1.Height), rnd.Next(-40, 40), rnd.Next(-40, 40), 1, 30);
                listParticle.Add(p);
            }
            //listParticle.Add(new particle(pictureBox1.Width / 2, pictureBox1.Height / 4, 0, 40, 1, 40));
            //listParticle.Add(new particle(pictureBox1.Width / 2, pictureBox1.Height / 2, 0, -40, 1, 40));
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.Black);
            for (int i = 0; i < listParticle.Count; i++)
            {
                g.DrawEllipse(Pens.White, listParticle[i].x, listParticle[i].y, listParticle[i].size, listParticle[i].size);
            }
            timer.Tick += new EventHandler((o, ev) =>
            {
                for (int i = 0; i < listParticle.Count; i++)
                {
                    g.DrawEllipse(Pens.Black, listParticle[i].x, listParticle[i].y, listParticle[i].size, listParticle[i].size);
                }

                for (int i = 0; i < listParticle.Count; i++)
                {
                    for (int j = 0; j < listParticle.Count; j++)
                    {
                        if (i != j)
                        {
                            float d = (float)Math.Sqrt((listParticle[i].x / 2 - listParticle[j].x / 2) * (listParticle[i].x / 2 - listParticle[j].x / 2) +
                                (listParticle[i].y / 2 - listParticle[j].y / 2) * (listParticle[i].y / 2 - listParticle[j].y / 2)) - (listParticle[i].size / 2);

                            if (d < 1 && d > 0)
                            {
                                //Console.WriteLine("well");
                                float f = listParticle[i].size / 2 + listParticle[j].size / 2  - d;
                                listParticle[i].vx +=(float)(f * (listParticle[i].x / 2 - listParticle[j].x / 2) / d / listParticle[i].m * dt);
                                listParticle[i].vy += (float)(f * (listParticle[i].y / 2 - listParticle[j].y / 2) / d / listParticle[i].m * dt);
                                listParticle[j].vx -= (float)(f * (listParticle[i].x / 2 - listParticle[j].x / 2) / d / listParticle[j].m * dt);
                                listParticle[j].vy -= (float)(f * (listParticle[i].y / 2 - listParticle[j].y / 2) / d / listParticle[j].m * dt);
                            }
                        }
                    }
                }
                for (int i = 0; i < listParticle.Count; i++)
                {
                    listParticle[i].x += listParticle[i].vx * dt;
                    listParticle[i].y += listParticle[i].vy * dt;
                    //if (listParticle[i].x < 480)
                    //    listParticle[i].x += 480;

                    //if (listParticle[i].y < 0)
                    //    listParticle[i].y += 480;

                    //if (listParticle[i].x > 0)
                    //    listParticle[i].x -= 480;

                    //if (listParticle[i].y > 0)
                    //    listParticle[i].y -= 480;
                }
                for (int i = 0; i < listParticle.Count; i++)
                    g.DrawEllipse(Pens.White, listParticle[i].x, listParticle[i].y, listParticle[i].size, listParticle[i].size);
            }
            );
            timer.Start();
        }
    }
}
