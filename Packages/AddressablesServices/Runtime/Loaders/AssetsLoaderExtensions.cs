using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
#if VCONTAINER
using VContainer;
#endif

namespace AddressableAssets.Loaders
{
    public static class AssetsLoaderExtensions
    {
        public static bool TryGetComponent<T>(
            this IAssetsLoader<AssetReferenceT<GameObject>, GameObject> assetsLoader,
            AssetReferenceComponent<T> key, out T component)
            where T : Component
        {
            if (assetsLoader.TryGetAsset(key, out var gameObject))
            {
                return gameObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        public static T GetComponent<T>(
            this IAssetsLoader<AssetReferenceT<GameObject>, GameObject> assetsLoader,
            AssetReferenceComponent<T> key)
            where T : Component =>
            assetsLoader.GetAsset(key).GetComponent<T>();

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            params TKey[] keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(assetsLoader.PreloadAssetAsync));

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(assetsLoader.PreloadAssetAsync));

        public static void UnloadAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            params TKey[] keys)
            where TAsset : Object =>
            assetsLoader.UnloadAssets((IEnumerable<TKey>)keys);

        public static void UnloadAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object
        {
            foreach (var key in keys)
            {
                assetsLoader.UnloadAsset(key);
            }
        }

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            params TKey[] keys)
            where TAsset : Object =>
            LoadAssetsAsync(assetsLoader, (IEnumerable<TKey>)keys);

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll<TAsset>(keys.Select(assetsLoader.LoadAssetAsync));

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            params TKey[] keys)
            where TAsset : Object =>
            assetsLoader.GetAssets((IEnumerable<TKey>)keys);

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            keys.Select(assetsLoader.GetAsset);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            out IEnumerable<TAsset> assets, params TKey[] keys)
            where TAsset : Object =>
            assetsLoader.TryGetAssets(keys, out assets);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAssetsLoader<TKey, TAsset> assetsLoader,
            IEnumerable<TKey> keys, out IEnumerable<TAsset> assets)
            where TAsset : Object
        {
            if (keys.All(assetsLoader.IsAssetLoaded) == false)
            {
                assets = Enumerable.Empty<TAsset>();
                return false;
            }

            assets = assetsLoader.GetAssets(keys);
            return true;
        }

        #if VCONTAINER

        public static RegistrationBuilder RegisterAssetsReferenceLoader<TAsset>(this IContainerBuilder builder)
            where TAsset : Object
        {
            return builder.Register<IAssetsReferenceLoader<TAsset>, AssetsReferenceLoader<TAsset>>(Lifetime.Singleton);
        }

        #endif
    }
}
