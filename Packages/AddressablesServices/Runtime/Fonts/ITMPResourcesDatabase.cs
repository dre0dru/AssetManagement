#if TEXTMESHPRO

using TMPro;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Fonts
{
    public interface ITMPResourcesDatabase
    {
        AssetReferenceT<TMP_FontAsset> MasterFontAsset { get; }
        
        AssetReferenceT<TMP_SpriteAsset> MasterSpriteAsset { get; }

        AssetReferenceT<TMP_FontAsset> GetFontAssetForLocale(string locale);
        
        AssetReferenceT<TMP_SpriteAsset> GetSpriteAsset(string assetName);
    }
}

#endif