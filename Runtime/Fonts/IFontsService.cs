#if TEXTMESHPRO

using Cysharp.Threading.Tasks;

namespace AddressableAssets.Fonts
{
    public interface IFontsService<in TLocaleKey, in TSpriteAssetKey>
    {
        UniTask LoadFontForLocale(TLocaleKey localeKey);
        void UnloadFontForLocale(TLocaleKey localeKey);
        UniTask LoadSpriteAsset(TSpriteAssetKey spriteAssetKey);
        void UnloadSpriteAsset(TSpriteAssetKey spriteAssetKey);
    }
}

#endif
