using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RendererTry
{
    public class Cube
    {
        public Vector3 position, size, rotation;
        public Triangle[] triangles = new Triangle[12];
        public Vector3[] points = new Vector3[8];
        public Vector3[] points_r = new Vector3[8];
        public Vector2[] points_2D = new Vector2[8];

        public Cube(Vector3 position, Vector3 size, Vector3 rotation)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;

            points = new Vector3[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z)
            };

            triangles = new Triangle[]//前1，前2，后1，后2，左1，左2，右1，右2，上1，上2，下1，下2
            {
                new Triangle(0, 2, 1, this),
                new Triangle(3, 1, 2, this),
                new Triangle(4, 6, 5, this),
                new Triangle(7, 5, 6, this),
                new Triangle(4, 0, 6, this),
                new Triangle(2, 0, 6, this),
                new Triangle(1, 3, 5, this),
                new Triangle(7, 3, 5, this),
                new Triangle(4, 0, 5, this),
                new Triangle(1, 5, 0, this),
                new Triangle(2, 3, 6, this),
                new Triangle(7, 3, 6, this)
            };
            RotateTo(rotation);
        }

        public Cube(Vector3 position, Vector3 size)
        {
            this.position = position;
            this.size = size;
            rotation = new Vector3();

            points = new Vector3[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z)
            };

            triangles = new Triangle[]//前1，前2，后1，后2，左1，左2，右1，右2，上1，上2，下1，下2
            {
                new Triangle(0, 2, 1, this),
                new Triangle(3, 1, 2, this),
                new Triangle(4, 6, 5, this),
                new Triangle(7, 5, 6, this),
                new Triangle(4, 0, 6, this),
                new Triangle(2, 0, 6, this),
                new Triangle(1, 3, 5, this),
                new Triangle(7, 3, 5, this),
                new Triangle(4, 0, 5, this),
                new Triangle(1, 5, 0, this),
                new Triangle(2, 3, 6, this),
                new Triangle(7, 3, 6, this)
            };
            RotateTo(rotation);
        }

        public Cube(Vector3 position)
        {
            this.position = position;
            size = new Vector3(1, 1, 1);
            rotation = new Vector3();

            points = new Vector3[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),
                new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),
                new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),
                new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z)
            };

            triangles = new Triangle[]//前1，前2，后1，后2，左1，左2，右1，右2，上1，上2，下1，下2
            {
                new Triangle(0, 2, 1, this),
                new Triangle(3, 1, 2, this),
                new Triangle(4, 6, 5, this),
                new Triangle(7, 5, 6, this),
                new Triangle(4, 0, 6, this),
                new Triangle(2, 0, 6, this),
                new Triangle(1, 3, 5, this),
                new Triangle(7, 3, 5, this),
                new Triangle(4, 0, 5, this),
                new Triangle(1, 5, 0, this),
                new Triangle(2, 3, 6, this),
                new Triangle(7, 3, 6, this)
            };
            RotateTo(rotation);
        }

        public void RotateTo(Vector3 r)
        {
            rotation = r;
            for (int i = 0; i < points.Length; i++)//y
            {
                Vector3 point = points[i];
                points_r[i] = new Vector3((float)(point.z * System.Math.Sin(r.y) + point.x * System.Math.Cos(r.y)), point.y, (float)(point.z * System.Math.Cos(r.y) - point.x * System.Math.Sin(r.y)));
            }
            for (int i = 0; i < points.Length; i++)//x
            {
                Vector3 point = points_r[i];
                points_r[i] = new Vector3(point.x, (float)(point.y * System.Math.Cos(r.x) - point.z * System.Math.Sin(r.x)), (float)(point.y * System.Math.Sin(r.x) + point.z * System.Math.Cos(r.x)));
            }
            for (int i = 0; i < points.Length; i++)//z
            {
                Vector3 point = points_r[i];
                points_r[i] = new Vector3((float)(point.x * System.Math.Cos(r.z) - point.y * System.Math.Sin(r.z)), (float)(point.x * System.Math.Sin(r.z) + point.y * System.Math.Cos(r.z)), point.z);
            }
        }

        public void PointsTo2D()
        {
            Vector2[] vs = new Vector2[8];
            for (int i = 0; i < points.Length; i++)
            {
                vs[i] = Math.PointTo2D(points_r[i] + position);
            }
            points_2D = vs;
        }
    }
}
