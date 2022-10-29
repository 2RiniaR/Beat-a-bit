using UnityEngine;

namespace RineaR.BeatABit.General
{
    /// <summary>
    ///     一定時間後に消滅する
    /// </summary>
    public class DestroyTimer : MonoBehaviour
    {
        [Header("Properties")]
        public float duration;

        [Header("States")]
        public float left;

        private void Start()
        {
            left = duration;
        }

        private void Update()
        {
            left -= Time.deltaTime;
            if (left <= 0) Destroy(gameObject);
        }
    }
}