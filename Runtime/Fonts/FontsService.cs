#if TEXTMESHPRO

using System.Collections.Generic;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using TMPro;

namespace AddressableAssets.Fonts
{
    public class FontsService : IFontsService
    {
        private readonly ITMPAddressableAssets _tmpAddressableAssets;

        private readonly IAssetsReferenceLoader<TMP_FontAsset> _fontsLoader;

        private readonly IAssetsReferenceLoader<TMP_SpriteAsset> _spriteAssetsLoader;

        private readonly List<TMP_FontAsset> _loadedFonts;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public FontsService(ITMPAddressableAssets itmpAddressableAssets,
            IAssetsReferenceLoader<TMP_FontAsset> fontsLoader,
            IAssetsReferenceLoader<TMP_SpriteAsset> spriteAssetsLoader)
        {
            _tmpAddressableAssets = itmpAddressableAssets;
            _fontsLoader = fontsLoader;
            _spriteAssetsLoader = spriteAssetsLoader;
            _loadedFonts = new List<TMP_FontAsset>();
        }

        public async UniTask LoadFontForLocale(string locale)
        {
            await LoadMasterFontAsset();
            
            var font = await _fontsLoader.LoadAssetAsync(_tmpAddressableAssets.GetFontAssetForLocale(locale));

            AddFontAssetToFallback(font);
        }

        public void UnloadFontForLocale(string locale)
        {
            var key = _tmpAddressableAssets.GetFontAssetForLocale(locale);
            var font = _fontsLoader.GetAsset(key);

            RemoveFontAssetFromFallback(font);
            _fontsLoader.UnloadAsset(key);
        }

        public async UniTask LoadSpriteAsset(string spriteAssetName)
        {
            var spriteAssetBase = await LoadMasterSpriteAsset();

            var spriteAsset =
                await _spriteAssetsLoader.LoadAssetAsync(_tmpAddressableAssets.GetSpriteAsset(spriteAssetName));

            spriteAssetBase.fallbackSpriteAssets.Add(spriteAsset);
        }

        public void UnloadSpriteAsset(string spriteAssetName)
        {
            var key = _tmpAddressableAssets.GetSpriteAsset(spriteAssetName);
            var font = _spriteAssetsLoader.GetAsset(key);

            TMP_Settings.GetSpriteAsset().fallbackSpriteAssets.Remove(font);

            _spriteAssetsLoader.UnloadAsset(key);
        }

        private UniTask<TMP_SpriteAsset> LoadMasterSpriteAsset()
        {
            return _spriteAssetsLoader.LoadAssetAsync(_tmpAddressableAssets.MasterSpriteAsset);
        }

        private async UniTask LoadMasterFontAsset()
        {
            if (_fontsLoader.IsAssetLoaded(_tmpAddressableAssets.MasterFontAsset) == false)
            {
                var font = await _fontsLoader.LoadAssetAsync(_tmpAddressableAssets.MasterFontAsset);
                AddFontAssetToFallback(font);
            }
        }

        private void AddFontAssetToFallback(TMP_FontAsset fontAsset)
        {
            _loadedFonts.Add(fontAsset);

            TMP_Settings.fallbackFontAssets.Clear();
            TMP_Settings.fallbackFontAssets.AddRange(_loadedFonts);
        }

        private void RemoveFontAssetFromFallback(TMP_FontAsset fontAsset)
        {
            _loadedFonts.Remove(fontAsset);

            TMP_Settings.fallbackFontAssets.Clear();
            TMP_Settings.fallbackFontAssets.AddRange(_loadedFonts);
        }
    }
}

#endif
