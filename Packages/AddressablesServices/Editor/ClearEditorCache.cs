using UnityEditor;
using UnityEngine;

namespace AddressableAssets.Editor
{
    public static class ClearEditorCache
    {
        [MenuItem("Window/Asset Management/Addressables/Editor Cache/Clear")]
        private static void ClearEditorAddressablesCache()
        {
            if (Caching.ClearCache())
            {
                Debug.Log("Successfully cleared cache");
            }
            else
            {
                Debug.LogError("Failed to clear cache, cache was in use");
            }
        }
    }
}
