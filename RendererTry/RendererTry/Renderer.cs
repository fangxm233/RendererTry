using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTry
{
    class Renderer
    {
        public static Bitmap buff;
        public static PointBitmap bitmap;
        public static Graphics graphics;
        public static Pen pen;
        public static Color color;

        public static void StartRender(int width, int height, Graphics graphics, Pen pen, Color color)
        {
            buff = new Bitmap(width, height);
            Renderer.graphics = graphics;
            Renderer.pen = pen;
            Renderer.color = color;
        }

        public static Bitmap Draw(Cube cube)
        {
            //buff = new Bitmap(buff.Width, buff.Height);
            bitmap = new PointBitmap(buff);
            bitmap.LockBits();
            cube.PointsTo2D();
            foreach (var item in cube.triangles)
            {
                DrawTrangle(item);
            }
            foreach (var item in cube.triangles)
            {
                DrawLines(item, Color.Red);
            }
            bitmap.UnlockBits();
            return buff;
        }

        public static void DrawTrangle(Triangle t)
        {
            //if(t.GetTrangleType() == DirectionType.left || t.GetTrangleType() == DirectionType.right)
            //{
            //    t.ToTwoTrangles(out Triangle t1, out Triangle t2);
            //    DrawTrangle(t1);
            //    DrawTrangle(t2);
            //    return;
            //}
            if(t.type == DirectionType.top)
            {
                Vector2 vt = t.GetPoint(DirectionType.top)[0];
                if(t.GetPoint(DirectionType.left).Length == 2)
                {
                    Vector2 vr = t.GetPoint(DirectionType.right)[0];
                    Vector2 vl = new Vector2(vt.x, vr.y);
                    Vector2 v = vt - vr;
                    float k = v.y / v.x;
                    for (int y = 0; y < -v.y; y++)
                    {
                        DrawLine(new Vector2(vl.x, vl.y - y), new Vector2(vr.x - y / k, vl.y - y));
                    }
                    return;
                }
                if (t.GetPoint(DirectionType.right).Length == 2)
                {
                    Vector2 vl = t.GetPoint(DirectionType.left)[0];
                    Vector2 vr = new Vector2(vt.x, vl.y);
                    Vector2 v = vt - vl;
                    float k = v.y / v.x;
                    for (int y = 0; y < -v.y; y++)
                    {
                        DrawLine(new Vector2(vl.x - y / k, vl.y - y), new Vector2(vt.x, vl.y - y));
                    }
                    return;
                }
                {
                    Vector2 vl = t.GetPoint(DirectionType.bottom)[0];
                    Vector2 vr = t.GetPoint(DirectionType.bottom)[1];
                    Vector2 v1 = vt - vl;
                    Vector2 v2 = vt - vr;
                    float k1 = v1.y / v1.x;
                    float k2 = v2.y / v2.x;
                    for (int y = 0; y < -v1.y; y++)
                    {
                        DrawLine(new Vector2(vl.x - y / k1, vl.y - y), new Vector2(vr.x - y / k2, vl.y - y));
                    }
                    return;
                }
            }
            if (t.type == DirectionType.bottom)
            {
                Vector2 vb = t.GetPoint(DirectionType.bottom)[0];
                if (t.GetPoint(DirectionType.left).Length == 2)
                {
                    Vector2 vr = t.GetPoint(DirectionType.right)[0];
                    Vector2 vl = new Vector2(vb.x, vr.y);
                    Vector2 v = vb - vr;
                    float k = v.y / v.x;
                    for (int y = 0; y < v.y; y++)
                    {
                        DrawLine(new Vector2(vl.x, vl.y + y), new Vector2(vr.x + y / k, vl.y + y));
                    }
                    return;
                }
                if (t.GetPoint(DirectionType.right).Length == 2)
                {
                    Vector2 vl = t.GetPoint(DirectionType.left)[0];
                    Vector2 vr = new Vector2(vb.x, vl.y);
                    Vector2 v = vb - vl;
                    float k = v.y / v.x;
                    for (int y = 0; y < v.y; y++)
                    {
                        DrawLine(new Vector2(vl.x + y / k, vl.y + y), new Vector2(vb.x, vl.y + y));
                    }
                    return;
                }
                {
                    Vector2 vl = t.GetPoint(DirectionType.top)[0];
                    Vector2 vr = t.GetPoint(DirectionType.top)[1];
                    Vector2 v1 = vb - vl;
                    Vector2 v2 = vb - vr;
                    float k1 = v1.y / v1.x;
                    float k2 = v2.y / v2.x;
                    for (int y = 0; y < v1.y; y++)
                    {
                        DrawLine(new Vector2(vl.x + y / k1, vl.y + y), new Vector2(vr.x + y / k2, vl.y + y));
                    }
                    return;
                }
            }
            t.ToTwoTrangles(out Triangle t1, out Triangle t2);
            DrawTrangle(t1);
            DrawTrangle(t2);

        }

        //public static void DrawLine(Vector2 v1,Vector2 v2)
        //{
        //    graphics.DrawLine(pen, new Point((int)v1.x, (int)v1.y), new Point((int)v2.x, (int)v2.y));
        //}

        public static void DrawLine(Vector2 v1, Vector2 v2)
        {
            Vector2 v = v1 - v2;
            v = new Vector2((int)v.x, (int)v.y);
            if(v.x == 0)
            {
                if (v.y > 0)
                {
                    for (int i = 0; i < v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y - i);
                    }
                }
                else
                {
                    for (int i = 0; i < -v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y + i);
                    }
                }
            }
            float k = v.y / v.x;
            if (v.x > 0)
            {
                for (int i = 0; i < (int)v.x; i++)
                {
                    SetPixel((int)v1.x - i, (int)v1.y - (int)(i * k));
                }
            }
            else
            {
                for (int i = 0; i < -(int)v.x; i++)
                {
                    SetPixel((int)v1.x + i, (int)v1.y + (int)(i * k));
                }
            }
        }

        public static void DrawLine(Vector2 v1, Vector2 v2, Color color)
        {
            Vector2 v = v1 - v2;
            v = new Vector2((int)v.x, (int)v.y);
            if (v.x == 0)
            {
                if (v.y > 0)
                {
                    for (int i = 0; i < v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y - i, color);
                    }
                }
                else
                {
                    for (int i = 0; i < -v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y + i, color);
                    }
                }
            }
            float k = v.y / v.x;
            if (v.x > 0)
            {
                for (int i = 0; i < (int)v.x; i++)
                {
                    SetPixel((int)v1.x - i, (int)v1.y - (int)(i * k), color);
                }
            }
            else
            {
                for (int i = 0; i < -(int)v.x; i++)
                {
                    SetPixel((int)v1.x + i, (int)v1.y + (int)(i * k), color);
                }
            }
        }

        public static void DrawLines(Triangle triangle)
        {
            DrawLine(triangle.GetPoint2D(0), triangle.GetPoint2D(1));
            DrawLine(triangle.GetPoint2D(1), triangle.GetPoint2D(2));
            DrawLine(triangle.GetPoint2D(0), triangle.GetPoint2D(2));
        }

        public static void DrawLines(Triangle triangle, Color color)
        {
            DrawLine(triangle.GetPoint2D(0), triangle.GetPoint2D(1), color);
            DrawLine(triangle.GetPoint2D(1), triangle.GetPoint2D(2), color);
            DrawLine(triangle.GetPoint2D(0), triangle.GetPoint2D(2), color);
        }

        public static void SetPixel(int x,int y)
        {
            bitmap.SetPixel(x, y, color);
        }

        public static void SetPixel(int x, int y, Color color)
        {
            bitmap.SetPixel(x, y, color);
        }
    }
}
