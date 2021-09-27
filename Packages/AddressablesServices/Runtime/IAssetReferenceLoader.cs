using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IAssetReferenceLoader<TAsset> : IAssetLoader<AssetReferenceT<TAsset>, TAsset>
        where TAsset : Object
    {
    }
}