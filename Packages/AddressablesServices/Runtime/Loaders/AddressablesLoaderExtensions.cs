using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public static class AddressablesLoaderExtensions
    {
        public static bool TryGetComponent<T>(
            this IAddressablesLoader<GameObject> addressablesLoader,
            AssetReferenceComponent<T> assetReference, out T component)
            where T : Component
        {
            if (addressablesLoader.TryGetAsset(assetReference, out var gameObject))
            {
                return gameObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        public static T GetComponent<T>(
            this IAddressablesLoader<GameObject> addressablesLoader,
            AssetReferenceComponent<T> assetReference)
            where T : Component
        {
            return addressablesLoader.GetAsset(assetReference).GetComponent<T>();
        }

        public static UniTask PreloadAssetsAsync<TAsset>(this IAddressablesLoader<TAsset> addressablesLoader,
            params AssetReferenceT<TAsset>[] assetReferences)
            where TAsset : Object
        {
            return addressablesLoader.PreloadAssetsAsync(assetReferences);
        }

        public static void UnloadAssets<TAsset>(this IAddressablesLoader<TAsset> addressablesLoader,
            params AssetReferenceT<TAsset>[] assetReferences)
            where TAsset : Object
        {
            addressablesLoader.UnloadAssets(assetReferences);
        }

        public static async UniTask<TAsset> LoadAssetAsync<TAsset>(
            this IAddressablesLoader<TAsset> addressablesLoader,
            AssetReferenceT<TAsset> assetReference)
            where TAsset : Object
        {
            if (addressablesLoader.IsAssetPreloaded(assetReference))
            {
                return addressablesLoader.GetAsset(assetReference);
            }

            await addressablesLoader.PreloadAssetAsync(assetReference);

            return addressablesLoader.GetAsset(assetReference);
        }

        public static UniTask<IEnumerable<TAsset>> LoadAssetsAsync<TAsset>(
            this IAddressablesLoader<TAsset> addressablesLoader,
            IEnumerable<AssetReferenceT<TAsset>> assetReferences)
            where TAsset : Object
        {
            return addressablesLoader.LoadAssetsAsync(assetReferences.ToArray());
        }

        public static async UniTask<IEnumerable<TAsset>> LoadAssetsAsync<TAsset>(
            this IAddressablesLoader<TAsset> addressablesLoader,
            params AssetReferenceT<TAsset>[] assetReferences)
            where TAsset : Object
        {
            var assetsToLoad = assetReferences
                .Where(assetReference => addressablesLoader.IsAssetPreloaded(assetReference) == false);

            await addressablesLoader.PreloadAssetsAsync(assetsToLoad);

            return addressablesLoader.GetAssets(assetReferences);
        }

        public static IEnumerable<TAsset> GetAssets<TAsset>(this IAddressablesLoader<TAsset> addressablesLoader,
            IEnumerable<AssetReferenceT<TAsset>> assetReferences)
            where TAsset : Object
        {
            return assetReferences.Select(addressablesLoader.GetAsset);
        }

        public static bool TryGetAssets<TAsset>(this IAddressablesLoader<TAsset> addressablesLoader,
            IEnumerable<AssetReferenceT<TAsset>> assetReferences, out IEnumerable<TAsset> assets)
            where TAsset : Object
        {
            return addressablesLoader.TryGetAssets(out assets, assetReferences.ToArray());
        }
        
        public static bool TryGetAssets<TAsset>(this IAddressablesLoader<TAsset> addressablesLoader,
            out IEnumerable<TAsset> assets, params AssetReferenceT<TAsset>[] assetReferences)
            where TAsset : Object
        {
            if (assetReferences.All(addressablesLoader.IsAssetPreloaded) == false)
            {
                assets = Enumerable.Empty<TAsset>();
                return false;
            }

            assets = addressablesLoader.GetAssets(assetReferences);
            return true;
        }
        
    }
}