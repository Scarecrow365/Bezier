using System;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetBezierPoint(BezierStruct bezierStruct, float t)
    {
        var p0 = Vector3.Lerp(bezierStruct.p0.position, bezierStruct.p1.position, t);
        var p1 = Vector3.Lerp(bezierStruct.p1.position, bezierStruct.p2.position, t);
        
        return Vector3.Lerp(p0, p1, t);
    }
}

[Serializable]
public struct BezierStruct
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
}