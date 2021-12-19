#if TEXTMESHPRO

using System.Collections.Generic;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using TMPro;

namespace AddressableAssets.Fonts
{
    public class FontsService<TLocaleKey, TSpriteAssetKey> : IFontsService<TLocaleKey, TSpriteAssetKey>
    {
        private readonly ITMPAddressableAssets<TLocaleKey, TSpriteAssetKey> _tmpAddressableAssets;

        private readonly IAssetsReferenceLoader<TMP_FontAsset> _fontsLoader;

        private readonly IAssetsReferenceLoader<TMP_SpriteAsset> _spriteAssetsLoader;

        private readonly List<TMP_FontAsset> _loadedFonts;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public FontsService(ITMPAddressableAssets<TLocaleKey, TSpriteAssetKey> itmpAddressableAssets,
            IAssetsReferenceLoader<TMP_FontAsset> fontsLoader,
            IAssetsReferenceLoader<TMP_SpriteAsset> spriteAssetsLoader)
        {
            _tmpAddressableAssets = itmpAddressableAssets;
            _fontsLoader = fontsLoader;
            _spriteAssetsLoader = spriteAssetsLoader;
            _loadedFonts = new List<TMP_FontAsset>();
        }

        public async UniTask LoadFontForLocale(TLocaleKey localeKey)
        {
            await LoadMasterFontAsset();

            var font = await _fontsLoader.LoadAssetAsync(_tmpAddressableAssets.GetFontAssetForLocale(localeKey));

            AddFontAssetToFallback(font);
        }

        public void UnloadFontForLocale(TLocaleKey localeKey)
        {
            var key = _tmpAddressableAssets.GetFontAssetForLocale(localeKey);
            var font = _fontsLoader.GetAsset(key);

            RemoveFontAssetFromFallback(font);
            _fontsLoader.UnloadAsset(key);
        }

        public async UniTask LoadSpriteAsset(TSpriteAssetKey spriteAssetKey)
        {
            var spriteAssetBase = await LoadMasterSpriteAsset();

            var spriteAsset =
                await _spriteAssetsLoader.LoadAssetAsync(_tmpAddressableAssets.GetSpriteAsset(spriteAssetKey));

            spriteAssetBase.fallbackSpriteAssets.Add(spriteAsset);
        }

        public void UnloadSpriteAsset(TSpriteAssetKey spriteAssetKey)
        {
            var key = _tmpAddressableAssets.GetSpriteAsset(spriteAssetKey);
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
