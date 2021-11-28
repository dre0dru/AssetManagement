using AddressableAssets.AssetReferencesDatabases;
using AddressableAssets.SpriteAtlases;
using Shared.Sources.ScriptableDatabase;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using VContainer;
using VContainer.Unity;

public class LifetimeScopeBase : LifetimeScope
{
    [SerializeField]
    private SpriteAtlasesDatabase _spriteAtlasesDatabase;

    [SerializeField]
    private KvpScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> _v2;

    [SerializeField]
    private AssetReferencesScriptableDatabase<SpriteAtlas> _v3;

    protected override void Configure(IContainerBuilder builder)
    {
        IScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> sad = _v2;
        KvpScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> ssad = _v2;
        
        
        builder.RegisterSpriteAtlasesDatabase(_spriteAtlasesDatabase);
    }
}
