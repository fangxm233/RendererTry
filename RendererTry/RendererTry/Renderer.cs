using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTry
{
    enum RenderType
    {
        GouraudShading, NoShading
    }
    class Renderer
    {
        public static Bitmap buff;
        public static PointBitmap bitmap;
        public static Graphics graphics;
        public static Pen pen;
        public static Color color;
        public static Vector3 camera_rotation = new Vector3(0, 0, 0), camera_position = new Vector3(0, 0, 0);
        public static int width, height;
        public static RenderType renderType;

        public static void StartRender(int width, int height, Graphics graphics, Pen pen, Color color, RenderType renderType)
        {
            Renderer.width = width;
            Renderer.height = height;
            buff = new Bitmap(width, height);
            Renderer.graphics = graphics;
            Renderer.pen = pen;
            Renderer.color = color;
            Renderer.renderType = renderType;
        }

        public static Bitmap Draw(Cube cube)
        {
            //buff = new Bitmap(buff.Width, buff.Height);
            bitmap = new PointBitmap(buff);
            bitmap.LockBits();
            if (IsOutOfCamera(cube)) { bitmap.UnlockBits(); return buff; }
            foreach (var item in cube.triangles)
            {
                if (!BackFaceCulling(item))
                    DrawTrangle(item);
            }
            //foreach (var item in cube.triangles)
            //{
            //    if (!BackFaceCulling(item))
            //        DrawLines(item, Color.Red);
            //    //break;
            //}//82 255 94 215 117//82 212 255 296 394
             //DrawLine(new Vectorx(new Vector2(100, 117), Color.FromArgb(82, 255, 94)), new Vectorx(new Vector2(300, 117), Color.FromArgb(82, 212, 255)));
             //Console.WriteLine(Math.Lerp(Color.FromArgb(82, 255, 94), Color.FromArgb(82, 212, 255), 0.6f));
            bitmap.UnlockBits();
            return buff;
        }

        public static void DrawTrangle(Triangle t)
        {
            switch (renderType)
            {
                case RenderType.GouraudShading:
                    if (t.GetTrangleType() == DirectionType.top)
                    {
                        Vectorx vt = t.GetPoint(DirectionType.top)[0];
                        if (t.GetPoint(DirectionType.left).Length == 2)
                        {
                            Vectorx vr = t.GetPoint(DirectionType.right)[0];
                            Vectorx vl = t.GetOtherPoint(vt.point_2D, vr.point_2D);
                            Vector2 v = vt.point_2D - vr.point_2D;
                            float k = v.y / v.x;
                            for (int y = 0; y < -v.y; y++)
                            {
                                //DrawLine(new Vector2(vl.point_2D.x, vl.point_2D.y - y), new Vector2(vr.point_2D.x - y / k, vl.point_2D.y - y),Math.Lerp());
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x, vl.point_2D.y - y), Math.Lerp(vl, vt, new Vector2(vl.point_2D.x, vl.point_2D.y - y))),
                                    new Vectorx(new Vector2(vr.point_2D.x - y / k, vl.point_2D.y - y), Math.Lerp(vr, vt, new Vector2(vr.point_2D.x - y / k, vl.point_2D.y - y))));
                            }
                            return;
                        }
                        if (t.GetPoint(DirectionType.right).Length == 2)
                        {
                            Vectorx vl = t.GetPoint(DirectionType.left)[0];
                            Vectorx vr = t.GetOtherPoint(vt.point_2D, vl.point_2D);
                            Vector2 v = vt.point_2D - vl.point_2D;
                            float k = v.y / v.x;
                            for (int y = 0; y < -v.y; y++)
                            {
                                //DrawLine(new Vector2(vl.point_2D.x - y / k, vl.point_2D.y - y), new Vector2(vt.point_2D.x, vl.point_2D.y - y));
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x - y / k, vl.point_2D.y - y), Math.Lerp(vl, vt, new Vector2(vl.point_2D.x - y / k, vl.point_2D.y - y))),
                                    new Vectorx(new Vector2(vt.point_2D.x, vl.point_2D.y - y), Math.Lerp(vr, vt, new Vector2(vt.point_2D.x, vl.point_2D.y - y))));

                            }
                            return;
                        }
                        {
                            Vectorx vl = t.GetPoint(DirectionType.bottom)[0];
                            Vectorx vr = t.GetPoint(DirectionType.bottom)[1];
                            Vector2 v1 = vt.point_2D - vl.point_2D;
                            Vector2 v2 = vt.point_2D - vr.point_2D;
                            float k1 = v1.y / v1.x;
                            float k2 = v2.y / v2.x;
                            for (int y = 0; y < -v1.y; y++)
                            {
                                //DrawLine(new Vector2(vl.point_2D.x - y / k1, vl.point_2D.y - y), new Vector2(vr.point_2D.x - y / k2, vl.point_2D.y - y));
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x - y / k1, vl.point_2D.y - y), Math.Lerp(vl, vt, new Vector2(vl.point_2D.x - y / k1, vl.point_2D.y - y))),
                                    new Vectorx(new Vector2(vr.point_2D.x - y / k2, vl.point_2D.y - y), Math.Lerp(vr, vt, new Vector2(vr.point_2D.x - y / k2, vl.point_2D.y - y))));
                            }
                            return;
                        }
                    }
                    if (t.GetTrangleType() == DirectionType.bottom)
                    {
                        Vectorx vb = t.GetPoint(DirectionType.bottom)[0];
                        if (t.GetPoint(DirectionType.left).Length == 2)
                        {
                            Vectorx vr = t.GetPoint(DirectionType.right)[0];
                            Vectorx vl = t.GetOtherPoint(vb.point_2D, vr.point_2D);
                            Vector2 v = vb.point_2D - vr.point_2D;
                            float k = v.y / v.x;
                            for (int y = 0; y < v.y; y++)
                            {
                                //DrawLine(new Vector2(vl.point_2D.x, vl.point_2D.y + y), new Vector2(vr.point_2D.x + y / k, vl.point_2D.y + y));
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x, vl.point_2D.y + y), Math.Lerp(vl, vb, new Vector2(vl.point_2D.x, vl.point_2D.y + y))),
                                    new Vectorx(new Vector2(vr.point_2D.x + y / k, vl.point_2D.y + y), Math.Lerp(vr, vb, new Vector2(vr.point_2D.x + y / k, vl.point_2D.y + y))));
                            }
                            return;
                        }
                        if (t.GetPoint(DirectionType.right).Length == 2)
                        {
                            Vectorx vl = t.GetPoint(DirectionType.left)[0];
                            Vectorx vr = t.GetOtherPoint(vb.point_2D, vl.point_2D);
                            Vector2 v = vb.point_2D - vl.point_2D;
                            float k = v.y / v.x;
                            for (int y = 0; y < v.y; y++)
                            {
                                //DrawLine(new Vector2(vl.point_2D.x + y / k, vl.point_2D.y + y), new Vector2(vb.point_2D.x, vl.point_2D.y + y));
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x + y / k, vl.point_2D.y + y), Math.Lerp(vl, vb, new Vector2(vl.point_2D.x + y / k, vl.point_2D.y + y))),
                                    new Vectorx(new Vector2(vb.point_2D.x, vl.point_2D.y + y), Math.Lerp(vr, vb, new Vector2(vb.point_2D.x, vl.point_2D.y + y))));
                            }
                            return;
                        }
                        {
                            Vectorx vl = t.GetPoint(DirectionType.top)[0];
                            Vectorx vr = t.GetPoint(DirectionType.top)[1];
                            Vector2 v1 = vb.point_2D - vl.point_2D;
                            Vector2 v2 = vb.point_2D - vr.point_2D;
                            float k1 = v1.y / v1.x;
                            float k2 = v2.y / v2.x;
                            for (int y = 0; y < v1.y; y++)
                            {
                                //DrawLine(new Vector2(vl.x + y / k1, vl.y + y), new Vector2(vr.x + y / k2, vl.y + y));
                                DrawLine(new Vectorx(new Vector2(vl.point_2D.x + y / k1, vl.point_2D.y + y), Math.Lerp(vl, vb, new Vector2(vl.point_2D.x + y / k1, vl.point_2D.y + y))),
                                    new Vectorx(new Vector2(vr.point_2D.x + y / k2, vl.point_2D.y + y), Math.Lerp(vr, vb, new Vector2(vr.point_2D.x + y / k2, vl.point_2D.y + y))));

                            }
                            return;
                        }
                    }
                    break;
                case RenderType.NoShading:
                    if (t.GetTrangleType() == DirectionType.top)
                    {
                        Vector2 vt = t.GetPoint(DirectionType.top)[0].point_2D;
                        if (t.GetPoint(DirectionType.left).Length == 2)
                        {
                            Vector2 vr = t.GetPoint(DirectionType.right)[0].point_2D;
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
                            Vector2 vl = t.GetPoint(DirectionType.left)[0].point_2D;
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
                            Vector2 vl = t.GetPoint(DirectionType.bottom)[0].point_2D;
                            Vector2 vr = t.GetPoint(DirectionType.bottom)[1].point_2D;
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
                    if (t.GetTrangleType() == DirectionType.bottom)
                    {
                        Vector2 vb = t.GetPoint(DirectionType.bottom)[0].point_2D;
                        if (t.GetPoint(DirectionType.left).Length == 2)
                        {
                            Vector2 vr = t.GetPoint(DirectionType.right)[0].point_2D;
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
                            Vector2 vl = t.GetPoint(DirectionType.left)[0].point_2D;
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
                            Vector2 vl = t.GetPoint(DirectionType.top)[0].point_2D;
                            Vector2 vr = t.GetPoint(DirectionType.top)[1].point_2D;
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
                    break;
                default:
                    break;
            }
            t.ToTwoTrangles(out Triangle t1, out Triangle t2);
            DrawTrangle(t1);
            DrawTrangle(t2);
        }

        //public static void DrawLine(Vector2 v1,Vector2 v2)
        //{
        //    graphics.DrawLine(pen, new Point((int)v1.x, (int)v1.y), new Point((int)v2.x, (int)v2.y));
        //}

        public static void CameraRotateTo(Vector3 r, Cube[] objects)
        {
            camera_rotation = r;
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].position_r = Math.GetRelativePosition(objects[i].position, new Vector3(0, 0, 0), r);
            }
        }

        public static bool BackFaceCulling(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 v1 = p2 - p1;
            Vector3 v2 = p3 - p2;
            Vector3 normal = v1.Cross(v2);
            Vector3 view_dir = p1 - camera_position;
            return normal.Dot(view_dir) > 0;
        }

        //false 前面 true 后面
        public static bool BackFaceCulling(Triangle t)
        {
            Vector3 v1 = t.GetPoint(1) - t.GetPoint(0);
            Vector3 v2 = t.GetPoint(2) - t.GetPoint(1);
            Vector3 normal = v1.Cross(v2);
            Vector3 view_dir = t.GetPoint(0) - camera_position;
            return normal.Dot(view_dir) > 0;
        }

        public static bool IsOutOfCamera(Cube Object)
        {
            if (Object.position_r.z < 0) return true;
            bool isout = true;
            foreach (var item in Object.points)
            {
                if (item.point_2D.x > width || item.point_2D.y > height) continue;
                if (item.point_2D.x < 0 || item.point_2D.y < 0) continue;
                isout = false;
            }
            return isout;
        }

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
                return;
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
                return;
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

        public static void DrawLine(Vector2 v1, Vector2 v2, Color le, Color ri)
        {
            Vector2 v = v1 - v2;
            v = new Vector2((int)v.x, (int)v.y);
            if (v.x == 0)
            {
                if (v.y > 0)
                {
                    for (int i = 0; i < v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y - i, Math.Lerp(le, ri, i / v.y));
                    }
                }
                else
                {
                    for (int i = 0; i < -v.y; i++)
                    {
                        SetPixel((int)v1.x, (int)v1.y + i, Math.Lerp(le, ri, i / -v.y));
                    }
                }
                return;
            }
            float k = v.y / v.x;
            if (v.x > 0)
            {
                for (int i = 0; i < (int)v.x; i++)
                {
                    SetPixel((int)v1.x - i, (int)v1.y - (int)(i * k), Math.Lerp(le, ri, Math.GetLength(new Vector2(), new Vector2(v1.x - i, v1.y - (int)(i * k))) / Math.GetLength(v1, v2)));
                }
            }
            else
            {
                for (int i = 0; i < -(int)v.x; i++)
                {
                    SetPixel((int)v1.x + i, (int)v1.y + (int)(i * k), Math.Lerp(le, ri, Math.GetLength(new Vector2(), new Vector2(v1.x + i, v1.y + (int)(i * k))) / Math.GetLength(v1, v2)));
                }
            }
        }

        public static void DrawLine(Vectorx v1, Vectorx v2)
        {
            Vector2 v = v1.point_2D - v2.point_2D;
            v = new Vector2((int)v.x, (int)v.y);
            if (v.x == 0)
            {
                if (v.y > 0)
                {
                    for (int i = 0; i < v.y; i++)
                    {
                        SetPixel((int)v1.point_2D.x, (int)v1.point_2D.y - i, Math.Lerp(v1.color, v2.color, i / v.y));
                    }
                }
                else
                {
                    for (int i = 0; i < -v.y; i++)
                    {
                        SetPixel((int)v1.point_2D.x, (int)v1.point_2D.y + i, Math.Lerp(v1.color, v2.color, i / -v.y));
                    }
                }
                return;
            }
            float k = v.y / v.x;
            if (v.x > 0)
            {
                for (int i = 0; i < (int)v.x; i++)
                {
                    SetPixel((int)v1.point_2D.x - i, (int)v1.point_2D.y - (int)(i * k), Math.Lerp(v1.color, v2.color, Math.GetLength(v1.point_2D, new Vector2(v1.point_2D.x - i, v1.point_2D.y - (int)(i * k))) / Math.GetLength(v1.point_2D, v2.point_2D)));
                }
            }
            else
            {
                for (int i = 0; i < -(int)v.x; i++)
                {
                    //SetPixel((int)v1.point_2D.x + i, (int)v1.point_2D.y + (int)(i * k), Math.Lerp(v1.color, v2.color, Math.GetLength(v1.point_2D, new Vector2(v1.point_2D.x + i, v1.point_2D.y + (int)(i * k))) / Math.GetLength(v1.point_2D, v2.point_2D)));
                    SetPixel((int)v1.point_2D.x + i, (int)v1.point_2D.y + (int)(i * k), Math.Lerp(v1, v2, new Vector2(v1.point_2D.x + i, v1.point_2D.y + (int)(i * k))));
                }
            }
        }

        public static void DrawLines(Triangle triangle)
        {
            DrawLine(triangle.GetPoint2D(0).point_2D, triangle.GetPoint2D(1).point_2D);
            DrawLine(triangle.GetPoint2D(1).point_2D, triangle.GetPoint2D(2).point_2D);
            DrawLine(triangle.GetPoint2D(0).point_2D, triangle.GetPoint2D(2).point_2D);
        }

        public static void DrawLines(Triangle triangle, Color color)
        {
            DrawLine(triangle.GetPoint2D(0).point_2D, triangle.GetPoint2D(1).point_2D, color);
            DrawLine(triangle.GetPoint2D(1).point_2D, triangle.GetPoint2D(2).point_2D, color);
            DrawLine(triangle.GetPoint2D(0).point_2D, triangle.GetPoint2D(2).point_2D, color);
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
