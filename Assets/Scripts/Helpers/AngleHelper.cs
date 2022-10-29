using UnityEngine;

namespace RineaR.BeatABit.Helpers
{
    public static class AngleHelper
    {
        public static Vector2 AngleToVector2(float angle)
        {
            var radian = angle * (Mathf.PI / 180);
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
        }

        public static float Vector2ToAngle(Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }
    }
}