using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    public interface ISpriteAtlasAddressableAssets
    {
        AssetReferenceT<SpriteAtlas> GetAsset(string spriteAtlasName);
    }
}
