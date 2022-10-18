using UnityEngine;

namespace RineaR.BeatABit.General
{
    /// <summary>
    ///     Collider に衝突した DestroyBorderTrigger を持つ GameObject を消去する
    /// </summary>
    public class DestroyBorder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var trigger = col.GetComponent<DestroyBorderIgnore>();
            if (trigger == null) Destroy(col.gameObject);
        }
    }
}