#if SHARED_SOURCES

using Shared.Sources.ScriptableDatabase;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.AssetReferencesDatabases
{
    public interface IAssetReferencesDatabase<TAsset> : IScriptableDatabase<string, AssetReferenceT<TAsset>>
        where TAsset : Object
    {
    }
}

#endif
