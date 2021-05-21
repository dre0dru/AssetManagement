using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IAddressablesLoader<TAsset> where TAsset : Object
    {
        UniTask PreloadAssetsAsync(IEnumerable<AssetReferenceT<TAsset>> assetReferences);
        
        UniTask PreloadAssetAsync(AssetReferenceT<TAsset> assetReference);

        bool IsAssetLoaded(AssetReferenceT<TAsset> assetReference);
        
        TAsset GetAsset(AssetReferenceT<TAsset> assetReference);

        bool TryGetAsset(AssetReferenceT<TAsset> assetReference, out TAsset asset);

        void UnloadAsset(AssetReferenceT<TAsset> assetReference);
        
        void UnloadAssets(IEnumerable<AssetReferenceT<TAsset>> assetReferences);

        void UnloadAllAssets();
    }
}