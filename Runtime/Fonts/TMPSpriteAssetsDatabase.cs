#if TEXTMESHPRO && SHARED_SOURCES

using AddressableAssets.AssetReferencesDatabases;
using TMPro;
using UnityEngine;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPSpriteAssetsDatabase", menuName = "AddressableAssets/Fonts/TMP SpriteAssets Database")]
    public class TMPSpriteAssetsDatabase : AssetReferencesScriptableDatabase<TMP_SpriteAsset>
    {
        
    }
}

#endif