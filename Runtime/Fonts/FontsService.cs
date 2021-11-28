#if TEXTMESHPRO && SHARED_SOURCES

using System.Collections.Generic;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using TMPro;

namespace AddressableAssets.Fonts
{
    public class FontsService : IFontsService
    {
        private readonly ITMPResourcesDatabase _tmpResourcesDatabase;

        private readonly IAssetsReferenceLoader<TMP_FontAsset> _fontsLoader;

        private readonly IAssetsReferenceLoader<TMP_SpriteAsset> _spriteAssetsLoader;

        private readonly List<TMP_FontAsset> _loadedFonts;

        [UnityEngine.Scripting.RequiredMember]
        public FontsService(ITMPResourcesDatabase tmpResourcesDatabase,
            IAssetsReferenceLoader<TMP_FontAsset> fontsLoader,
            IAssetsReferenceLoader<TMP_SpriteAsset> spriteAssetsLoader)
        {
            _tmpResourcesDatabase = tmpResourcesDatabase;
            _fontsLoader = fontsLoader;
            _spriteAssetsLoader = spriteAssetsLoader;
            _loadedFonts = new List<TMP_FontAsset>();
        }

        public async UniTask LoadFontForLocale(string locale)
        {
            await LoadMasterFontAsset();
            
            var font = await _fontsLoader.LoadAssetAsync(_tmpResourcesDatabase.GetFontAssetForLocale(locale));

            AddFontAssetToFallback(font);
        }

        public void UnloadFontForLocale(string locale)
        {
            var key = _tmpResourcesDatabase.GetFontAssetForLocale(locale);
            var font = _fontsLoader.GetAsset(key);

            RemoveFontAssetFromFallback(font);
            _fontsLoader.UnloadAsset(key);
        }

        public async UniTask LoadSpriteAsset(string spriteAssetName)
        {
            var spriteAssetBase = await LoadMasterSpriteAsset();

            var spriteAsset =
                await _spriteAssetsLoader.LoadAssetAsync(_tmpResourcesDatabase.GetSpriteAsset(spriteAssetName));

            spriteAssetBase.fallbackSpriteAssets.Add(spriteAsset);
        }

        public void UnloadSpriteAsset(string spriteAssetName)
        {
            var key = _tmpResourcesDatabase.GetSpriteAsset(spriteAssetName);
            var font = _spriteAssetsLoader.GetAsset(key);

            TMP_Settings.GetSpriteAsset().fallbackSpriteAssets.Remove(font);

            _spriteAssetsLoader.UnloadAsset(key);
        }

        private UniTask<TMP_SpriteAsset> LoadMasterSpriteAsset()
        {
            return _spriteAssetsLoader.LoadAssetAsync(_tmpResourcesDatabase.MasterSpriteAsset);
        }

        private async UniTask LoadMasterFontAsset()
        {
            if (_fontsLoader.IsAssetLoaded(_tmpResourcesDatabase.MasterFontAsset) == false)
            {
                var font = await _fontsLoader.LoadAssetAsync(_tmpResourcesDatabase.MasterFontAsset);
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
