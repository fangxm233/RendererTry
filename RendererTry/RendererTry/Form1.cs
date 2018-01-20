using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RendererTry
{
    public partial class Form1 : Form
    {
        public static Form1 main;
        Cube cube = new Cube(new Vector3(0, 0, 2));
        Graphics graphics;
        Graphics g;
        Pen pen;
        Color color;
        public static bool gg;

        public Form1()
        {
            main = this;
            InitializeComponent();
            DoubleBuffered = true;
            Renderer.StartRender(512, 512, graphics, pen, Color.Black);
            g = Graphics.FromImage(Renderer.buff);
            graphics = CreateGraphics();
            pen = new Pen(Brushes.Red, 1);
            color = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer1 = new Timer
            {
                Interval = 1,
                Enabled = true
            };
            timer1.Tick += new EventHandler(timer1EventProcessor);//添加事件
        }

        private void timer1EventProcessor(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            cube.RotateTo(cube.rotation + new Vector3(0, 0.01f, 0));
            g.DrawImage(Renderer.Draw(cube), 0, 0);
            //Bitmap bitmap = Renderer.Draw(cube);
            //Renderer.buff = new Bitmap(Renderer.buff.Width, Renderer.buff.Height);
            //Renderer.bitmap = new PointBitmap(Renderer.buff);
            //Renderer.bitmap.LockBits();
            //Renderer.DrawTrangle(new Triangle(new Vector2(441.3164f, 124.6115f), new Vector2(265.7621f, 161.3682f), new Vector2(235.6037f, 58.28254f)));
            //Bitmap bitmap = Renderer.buff;
            //Renderer.bitmap.UnlockBits();
            //Console.WriteLine("x:{0} y:{1} z:{2}", cube.rotation.x, cube.rotation.y, cube.rotation.z);
            //Console.WriteLine("x:{0} y:{1} x:{2} y:{3} x:{4} y:{5}", cube.points_2D[2].x, cube.points_2D[2].y, cube.points_2D[3].x, cube.points_2D[3].y, cube.points_2D[6].x, cube.points_2D[6].y);
            graphics.Clear(Color.White);
            graphics.DrawImage(Renderer.buff, 0, 0);
            GC.Collect();
            //graphics.Clear(Color.White);
            //Renderer.buff = new Bitmap(Renderer.buff.Width, Renderer.buff.Height);
            //Renderer.DrawLine(new Vector2(0, 0), new Vector2(100, 100));
            //graphics.DrawImage(Renderer.buff, new Point(0, 0));x:441.3164 y:124.6115 x:265.7621 y:161.3682 x:235.6037 y:58.28254
        }
    }
}
