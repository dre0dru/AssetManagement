using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dre0Dru.GameAssets
{
    [Serializable]
    public class TransientAsset<TAsset> 
        where TAsset : ScriptableObject
    {
        [SerializeField]
        private TAsset _asset;

        public TAsset Asset => Object.Instantiate(_asset);
        
        public static implicit operator TAsset(TransientAsset<TAsset> scopedAsset)
        {
            return scopedAsset.Asset;
        }
    }
}
