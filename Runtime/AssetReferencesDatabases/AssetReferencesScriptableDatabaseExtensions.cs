#if VCONTAINER && SHARED_SOURCES

using Shared.Sources.ScriptableDatabase;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace AddressableAssets.AssetReferencesDatabases
{
    public static class AssetReferencesScriptableDatabaseExtensions
    {
        public static RegistrationBuilder RegisterAssetReferencesScriptableDatabase<TAsset>(this IContainerBuilder builder,
            IScriptableDatabase<string, AssetReferenceT<TAsset>> assetReferenceScriptableDatabase)
            where TAsset: Object

        {
            return builder.RegisterInstance<IScriptableDatabase<string, AssetReferenceT<TAsset>>>(
                assetReferenceScriptableDatabase);
        }
    }
}

#endif
