#if TEXTMESHPRO && SHARED_SOURCES

using Shared.Sources.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Fonts
{
    [CreateAssetMenu(fileName = "TMPAddressableAssets", menuName = "AddressableAssets/Fonts/TMP Addressable Assets")]
    public class TMPAddressableAssets : ScriptableObject, ITMPAddressableAssets
    {
        [SerializeField]
        private AssetReferenceT<TMP_FontAsset> _masterFontAsset;

        [SerializeField]
        private AssetReferenceT<TMP_SpriteAsset> _masterSpriteAsset;

        [SerializeField]
        private DictionarySo<string, AssetReferenceT<TMP_FontAsset>> _fontAssets;

        [SerializeField]
        private DictionarySo<string, AssetReferenceT<TMP_SpriteAsset>> _spriteAssets;

        public AssetReferenceT<TMP_FontAsset> MasterFontAsset => _masterFontAsset;

        public AssetReferenceT<TMP_SpriteAsset> MasterSpriteAsset => _masterSpriteAsset;

        public AssetReferenceT<TMP_FontAsset> GetFontAssetForLocale(string locale) =>
            _fontAssets[locale];

        public AssetReferenceT<TMP_SpriteAsset> GetSpriteAsset(string assetName) =>
            _spriteAssets[assetName];
    }
}

#endif
