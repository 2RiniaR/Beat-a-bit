using UnityEngine;

namespace RineaR.BeatABit.Helpers
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 source, float z = 0)
        {
            return new Vector3(source.x, source.y, z);
        }
    }
}