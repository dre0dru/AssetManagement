#if TEXTMESHPRO && SHARED_SOURCES

using AddressableAssets.AssetReferences;
using TMPro;
using UnityEngine;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPFontAssetReferences", menuName = "AddressableAssets/Fonts/TMP Font Asset References")]
    public class TMPFontAssetReferences : AssetReferencesUDictionarySo<TMP_FontAsset>
    {
        
    }
}

#endif