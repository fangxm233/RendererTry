using System;
using System.Collections.Generic;
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
        public Vector2[] points2D;

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

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            points2D = new Vector2[] { v1, v2, v3 };
        }

        /// <param name="i">0,1,2</param>
        public Vector3 GetPoint(int i)
        {
            switch (i)
            {
                case 0:
                    {
                        return Object.points_r[(int)points.x] + Object.position;
                    }
                case 1:
                    {
                        return Object.points_r[(int)points.y] + Object.position;
                    }
                case 2:
                    {
                        return Object.points_r[(int)points.z] + Object.position;
                    }
            }
            return null;
        }

        public Vector2 GetPoint2D(int i)
        {
            if (points2D != null) return points2D[i];
            switch (i)
            {
                case 0:
                    {
                        return Object.points_2D[(int)points.x] + Object.position;
                    }
                case 1:
                    {
                        return Object.points_2D[(int)points.y] + Object.position;
                    }
                case 2:
                    {
                        return Object.points_2D[(int)points.z] + Object.position;
                    }
            }
            return null;
        }

        public Vector3[] GetPoints()
        {
            Vector3[] p = new Vector3[3];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = GetPoint(i);
            }
            return p;
        }

        public Vector2[] GetPoint(DirectionType type)
        {
            switch (type)
            {
                case DirectionType.top:
                    if (GetPoint2D(0).y <= GetPoint2D(1).y && GetPoint2D(0).y <= GetPoint2D(2).y)
                    {
                        if (GetPoint2D(0).y == GetPoint2D(1).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        if (GetPoint2D(0).y == GetPoint2D(2).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(0) };
                    }
                    if (GetPoint2D(1).y < GetPoint2D(0).y && GetPoint2D(1).y <= GetPoint2D(2).y)
                    {
                        if (GetPoint2D(1).y == GetPoint2D(2).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(1),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(1) };
                    }
                    if (GetPoint2D(2).y <= GetPoint2D(0).y && GetPoint2D(2).y < GetPoint2D(1).y)
                    {
                        if (GetPoint2D(2).y == GetPoint2D(0).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        return new Vector2[] { GetPoint2D(2) };
                    }
                    break;
                case DirectionType.bottom:
                    if (GetPoint2D(0).y >= GetPoint2D(1).y && GetPoint2D(0).y >= GetPoint2D(2).y)
                    {
                        if (GetPoint2D(0).y == GetPoint2D(1).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        if (GetPoint2D(0).y == GetPoint2D(2).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(0) };
                    }
                    if (GetPoint2D(1).y > GetPoint2D(0).y && GetPoint2D(1).y >= GetPoint2D(2).y)
                    {
                        if (GetPoint2D(1).y == GetPoint2D(2).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(1),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(1) };
                    }
                    if (GetPoint2D(2).y >= GetPoint2D(0).y && GetPoint2D(2).y > GetPoint2D(1).y)
                    {
                        if (GetPoint2D(2).y == GetPoint2D(0).y)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        return new Vector2[] { GetPoint2D(2) };
                    }
                    break;
                case DirectionType.left:
                    if (GetPoint2D(0).x <= GetPoint2D(1).x && GetPoint2D(0).x <= GetPoint2D(2).x)
                    {
                        if (GetPoint2D(0).x == GetPoint2D(1).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        if (GetPoint2D(0).x == GetPoint2D(2).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(0) };
                    }
                    if (GetPoint2D(1).x < GetPoint2D(0).x && GetPoint2D(1).x <= GetPoint2D(2).x)
                    {
                        if (GetPoint2D(1).x == GetPoint2D(2).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(1),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(1) };
                    }
                    if (GetPoint2D(2).x <= GetPoint2D(0).x && GetPoint2D(2).x < GetPoint2D(1).x)
                    {
                        if (GetPoint2D(2).x == GetPoint2D(0).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        return new Vector2[] { GetPoint2D(2) };
                    }
                    break;
                case DirectionType.right:
                    if (GetPoint2D(0).x >= GetPoint2D(1).x && GetPoint2D(0).x >= GetPoint2D(2).x)
                    {
                        if (GetPoint2D(0).x == GetPoint2D(1).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        if (GetPoint2D(0).x == GetPoint2D(2).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(0) };
                    }
                    if (GetPoint2D(1).x > GetPoint2D(0).x && GetPoint2D(1).x >= GetPoint2D(2).x)
                    {
                        if (GetPoint2D(1).x == GetPoint2D(2).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(1),
                                GetPoint2D(2)
                            };
                        }
                        return new Vector2[] { GetPoint2D(1) };
                    }
                    if (GetPoint2D(2).x >= GetPoint2D(0).x && GetPoint2D(2).x > GetPoint2D(1).x)
                    {
                        if (GetPoint2D(2).x == GetPoint2D(0).x)
                        {
                            return new Vector2[]
                            {
                                GetPoint2D(0),
                                GetPoint2D(1)
                            };
                        }
                        return new Vector2[] { GetPoint2D(2) };
                    }
                    break;
                default:
                    break;
            }
            return new Vector2[0];
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
                Vector2 vt = GetPoint(DirectionType.top)[0];
                Vector2 vb = GetPoint(DirectionType.bottom)[0];
                Vector2 vc = GetOtherPoint(vt, vb);
                Vector2 v = vt - vb;
                float k = v.y / v.x;
                if (v.x == 0)
                {
                    t1 = new Triangle(new Vector2(vt.x, vc.y), vc, vt);
                    t2 = new Triangle(new Vector2(vt.x, vc.y), vc, vb);
                    t1.SetTrangleType(DirectionType.top);
                    t2.SetTrangleType(DirectionType.bottom);
                    return;
                }
                {
                    t1 = new Triangle(new Vector2(vt.x + (vc.y - vt.y) / k, vc.y), vc, vt);
                    t2 = new Triangle(new Vector2(vt.x + (vc.y - vt.y) / k, vc.y), vc, vb);
                    t1.SetTrangleType(DirectionType.top);
                    t2.SetTrangleType(DirectionType.bottom);
                    return;
                }
            }
        }

        public Vector2 GetOtherPoint(Vector2 v1,Vector2 v2)
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
                if (GetPoint2D(0) == v2) return GetPoint2D(2);
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
                if (GetPoint2D(0) == v1) return GetPoint2D(2);
            }
            return null;
        }
    }
}
