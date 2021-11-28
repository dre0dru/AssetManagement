#if SHARED_RESOURCES && VCONTAINER

using Shared.Sources.ScriptableDatabase;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using VContainer;
using VContainer.Unity;

namespace AddressableAssets.SpriteAtlases
{
    public static class SpriteAtlasesExtensions
    {
        public static RegistrationBuilder RegisterSpriteAtlasesProvider(this IContainerBuilder builder, Lifetime lifetime)
        {
           return builder.Register<ISpriteAtlasProvider, SpriteAtlasProvider>(Lifetime.Singleton);
        }
        
        public static RegistrationBuilder RegisterSpriteAtlasesProviderEntryPoint(this IContainerBuilder builder, Lifetime lifetime)
        {
            return builder.RegisterEntryPoint<SpriteAtlasesProviderEntryPoint>(lifetime);
        }

        public static RegistrationBuilder RegisterSpriteAtlasesDatabase(this IContainerBuilder builder,
            IScriptableDatabase<string, AssetReferenceT<SpriteAtlas>> spriteAtlasesDatabase)
        {
            return builder.RegisterInstance<IScriptableDatabase<string, AssetReferenceT<SpriteAtlas>>>(spriteAtlasesDatabase);
        }
    }
}

#endif
