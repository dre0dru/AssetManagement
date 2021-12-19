using AddressableAssets.AssetReferences;
using AddressableAssets.SpriteAtlases;
using Shared.Sources.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using VContainer;
using VContainer.Unity;

public class LifetimeScopeBase : LifetimeScope
{
    [SerializeField]
    private SpriteAtlasAssetReferences spriteAtlasAssetReferences;

    [SerializeField]
    private UDictionarySo<string, AssetReferenceT<SpriteAtlas>> _v2;

    [SerializeField]
    private AssetReferencesUDictionarySo<string, SpriteAtlas> _v3;

    protected override void Configure(IContainerBuilder builder)
    {
    }
}
