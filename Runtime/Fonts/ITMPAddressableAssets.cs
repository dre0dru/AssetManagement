#if TEXTMESHPRO

using TMPro;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Fonts
{
    public interface ITMPAddressableAssets<in TLocaleKey, in TSpriteAssetKey>
    {
        AssetReferenceT<TMP_FontAsset> MasterFontAsset { get; }
        
        AssetReferenceT<TMP_SpriteAsset> MasterSpriteAsset { get; }

        AssetReferenceT<TMP_FontAsset> GetFontAssetForLocale(TLocaleKey localeKey);
        
        AssetReferenceT<TMP_SpriteAsset> GetSpriteAsset(TSpriteAssetKey spriteAssetKey);
    }
}

#endif