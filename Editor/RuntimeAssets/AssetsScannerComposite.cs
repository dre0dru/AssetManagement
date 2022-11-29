using System.Collections.Generic;
using Dre0Dru.EditorExtensions.Editor;
using UnityEditor;
using UnityEngine;

namespace Dre0Dru.RuntimeAssets.Editor
{
    [CreateAssetMenu(fileName = "AssetsScannerComposite", menuName = "RuntimeAssets/AssetsScannerComposite")]
    public class AssetsScannerComposite : AssetsScanner
    {
        [SerializeField]
        protected List<AssetsScanner> _assetsScanners;

        public override void Scan()
        {
            foreach (var assetsScanner in _assetsScanners)
            {
                assetsScanner.Scan();
            }
        }

        #if EASY_BUTTONS_SUPPORT
        [EasyButtons.Button]
        #endif
        public void FindAllScanners()
        {
            _assetsScanners.Clear();
            
            foreach (var assetsScanner in AssetDatabaseUtils.LoadAssets<AssetsScanner>())
            {
                if (assetsScanner != this)
                {
                    _assetsScanners.Add(assetsScanner);
                }
            }
            
            AssetDatabaseUtils.SetDirtyAndSave(this);
            AssetDatabase.Refresh();
        }
    }
}
