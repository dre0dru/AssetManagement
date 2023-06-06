using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dre0Dru.GameAssets
{
    [Serializable]
    public class ScopedAsset<TAsset> : IDisposable
        where TAsset : ScriptableObject
    {
        [SerializeField]
        private TAsset _asset;

        private TAsset _instance;
        
        public TAsset Asset
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Object.Instantiate(_asset);
                }

                return _instance;
            }
        }

        public void Dispose()
        {
            if (_instance != null)
            {
                Object.Destroy(_instance);
            }
        }

        public static implicit operator TAsset(ScopedAsset<TAsset> scopedAsset)
        {
            return scopedAsset.Asset;
        }
    }
}
