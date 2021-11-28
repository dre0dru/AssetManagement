#if TEXTMESHPRO && SHARED_SOURCES

using AddressableAssets.AssetReferencesDatabases;
using TMPro;
using UnityEngine;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPFontAssetsDatabase", menuName = "AddressableAssets/Fonts/TMP FontAssets Database")]
    public class TMPFontAssetsDatabase : AssetReferencesScriptableDatabase<TMP_FontAsset>
    {
        
    }
}

#endif