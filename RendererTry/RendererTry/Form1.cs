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
        Cube cube;
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
            Renderer.StartRender(512, 512, graphics, pen, Color.Black, RenderType.GouraudShading);
            g = Graphics.FromImage(Renderer.buff);
            graphics = CreateGraphics();
            pen = new Pen(Brushes.Red, 1);
            color = Color.Black;
            cube = new Cube(new Vector3(0, 0, 3));
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

        private void timer1EventProcessor(object sender, EventArgs e)//cube.rotation + new Vector3(0, 0.01f, 0)
        {
            int time1 = DateTime.Now.Millisecond;
            g.Clear(Color.White);
            Renderer.CameraRotateTo(Renderer.camera_rotation + new Vector3(0, 0, 0), new Cube[] { cube });
            cube.RotateTo(cube.rotation + new Vector3(0.01f, 0.01f, 0.01f));
            Renderer.Draw(cube);
            //Bitmap bitmap = Renderer.Draw(cube);
            //Renderer.buff = new Bitmap(Renderer.buff.Width, Renderer.buff.Height);
            //Renderer.bitmap = new PointBitmap(Renderer.buff);
            //Renderer.bitmap.LockBits();
            //Renderer.DrawTrangle(new Triangle(new Vector2(235.6037f, 453.7175f), new Vector2(441.3164f, 387.3885f), new Vector2(80.00184f, 380.7819f)));
            //Bitmap bitmap = Renderer.buff;
            //Renderer.bitmap.UnlockBits();
            //Console.WriteLine("x:{0} y:{1} z:{2}", cube.rotation.x, cube.rotation.y, cube.rotation.z);
            //int p1 = 4, p2 = 0, p3 = 5;
            //Console.WriteLine("x:{0} y:{1} x:{2} y:{3} x:{4} y:{5}", cube.points_2D[p1].x, cube.points_2D[p1].y, cube.points_2D[p2].x, cube.points_2D[p2].y, cube.points_2D[p3].x, cube.points_2D[p3].y);
            //graphics.Clear(Color.White);
            graphics.DrawImage(Renderer.buff, 0, 0);
            int time2 = DateTime.Now.Millisecond;
            if ((time2 - time1) != 0)
                main.Text = "帧数：" + 1000 / (time2 - time1);
            GC.Collect();//x:235.6037 y:453.7175 x:441.3164 y:387.3885 x:80.00184 y:380.7819
            //graphics.Clear(Color.White);
            //Renderer.buff = new Bitmap(Renderer.buff.Width, Renderer.buff.Height);
            //Renderer.DrawLine(new Vector2(0, 0), new Vector2(100, 100));
            //graphics.DrawImage(Renderer.buff, new Point(0, 0));x:441.3164 y:124.6115 x:265.7621 y:161.3682 x:235.6037 y:58.28254
        }
    }
}
