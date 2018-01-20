﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTry
{
    public class Math
    {
        public static float f = 100;
        public static Vector3 CameraRotation = new Vector3();

        public static Vector2 PointTo2D(Vector3 point)
        {
            return new Vector2(point.x / point.z * Form1.main.Width + Form1.main.Width / 2, point.y / point.z * Form1.main.Height + Form1.main.Height / 2);
        }

        public static Vector2 GetDirection(Vector2 v)
        {
            return new Vector2((v.x - Form1.main.Width / 2) / Form1.main.Width, (v.y - Form1.main.Height / 2) / Form1.main.Height);
        }

        public static bool IntersectTriangle(Vector3 orig, Vector3 dir, Vector3 v0, Vector3 v1, Vector3 v2, out float t, out float u, out float v)
        {
            t = 0;
            u = 0;
            v = 0;
            // E1
            Vector3 E1 = v1 - v0;

            // E2
            Vector3 E2 = v2 - v0;

            // P
            Vector3 P = dir.Cross(E2);

            // determinant
            float det = E1.Dot(P);

            // keep det > 0, modify T accordingly
            Vector3 T;
            if (det > 0)
            {
                T = orig - v0;
            }
            else
            {
                T = v0 - orig;
                det = -det;
            }

            // If determinant is near zero, ray lies in plane of triangle
            if (det < 0.0001f)
                return false;

            // Calculate u and make sure u <= 1
            u = T.Dot(P);
            if (u < 0.0f || u > det)
                return false;

            // Q
            Vector3 Q = T.Cross(E1);

            // Calculate v and make sure u + v <= 1
            v = dir.Dot(Q);
            //Console.WriteLine("v:{0}, u+v:{1},det:{2},{3}", v, u + v, det, v < 0.0f || u + v > det);

            if (v < 0.0f || u + v > det)
                return false;

            // Calculate t, scale parameters, ray intersects triangle
            t = E2.Dot(Q);

            float fInvDet = 1.0f / det;
            t *= fInvDet;
            u *= fInvDet;
            v *= fInvDet;
            //Console.WriteLine(true);
            Form1.gg = true;
            return true;
        }

        public static Vector2[] PointsTo2D(Vector3[] point)
        {
            Vector2[] v = new Vector2[point.Length];
            for (int i = 0; i < point.Length; i++)
            {
                v[i] = PointTo2D(point[i]);
            }
            return v;
        }

        public static Point Vector2ToPoint(Vector2 v)
        {
            return new Point((int)v.x, (int)v.y);
        }

        public static Point[] Vector2ToPoints(Vector2[] v)
        {
            Point[] p = new Point[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                p[i] = Vector2ToPoint(v[i]);
            }
            return p;
        }
    }
}
