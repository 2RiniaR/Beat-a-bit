using RineaR.BeatABit.Core;
using UnityEngine;

namespace RineaR.BeatABit.Stages
{
    [ExecuteAlways]
    public class AthleticElement : MonoBehaviour
    {
        [Header("Rotate")]
        public float rotation;

        public bool freeRotate;
        public AthleticSystem System { get; private set; }

        private void Awake()
        {
            System = GetComponentInParent<AthleticSystem>();
        }

        private void Start()
        {
            rotation = transform.rotation.eulerAngles.z + transform.rotation.eulerAngles.y;
        }

        private void Update()
        {
            rotation %= 360;
            var flipX = !freeRotate && rotation is >= 90 and <= 270;
            transform.rotation = Quaternion.Euler(0, flipX ? 180 : 0, flipX ? 180 - rotation : rotation);
        }
    }
}