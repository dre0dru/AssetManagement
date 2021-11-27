using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Loaders
{
    public interface IAssetsReferenceLoader<TAsset> : IAssetsLoader<AssetReferenceT<TAsset>, TAsset>
        where TAsset : Object
    {
    }
}