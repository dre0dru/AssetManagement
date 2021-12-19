using System;
using AddressableAssets.Loaders;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    public class SpriteAtlasProvider : ISpriteAtlasProvider
    {
        private readonly IAssetsReferenceLoader<SpriteAtlas> _spriteAtlasLoader;
        private readonly ISpriteAtlasAddressableAssets _spriteAtlasAddressableAssets;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public SpriteAtlasProvider(IAssetsReferenceLoader<SpriteAtlas> spriteAtlasLoader,
            ISpriteAtlasAddressableAssets spriteAtlasAddressableAssets)
        {
            _spriteAtlasLoader = spriteAtlasLoader;
            _spriteAtlasAddressableAssets = spriteAtlasAddressableAssets;
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
            var spriteAtlas =
                await _spriteAtlasLoader.LoadAssetAsync(_spriteAtlasAddressableAssets.GetAsset(atlasName));

            callback?.Invoke(spriteAtlas);
        }
    }
}
