#if SHARED_RESOURCES

using AddressableAssets.AssetReferencesDatabases;
using UnityEngine;
using UnityEngine.U2D;

namespace AddressableAssets.SpriteAtlases
{
    [CreateAssetMenu(fileName = "SpriteAtlasesDatabase", menuName = "AddressableAssets/SpriteAtlases/Sprite Atlases Database")]
    public class SpriteAtlasesDatabase : AssetReferencesScriptableDatabase<SpriteAtlas>
    {
        
    }
}

#endif