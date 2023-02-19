using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckType 
{
    outBottom,
    outTop,
    outLeft,
    outRight
}

public class Utils
{
    Camera camera;

    public Utils() 
    {
        camera = Camera.main;
        Debug.Log("Camera Name:" + camera.name);
    }

    public static bool CheckInBounds(Bounds b1,Bounds b2,CheckType type) //b1 - Panel  b2 - prize
    {
        switch (type)
        {
            case CheckType.outBottom:
                if (b1.min.y > b2.max.y)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            case CheckType.outTop:
                if (b1.max.y < b2.min.y)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            case CheckType.outLeft:
                if (b1.min.x > b2.max.x)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            case CheckType.outRight:
                if (b1.max.x < b2.min.x)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            default:
                return false;
        }
    }
}
