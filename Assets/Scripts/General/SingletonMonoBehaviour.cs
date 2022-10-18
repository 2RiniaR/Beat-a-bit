using UnityEngine;

namespace RineaR.BeatABit.General
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _current;

        public static T Current
        {
            get
            {
                if (_current) return _current;

                _current = FindObjectOfType<T>();
                if (_current == null) Debug.LogError($"{nameof(T)} はシーンに存在しません。");
                return _current;
            }
        }
    }
}