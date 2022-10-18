using UnityEngine;

namespace RineaR.BeatABit.General
{
    /// <summary>
    ///     一定時間後に消滅する
    /// </summary>
    public class DestroyTimer : MonoBehaviour
    {
        [Header("Properties")]
        public float start;

        [Header("States")]
        public float left;

        private void Awake()
        {
            left = start;
        }

        private void Update()
        {
            left -= Time.deltaTime;
            if (left <= 0) Destroy(gameObject);
        }
    }
}