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
        public Vector3 position_r = new Vector3();
        public Triangle[] triangles = new Triangle[12];
        public Vectorx[] points = new Vectorx[8];
        //public Vector3[] points_r = new Vector3[8];
        //public Vector2[] points_2D = new Vector2[8];
        //public Color[] Colors = new Color[8];

        public Cube(Vector3 position, Vector3 size, Vector3 rotation)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;

            points = new Vectorx[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,82,188),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,82,188),position)
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

            points = new Vectorx[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,82,188),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,82,188),position)
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

            points = new Vectorx[]//前左上，前右上，前左下，前右下，后左上，后右上，后左下，后右下
            {
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,82,188),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, -0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(-0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,237,82),position),
                new Vectorx(new Vector3(0.5f * size.x, 0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,255,94),position),
                new Vectorx(new Vector3(-0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(82,212,255),position),
                new Vectorx(new Vector3(0.5f * size.x, -0.5f * size.y, 0.5f * size.z),Color.FromArgb(255,82,188),position)
            };

            triangles = new Triangle[]//前1，前2，后1，后2，左1，左2，右1，右2，上1，上2，下1，下2
            {
                new Triangle(0, 1,2, this),
                new Triangle(3, 2,1, this),
                new Triangle(4, 6,5, this),
                new Triangle(7, 5,6, this),
                new Triangle(4, 0, 6, this),
                new Triangle(2, 6,0, this),
                new Triangle(1, 5,3, this),
                new Triangle(7, 3, 5, this),
                new Triangle(4, 5,0, this),
                new Triangle(1, 0,5, this),
                new Triangle(2, 3, 6, this),
                new Triangle(7, 6,3, this)
            };
            RotateTo(rotation);
        }

        public void RotateTo(Vector3 r)
        {
            rotation = r;
            foreach (var item in points)
            {
                item.RotateTo(r, position_r);
            }
        }
    }
}
