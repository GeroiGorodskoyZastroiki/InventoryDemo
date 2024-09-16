using UnityEngine;

public static class Vector3Extensions
{
    public static float[] ToArray(this Vector3 vector) =>
        new float[3] { vector.x, vector.y, vector.z };
}