#if SHARED_SOURCES

using AddressableAssets.AssetReferences;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    [CreateAssetMenu(fileName = "SpriteAtlasAssetReferences", menuName = "AddressableAssets/SpriteAtlases/Sprite Atlas Asset References")]
    public class SpriteAtlasAssetReferences : AssetReferencesUDictionarySo<SpriteAtlas>, ISpriteAtlasAddressableAssets
    {
        public AssetReferenceT<SpriteAtlas> GetAsset(string spriteAtlasName) =>
            this[spriteAtlasName];
    }
}

#endif