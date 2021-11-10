using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Helper;

public class CameraRectBounds : BaseCameraBounds
{
    public float width = 1f;
    public float height = 1f;
    private Rect bounds;

    void Start()
    {
        bounds = new Rect();
    }

    override protected void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Rect rect = GetRect();

        if(type == Type.Inner)
        {
            if (width >= rect.width)
                width = rect.width;            

            if (height >= rect.height)
                height = rect.height;            
        }
        else if(type == Type.Outer)
        {
            if (width <= rect.width)
                width = rect.width;
            else if (height <= rect.height)
                height = rect.height;
        }

        if (width <= 0)
            width = 0;
        else if (height <= 0)
            height = 0;

        bounds.width = width;
        bounds.height = height;
        bounds.x = camera.transform.position.x - bounds.width / 2;
        bounds.y = camera.transform.position.y - bounds.height / 2;

        GizmoHelper.DrawRect(bounds, Color.magenta);
    }
}
