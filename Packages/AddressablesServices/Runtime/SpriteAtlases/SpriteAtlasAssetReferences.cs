#if SHARED_SOURCES

using AddressableAssets.AssetReferences;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    public class SpriteAtlasAssetReferences : AssetReferencesUDictionarySo<string, SpriteAtlas>, ISpriteAtlasAddressableAssets
    {
        public AssetReferenceT<SpriteAtlas> GetAsset(string spriteAtlasName) =>
            this[spriteAtlasName];
    }
}

#endif