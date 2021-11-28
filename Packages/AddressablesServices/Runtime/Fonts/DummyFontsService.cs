#if TEXTMESHPRO

using System;
using Cysharp.Threading.Tasks;

namespace AddressableAssets.Fonts
{
    public class DummyFontsService : IFontsService
    {

        [UnityEngine.Scripting.RequiredMember]
        public DummyFontsService()
        {
            
        }
        
        public UniTask LoadFontForLocale(string locale)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public void UnloadFontForLocale(string locale)
        {
        }

        public UniTask LoadSpriteAsset(string spriteAssetName)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public void UnloadSpriteAsset(string spriteAssetName)
        {
        }
    }
}

#endif
