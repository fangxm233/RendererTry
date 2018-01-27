using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendererTry
{
    public enum DirectionType
    {
        unknow,
        known,
        top,
        bottom,
        left,
        right
    }
    public class Triangle
    {
        public Vector3 points;
        public Cube Object;
        public DirectionType type = DirectionType.unknow;
        public Vectorx[] points2D;

        public Triangle(int v1, int v2, int v3, Cube c)
        {
            Object = c;
            points = new Vector3(v1, v2, v3);
        }

        public Triangle(Vector3 points, Cube c)
        {
            Object = c;
            this.points = points;
        }

        public Triangle(Vectorx v1, Vectorx v2, Vectorx v3, Cube c)
        {
            points2D = new Vectorx[] { v1, v2, v3 };
        }

        /// <param name="i">0,1,2</param>
        public Vector3 GetPoint(int i)
        {
            switch (i)
            {
                case 0:
                    {
                        return Object.points[(int)points.x].points_r + Object.position;
                    }
                case 1:
                    {
                        return Object.points[(int)points.y].points_r + Object.position;
                    }
                case 2:
                    {
                        return Object.points[(int)points.z].points_r + Object.position;
                    }
            }
            return null;
        }

        public Vectorx GetPoint2D(int i)
        {
            if (points2D != null) return points2D[i];
            switch (i)
            {
                case 0:
                    {
                        return Object.points[(int)points.x];
                    }
                case 1:
                    {
                        return Object.points[(int)points.y];
                    }
                case 2:
                    {
                        return Object.points[(int)points.z];
                    }
            }
            return null;
        }

        public Vectorx[] GetPoint(DirectionType type)
        {
            Vectorx point0 = GetPoint2D(0);
            Vectorx point1 = GetPoint2D(1);
            Vectorx point2 = GetPoint2D(2);
            switch (type)
            {
                case DirectionType.top:
                    if (point0.point_2D.y <= point1.point_2D.y && point0.point_2D.y <= point2.point_2D.y)
                    {
                        if (point0.point_2D.y == point1.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        if (point0.point_2D.y == point2.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point2
                            };
                        }
                        return new Vectorx[] { point0 };
                    }
                    if (point1.point_2D.y < point0.point_2D.y && point1.point_2D.y <= point2.point_2D.y)
                    {
                        if (point1.point_2D.y == point2.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point1,
                                point2
                            };
                        }
                        return new Vectorx[] { point1 };
                    }
                    if (point2.point_2D.y <= point0.point_2D.y && point2.point_2D.y < point1.point_2D.y)
                    {
                        if (point2.point_2D.y == point0.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        return new Vectorx[] { point2 };
                    }
                    break;
                case DirectionType.bottom:
                    if (point0.point_2D.y >= point1.point_2D.y && point0.point_2D.y >= point2.point_2D.y)
                    {
                        if (point0.point_2D.y == point1.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        if (point0.point_2D.y == point2.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point2
                            };
                        }
                        return new Vectorx[] { point0 };
                    }
                    if (point1.point_2D.y > point0.point_2D.y && point1.point_2D.y >= point2.point_2D.y)
                    {
                        if (point1.point_2D.y == point2.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point1,
                                point2
                            };
                        }
                        return new Vectorx[] { point1 };
                    }
                    if (point2.point_2D.y >= point0.point_2D.y && point2.point_2D.y > point1.point_2D.y)
                    {
                        if (point2.point_2D.y == point0.point_2D.y)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        return new Vectorx[] { point2 };
                    }
                    break;
                case DirectionType.left:
                    if (point0.point_2D.x <= point1.point_2D.x && point0.point_2D.x <= point2.point_2D.x)
                    {
                        if (point0.point_2D.x == point1.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        if (point0.point_2D.x == point2.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point2
                            };
                        }
                        return new Vectorx[] { point0 };
                    }
                    if (point1.point_2D.x < point0.point_2D.x && point1.point_2D.x <= point2.point_2D.x)
                    {
                        if (point1.point_2D.x == point2.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point1,
                                point2
                            };
                        }
                        return new Vectorx[] { point1 };
                    }
                    if (point2.point_2D.x <= point0.point_2D.x && point2.point_2D.x < point1.point_2D.x)
                    {
                        if (point2.point_2D.x == point0.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        return new Vectorx[] { point2 };
                    }
                    break;
                case DirectionType.right:
                    if (point0.point_2D.x >= point1.point_2D.x && point0.point_2D.x >= point2.point_2D.x)
                    {
                        if (point0.point_2D.x == point1.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        if (point0.point_2D.x == point2.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point2
                            };
                        }
                        return new Vectorx[] { point0 };
                    }
                    if (point1.point_2D.x > point0.point_2D.x && point1.point_2D.x >= point2.point_2D.x)
                    {
                        if (point1.point_2D.x == point2.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point1,
                                point2
                            };
                        }
                        return new Vectorx[] { point1 };
                    }
                    if (point2.point_2D.x >= point0.point_2D.x && point2.point_2D.x > point1.point_2D.x)
                    {
                        if (point2.point_2D.x == point0.point_2D.x)
                        {
                            return new Vectorx[]
                            {
                                point0,
                                point1
                            };
                        }
                        return new Vectorx[] { point2 };
                    }
                    break;
                default:
                    break;
            }
            return new Vectorx[0];
        }

        public DirectionType GetTrangleType()
        {
            if (type != DirectionType.unknow) return type;
            if (GetPoint(DirectionType.top).Length == 2) { type = DirectionType.bottom; return DirectionType.bottom; }
            if (GetPoint(DirectionType.bottom).Length == 2) { type = DirectionType.top; return DirectionType.top; }
            //if (GetPoint(DirectionType.left).Length == 2) { type = DirectionType.right; return DirectionType.right; }
            //if (GetPoint(DirectionType.right).Length == 2) { type = DirectionType.left; return DirectionType.left; }
            //if (GetPoint(DirectionType.right)[0].x > GetPoint(DirectionType.top)[0].x && GetPoint(DirectionType.right)[0].x > GetPoint(DirectionType.bottom)[0].x) { type = DirectionType.right; return DirectionType.right; }
            type = DirectionType.known;
            //GetOtherPoint(GetPoint(DirectionType.top)[0], GetPoint(DirectionType.bottom)[0]);
            return DirectionType.known;
        }

        public void SetTrangleType(DirectionType type) { this.type = type; }

        public void ToTwoTrangles(out Triangle t1,out Triangle t2)
        {
            {
                Vectorx vt = GetPoint(DirectionType.top)[0];
                Vectorx vb = GetPoint(DirectionType.bottom)[0];
                Vectorx vc = GetOtherPoint(vt.point_2D, vb.point_2D);
                Vector2 v = vt.point_2D - vb.point_2D;
                float k = v.y / v.x;
                if (v.x == 0)
                {
                    t1 = new Triangle(new Vectorx(new Vector2(vt.point_2D.x, vc.point_2D.y), Math.Lerp(vt, vb, new Vector2(vt.point_2D.x, vc.point_2D.y))), vc, vt, Object);
                    t2 = new Triangle(new Vectorx(new Vector2(vt.point_2D.x, vc.point_2D.y), Math.Lerp(vt, vb, new Vector2(vt.point_2D.x, vc.point_2D.y))), vc, vb, Object);
                    t1.SetTrangleType(DirectionType.top);
                    t2.SetTrangleType(DirectionType.bottom);
                    return;
                }
                {
                    t1 = new Triangle(new Vectorx(new Vector2(vt.point_2D.x + (vc.point_2D.y - vt.point_2D.y) / k, vc.point_2D.y)
                        , Math.Lerp(vt, vb, new Vector2(vt.point_2D.x + (vc.point_2D.y - vt.point_2D.y) / k, vc.point_2D.y))), vc, vt, Object);
                    t2 = new Triangle(new Vectorx(new Vector2(vt.point_2D.x + (vc.point_2D.y - vt.point_2D.y) / k, vc.point_2D.y)
                        , Math.Lerp(vt, vb, new Vector2(vt.point_2D.x + (vc.point_2D.y - vt.point_2D.y) / k, vc.point_2D.y))), vc, vb, Object);
                    t1.SetTrangleType(DirectionType.top);
                    t2.SetTrangleType(DirectionType.bottom);
                    return;
                }
            }
        }

        public Vectorx GetOtherPoint(Vector2 v1,Vector2 v2)
        {
            if (GetPoint2D(0) == v1)
            {
                if (GetPoint2D(1) == v2) return GetPoint2D(2);
                if (GetPoint2D(2) == v2) return GetPoint2D(1);
            }
            if (GetPoint2D(1) == v1)
            {
                if (GetPoint2D(0) == v2) return GetPoint2D(2);
                if (GetPoint2D(2) == v2) return GetPoint2D(0);
            }
            if (GetPoint2D(2) == v1)
            {
                if (GetPoint2D(1) == v2) return GetPoint2D(0);
                if (GetPoint2D(0) == v2) return GetPoint2D(1);
            }
            if (GetPoint2D(0) == v2)
            {
                if (GetPoint2D(1) == v1) return GetPoint2D(2);
                if (GetPoint2D(2) == v1) return GetPoint2D(1);
            }
            if (GetPoint2D(1) == v2)
            {
                if (GetPoint2D(0) == v1) return GetPoint2D(2);
                if (GetPoint2D(2) == v1) return GetPoint2D(0);
            }
            if (GetPoint2D(2) == v2)
            {
                if (GetPoint2D(1) == v1) return GetPoint2D(0);
                if (GetPoint2D(0) == v1) return GetPoint2D(1);
            }
            return null;
        }
    }
}
