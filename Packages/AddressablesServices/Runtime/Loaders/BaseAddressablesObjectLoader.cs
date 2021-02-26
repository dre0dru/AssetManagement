using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public abstract class
        BaseAddressablesObjectLoader<TAssetReference, TResult> : BaseAddressablesLoader<TAssetReference, TResult,
            TResult> where TAssetReference : AssetReference where TResult : Object
    {
        protected override bool TryExtractResult(TResult loadedAsset, out TResult result)
        {
            result = loadedAsset;
            return true;
        }
    }
}