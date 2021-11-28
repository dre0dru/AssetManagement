#if TEXTMESHPRO && SHARED_SOURCES

using Shared.Sources.ScriptableDatabase;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPResourcesDatabase", menuName = "AddressableAssets/Fonts/TMP Resources Database")]
    public class TMPResourcesDatabase : ScriptableObject, ITMPResourcesDatabase
    {
        [SerializeField]
        private AssetReferenceT<TMP_FontAsset> _masterFontAsset;

        [SerializeField]
        private AssetReferenceT<TMP_SpriteAsset> _masterSpriteAsset;

        [SerializeField]
        private KvpScriptableDatabase<string, AssetReferenceT<TMP_FontAsset>> _fontAssetsDatabase;

        [SerializeField]
        private KvpScriptableDatabase<string, AssetReferenceT<TMP_SpriteAsset>> _spriteAssetsDatabase;

        public AssetReferenceT<TMP_FontAsset> MasterFontAsset => _masterFontAsset;

        public AssetReferenceT<TMP_SpriteAsset> MasterSpriteAsset => _masterSpriteAsset;

        public AssetReferenceT<TMP_FontAsset> GetFontAssetForLocale(string locale) =>
            _fontAssetsDatabase.Get(locale);

        public AssetReferenceT<TMP_SpriteAsset> GetSpriteAsset(string assetName) =>
            _spriteAssetsDatabase.Get(assetName);
    }
}

#endif
