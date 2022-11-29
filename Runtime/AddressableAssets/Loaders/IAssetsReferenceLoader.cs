using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Dre0Dru.AddressableAssets.Loaders
{
    public interface IAssetsReferenceLoader<TAsset> : IAssetsLoader<AssetReferenceT<TAsset>, TAsset>
        where TAsset : Object
    {
    }
}