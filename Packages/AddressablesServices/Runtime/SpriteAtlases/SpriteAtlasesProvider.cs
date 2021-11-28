#if SHARED_RESOURCES

using System;
using AddressableAssets.Loaders;
using Shared.Sources.ScriptableDatabase;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    public class SpriteAtlasProvider : ISpriteAtlasProvider
    {
        private readonly IAssetsReferenceLoader<SpriteAtlas> _spriteAtlasLoader;
        private readonly IScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> _spriteAtlasesDatabase;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public SpriteAtlasProvider(IAssetsReferenceLoader<SpriteAtlas> spriteAtlasLoader,
            IScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> spriteAtlasesDatabase)
        {
            _spriteAtlasLoader = spriteAtlasLoader;
            _spriteAtlasesDatabase = spriteAtlasesDatabase;
        }

        public void SubscribeToAtlasManagerRequests()
        {
            SpriteAtlasManager.atlasRequested += OnAtlasRequested;
        }

        public void UnsubscribeFromAtlasManagerRequests()
        {
            SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
        }

        public void UnloadSpriteAtlases()
        {
            _spriteAtlasLoader.UnloadAllAssets();
        }
        
        private async void OnAtlasRequested(string atlasName, Action<SpriteAtlas> callback)
        {
            if (_spriteAtlasesDatabase.TryGet(atlasName, out var spriteAtlasReference))
            {
                var spriteAtlas = await _spriteAtlasLoader.LoadAssetAsync(spriteAtlasReference);

                callback?.Invoke(spriteAtlas);
            }
        }
    }
}

#endif
