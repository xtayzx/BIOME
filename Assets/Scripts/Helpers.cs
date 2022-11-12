using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    // A general class that can be applied to other objects as well. such as the translation of the coordinates when moving around in this game
    private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
    public static Vector3 ToIso(this Vector3 input) => isoMatrix.MultiplyPoint3x4(input);
}
