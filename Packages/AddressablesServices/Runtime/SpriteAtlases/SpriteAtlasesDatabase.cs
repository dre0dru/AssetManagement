#if SHARED_RESOURCES

using AddressableAssets.AssetReferencesDatabases;
using UnityEditor.U2D;
using UnityEngine;

namespace AddressableAssets.SpriteAtlases
{
    [CreateAssetMenu(fileName = "SpriteAtlasesDatabase", menuName = "AddressableAssets/SpriteAtlases/Sprite Atlases Database")]
    public class SpriteAtlasesDatabase : AssetReferencesScriptableDatabase<SpriteAtlasAsset>
    {
        
    }
}

#endif