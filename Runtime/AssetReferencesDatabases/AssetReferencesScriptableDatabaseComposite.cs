#if SHARED_SOURCES

using Shared.Sources.ScriptableDatabase;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.AssetReferencesDatabases
{
    public class AssetReferencesScriptableDatabaseComposite<TAsset> : ScriptableDatabaseComposite<string, AssetReferenceT<TAsset>>,
        IAssetReferencesDatabase<TAsset>
        where TAsset : Object
    {
        
    }
}

#endif