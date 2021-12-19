#if SHARED_SOURCES

using Shared.Sources.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.AssetReferences
{
    public class AssetReferencesUDictionarySo<TAsset, TKvp> : UDictionarySo<string, AssetReferenceT<TAsset>, TKvp>
        where TAsset : Object
        where TKvp : IKvp<string, AssetReferenceT<TAsset>>, new()
    {

    }
    
    public class AssetReferencesUDictionarySo<TAsset> : 
        AssetReferencesUDictionarySo<TAsset, Kvp<string, AssetReferenceT<TAsset>>>
        where TAsset : Object
    {

    }
}

#endif