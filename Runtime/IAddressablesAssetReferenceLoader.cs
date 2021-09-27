using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IAddressablesAssetReferenceLoader<TAsset> : IAddressablesLoader<AssetReferenceT<TAsset>, TAsset>
        where TAsset : Object
    {
    }
}