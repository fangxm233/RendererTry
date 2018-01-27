using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTry
{
    public class Vector2
    {
        public float x, y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2() { }

        public static Vector2 operator +(Vector2 v1,Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return (v1.x == v2.x && v1.y == v2.y) ? true : false;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            return this == (Vector2)obj;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, 1);
        }
    }

    public class Vector3 : Vector2
    {
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector3() { }

        public float Dot(Vector3 rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z;
        }

        public Vector3 Cross(Vector3 rhs)
        {
            return new Vector3(y * rhs.z - z * rhs.y, z * rhs.x - x * rhs.z, x * rhs.y - y * rhs.x);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator *(int i,Vector3 v)
        {
            return new Vector3(v.x * i, v.y * i, v.z * i);
        }
    }

    public class Vectorx
    {
        public Vector3 point, points_r;
        public Color color;
        public Vector2 point_2D;
        public Vectorx(Vector3 po, Color co, Vector3 cubepo)
        {
            point = po;
            color = co;
            point_2D = Math.PointTo2D(po + cubepo);
        }

        public Vectorx(Vector2 po, Color co)
        {
            color = co;
            point_2D = po;
        }

        public void RotateTo(Vector3 r, Vector3 po)
        {
            points_r = Math.GetRelativePosition(point, new Vector3(), r);
            point_2D = Math.PointTo2D(points_r + po);
        }

        public static bool operator ==(Vectorx v1, Vector2 v2)
        {
            return (v1.point_2D.x == v2.x && v1.point_2D.y == v2.y) ? true : false;
        }

        public static bool operator !=(Vectorx v1, Vector2 v2)
        {
            return !(v1 == v2);
        }
    }
}
