using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public interface IAddressablesLoader<in TAssetReference, TResult> where TAssetReference : AssetReference where TResult: Object
    {
        UniTask PreloadAssets(IEnumerable<TAssetReference> assetKeys);

        UniTask PreloadAsset(TAssetReference assetKey);

        void UnloadAssets(IEnumerable<TAssetReference> assetKeys);

        void UnloadAsset(TAssetReference assetKey);

        void UnloadAllAssets();

        bool TryGetAsset(TAssetReference assetKey, out TResult asset);
    }
}