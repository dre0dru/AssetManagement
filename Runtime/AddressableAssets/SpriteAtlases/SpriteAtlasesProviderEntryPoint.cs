#if VCONTAINER_SUPPORT

using System;
using VContainer.Unity;

namespace Dre0Dru.AddressableAssets.SpriteAtlases
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
