using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static bool IsInView(this Transform transform)
    {
        var viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
    }
}
