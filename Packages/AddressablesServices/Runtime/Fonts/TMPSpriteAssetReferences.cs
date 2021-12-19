#if TEXTMESHPRO && SHARED_SOURCES

using AddressableAssets.AssetReferences;
using TMPro;
using UnityEngine;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPSpriteAssetReferences", menuName = "AddressableAssets/Fonts/TMP Sprite Asset References")]
    public class TMPSpriteAssetReferences : AssetReferencesUDictionarySo<TMP_SpriteAsset>
    {
        
    }
}

#endif