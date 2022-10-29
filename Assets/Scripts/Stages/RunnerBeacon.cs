using UnityEngine;

namespace RineaR.BeatABit.Stages
{
    public class RunnerBeacon : MonoBehaviour
    {
        public Runner runner;

        private void Awake()
        {
            runner = GetComponent<Runner>();
        }
    }
}