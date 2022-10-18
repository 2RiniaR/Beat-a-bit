using UnityEngine;

namespace RineaR.BeatABit.General
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class SubWindowLayout : MonoBehaviour
    {
        public RectTransform window;

        public RectTransform frame;
        public RectTransform owner;
        public float distance;

        private void Reset()
        {
            window = GetComponent<RectTransform>();
        }

        private void Update()
        {
            FixPosition();
        }

        private void OnEnable()
        {
            FixPosition();
        }

        public void FixPosition()
        {
            if (!owner || !frame) return;

            var isX = frame.rect.size.x >= frame.rect.size.y;
            var centerOfFrame = frame.CenterPosition();
            var centerOfOwner = owner.CenterPosition();
            var isRightSide = centerOfOwner.x <= centerOfFrame.x;
            var isTopSide = centerOfOwner.y <= centerOfFrame.y;
            window.pivot = new Vector2
            {
                x = isX ? isRightSide ? 0 : 1 : 0.5f,
                y = !isX ? isTopSide ? 0 : 1 : 0.5f,
            };
            window.position = new Vector2
            {
                x = centerOfOwner.x + (isX ? isRightSide ? owner.rect.xMax + distance : owner.rect.xMin - distance : 0),
                y = centerOfOwner.y + (!isX ? isTopSide ? owner.rect.yMax + distance : owner.rect.yMin - distance : 0),
            };
        }
    }
}