#if SHARED_SOURCES

using Shared.Sources.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.AssetReferences
{
    public class AssetReferencesUDictionarySo<TKey, TAsset> : UDictionarySo<TKey, AssetReferenceT<TAsset>>
        where TAsset : Object
    {

    }
}

#endif