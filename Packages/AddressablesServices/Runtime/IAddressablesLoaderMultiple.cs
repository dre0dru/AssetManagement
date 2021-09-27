using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AddressablesServices
{
    public interface IAddressablesLoaderMultiple<in TKey, TAsset, TAssetCollection> : IAddressablesUnloader
        where TAsset : Object
        where TAssetCollection : ICollection<TAsset>
    {
        UniTask PreloadAssetsAsync(TKey key);

        UniTask<TAssetCollection> LoadAssetsAsync(TKey key);

        bool AreAssetsLoaded(TKey key);

        TAssetCollection GetAssets(TKey key);

        bool TryGetAssets(TKey key, out TAssetCollection assets);

        void UnloadAssets(TKey key);
    }
}