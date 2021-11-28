#if SHARED_RESOURCES && VCONTAINER

using System;
using VContainer.Unity;

namespace AddressableAssets.SpriteAtlases
{
    public class SpriteAtlasesProviderEntryPoint : IInitializable, IDisposable
    {
        private readonly ISpriteAtlasProvider _spriteAtlasProvider;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public SpriteAtlasesProviderEntryPoint(ISpriteAtlasProvider spriteAtlasProvider)
        {
            _spriteAtlasProvider = spriteAtlasProvider;
        }

        public void Initialize()
        {
            _spriteAtlasProvider.SubscribeToAtlasManagerRequests();
        }

        public void Dispose()
        {
            _spriteAtlasProvider.UnsubscribeFromAtlasManagerRequests();
            _spriteAtlasProvider.UnloadSpriteAtlases();
        }
    }
}

#endif
