using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class CameraCircleBounds : BaseCameraBounds
{
    public float radius = 1f;

    override protected void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Rect rect = GetRect();

        float min = Mathf.Min(rect.width, rect.height) / 2;
        float max = Mathf.Sqrt(Mathf.Pow(rect.width / 2, 2) + Mathf.Pow(rect.height / 2, 2));

        if (type == Type.Inner)
        {
            if (radius >= min)
                radius = min;
        }
        else if (type == Type.Outer)
        {
            if (radius <= max)
                radius = max;
        }

        if (radius <= 0)
            radius = 0;

        GizmoHelper.DrawCircle(camera.transform.position, radius, Color.magenta);
    }
}
