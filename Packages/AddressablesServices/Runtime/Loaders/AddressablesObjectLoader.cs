using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public class AddressablesObjectLoader<TAssetReference, TResult> :
        AddressablesLoaderBase<TAssetReference, TResult, TResult>
        where TAssetReference : AssetReference where TResult : Object
    {
        protected override bool TryExtractResult(TResult loadedAsset, out TResult result)
        {
            result = loadedAsset;
            return true;
        }
    }
}