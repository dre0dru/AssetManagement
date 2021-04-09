using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public interface IAddressablesLoader<TAsset> where TAsset : Object
    {
        UniTask PreloadAssetsAsync(IEnumerable<AssetReferenceT<TAsset>> assetReferences);
        
        UniTask PreloadAssetAsync(AssetReferenceT<TAsset> assetReference);

        void UnloadAssets(IEnumerable<AssetReferenceT<TAsset>> assetReferences);
        
        void UnloadAsset(AssetReferenceT<TAsset> assetReference);

        void UnloadAllAssets();

        bool TryGetAsset(AssetReferenceT<TAsset> assetReference, out TAsset asset);
        
        bool IsAssetPreloaded(AssetReferenceT<TAsset> assetReference);

        TAsset GetAsset(AssetReferenceT<TAsset> assetReference);
    }
}