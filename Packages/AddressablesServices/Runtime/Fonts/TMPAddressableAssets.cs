#if TEXTMESHPRO && SHARED_SOURCES

using Shared.Sources.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Fonts
{
    public class TMPAddressableAssets<TLocaleKey, TSpriteAssetKey> : ScriptableObject, ITMPAddressableAssets<TLocaleKey, TSpriteAssetKey>
    {
        [SerializeField]
        private AssetReferenceT<TMP_FontAsset> _masterFontAsset;

        [SerializeField]
        private AssetReferenceT<TMP_SpriteAsset> _masterSpriteAsset;

        [SerializeField]
        private DictionarySo<TLocaleKey, AssetReferenceT<TMP_FontAsset>> _fontAssets;

        [SerializeField]
        private DictionarySo<TSpriteAssetKey, AssetReferenceT<TMP_SpriteAsset>> _spriteAssets;

        public AssetReferenceT<TMP_FontAsset> MasterFontAsset => _masterFontAsset;

        public AssetReferenceT<TMP_SpriteAsset> MasterSpriteAsset => _masterSpriteAsset;

        public AssetReferenceT<TMP_FontAsset> GetFontAssetForLocale(TLocaleKey locale) =>
            _fontAssets[locale];

        public AssetReferenceT<TMP_SpriteAsset> GetSpriteAsset(TSpriteAssetKey assetName) =>
            _spriteAssets[assetName];
    }
}

#endif
