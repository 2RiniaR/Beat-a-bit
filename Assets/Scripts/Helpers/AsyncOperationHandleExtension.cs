using UnityEngine.ResourceManagement.AsyncOperations;

namespace RineaR.BeatABit.Helpers
{
    public static class AsyncOperationHandleExtension
    {
        public static float GetPercentSafety(this AsyncOperationHandle source)
        {
            return source.IsValid() ? source.PercentComplete : 0;
        }

        public static float GetPercentSafety<T>(this AsyncOperationHandle<T> source)
        {
            return source.IsValid() ? source.PercentComplete : 0;
        }
    }
}