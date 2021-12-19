#if TEXTMESHPRO

using System;
using Cysharp.Threading.Tasks;

namespace AddressableAssets.Fonts
{
    public class DummyFontsService<TLocaleKey, TSpriteAssetKey> : IFontsService<TLocaleKey, TSpriteAssetKey>
    {
        [UnityEngine.Scripting.RequiredMember]
        public DummyFontsService()
        {
        }

        public UniTask LoadFontForLocale(TLocaleKey localeKey)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public void UnloadFontForLocale(TLocaleKey localeKey)
        {
        }

        public UniTask LoadSpriteAsset(TSpriteAssetKey spriteAssetKey)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public void UnloadSpriteAsset(TSpriteAssetKey spriteAssetKey)
        {
        }
    }
}

#endif
