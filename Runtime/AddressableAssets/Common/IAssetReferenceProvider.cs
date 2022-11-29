using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Dre0Dru.AddressableAssets.Common
{
    //TODO as generic key-value SO in separate module?
    public interface IAssetReferenceProvider<TAsset, in TKey>
        where TAsset : Object
    {
        AssetReferenceT<TAsset> GetByKey(TKey key);
    }
}
