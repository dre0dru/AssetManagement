using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public static class AssetLoaderExtensions
    {
        public static bool TryGetComponent<T>(
            this IAssetLoader<AssetReferenceT<GameObject>, GameObject> assetLoader,
            AssetReferenceComponent<T> key, out T component)
            where T : Component
        {
            if (assetLoader.TryGetAsset(key, out var gameObject))
            {
                return gameObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        public static T GetComponent<T>(
            this IAssetLoader<AssetReferenceT<GameObject>, GameObject> assetLoader,
            AssetReferenceComponent<T> key)
            where T : Component =>
            assetLoader.GetAsset(key).GetComponent<T>();

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            params TKey[] keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(assetLoader.PreloadAssetAsync));

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(assetLoader.PreloadAssetAsync));

        public static void UnloadAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            params TKey[] keys)
            where TAsset : Object =>
            assetLoader.UnloadAssets((IEnumerable<TKey>) keys);

        public static void UnloadAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object
        {
            foreach (var key in keys)
            {
                assetLoader.UnloadAsset(key);
            }
        }

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            params TKey[] keys)
            where TAsset : Object =>
            LoadAssetsAsync(assetLoader, (IEnumerable<TKey>) keys);

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll<TAsset>(keys.Select(assetLoader.LoadAssetAsync));

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            params TKey[] keys)
            where TAsset : Object =>
            assetLoader.GetAssets((IEnumerable<TKey>) keys);

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            keys.Select(assetLoader.GetAsset);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            out IEnumerable<TAsset> assets, params TKey[] keys)
            where TAsset : Object =>
            assetLoader.TryGetAssets(keys, out assets);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAssetLoader<TKey, TAsset> assetLoader,
            IEnumerable<TKey> keys, out IEnumerable<TAsset> assets)
            where TAsset : Object
        {
            if (keys.All(assetLoader.IsAssetLoaded) == false)
            {
                assets = Enumerable.Empty<TAsset>();
                return false;
            }

            assets = assetLoader.GetAssets(keys);
            return true;
        }
    }
}