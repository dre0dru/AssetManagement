#if TEXTMESHPRO

using Cysharp.Threading.Tasks;

namespace AddressableAssets.Fonts
{
    public interface IFontsService
    {
        UniTask LoadFontForLocale(string locale);
        void UnloadFontForLocale(string locale);
        UniTask LoadSpriteAsset(string spriteAssetName);
        void UnloadSpriteAsset(string spriteAssetName);
    }
}

#endif
