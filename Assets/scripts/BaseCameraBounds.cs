using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCameraBounds : MonoBehaviour
{
    public enum Type
    {
        Free,
        Inner,
        Outer
    }

    public Type type = Type.Free;
    protected new Camera camera;

    virtual protected void OnDrawGizmosSelected()
    {
        if (camera != Camera.main)
            camera = Camera.main;
    }

    public Rect GetRect()
    {
        if(camera.orthographic)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            float x = camera.transform.position.x - width / 2;
            float y = camera.transform.position.y - height / 2;

            return new Rect(x, y, width, height);
        }
        else
        {
            return camera.rect;
        }        
    }
}
