using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public static partial class AddressablesLoaderExtensions
    {
        public static bool TryGetComponent<T>(
            this IAddressablesLoaderSingle<AssetReferenceT<GameObject>, GameObject> addressablesLoader,
            AssetReferenceComponent<T> key, out T component)
            where T : Component
        {
            if (addressablesLoader.TryGetAsset(key, out var gameObject))
            {
                return gameObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        public static T GetComponent<T>(
            this IAddressablesLoaderSingle<AssetReferenceT<GameObject>, GameObject> addressablesLoader,
            AssetReferenceComponent<T> key)
            where T : Component =>
            addressablesLoader.GetAsset(key).GetComponent<T>();

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            params TKey[] keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(addressablesLoader.PreloadAssetAsync));

        public static UniTask PreloadAssetsAsync<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll(keys.Select(addressablesLoader.PreloadAssetAsync));

        public static void UnloadAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            params TKey[] keys)
            where TAsset : Object =>
            addressablesLoader.UnloadAssets((IEnumerable<TKey>) keys);

        public static void UnloadAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object
        {
            foreach (var key in keys)
            {
                addressablesLoader.UnloadAsset(key);
            }
        }

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            params TKey[] keys)
            where TAsset : Object =>
            LoadAssetsAsync(addressablesLoader, (IEnumerable<TKey>) keys);

        public static UniTask<TAsset[]> LoadAssetsAsync<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            UniTask.WhenAll<TAsset>(keys.Select(addressablesLoader.LoadAssetAsync));

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            params TKey[] keys)
            where TAsset : Object =>
            addressablesLoader.GetAssets((IEnumerable<TKey>) keys);

        public static IEnumerable<TAsset> GetAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            IEnumerable<TKey> keys)
            where TAsset : Object =>
            keys.Select(addressablesLoader.GetAsset);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            out IEnumerable<TAsset> assets, params TKey[] keys)
            where TAsset : Object =>
            addressablesLoader.TryGetAssets(keys, out assets);

        public static bool TryGetAssets<TKey, TAsset>(
            this IAddressablesLoaderSingle<TKey, TAsset> addressablesLoader,
            IEnumerable<TKey> keys, out IEnumerable<TAsset> assets)
            where TAsset : Object
        {
            if (keys.All(addressablesLoader.IsAssetLoaded) == false)
            {
                assets = Enumerable.Empty<TAsset>();
                return false;
            }

            assets = addressablesLoader.GetAssets(keys);
            return true;
        }
    }
}