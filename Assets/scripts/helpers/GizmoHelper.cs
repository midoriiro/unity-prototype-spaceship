using System.Collections;
using System.Collections.Generic;
using Galaxeed.Math.Geometries;
using UnityEngine;
using Helper;

namespace Helper
{
    public class GizmoHelper
    {
        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
        }

        public static void DrawPolyLine(PolyLine poly, Color color)
        {
            for (int i = 0; i < poly.Count(); i++)
            {
                if (i == 0)
                    continue;

                GizmoHelper.DrawLine(poly[i - 1], poly[i], color);
            }
        }

        public static void DrawPolyLine(PolyLine from, PolyLine to, Color color)
        {
            for (int i = 0; i < from.Count(); i++)
                GizmoHelper.DrawLine(from[i], to[i], color);
        }

        public static void DrawRect(Rect rect, Color color)
        {
            Vector3 p1 = new Vector3(rect.x, rect.y);
            Vector3 p2 = new Vector3(rect.x + rect.width, rect.y);
            Vector3 p3 = new Vector3(rect.x + rect.width, rect.y + rect.height);
            Vector3 p4 = new Vector3(rect.x, rect.y + rect.height);

            Gizmos.color = color;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }

        public static void DrawCircle(Vector3 center, float radius, Color color)
        {
            for (int i = 0 ; i < 360; ++i)
            {
                Vector3 p1 = center + Vector3.up * radius;
                p1 = p1 - center;
                p1 = Quaternion.Euler(0, 0, i) * p1;
                p1 = p1 + center;

                Vector3 p2 = center + Vector3.up * radius;
                p2 = p2 - center;
                p2 = Quaternion.Euler(0, 0, i + 1) * p2;
                p2 = p2 + center;

                GizmoHelper.DrawLine(p1, p2, color);
            }            
        }

        public static void DrawEllipse(Vector3 center, Vector2 size, float angle, Color color)
        {
            Vector3 n = Vector3.zero;
            Vector3 o = Vector3.zero;

            float theta = angle;

            for (int i = 0; i <= 360; i++)
            {
                n.x = Mathf.Sin(Mathf.Deg2Rad * angle) * size.x;
                n.y = Mathf.Cos(Mathf.Deg2Rad * angle) * size.y;

                n = Quaternion.Euler(0, 0, theta) * n;

                if (i > 0)
                    GizmoHelper.DrawLine(center + o, center + n, color);

                o = n;

                angle += 1;
            }
        }
    }
}

