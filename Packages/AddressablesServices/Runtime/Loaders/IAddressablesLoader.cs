using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public interface IAddressablesLoader<in TAssetReference, TResult>
        where TAssetReference : AssetReference where TResult : Object
    {
        UniTask PreloadAssets(IEnumerable<TAssetReference> assetReferences);
        
        UniTask PreloadAssets(params TAssetReference[] assetReferences);

        UniTask PreloadAsset(TAssetReference assetReference);

        void UnloadAssets(IEnumerable<TAssetReference> assetReferences);

        void UnloadAssets(params TAssetReference[] assetReferences);

        void UnloadAsset(TAssetReference assetReference);

        void UnloadAllAssets();

        bool TryGetAsset(TAssetReference assetReference, out TResult asset);

        TResult GetAsset(TAssetReference assetReference);
    }
}