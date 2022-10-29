using RineaR.BeatABit.Core;
using UnityEngine;

namespace RineaR.BeatABit.Stages
{
    public class DamageTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var runner = col.GetComponent<Runner>();
            if (!runner) return;

            runner.Damage();
        }
    }
}