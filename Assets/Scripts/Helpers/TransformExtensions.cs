using UnityEngine;

namespace RineaR.BeatABit.Helpers
{
    public static class TransformExtensions
    {
        public static Transform[] GetChildren(this Transform parent)
        {
            var children = new Transform[parent.childCount];
            for (var i = 0; i < children.Length; i++)
            {
                children[i] = parent.GetChild(i);
            }

            return children;
        }
    }
}